// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using Xunit;
using WInterop.Authorization;
using System.Security.Principal;
using WInterop.Authorization.DataTypes;
using System;
using System.Diagnostics;
using WInterop.SystemInformation;

namespace DesktopTests.Authorization
{
    public class AuthorizationTests
    {
        [Fact]
        public void IsElevated()
        {
            // NOTE: This check may not always be true. Haven't tried actually running this
            // while logged in as the actual Administrator account. (Also, would the Domain admin
            // make any difference?) The Authorization method we're calling here isn't
            // terribly well documented.

            bool runningAsAdmin = 
                new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

            AuthorizationDesktopMethods.IsProcessElevated().Should().Be(runningAsAdmin);
        }

        [Fact]
        public void CreateWellKnownSid_Everyone()
        {
            SID sid = AuthorizationDesktopMethods.CreateWellKnownSid(WELL_KNOWN_SID_TYPE.WinWorldSid);
            AuthorizationDesktopMethods.IsValidSid(ref sid).Should().BeTrue();
            sid.Revision.Should().Be(1);
            sid.IdentifierAuthority.Should().Be(SID_IDENTIFIER_AUTHORITY.SECURITY_WORLD_SID_AUTHORITY);

            AuthorizationDesktopMethods.GetSidSubAuthorityCount(ref sid).Should().Be(1);
            AuthorizationDesktopMethods.GetSidSubAuthority(ref sid, 0).Should().Be(0);

            AuthorizationDesktopMethods.IsWellKnownSid(ref sid, WELL_KNOWN_SID_TYPE.WinWorldSid).Should().BeTrue();
            AuthorizationDesktopMethods.ConvertSidToString(ref sid).Should().Be("S-1-1-0");

            AccountSidInfo info = AuthorizationDesktopMethods.LookupAccountSidLocal(ref sid);
            info.Name.Should().Be("Everyone");
            info.DomainName.Should().Be("");
            info.Usage.Should().Be(SID_NAME_USE.SidTypeWellKnownGroup);
        }

        [Fact]
        public void IsValidSid_GoodSid()
        {
            SID sid = AuthorizationDesktopMethods.CreateWellKnownSid(WELL_KNOWN_SID_TYPE.WinBuiltinIUsersSid);
            AuthorizationDesktopMethods.IsValidSid(ref sid).Should().BeTrue();
        }

        // [Fact]
        public void DumpAllWellKnownSids()
        {
            foreach (WELL_KNOWN_SID_TYPE type in Enum.GetValues(typeof(WELL_KNOWN_SID_TYPE)))
            {
                Debug.WriteLine(@"/// <summary>");
                try
                {
                    SID sid = AuthorizationDesktopMethods.CreateWellKnownSid(type);
                    AccountSidInfo info = AuthorizationDesktopMethods.LookupAccountSidLocal(ref sid);
                    Debug.WriteLine($"/// {info.Name} ({AuthorizationDesktopMethods.ConvertSidToString(ref sid)}) [{info.Usage}]");
                }
                catch
                {
                    Debug.WriteLine($"/// Unable to retrieve");
                }
                Debug.WriteLine(@"/// </summary>");
                Debug.WriteLine($"{type} = {(int)type},");
                Debug.WriteLine("");
            }
        }

        [Fact]
        public void IsValidSid_BadSid()
        {
            SID sid = new SID();
            AuthorizationDesktopMethods.IsValidSid(ref sid).Should().BeFalse();
        }

        [Fact]
        public void GetTokenSid_ForCurrentProcess()
        {
            SID sid;
            using (var token = AuthorizationDesktopMethods.OpenProcessToken(TokenRights.TOKEN_READ))
            {
                token.IsInvalid.Should().BeFalse();
                sid = AuthorizationDesktopMethods.GetTokenSid(token);
            }
            AuthorizationDesktopMethods.IsValidSid(ref sid).Should().BeTrue();

            AccountSidInfo info = AuthorizationDesktopMethods.LookupAccountSidLocal(ref sid);
            info.Name.Should().Be(SystemInformationDesktopMethods.GetUserName());
        }

        [Fact]
        public void GetTokenPrivileges_ForCurrentProcess()
        {
            using (var token = AuthorizationDesktopMethods.OpenProcessToken(TokenRights.TOKEN_READ))
            {
                token.IsInvalid.Should().BeFalse();
                var privileges = AuthorizationDesktopMethods.GetTokenPrivileges(token);
                privileges.Should().NotBeEmpty();

                // This Privilege should always exist
                privileges.Should().Contain(s => s.Privilege == Privileges.SeChangeNotifyPrivilege);

                // Check the helper
                AuthorizationDesktopMethods.HasPrivilege(token, Privileges.SeChangeNotifyPrivilege).Should().BeTrue();
            }
        }

        [Fact]
        public void IsPrivilegeEnabled_ForCurrentProcess()
        {
            using (var token = AuthorizationDesktopMethods.OpenProcessToken(TokenRights.TOKEN_READ))
            {
                token.IsInvalid.Should().BeFalse();
                AuthorizationDesktopMethods.IsPrivilegeEnabled(token, Privileges.SeChangeNotifyPrivilege).Should().BeTrue();
                AuthorizationDesktopMethods.IsPrivilegeEnabled(token, Privileges.SeBackupPrivilege).Should().BeFalse();
            }
        }
    }
}
