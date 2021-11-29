// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Network.Native;

namespace WInterop.Network;

public struct UserInfo2
{
    public string Name { get; }
    public string FullName { get; }
    public string Comment { get; }
    public string UserComment { get; }
    public UserPrivilege UserPrivilege { get; }
    public UserFlags UserFlags { get; }
    public string HomeDirectory { get; }
    public string LogonScript { get; }
    public string Parameters { get; }
    public string Workstations { get; }
    public string LogonServer { get; }

    public unsafe UserInfo2(USER_INFO_2 data)
    {
        static string StringOrEmpty(char* c) => c == null ? string.Empty : new string(c);

        Name = new string(data.usri2_name);
        FullName = StringOrEmpty(data.usri2_full_name);
        Comment = StringOrEmpty(data.usri2_comment);
        UserComment = StringOrEmpty(data.usri2_usr_comment);
        UserPrivilege = data.usri2_priv;
        UserFlags = data.usri2_flags;
        HomeDirectory = StringOrEmpty(data.usri2_home_dir);
        LogonScript = StringOrEmpty(data.usri2_script_path);
        Parameters = StringOrEmpty(data.usri2_parms);
        Workstations = StringOrEmpty(data.usri2_workstations);
        LogonServer = StringOrEmpty(data.usri2_logon_server);
    }
}