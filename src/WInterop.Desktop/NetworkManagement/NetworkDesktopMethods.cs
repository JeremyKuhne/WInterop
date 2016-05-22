// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WInterop.ErrorHandling;
using WInterop.Handles.Desktop;
using WInterop.NetworkManagement.Desktop;

namespace WInterop.NetworkManagement
{
    /// <summary>
    /// These methods are only available from Windows desktop apps. Windows store apps cannot access them.
    /// </summary>
    public static class NetworkDesktopMethods
    {
        /// <summary>
        /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        /// <remarks>
        /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
        /// </remarks>
        public static class Direct
        {
            // NET_API_STATUS is a DWORD (uint)
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa370304.aspx
            [DllImport(Libraries.Netapi32, ExactSpelling = true)]
            public static extern WindowsError NetApiBufferFree(
                IntPtr Buffer);

            // Local Group Functions
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa370283.aspx

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa370434.aspx
            [DllImport(Libraries.Netapi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public unsafe static extern WindowsError NetLocalGroupAdd(
                string servername,
                uint level,
                void* buf,
                out uint parm_err);

            // LOCALGROUP_INFO_0 is a pointer to a Unicode name string
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa370275.aspx
            //
            // LOCALGROUP_INFO_1 is two pointers, one to a name and one to the comment
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa370277.aspx

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa370437.aspx
            [DllImport(Libraries.Netapi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern uint NetLocalGroupDel(
                string servername,
                string groupname);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa370440.aspx
            [DllImport(Libraries.Netapi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern WindowsError NetLocalGroupEnum(
                string servername,
                uint level,
                out SafeNetApiBufferHandle bufptr,
                uint prefmaxlen,
                out uint entriesread,
                out uint totalentries,
                IntPtr resumehandle);

            public const uint MAX_PREFERRED_LENGTH = unchecked((uint)-1);

            // LOCALGROUP_INFO_0 is simply a pointer to a Unicode name string
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa370275.aspx
            //
            // LOCALGROUP_INFO_1 is two Unicode pointers, one to the name and one to the comment
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa370277.aspx

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa370601.aspx
            [DllImport(Libraries.Netapi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern WindowsError NetLocalGroupGetMembers(
                string servername,
                string localgroupname,
                uint level,
                out SafeNetApiBufferHandle bufptr,
                uint prefmaxlen,
                out uint entriesread,
                out uint totalentries,
                IntPtr resumehandle);

            // LOCALGROUP_MEMBERS_INFO_0 is a pointer to a SID
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa370278.aspx

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa370279.aspx
            [StructLayout(LayoutKind.Sequential)]
            public struct LOCALGROUP_MEMBERS_INFO_1
            {
                public IntPtr lgrmi1_sid;
                public SID_NAME_USE lgrmi1_sidusage;
                public IntPtr lgrmi1_name;
            }

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa370280.aspx
            [StructLayout(LayoutKind.Sequential)]
            public struct LOCALGROUP_MEMBERS_INFO_2
            {
                public IntPtr lgrmi2_sid;
                public SID_NAME_USE lgrmi2_sidusage;
                public IntPtr lgrmi2_domainandname;
            }

            // LOCALGROUP_MEMBERS_INFO_3 is a pointer to a Unicode DOMAIN\name string

            // Group Functions
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa370266.aspx
        }

        public static void NetApiBufferFree(IntPtr buffer)
        {
            WindowsError result = Direct.NetApiBufferFree(buffer);
            if (result != WindowsError.NERR_Success)
                throw ErrorHelper.GetIoExceptionForError(result);
        }

        public unsafe static void AddLocalGroup(string groupName, string comment = null, string server = null)
        {
            uint level = string.IsNullOrEmpty(comment) ? 0u : 1;
            uint parameter;

            fixed (char* fixedName = groupName)
            fixed (char* fixedComment = level == 0 ? null : comment)
            {
                char*[] data = new char*[] { fixedName, fixedComment };
                fixed (void* buffer = data)
                {
                    WindowsError result = Direct.NetLocalGroupAdd(
                        servername: server,
                        level: level,
                        buf: buffer,
                        parm_err: out parameter);

                    if (result != WindowsError.NERR_Success)
                        throw ErrorHelper.GetIoExceptionForError(result, groupName);
                }
            }
        }

        public static IEnumerable<string> EnumerateLocalGroups(string server = null)
        {
            var groups = new List<string>();

            SafeNetApiBufferHandle buffer;
            uint entriesRead;
            uint totalEntries;

            WindowsError result = Direct.NetLocalGroupEnum(
                servername: server,
                level: 0,
                bufptr: out buffer,
                prefmaxlen: Direct.MAX_PREFERRED_LENGTH,
                entriesread: out entriesRead,
                totalentries: out totalEntries,
                resumehandle: IntPtr.Zero);

            if (result != WindowsError.NERR_Success)
                throw ErrorHelper.GetIoExceptionForError(result, server);

            foreach (IntPtr pointer in ReadStructsFromBuffer<IntPtr>(buffer, entriesRead))
            {
                groups.Add(Marshal.PtrToStringUni(pointer));
            }

            return groups;
        }

        public static IEnumerable<MemberInfo> EnumerateGroupUsers(string groupName, string server = null)
        {
            var members = new List<MemberInfo>();

            SafeNetApiBufferHandle buffer;
            uint entriesRead;
            uint totalEntries;

            WindowsError result = Direct.NetLocalGroupGetMembers(
                servername: server,
                localgroupname: groupName,
                level: 1,
                bufptr: out buffer,
                prefmaxlen: Direct.MAX_PREFERRED_LENGTH,
                entriesread: out entriesRead,
                totalentries: out totalEntries,
                resumehandle: IntPtr.Zero);

            if (result != WindowsError.NERR_Success)
                throw ErrorHelper.GetIoExceptionForError(result, server);

            foreach (Direct.LOCALGROUP_MEMBERS_INFO_1 info in ReadStructsFromBuffer<Direct.LOCALGROUP_MEMBERS_INFO_1>(buffer, entriesRead))
            {
                members.Add(new MemberInfo
                {
                    Name = Marshal.PtrToStringUni(info.lgrmi1_name),
                    AccountType = (SID_NAME_USE)info.lgrmi1_sidusage
                });
            }

            return members;
        }

        private static IEnumerable<T> ReadStructsFromBuffer<T>(SafeNetApiBufferHandle buffer, uint count) where T : struct
        {
            uint size = (uint)Marshal.SizeOf<T>();
            var items = new List<T>((int)count);
            buffer.Initialize(numElements: count, sizeOfEachElement: size);

            for (uint i = 0; i < count; i++)
            {
                var current = buffer.Read<T>(i * size);
                items.Add(current);
            }

            return items;
        }
    }
}
