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

            AuthorizationMethods.IsProcessElevated().Should().Be(runningAsAdmin);
        }

        [Fact]
        public void CreateWellKnownSid_Everyone()
        {
            SID sid = AuthorizationMethods.CreateWellKnownSid(WELL_KNOWN_SID_TYPE.WinWorldSid);
            AuthorizationMethods.IsValidSid(ref sid).Should().BeTrue();
            sid.Revision.Should().Be(1);
            sid.IdentifierAuthority.Should().Be(SID_IDENTIFIER_AUTHORITY.SECURITY_WORLD_SID_AUTHORITY);

            AuthorizationMethods.GetSidSubAuthorityCount(ref sid).Should().Be(1);
            AuthorizationMethods.GetSidSubAuthority(ref sid, 0).Should().Be(0);

            AuthorizationMethods.IsWellKnownSid(ref sid, WELL_KNOWN_SID_TYPE.WinWorldSid).Should().BeTrue();
            AuthorizationMethods.ConvertSidToString(ref sid).Should().Be("S-1-1-0");

            AccountSidInfo info = AuthorizationMethods.LookupAccountSidLocal(ref sid);
            info.Name.Should().Be("Everyone");
            info.DomainName.Should().Be("");
            info.Usage.Should().Be(SID_NAME_USE.SidTypeWellKnownGroup);
        }

        [Fact]
        public void IsValidSid_GoodSid()
        {
            SID sid = AuthorizationMethods.CreateWellKnownSid(WELL_KNOWN_SID_TYPE.WinBuiltinIUsersSid);
            AuthorizationMethods.IsValidSid(ref sid).Should().BeTrue();
        }

        // [Fact]
        public void DumpAllWellKnownSids()
        {
            foreach (WELL_KNOWN_SID_TYPE type in Enum.GetValues(typeof(WELL_KNOWN_SID_TYPE)))
            {
                Debug.WriteLine(@"/// <summary>");
                try
                {
                    SID sid = AuthorizationMethods.CreateWellKnownSid(type);
                    AccountSidInfo info = AuthorizationMethods.LookupAccountSidLocal(ref sid);
                    Debug.WriteLine($"/// {info.Name} ({AuthorizationMethods.ConvertSidToString(ref sid)}) [{info.Usage}]");
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
            AuthorizationMethods.IsValidSid(ref sid).Should().BeFalse();
        }

        [Fact]
        public void GetTokenSid_ForCurrentProcess()
        {
            SID sid;
            using (var token = AuthorizationMethods.OpenProcessToken(TokenRights.TOKEN_READ))
            {
                token.IsInvalid.Should().BeFalse();
                sid = AuthorizationMethods.GetTokenSid(token);
            }
            AuthorizationMethods.IsValidSid(ref sid).Should().BeTrue();

            AccountSidInfo info = AuthorizationMethods.LookupAccountSidLocal(ref sid);
            info.Name.Should().Be(SystemInformationMethods.GetUserName());
        }

        [Fact]
        public void GetTokenPrivileges_ForCurrentProcess()
        {
            using (var token = AuthorizationMethods.OpenProcessToken(TokenRights.TOKEN_READ))
            {
                token.IsInvalid.Should().BeFalse();
                var privileges = AuthorizationMethods.GetTokenPrivileges(token);
                privileges.Should().NotBeEmpty();

                // This Privilege should always exist
                privileges.Should().Contain(s => s.Privilege == Privileges.SeChangeNotifyPrivilege);

                // Check the helper
                AuthorizationMethods.HasPrivilege(token, Privileges.SeChangeNotifyPrivilege).Should().BeTrue();
            }
        }

        [Fact]
        public void IsPrivilegeEnabled_ForCurrentProcess()
        {
            using (var token = AuthorizationMethods.OpenProcessToken(TokenRights.TOKEN_READ))
            {
                token.IsInvalid.Should().BeFalse();
                AuthorizationMethods.IsPrivilegeEnabled(token, Privileges.SeChangeNotifyPrivilege).Should().BeTrue();
                AuthorizationMethods.IsPrivilegeEnabled(token, Privileges.SeBackupPrivilege).Should().BeFalse();
            }
        }
    }
}
