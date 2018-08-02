// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using Xunit;
using WInterop.Authorization;
using System.Security.Principal;
using WInterop.Authorization.Types;
using System;
using System.Diagnostics;
using WInterop.SystemInformation;
using System.Collections.Generic;
using System.Linq;

namespace Authorization
{
    public class Basic
    {
        [Fact]
        public unsafe void SidSize()
        {
            sizeof(SID).Should().Be(68);
        }

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
            SID sid = AuthorizationMethods.CreateWellKnownSid(WellKnownSID.World);
            AuthorizationMethods.IsValidSid(in sid).Should().BeTrue();
            sid.Revision.Should().Be(1);
            sid.IdentifierAuthority.Should().Be(IdentifierAuthority.World);

            AuthorizationMethods.GetSidSubAuthorityCount(in sid).Should().Be(1);
            AuthorizationMethods.GetSidSubAuthority(in sid, 0).Should().Be(0);

            AuthorizationMethods.IsWellKnownSid(in sid, WellKnownSID.World).Should().BeTrue();
            AuthorizationMethods.ConvertSidToString(in sid).Should().Be("S-1-1-0");

            AccountSidInformation info = AuthorizationMethods.LookupAccountSidLocal(sid);
            info.Name.Should().Be("Everyone");
            info.DomainName.Should().Be("");
            info.Usage.Should().Be(SidNameUse.WellKnownGroup);
        }

        [Fact]
        public void IsValidSid_GoodSid()
        {
            SID sid = AuthorizationMethods.CreateWellKnownSid(WellKnownSID.IISUser);
            AuthorizationMethods.IsValidSid(in sid).Should().BeTrue();
        }

        // [Fact]
        private void DumpAllWellKnownSids()
        {
            foreach (WellKnownSID type in Enum.GetValues(typeof(WellKnownSID)))
            {
                Debug.WriteLine(@"/// <summary>");
                try
                {
                    SID sid = AuthorizationMethods.CreateWellKnownSid(type);
                    AccountSidInformation info = AuthorizationMethods.LookupAccountSidLocal(sid);
                    Debug.WriteLine($"/// {info.Name} ({AuthorizationMethods.ConvertSidToString(in sid)}) [{info.Usage}]");
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
            AuthorizationMethods.IsValidSid(in sid).Should().BeFalse();
        }

        [Fact]
        public void GetTokenUserSid_ForCurrentProcess()
        {
            SID sid;
            using (var token = AuthorizationMethods.OpenProcessToken(AccessTokenRights.Read))
            {
                token.IsInvalid.Should().BeFalse();
                sid = AuthorizationMethods.GetTokenUserSid(token);
            }
            AuthorizationMethods.IsValidSid(in sid).Should().BeTrue();

            AccountSidInformation info = AuthorizationMethods.LookupAccountSidLocal(sid);
            info.Name.Should().Be(SystemInformationMethods.GetUserName());
        }

        [Fact]
        public void GetTokenGroupSids_ForCurrentProcess()
        {
            using (var token = AuthorizationMethods.OpenProcessToken(AccessTokenRights.Read))
            {
                token.IsInvalid.Should().BeFalse();
                List<GroupSidInformation> groupSids = AuthorizationMethods.GetTokenGroupSids(token).ToList();
                groupSids.Should().NotBeEmpty();
                groupSids.Should().Contain((sid) => AuthorizationMethods.LookupAccountSidLocal(sid.Sid).Name.Equals("Everyone"));
                TokenStatistics stats = AuthorizationMethods.GetTokenStatistics(token);
                groupSids.Count.Should().Be((int)stats.GroupCount);
            }
        }

        [Fact]
        public void GetTokenStatistics_ForCurrentProcess()
        {
            using (var token = AuthorizationMethods.OpenProcessToken(AccessTokenRights.Read))
            {
                TokenStatistics stats = AuthorizationMethods.GetTokenStatistics(token);
                stats.TokenType.Should().Be(TokenType.Primary);
            }
        }

        [Fact]
        public void GetTokenPrivileges_ForCurrentProcess()
        {
            using (var token = AuthorizationMethods.OpenProcessToken(AccessTokenRights.Read))
            {
                token.IsInvalid.Should().BeFalse();
                var privileges = AuthorizationMethods.GetTokenPrivileges(token);
                privileges.Should().NotBeEmpty();

                // This Privilege should always exist
                privileges.Should().Contain(s => s.Privilege == Privilege.ChangeNotify);

                // Check the helper
                AuthorizationMethods.HasPrivilege(token, Privilege.ChangeNotify).Should().BeTrue();
            }
        }

        [Fact]
        public void GetTokenPrivileges_NoReadRights()
        {
            // You need Query or Read (which includes Query) rights
            using (var token = AuthorizationMethods.OpenProcessToken(AccessTokenRights.Duplicate))
            {
                token.IsInvalid.Should().BeFalse();
                Action action = () => AuthorizationMethods.GetTokenPrivileges(token);
                action.Should().Throw<UnauthorizedAccessException>();
            }
        }

        [Fact]
        public void GetTokenOwnerSid_ForCurrentProcess()
        {
            SID sid;
            using (var token = AuthorizationMethods.OpenProcessToken(AccessTokenRights.Read))
            {
                token.IsInvalid.Should().BeFalse();
                sid = AuthorizationMethods.GetTokenOwnerSid(token);
            }
            AuthorizationMethods.IsValidSid(in sid).Should().BeTrue();

            AccountSidInformation info = AuthorizationMethods.LookupAccountSidLocal(sid);
            info.Name.Should().Be(SystemInformationMethods.GetUserName());
        }

        [Fact]
        public void GetTokenPrimaryGroupSid_ForCurrentProcess()
        {
            SID sid;
            using (var token = AuthorizationMethods.OpenProcessToken(AccessTokenRights.Read))
            {
                token.IsInvalid.Should().BeFalse();
                sid = AuthorizationMethods.GetTokenPrimaryGroupSid(token);
            }
            AuthorizationMethods.IsValidSid(in sid).Should().BeTrue();

            AccountSidInformation info = AuthorizationMethods.LookupAccountSidLocal(sid);
            info.Name.Should().Be(SystemInformationMethods.GetUserName());
        }

        [Fact]
        public void IsPrivilegeEnabled_ForCurrentProcess()
        {
            using (var token = AuthorizationMethods.OpenProcessToken(AccessTokenRights.Read))
            {
                token.IsInvalid.Should().BeFalse();
                AuthorizationMethods.IsPrivilegeEnabled(token, Privilege.ChangeNotify).Should().BeTrue();
                AuthorizationMethods.IsPrivilegeEnabled(token, Privilege.Backup).Should().BeFalse();
            }
        }

        [Fact]
        public void AreAllPrivilegesEnabled_ForCurrentProcess()
        {
            using (var token = AuthorizationMethods.OpenProcessToken(AccessTokenRights.Read))
            {
                token.IsInvalid.Should().BeFalse();
                AuthorizationMethods.AreAllPrivilegesEnabled(token, Privilege.ChangeNotify, Privilege.Backup).Should().BeFalse();
                AuthorizationMethods.AreAnyPrivilegesEnabled(token, Privilege.ChangeNotify, Privilege.Backup).Should().BeTrue();
                AuthorizationMethods.AreAllPrivilegesEnabled(token, Privilege.Backup).Should().BeFalse();
                AuthorizationMethods.AreAnyPrivilegesEnabled(token, Privilege.ChangeNotify).Should().BeTrue();
                AuthorizationMethods.AreAllPrivilegesEnabled(token, Privilege.ChangeNotify, Privilege.ChangeNotify).Should().BeTrue();
            }
        }

        [Fact]
        public void GetDomainName()
        {
            AuthorizationMethods.GetDomainName().Should().NotBeNull();
        }

        [Theory,
            InlineData(true),
            InlineData(false)]
        public void GetThreadToken(bool openAsSelf)
        {
            // Unless we're impersonating we shouldn't get a token for the thread
            using (var token = AuthorizationMethods.OpenThreadToken(AccessTokenRights.Query, openAsSelf))
            {
                token.Should().BeNull();
            }
        }

        [Fact]
        public void DuplicateProcessToken_NoDuplicateRights()
        {
            using (var token = AuthorizationMethods.OpenProcessToken(AccessTokenRights.Query))
            {
                Action action = () => AuthorizationMethods.DuplicateToken(token);
                action.Should().Throw<UnauthorizedAccessException>("didn't ask for duplicate rights");
            }
        }

        [Fact]
        public void DuplicateProcessToken()
        {
            using (var token = AuthorizationMethods.OpenProcessToken(AccessTokenRights.Duplicate | AccessTokenRights.Query))
            {
                var privileges = AuthorizationMethods.GetTokenPrivileges(token);

                using (var duplicate = AuthorizationMethods.DuplicateToken(token))
                {
                    duplicate.IsInvalid.Should().BeFalse();
                    var duplicatePrivileges = AuthorizationMethods.GetTokenPrivileges(duplicate);

                    duplicatePrivileges.Should().Equal(privileges);
                }

                using (var duplicate = AuthorizationMethods.DuplicateToken(token, rights: AccessTokenRights.MaximumAllowed))
                {
                    duplicate.IsInvalid.Should().BeFalse();
                    var duplicatePrivileges = AuthorizationMethods.GetTokenPrivileges(duplicate);

                    duplicatePrivileges.Count().Should().BeGreaterOrEqualTo(privileges.Count());
                }
            }
        }

    }
}
