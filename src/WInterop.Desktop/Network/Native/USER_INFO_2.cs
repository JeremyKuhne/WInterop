// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Network.Native;

// https://docs.microsoft.com/en-us/windows/desktop/api/lmaccess/ns-lmaccess-_user_info_2
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public unsafe readonly struct USER_INFO_2
{
    public readonly char* usri2_name;
    public readonly char* usri2_password;
    public readonly uint usri2_password_age;
    public readonly UserPrivilege usri2_priv;
    public readonly char* usri2_home_dir;
    public readonly char* usri2_comment;
    public readonly UserFlags usri2_flags;
    public readonly char* usri2_script_path;
    public readonly uint usri2_auth_flags;
    public readonly char* usri2_full_name;
    public readonly char* usri2_usr_comment;
    public readonly char* usri2_parms;
    public readonly char* usri2_workstations;
    public readonly uint usri2_last_logon;
    public readonly uint usri2_last_logoff;
    public readonly uint usri2_acct_expires;
    public readonly uint usri2_max_storage;
    public readonly uint usri2_units_per_week;
    public readonly byte* usri2_logon_hours;
    public readonly uint usri2_bad_pw_count;
    public readonly uint usri2_num_logons;
    public readonly char* usri2_logon_server;
    public readonly uint usri2_country_code;
    public readonly uint usri2_code_page;
}