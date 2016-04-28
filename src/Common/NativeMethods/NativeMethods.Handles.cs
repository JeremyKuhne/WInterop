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

namespace WInterop
{
    public static partial class NativeMethods
    {
        internal static class Handles
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
                internal static extern bool CloseHandle(
                    IntPtr handle);

                // http://forum.sysinternals.com/howto-enumerate-handles_topic18892.html

                // https://msdn.microsoft.com/en-us/library/bb432383.aspx
                // https://msdn.microsoft.com/en-us/library/windows/hardware/ff567062.aspx
                // The Zw version of this is documented as available to UWP, Nt has no clarifying restrictions and is not included in a header.
                [DllImport(Libraries.Ntdll, SetLastError = true, ExactSpelling = true)]
                internal static extern int NtQueryObject(
                    IntPtr Handle,
                    OBJECT_INFORMATION_CLASS ObjectInformationClass,
                    IntPtr ObjectInformation,
                    uint ObjectInformationLength,
                    out uint ReturnLength);

                // https://msdn.microsoft.com/en-us/library/windows/hardware/ff550964.aspx
                internal enum OBJECT_INFORMATION_CLASS
                {
                    ObjectBasicInformation,

                    // Undocumented directly, returns a UNICODE_STRING
                    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff548474(v=vs.85).aspx
                    ObjectNameInformation,

                    ObjectTypeInformation,

                    // Undocumented
                    // https://ntquery.wordpress.com/2014/03/30/anti-debug-ntqueryobject/#more-21
                    ObjectTypesInformation
                }

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

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa446633.aspx
                // ACCESS_MASK
                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa374892.aspx
                [StructLayout(LayoutKind.Sequential)]
                internal struct GENERIC_MAPPING
                {
                    uint GenericRead;
                    uint GenericWrite;
                    uint GenericExecute;
                    uint GenericAll;
                }

                // https://msdn.microsoft.com/en-us/library/windows/hardware/ff564586.aspx
                // https://msdn.microsoft.com/en-us/library/windows/hardware/ff547804.aspx
                // 
                [Flags]
                internal enum ObjectAttributes : uint
                {
                    /// <summary>
                    /// This handle can be inherited by child processes of the current process.
                    /// </summary>
                    OBJ_INHERIT = 0x00000002,

                    /// <summary>
                    /// This flag only applies to objects that are named within the object manager.
                    /// By default, such objects are deleted when all open handles to them are closed.
                    /// If this flag is specified, the object is not deleted when all open handles are closed.
                    /// </summary>
                    OBJ_PERMANENT = 0x00000010,

                    /// <summary>
                    /// Only a single handle can be open for this object.
                    /// </summary>
                    OBJ_EXCLUSIVE = 0x00000020,

                    /// <summary>
                    /// Lookups for this object should be case insensitive
                    /// </summary>
                    OBJ_CASE_INSENSITIVE = 0x00000040,

                    /// <summary>
                    /// Create on existing object should open, not fail with STATUS_OBJECT_NAME_COLLISION
                    /// </summary>
                    OBJ_OPENIF = 0x00000080,

                    /// <summary>
                    /// Open the symbolic link, not it's target
                    /// </summary>
                    OBJ_OPENLINK = 0x00000100,

                    // Only accessible from kernel mode
                    // OBJ_KERNEL_HANDLE

                    // Access checks enforced, even in kernel mode
                    // OBJ_FORCE_ACCESS_CHECK
                    // OBJ_VALID_ATTRIBUTES = 0x000001F2
                }

                // The full struct isn't officially documented, names may be wrong.
                //
                //  https://msdn.microsoft.com/en-us/library/windows/hardware/ff551947.aspx
                //      typedef struct __PUBLIC_OBJECT_TYPE_INFORMATION
                //      {
                //          UNICODE_STRING TypeName;
                //          ULONG Reserved[22];    // reserved for internal use
                //      } PUBLIC_OBJECT_TYPE_INFORMATION, *PPUBLIC_OBJECT_TYPE_INFORMATION;
                //
                [StructLayout(LayoutKind.Sequential)]
                internal struct OBJECT_TYPE_INFORMATION
                {
                    public UNICODE_STRING TypeName;

                    // All below are not officially documented, names may be incorrect

                    public uint TotalNumberOfObjects;
                    public uint TotalNumberOfHandles;
                    public uint TotalPagedPoolUsage;
                    public uint TotalNonPagedPoolUsage;
                    public uint TotalNamePoolUsage;
                    public uint TotalHandleTableUsage;

                    // HighWater is the peak value
                    public uint HighWaterNumberOfObjects;
                    public uint HighWaterNumberOfHandles;
                    public uint HighWaterPagedPoolUsage;
                    public uint HighWaterNonPagedPoolUsage;
                    public uint HighWaterNamePoolUsage;
                    public uint HighWaterHandleTableUsage;

                    /// <summary>
                    /// Attributes that can't be used on instances
                    /// </summary>
                    public ObjectAttributes InvalidAttributes;

                    /// <summary>
                    /// Mapping of generic access rights (read/write/execute/all) to the type's specific rights
                    /// </summary>
                    public GENERIC_MAPPING GenericMapping;

                    /// <summary>
                    /// Types of access a thread can request when opening a handle to an object of this type
                    /// (read, write, terminate, suspend, etc.)
                    /// </summary>
                    public uint ValidAccessMask;
                    public byte SecurityRequired;
                    public byte MaintainHandleCount;
                    public byte TypeIndex;
                    public byte ReservedByte;

                    /// <summary>
                    /// Instances are allocated from paged or non-paged (0) memory
                    /// </summary>
                    public uint PoolType;
                    public uint DefaultPagedPoolCharge;
                    public uint DefaultNonPagedPoolCharge;
                }

                internal static uint ObjectTypeInformationSize = (uint)Marshal.SizeOf<OBJECT_TYPE_INFORMATION>();

                // The full struct isn't officially documented, names may be wrong.
                //
                //  http://msdn.microsoft.com/en-us/library/windows/hardware/ff551944.aspx
                //      typedef struct _PUBLIC_OBJECT_BASIC_INFORMATION
                //      {
                //          ULONG Attributes;
                //          ACCESS_MASK GrantedAccess;
                //          ULONG HandleCount;
                //          ULONG PointerCount;
                //          ULONG Reserved[10];    // reserved for internal use
                //      } PUBLIC_OBJECT_BASIC_INFORMATION, *PPUBLIC_OBJECT_BASIC_INFORMATION;
                //
                // ACCESS_MASK
                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa374892.aspx
                //
                [StructLayout(LayoutKind.Sequential)]
                internal struct OBJECT_BASIC_INFORMATION
                {
                    public ObjectAttributes Attributes;
                    public uint GrantedAccess;
                    public uint HandleCount;
                    public uint PointerCount;
                    public uint PagedPoolCharge;
                    public uint NonPagedPoolCharge;
                    public uint Reserved1;
                    public uint Reserved2;
                    public uint Reserved3;
                    public uint NameInfoSize;
                    public uint TypeInfoSize;
                    public uint SecurityDescriptorSize;
                    public long CreationTime;
                }

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

            unsafe public static string GetObjectName(IntPtr windowsObject)
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

                using (NativeBuffer nb = new NativeBuffer())
                {
                    int status = NtStatus.STATUS_BUFFER_OVERFLOW;
                    uint returnLength = 260 * sizeof(char);

                    while (status == NtStatus.STATUS_BUFFER_OVERFLOW || status == NtStatus.STATUS_BUFFER_TOO_SMALL)
                    {
                        nb.EnsureByteCapacity(returnLength);

                        status = Direct.NtQueryObject(
                            Handle: windowsObject,
                            ObjectInformationClass: Direct.OBJECT_INFORMATION_CLASS.ObjectNameInformation,
                            ObjectInformation: nb.DangerousGetHandle(),
                            ObjectInformationLength: checked((uint)nb.ByteCapacity),
                            ReturnLength: out returnLength);
                    }

                    if (!ErrorHandling.NT_SUCCESS(status))
                    {
                        throw ErrorHelper.GetIoExceptionForError(ErrorHandling.NtStatusToWinError(status));
                    }

                    var info = Marshal.PtrToStructure<UNICODE_STRING>(nb.DangerousGetHandle());

                    // The string isn't null terminated so we have to explicitly pass the size
                    return new string(info.Buffer, 0, info.Length / sizeof(char));
                }
            }

            unsafe public static string GetObjectTypeName(IntPtr windowsObject)
            {
                using (NativeBuffer nb = new NativeBuffer())
                {
                    int status = NtStatus.STATUS_BUFFER_OVERFLOW;

                    // We'll initially give room for 50 characters for the type name
                    uint returnLength = Direct.ObjectTypeInformationSize + 50 * sizeof(char);

                    while (status == NtStatus.STATUS_BUFFER_OVERFLOW || status == NtStatus.STATUS_BUFFER_TOO_SMALL || status == NtStatus.STATUS_INFO_LENGTH_MISMATCH)
                    {
                        nb.EnsureByteCapacity(returnLength);

                        status = Direct.NtQueryObject(
                            Handle: windowsObject,
                            ObjectInformationClass: Direct.OBJECT_INFORMATION_CLASS.ObjectTypeInformation,
                            ObjectInformation: nb.DangerousGetHandle(),
                            ObjectInformationLength: checked((uint)nb.ByteCapacity),
                            ReturnLength: out returnLength);
                    }

                    if (!ErrorHandling.NT_SUCCESS(status))
                    {
                        throw ErrorHelper.GetIoExceptionForError(ErrorHandling.NtStatusToWinError(status));
                    }

                    var info = Marshal.PtrToStructure<Direct.OBJECT_TYPE_INFORMATION>(nb.DangerousGetHandle());

                    // The string isn't null terminated so we have to explicitly pass the size
                    return new string(info.TypeName.Buffer, 0, info.TypeName.Length / sizeof(char));
                }
            }
        }
    }
}
