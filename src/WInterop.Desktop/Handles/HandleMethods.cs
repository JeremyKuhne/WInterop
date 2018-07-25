// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling;
using WInterop.Handles.Types;
using WInterop.SafeString.Types;
using WInterop.Support.Buffers;

namespace WInterop.Handles
{
    public static partial class HandleMethods
    {
        // Windows Kernel Architecture Internals
        // http://research.microsoft.com/en-us/um/redmond/events/wincore2010/Dave_Probert_1.pdf

        // Object Manager Routines (kernel-mode file systems and file system filter drivers)
        // https://msdn.microsoft.com/en-us/library/windows/hardware/ff550969.aspx
        // https://msdn.microsoft.com/en-us/library/windows/hardware/ff557759.aspx

        // Managing Kernel Objects
        // https://msdn.microsoft.com/en-us/library/windows/hardware/ff554383.aspx

        /// <summary>
        /// Open a handle to a directory object at the given NT path.
        /// </summary>
        public static DirectoryObjectHandle OpenDirectoryObject(
            string path,
            DirectoryObjectRights desiredAccess = DirectoryObjectRights.Query)
        {
            return (DirectoryObjectHandle)OpenObjectHelper(path, (attributes) =>
            {
                NTSTATUS status = Imports.NtOpenDirectoryObject(
                    DirectoryHandle: out var directory,
                    DesiredAccess: desiredAccess,
                    ObjectAttributes: ref attributes);

                if (status != NTSTATUS.STATUS_SUCCESS)
                    throw ErrorMethods.GetIoExceptionForNTStatus(status, path);

                return directory;
            });
        }

        /// <summary>
        /// Open a handle to a symbolic link at the given NT path.
        /// </summary>
        public static SymbolicLinkObjectHandle OpenSymbolicLinkObject(
            string path,
            SymbolicLinkObjectRights desiredAccess = SymbolicLinkObjectRights.GENERIC_READ)
        {
            return (SymbolicLinkObjectHandle)OpenObjectHelper(path, (attributes) =>
            {
                NTSTATUS status = Imports.NtOpenSymbolicLinkObject(
                    LinkHandle: out var link,
                    DesiredAccess: desiredAccess,
                    ObjectAttributes: ref attributes);

                if (status != NTSTATUS.STATUS_SUCCESS)
                    throw ErrorMethods.GetIoExceptionForNTStatus(status, path);

                return link;
            });
        }

        private unsafe static SafeHandle OpenObjectHelper(string path, Func<OBJECT_ATTRIBUTES, SafeHandle> invoker)
        {
            fixed (char* pathPointer = path)
            {
                ushort length = checked((ushort)(path.Length * sizeof(char)));
                var objectName = new UNICODE_STRING
                {
                    Length = length,
                    MaximumLength = length,
                    Buffer = pathPointer
                };

                OBJECT_ATTRIBUTES attributes = new OBJECT_ATTRIBUTES
                {
                    Length = (uint)sizeof(OBJECT_ATTRIBUTES),
                    RootDirectory = IntPtr.Zero,
                    ObjectName = &objectName,
                    SecurityDescriptor = null,
                    SecurityQualityOfService = null
                };

                SafeHandle handle = invoker(attributes);
                return handle;
            }
        }

        /// <summary>
        /// Get the symbolic link's target path.
        /// </summary>
        public static string GetSymbolicLinkTarget(SymbolicLinkObjectHandle linkHandle)
        {
            return BufferHelper.BufferInvoke((StringBuffer buffer) =>
            {
                UNICODE_STRING target = new UNICODE_STRING(buffer);
                NTSTATUS status;
                while ((status = Imports.NtQuerySymbolicLinkObject(linkHandle, ref target, out uint returnedLength)) == NTSTATUS.STATUS_BUFFER_TOO_SMALL)
                {
                    buffer.EnsureByteCapacity(returnedLength);
                    target.UpdateFromStringBuffer(buffer);
                }

                if (status != NTSTATUS.STATUS_SUCCESS)
                    throw ErrorMethods.GetIoExceptionForNTStatus(status);

                buffer.Length = (uint)(target.Length / sizeof(char));
                return buffer.ToString();
            });
        }

        public unsafe static IEnumerable<ObjectInformation> GetDirectoryEntries(DirectoryObjectHandle directoryHandle)
        {
            List<ObjectInformation> infos = new List<ObjectInformation>();

            BufferHelper.BufferInvoke((StringBuffer buffer) =>
            {
                buffer.EnsureCharCapacity(1024);

                uint context = 0;
                NTSTATUS status;

                do
                {
                    status = Imports.NtQueryDirectoryObject(
                        DirectoryHandle: directoryHandle,
                        Buffer: buffer,
                        Length: (uint)buffer.ByteCapacity,
                        ReturnSingleEntry: false,
                        RestartScan: false,
                        Context: ref context,
                        ReturnLength: out uint returnLength);

                    if (status != NTSTATUS.STATUS_SUCCESS && status != NTSTATUS.STATUS_MORE_ENTRIES)
                        break;

                    UNICODE_STRING* name = (UNICODE_STRING*)buffer.VoidPointer;
                    while (name->Length != 0)
                    {
                        infos.Add(new ObjectInformation
                        {
                            Name = name++->ToString(),
                            TypeName = name++->ToString()
                        });
                    };

                } while (status == NTSTATUS.STATUS_MORE_ENTRIES);

                if (status != NTSTATUS.STATUS_SUCCESS)
                    throw ErrorMethods.GetIoExceptionForNTStatus(status);
            });

            return infos.OrderBy(i => i.Name); ;
        }

        /// <summary>
        /// Get the name fot he given handle. This is typically the NT path of the object.
        /// </summary>
        public unsafe static string GetObjectName(SafeHandle handle)
        {
            // IoQueryFileDosDeviceName wraps this for file handles, but requires calling ExFreePool to free the allocated memory
            // https://msdn.microsoft.com/en-us/library/windows/hardware/ff548474.aspx
            //
            // http://undocumented.ntinternals.net/index.html?page=UserMode%2FUndocumented%20Functions%2FNT%20Objects%2FType%20independed%2FOBJECT_NAME_INFORMATION.html
            //
            //  typedef struct _OBJECT_NAME_INFORMATION
            //  {
            //       UNICODE_STRING Name;
            //       WCHAR NameBuffer[0];
            //  } OBJECT_NAME_INFORMATION, *POBJECT_NAME_INFORMATION;
            //
            // The above definition means the API expects a buffer where it can stick a UNICODE_STRING with the buffer immediately following.

            return BufferHelper.BufferInvoke((HeapBuffer buffer) =>
            {
                NTSTATUS status = NTSTATUS.STATUS_BUFFER_OVERFLOW;
                uint returnLength = 260 * sizeof(char);

                while (status == NTSTATUS.STATUS_BUFFER_OVERFLOW || status == NTSTATUS.STATUS_BUFFER_TOO_SMALL)
                {
                    buffer.EnsureByteCapacity(returnLength);

                    status = Imports.NtQueryObject(
                        Handle: handle,
                        ObjectInformationClass: ObjectInformationClass.ObjectNameInformation,
                        ObjectInformation: buffer.DangerousGetHandle(),
                        ObjectInformationLength: checked((uint)buffer.ByteCapacity),
                        ReturnLength: out returnLength);
                }

                if (!ErrorMacros.NT_SUCCESS(status))
                    throw ErrorMethods.GetIoExceptionForNTStatus(status);

                return ((UNICODE_STRING*)(buffer.VoidPointer))->ToString();
            });
        }

        /// <summary>
        /// Get the type name of the given object.
        /// </summary>
        public unsafe static string GetObjectType(SafeHandle handle)
        {
            return BufferHelper.BufferInvoke((HeapBuffer buffer) =>
            {
                NTSTATUS status = NTSTATUS.STATUS_BUFFER_OVERFLOW;

                // We'll initially give room for 50 characters for the type name
                uint returnLength = (uint)sizeof(OBJECT_TYPE_INFORMATION) + 50 * sizeof(char);

                while (status == NTSTATUS.STATUS_BUFFER_OVERFLOW || status == NTSTATUS.STATUS_BUFFER_TOO_SMALL || status == NTSTATUS.STATUS_INFO_LENGTH_MISMATCH)
                {
                    buffer.EnsureByteCapacity(returnLength);

                    status = Imports.NtQueryObject(
                        Handle: handle,
                        ObjectInformationClass: ObjectInformationClass.ObjectTypeInformation,
                        ObjectInformation: buffer.DangerousGetHandle(),
                        ObjectInformationLength: checked((uint)buffer.ByteCapacity),
                        ReturnLength: out returnLength);
                }

                if (!ErrorMacros.NT_SUCCESS(status))
                    throw ErrorMethods.GetIoExceptionForNTStatus(status);

                return ((OBJECT_TYPE_INFORMATION*)(buffer.VoidPointer))->TypeName.ToString();
            });
        }
    }
}
