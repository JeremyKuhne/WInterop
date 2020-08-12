// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.Security;

namespace WInterop.Network.Native
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
        public static unsafe extern WindowsError NetLocalGroupAdd(
            string? servername,
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
            string? servername,
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
            string? servername,
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

        // https://docs.microsoft.com/en-us/windows/desktop/api/lmaccess/nf-lmaccess-netuserenum
        [DllImport(Libraries.Netapi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern WindowsError NetUserEnum(
            string? servername,
            uint level,
            UserEnumFilter filter,
            out NetApiBufferHandle bufptr,
            uint prefmaxlen,
            out uint entriesread,
            out uint totalentries,
            IntPtr resume_handle);

        // https://docs.microsoft.com/en-us/windows/desktop/api/lmaccess/nf-lmaccess-netusergetinfo
        [DllImport(Libraries.Netapi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern WindowsError NetUserGetInfo(
            string? servername,
            string username,
            uint level,
            out NetApiBufferHandle bufptr);
    }
}
