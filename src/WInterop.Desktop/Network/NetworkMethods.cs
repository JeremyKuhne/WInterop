// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.Network.Native;

namespace WInterop.Network
{
    public static partial class Network
    {
        public static void NetApiBufferFree(IntPtr buffer) => Imports.NetApiBufferFree(buffer).ThrowIfFailed();

        public unsafe static void AddLocalGroup(string groupName, string comment = null, string server = null)
        {
            uint level = string.IsNullOrEmpty(comment) ? 0u : 1;

            fixed (char* fixedName = groupName)
            fixed (char* fixedComment = level == 0 ? null : comment)
            {
                char*[] data = new char*[] { fixedName, fixedComment };
                fixed (void* buffer = data)
                {
                    Imports.NetLocalGroupAdd(
                        servername: server,
                        level: level,
                        buf: buffer,
                        parm_err: out uint parameter)
                        .ThrowIfFailed();
                }
            }
        }

        public static IEnumerable<string> EnumerateLocalGroups(string server = null)
        {
            var groups = new List<string>();

            Imports.NetLocalGroupEnum(
                servername: server,
                level: 0,
                bufptr: out var buffer,
                prefmaxlen: Imports.MAX_PREFERRED_LENGTH,
                entriesread: out uint entriesRead,
                totalentries: out uint totalEntries,
                resumehandle: IntPtr.Zero)
                .ThrowIfFailed(server);

            foreach (IntPtr pointer in ReadStructsFromBuffer<IntPtr>(buffer, entriesRead))
            {
                groups.Add(Marshal.PtrToStringUni(pointer));
            }

            return groups;
        }

        public static IEnumerable<MemberInfo> EnumerateGroupUsers(string groupName, string server = null)
        {
            var members = new List<MemberInfo>();

            Imports.NetLocalGroupGetMembers(
                servername: server,
                localgroupname: groupName,
                level: 1,
                bufptr: out var buffer,
                prefmaxlen: Imports.MAX_PREFERRED_LENGTH,
                entriesread: out uint entriesRead,
                totalentries: out uint totalEntries,
                resumehandle: IntPtr.Zero)
                .ThrowIfFailed();

            foreach (Imports.LOCALGROUP_MEMBERS_INFO_1 info in ReadStructsFromBuffer<Imports.LOCALGROUP_MEMBERS_INFO_1>(buffer, entriesRead))
            {
                members.Add(new MemberInfo
                {
                    Name = Marshal.PtrToStringUni(info.lgrmi1_name),
                    AccountType = info.lgrmi1_sidusage
                });
            }

            return members;
        }

        public static IEnumerable<string> EnumerateUsers(string server = null)
        {
            var groups = new List<string>();

            Imports.NetUserEnum(
                servername: server,
                level: 0,
                filter: default,
                bufptr: out var buffer,
                prefmaxlen: Imports.MAX_PREFERRED_LENGTH,
                entriesread: out uint entriesRead,
                totalentries: out uint totalEntries,
                resume_handle: IntPtr.Zero)
                .ThrowIfFailed(server);

            foreach (IntPtr pointer in ReadStructsFromBuffer<IntPtr>(buffer, entriesRead))
            {
                groups.Add(Marshal.PtrToStringUni(pointer));
            }

            return groups;
        }

        public static UserInfo2 GetUserInfo(string user, string server = null)
        {
            Imports.NetUserGetInfo(server, user, 2, out var buffer).ThrowIfFailed(user);
            return new UserInfo2(buffer.ReadStructFromBuffer<USER_INFO_2>());
        }

        private unsafe static T ReadStructFromBuffer<T>(this NetApiBufferHandle buffer) where T : unmanaged
        {
            uint size = (uint)sizeof(T);
            buffer.Initialize(numElements: 1, sizeOfEachElement: size);
            return buffer.Read<T>(0);
        }

        private unsafe static IEnumerable<T> ReadStructsFromBuffer<T>(this NetApiBufferHandle buffer, uint count) where T : unmanaged
        {
            uint size = (uint)sizeof(T);
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
