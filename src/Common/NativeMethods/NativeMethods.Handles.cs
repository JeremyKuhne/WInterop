// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using System.Security;
using WInterop.Authentication;
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
                [DllImport(Libraries.Ntdll, SetLastError = true, ExactSpelling = true)]
                public static extern int NtQueryObject(
                    IntPtr Handle,
                    OBJECT_INFORMATION_CLASS ObjectInformationClass,
                    IntPtr ObjectInformation,
                    uint ObjectInformationLength,
                    out uint ReturnLength);

                // https://msdn.microsoft.com/en-us/library/bb432383(v=vs.85).aspx
                //
                //  IoQueryFileDosDeviceName wraps this
                //  https://msdn.microsoft.com/en-us/library/windows/hardware/ff548474.aspx
                //      typedef struct _OBJECT_NAME_INFORMATION
                //      {
                //          UNICODE_STRING Name;
                //      } OBJECT_NAME_INFORMATION, *POBJECT_NAME_INFORMATION;
                //
                // There isn't any point in wrapping this as it is simply a UNICODE_STRING directly
                // followed by it's backing buffer.

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

            public static string GetObjectName(SafeHandle windowsHandle)
            {
                bool success = true;
                windowsHandle.DangerousAddRef(ref success);
                try
                {
                    return GetObjectName(windowsHandle.DangerousGetHandle());
                }
                finally
                {
                    windowsHandle.DangerousRelease();
                }
            }

            public static string GetObjectName(IntPtr windowsObject)
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
                            Handle: windowsObject,
                            ObjectInformationClass: OBJECT_INFORMATION_CLASS.ObjectNameInformation,
                            ObjectInformation: buffer.DangerousGetHandle(),
                            ObjectInformationLength: checked((uint)buffer.ByteCapacity),
                            ReturnLength: out returnLength);
                    }

                    if (!ErrorHandling.NT_SUCCESS(status))
                    {
                        throw ErrorHelper.GetIoExceptionForError(ErrorHandling.NtStatusToWinError(status));
                    }

                    var info = Marshal.PtrToStructure<UNICODE_STRING>(buffer.DangerousGetHandle());

                    // The string isn't null terminated so we have to explicitly pass the size
                    unsafe
                    {
                        return new string(info.Buffer, 0, info.Length / sizeof(char));
                    }
                }
            }

            public static string GetObjectTypeName(SafeHandle windowsHandle)
            {
                return GetObjectTypeName(windowsHandle.DangerousGetHandle());
            }

            public static string GetObjectTypeName(IntPtr windowsObject)
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
                            Handle: windowsObject,
                            ObjectInformationClass: OBJECT_INFORMATION_CLASS.ObjectTypeInformation,
                            ObjectInformation: buffer.DangerousGetHandle(),
                            ObjectInformationLength: checked((uint)buffer.ByteCapacity),
                            ReturnLength: out returnLength);
                    }

                    if (!ErrorHandling.NT_SUCCESS(status))
                    {
                        throw ErrorHelper.GetIoExceptionForError(ErrorHandling.NtStatusToWinError(status));
                    }

                    var info = Marshal.PtrToStructure<OBJECT_TYPE_INFORMATION>(buffer.DangerousGetHandle());

                    // The string isn't null terminated so we have to explicitly pass the size
                    unsafe
                    {
                        return new string(info.TypeName.Buffer, 0, info.TypeName.Length / sizeof(char));
                    }
                }
            }
        }
    }
}
