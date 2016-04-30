// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using WInterop.Authentication;
using WInterop.Authorization;
using WInterop.Buffers;
using WInterop.ErrorHandling;
using WInterop.Handles;

namespace WInterop
{
    public static partial class NativeMethods
    {
        public static class Handles
        {
            // Windows Kernel Architecture Internals
            // http://research.microsoft.com/en-us/um/redmond/events/wincore2010/Dave_Probert_1.pdf

            // Object Manager Routines (kernel-mode file systems and file system filter drivers)
            // https://msdn.microsoft.com/en-us/library/windows/hardware/ff550969.aspx
            // https://msdn.microsoft.com/en-us/library/windows/hardware/ff557759.aspx

            // Managing Kernel Objects
            // https://msdn.microsoft.com/en-us/library/windows/hardware/ff554383.aspx

            /// <summary>
            /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
            /// </summary>
            /// <remarks>
            /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
            /// </remarks>
#if DESKTOP
            [SuppressUnmanagedCodeSecurity] // We don't want a stack walk with every P/Invoke.
#endif
            public static class Direct
            {
                // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724211.aspx
                [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool CloseHandle(
                    IntPtr handle);

                // http://forum.sysinternals.com/howto-enumerate-handles_topic18892.html

                // https://msdn.microsoft.com/en-us/library/bb432383.aspx
                // https://msdn.microsoft.com/en-us/library/windows/hardware/ff567062.aspx
                // The Zw version of this is documented as available to UWP, Nt has no clarifying restrictions and is not included in a header.
                [DllImport(Libraries.Ntdll, ExactSpelling = true)]
                public static extern int NtQueryObject(
                    SafeHandle Handle,
                    OBJECT_INFORMATION_CLASS ObjectInformationClass,
                    IntPtr ObjectInformation,
                    uint ObjectInformationLength,
                    out uint ReturnLength);

                // https://msdn.microsoft.com/en-us/library/bb470234.aspx
                // https://msdn.microsoft.com/en-us/library/windows/hardware/ff566492.aspx
                [DllImport(Libraries.Ntdll, ExactSpelling = true)]
                public static extern int NtOpenDirectoryObject(
                    out SafeDirectoryObjectHandle DirectoryHandle,
                    ACCESS_MASK DesiredAccess,
                    OBJECT_ATTRIBUTES ObjectAttributes);

                // https://msdn.microsoft.com/en-us/library/bb470236.aspx
                // https://msdn.microsoft.com/en-us/library/windows/hardware/ff567030.aspx
                [DllImport(Libraries.Ntdll, ExactSpelling = true)]
                public static extern int NtOpenSymbolicLinkObject(
                    out SafeSymbolicLinkObjectHandle LinkHandle,
                    ACCESS_MASK DesiredAccess,
                    OBJECT_ATTRIBUTES ObjectAttributes);

                // https://msdn.microsoft.com/en-us/library/windows/hardware/ff567068.aspx
                // https://msdn.microsoft.com/en-us/library/bb470241.aspx
                [DllImport(Libraries.Ntdll, ExactSpelling = true)]
                public static extern int NtQuerySymbolicLinkObject(
                    SafeSymbolicLinkObjectHandle LinkHandle,
                    ref UNICODE_STRING LinkTarget,
                    out uint ReturnedLength);

                // https://msdn.microsoft.com/en-us/library/bb470238.aspx
                [DllImport(Libraries.Ntdll, ExactSpelling = true)]
                public static extern int NtQueryDirectoryObject(
                    SafeDirectoryObjectHandle DirectoryHandle,
                    SafeHandle Buffer,
                    uint Length,
                    [MarshalAs(UnmanagedType.U1)] bool ReturnSingleEntry,
                    [MarshalAs(UnmanagedType.U1)] bool RestartScan,
                    ref uint Context,
                    out uint ReturnLength);

                //  typedef struct _OBJECT_TYPES_INFORMATION
                //  {
                //      ULONG NumberOfTypes;
                //      OBJECT_TYPE_INFORMATION TypeInformation;
                //  } OBJECT_TYPES_INFORMATION, *POBJECT_TYPES_INFORMATION;
            }

            public static void CloseHandle(IntPtr handle)
            {
                if (!Direct.CloseHandle(handle))
                    throw ErrorHelper.GetIoExceptionForLastError();
            }

            /// <summary>
            /// Opend a handle to a directory object at the given NT path.
            /// </summary>
            public static SafeDirectoryObjectHandle OpenDirectoryObject(
                string path,
                ACCESS_MASK desiredAccess = ACCESS_MASK.DIRECTORY_QUERY)
            {
                return (SafeDirectoryObjectHandle)OpenObjectHelper(path, (attributes) =>
                {
                    SafeDirectoryObjectHandle directory;
                    int status = Direct.NtOpenDirectoryObject(
                        DirectoryHandle: out directory,
                        DesiredAccess: desiredAccess,
                        ObjectAttributes: attributes);

                    if (status != NtStatus.STATUS_SUCCESS)
                        throw ErrorHelper.GetIoExceptionForError(ErrorHandling.NtStatusToWinError(status), path);

                    return directory;
                });
            }

            /// <summary>
            /// Open a handle to a symbolic link at the given NT path.
            /// </summary>
            public static SafeSymbolicLinkObjectHandle OpenSymbolicLinkObject(
                string path,
                ACCESS_MASK desiredAccess = ACCESS_MASK.GENERIC_READ)
            {
                return (SafeSymbolicLinkObjectHandle)OpenObjectHelper(path, (attributes) =>
                {
                    SafeSymbolicLinkObjectHandle link;
                    int status = Direct.NtOpenSymbolicLinkObject(
                        LinkHandle: out link,
                        DesiredAccess: desiredAccess,
                        ObjectAttributes: attributes);

                    if (status != NtStatus.STATUS_SUCCESS)
                        throw ErrorHelper.GetIoExceptionForError(ErrorHandling.NtStatusToWinError(status), path);

                    return link;
                });
            }

            private static SafeHandle OpenObjectHelper(string path, Func<OBJECT_ATTRIBUTES, SafeHandle> invoker)
            {
                unsafe
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
                            Length = (uint)Marshal.SizeOf<OBJECT_ATTRIBUTES>(),
                            RootDirectory = IntPtr.Zero,
                            ObjectName = (IntPtr)(&objectName),
                            SecurityDescriptor = IntPtr.Zero,
                            SecurityQualityOfService = IntPtr.Zero
                        };

                        return invoker(attributes);
                    }
                }
            }

            /// <summary>
            /// Get the symbolic link's target path.
            /// </summary>
            public static string GetSymbolicLinkTarget(SafeSymbolicLinkObjectHandle linkHandle)
            {
                return StringBufferCache.CachedBufferInvoke((buffer) =>
                {
                    UNICODE_STRING target = new UNICODE_STRING(buffer);
                    uint returnedLength;
                    int status;
                    while ((status = Direct.NtQuerySymbolicLinkObject(linkHandle, ref target, out returnedLength)) == NtStatus.STATUS_BUFFER_TOO_SMALL)
                    {
                        buffer.EnsureByteCapacity(returnedLength);
                        target.UpdateFromStringBuffer(buffer);
                    }

                    if (status != NtStatus.STATUS_SUCCESS)
                        throw ErrorHelper.GetIoExceptionForError(ErrorHandling.NtStatusToWinError(status));

                    buffer.Length = (uint)(target.Length / sizeof(char));
                    return buffer.ToString();
                });
            }

            public static IEnumerable<ObjectInformation> GetDirectoryEntries(SafeDirectoryObjectHandle directoryHandle)
            {
                List<ObjectInformation> infos = new List<ObjectInformation>();

                StringBufferCache.CachedBufferInvoke((buffer) =>
                {
                    buffer.EnsureCharCapacity(1024);

                    uint context = 0;
                    uint returnLength;
                    int status;

                    do
                    {
                        status = Direct.NtQueryDirectoryObject(
                            DirectoryHandle: directoryHandle,
                            Buffer: buffer,
                            Length: (uint)buffer.ByteCapacity,
                            ReturnSingleEntry: false,
                            RestartScan: false,
                            Context: ref context,
                            ReturnLength: out returnLength);

                        if (status != NtStatus.STATUS_SUCCESS && status != NtStatus.STATUS_MORE_ENTRIES)
                            break;

                        NativeBufferReader reader = new NativeBufferReader(buffer);

                        do
                        {
                            UNICODE_STRING name = reader.ReadStruct<UNICODE_STRING>();
                            if (name.Length == 0) break;
                            UNICODE_STRING type = reader.ReadStruct<UNICODE_STRING>();

                            infos.Add(new ObjectInformation
                            {
                                Name = name.ToString(),
                                TypeName = type.ToString()
                            });
                        } while (true);

                    } while (status == NtStatus.STATUS_MORE_ENTRIES);

                    if (status != NtStatus.STATUS_SUCCESS)
                        throw ErrorHelper.GetIoExceptionForError(ErrorHandling.NtStatusToWinError(status));
                });

                return infos.OrderBy(i => i.Name); ;
            }

            /// <summary>
            /// Get the name fot he given handle. This is typically the NT path of the object.
            /// </summary>
            public static string GetObjectName(SafeHandle handle)
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

                using (NativeBuffer buffer = new NativeBuffer())
                {
                    int status = NtStatus.STATUS_BUFFER_OVERFLOW;
                    uint returnLength = 260 * sizeof(char);

                    while (status == NtStatus.STATUS_BUFFER_OVERFLOW || status == NtStatus.STATUS_BUFFER_TOO_SMALL)
                    {
                        buffer.EnsureByteCapacity(returnLength);

                        status = Direct.NtQueryObject(
                            Handle: handle,
                            ObjectInformationClass: OBJECT_INFORMATION_CLASS.ObjectNameInformation,
                            ObjectInformation: buffer.DangerousGetHandle(),
                            ObjectInformationLength: checked((uint)buffer.ByteCapacity),
                            ReturnLength: out returnLength);
                    }

                    if (!ErrorHandling.NT_SUCCESS(status))
                        throw ErrorHelper.GetIoExceptionForError(ErrorHandling.NtStatusToWinError(status));

                    return new NativeBufferReader(buffer).ReadStruct<UNICODE_STRING>().ToString();
                }
            }

            /// <summary>
            /// Get the type name of the given object.
            /// </summary>
            public static string GetObjectType(SafeHandle handle)
            {
                using (NativeBuffer buffer = new NativeBuffer())
                {
                    int status = NtStatus.STATUS_BUFFER_OVERFLOW;

                    // We'll initially give room for 50 characters for the type name
                    uint returnLength = (uint)Marshal.SizeOf<OBJECT_TYPE_INFORMATION>() + 50 * sizeof(char);

                    while (status == NtStatus.STATUS_BUFFER_OVERFLOW || status == NtStatus.STATUS_BUFFER_TOO_SMALL || status == NtStatus.STATUS_INFO_LENGTH_MISMATCH)
                    {
                        buffer.EnsureByteCapacity(returnLength);

                        status = Direct.NtQueryObject(
                            Handle: handle,
                            ObjectInformationClass: OBJECT_INFORMATION_CLASS.ObjectTypeInformation,
                            ObjectInformation: buffer.DangerousGetHandle(),
                            ObjectInformationLength: checked((uint)buffer.ByteCapacity),
                            ReturnLength: out returnLength);
                    }

                    if (!ErrorHandling.NT_SUCCESS(status))
                        throw ErrorHelper.GetIoExceptionForError(ErrorHandling.NtStatusToWinError(status));

                    return new NativeBufferReader(buffer).ReadStruct<OBJECT_TYPE_INFORMATION>().TypeName.ToString();
                }
            }
        }
    }
}
