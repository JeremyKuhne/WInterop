// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WInterop.Security;
using WInterop.Errors;
using WInterop.NetworkManagement.Types;
using WInterop.Support;

namespace WInterop.NetworkManagement
{
    public static partial class NetworkMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
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
                out NetApiBufferHandle bufptr,
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
                out NetApiBufferHandle bufptr,
                uint prefmaxlen,
                out uint entriesread,
                out uint totalentries,
                IntPtr resumehandle);

            // LOCALGROUP_MEMBERS_INFO_0 is a pointer to a SID
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa370278.aspx

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa370279.aspx
            public struct LOCALGROUP_MEMBERS_INFO_1
            {
                public IntPtr lgrmi1_sid;
                public SidNameUse lgrmi1_sidusage;
                public IntPtr lgrmi1_name;
            }

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa370280.aspx
            public struct LOCALGROUP_MEMBERS_INFO_2
            {
                public IntPtr lgrmi2_sid;
                public SidNameUse lgrmi2_sidusage;
                public IntPtr lgrmi2_domainandname;
            }

            // LOCALGROUP_MEMBERS_INFO_3 is a pointer to a Unicode DOMAIN\name string

            // Group Functions
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa370266.aspx
        }

        public static void NetApiBufferFree(IntPtr buffer)
        {
            WindowsError result = Imports.NetApiBufferFree(buffer);
            if (result != WindowsError.NERR_Success)
                throw Error.GetIoExceptionForError(result);
        }

        public unsafe static void AddLocalGroup(string groupName, string comment = null, string server = null)
        {
            uint level = string.IsNullOrEmpty(comment) ? 0u : 1;

            fixed (char* fixedName = groupName)
            fixed (char* fixedComment = level == 0 ? null : comment)
            {
                char*[] data = new char*[] { fixedName, fixedComment };
                fixed (void* buffer = data)
                {
                    WindowsError result = Imports.NetLocalGroupAdd(
                        servername: server,
                        level: level,
                        buf: buffer,
                        parm_err: out uint parameter);

                    if (result != WindowsError.NERR_Success)
                        throw Error.GetIoExceptionForError(result, groupName);
                }
            }
        }

        public static IEnumerable<string> EnumerateLocalGroups(string server = null)
        {
            var groups = new List<string>();

            WindowsError result = Imports.NetLocalGroupEnum(
                servername: server,
                level: 0,
                bufptr: out var buffer,
                prefmaxlen: Imports.MAX_PREFERRED_LENGTH,
                entriesread: out uint entriesRead,
                totalentries: out uint totalEntries,
                resumehandle: IntPtr.Zero);

            if (result != WindowsError.NERR_Success)
                throw Error.GetIoExceptionForError(result, server);

            foreach (IntPtr pointer in ReadStructsFromBuffer<IntPtr>(buffer, entriesRead))
            {
                groups.Add(Marshal.PtrToStringUni(pointer));
            }

            return groups;
        }

        public static IEnumerable<MemberInfo> EnumerateGroupUsers(string groupName, string server = null)
        {
            var members = new List<MemberInfo>();

            WindowsError result = Imports.NetLocalGroupGetMembers(
                servername: server,
                localgroupname: groupName,
                level: 1,
                bufptr: out var buffer,
                prefmaxlen: Imports.MAX_PREFERRED_LENGTH,
                entriesread: out uint entriesRead,
                totalentries: out uint totalEntries,
                resumehandle: IntPtr.Zero);

            if (result != WindowsError.NERR_Success)
                throw Error.GetIoExceptionForError(result, server);

            foreach (Imports.LOCALGROUP_MEMBERS_INFO_1 info in ReadStructsFromBuffer<Imports.LOCALGROUP_MEMBERS_INFO_1>(buffer, entriesRead))
            {
                members.Add(new MemberInfo
                {
                    Name = Marshal.PtrToStringUni(info.lgrmi1_name),
                    AccountType = (SidNameUse)info.lgrmi1_sidusage
                });
            }

            return members;
        }

        private static IEnumerable<T> ReadStructsFromBuffer<T>(NetApiBufferHandle buffer, uint count) where T : struct
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
