// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Tests.Shared.Support;
using WInterop.ProcessAndThreads;
using WInterop.Security;
using WInterop.SystemInformation;
using Xunit;

namespace SecurityTests
{
    public class Basic
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

            Security.IsProcessElevated().Should().Be(runningAsAdmin);
        }

        [Fact]
        public void GetTokenUserSid_ForCurrentProcess()
        {
            SID sid;
            using (var token = Security.OpenProcessToken(AccessTokenRights.Read))
            {
                token.IsInvalid.Should().BeFalse();
                sid = token.GetTokenUserSid().Sid;
            }
            sid.IsValidSid().Should().BeTrue();

            AccountSidInformation info = sid.LookupAccountSid();
            info.Name.Should().Be(SystemInformation.GetUserName());
        }

        [Fact]
        public void GetTokenGroupSids_ForCurrentProcess()
        {
            using (var token = Security.OpenProcessToken(AccessTokenRights.Read))
            {
                token.IsInvalid.Should().BeFalse();
                List<SidAndAttributes> groups = token.GetTokenGroupSids().ToList();
                groups.Should().NotBeEmpty();
                groups.Should().Contain((group) => group.Sid.LookupAccountSid(null).Name.Equals("Everyone"));
                TokenStatistics stats = token.GetTokenStatistics();
                groups.Count.Should().Be((int)stats.GroupCount);
            }
        }

        [Fact]
        public void GetTokenStatistics_ForCurrentProcess()
        {
            using (var token = Security.OpenProcessToken(AccessTokenRights.Read))
            {
                TokenStatistics stats = token.GetTokenStatistics();
                stats.TokenType.Should().Be(TokenType.Primary);
            }
        }

        [Fact]
        public void GetTokenPrivileges_ForCurrentProcess()
        {
            using (var token = Security.OpenProcessToken(AccessTokenRights.Read))
            {
                token.IsInvalid.Should().BeFalse();
                var privileges = token.GetTokenPrivileges();
                privileges.Should().NotBeEmpty();

                // This Privilege should always exist
                privileges.Should().Contain(s => s.Privilege == Privilege.ChangeNotify);

                // Check the helper
                token.HasPrivilege(Privilege.ChangeNotify).Should().BeTrue();
            }
        }

        [Fact]
        public void GetTokenPrivileges_NoReadRights()
        {
            // You need Query or Read (which includes Query) rights
            using (var token = Security.OpenProcessToken(AccessTokenRights.Duplicate))
            {
                token.IsInvalid.Should().BeFalse();
                Action action = () => token.GetTokenPrivileges();
                action.Should().Throw<UnauthorizedAccessException>();
            }
        }

        [Fact]
        public void GetTokenOwnerSid_ForCurrentProcess()
        {
            SID sid;
            using (var token = Security.OpenProcessToken(AccessTokenRights.Read))
            {
                token.IsInvalid.Should().BeFalse();
                sid = token.GetTokenOwnerSid();
            }
            sid.IsValidSid().Should().BeTrue();

            AccountSidInformation info = sid.LookupAccountSid();
            info.Name.Should().Be(SystemInformation.GetUserName());
        }

        [Fact]
        public void GetTokenPrimaryGroupSid_ForCurrentProcess()
        {
            SID sid;
            using (var token = Security.OpenProcessToken(AccessTokenRights.Read))
            {
                token.IsInvalid.Should().BeFalse();
                sid = token.GetTokenPrimaryGroupSid();
            }
            sid.IsValidSid().Should().BeTrue();

            AccountSidInformation info = sid.LookupAccountSid();
            info.Name.Should().Be(SystemInformation.GetUserName());
        }

        [Fact]
        public void IsPrivilegeEnabled_ForCurrentProcess()
        {
            using (var token = Security.OpenProcessToken(AccessTokenRights.Read))
            {
                token.IsInvalid.Should().BeFalse();
                token.IsPrivilegeEnabled(Privilege.ChangeNotify).Should().BeTrue();
                token.IsPrivilegeEnabled(Privilege.Backup).Should().BeFalse();
            }
        }

        [Fact]
        public void AreAllPrivilegesEnabled_ForCurrentProcess()
        {
            using (var token = Security.OpenProcessToken(AccessTokenRights.Read))
            {
                token.IsInvalid.Should().BeFalse();
                token.AreAllPrivilegesEnabled(Privilege.ChangeNotify, Privilege.Backup).Should().BeFalse();
                token.AreAnyPrivilegesEnabled(Privilege.ChangeNotify, Privilege.Backup).Should().BeTrue();
                token.AreAllPrivilegesEnabled(Privilege.Backup).Should().BeFalse();
                token.AreAnyPrivilegesEnabled(Privilege.ChangeNotify).Should().BeTrue();
                token.AreAllPrivilegesEnabled(Privilege.ChangeNotify, Privilege.ChangeNotify).Should().BeTrue();
            }
        }

        [Fact]
        public void GetDomainName()
        {
            Security.GetDomainName().Should().NotBeNull();
        }

        [Theory,
            InlineData(true),
            InlineData(false)]
        public void GetThreadToken(bool openAsSelf)
        {
            // Unless we're impersonating we shouldn't get a token for the thread
            using (var token = Security.OpenThreadToken(AccessTokenRights.Query, openAsSelf))
            {
                token.Should().BeNull();
            }
        }

        [Fact]
        public void DuplicateProcessToken_NoDuplicateRights()
        {
            using (var token = Security.OpenProcessToken(AccessTokenRights.Query))
            {
                Action action = () => token.DuplicateToken();
                action.Should().Throw<UnauthorizedAccessException>("didn't ask for duplicate rights");
            }
        }

        [Fact]
        public void DuplicateProcessToken()
        {
            using (var token = Security.OpenProcessToken(AccessTokenRights.Duplicate | AccessTokenRights.Query))
            {
                var privileges = token.GetTokenPrivileges();

                using (var duplicate = token.DuplicateToken())
                {
                    duplicate.IsInvalid.Should().BeFalse();
                    duplicate.GetTokenStatistics().TokenType.Should().Be(TokenType.Impersonation);

                    var duplicatePrivileges = duplicate.GetTokenPrivileges();
                    duplicatePrivileges.Should().Equal(privileges);
                }

                using (var duplicate = token.DuplicateToken(rights: AccessTokenRights.MaximumAllowed))
                {
                    duplicate.IsInvalid.Should().BeFalse();
                    duplicate.GetTokenStatistics().GroupCount.Should().BeGreaterOrEqualTo((uint)privileges.Count());
                }
            }
        }

        [Fact]
        public void ChangeThreadToImpersonate()
        {
            ThreadRunner.Run((() =>
            {
                using (var token = Security.OpenThreadToken(AccessTokenRights.Query, openAsSelf: true))
                {
                    token.Should().BeNull();
                }

                using (var token = Security.OpenProcessToken(AccessTokenRights.Duplicate | AccessTokenRights.Query))
                {
                    using (var duplicate = Security.DuplicateToken(token, AccessTokenRights.Query | AccessTokenRights.Impersonate, ImpersonationLevel.Impersonation))
                    {
                        using (ThreadHandle thread = Threads.GetCurrentThread())
                        {
                            Security.SetThreadToken(thread, duplicate);

                            // We didn't actually change from what the process token is
                            using (var threadToken = Security.OpenThreadToken(AccessTokenRights.Query, openAsSelf: false))
                            {
                                threadToken.Should().BeNull();
                            }
                        }
                    }
                }
            }));
        }

        [Fact]
        public void CreateRestrictedToken_Process()
        {
            using (var token = Security.OpenProcessToken(AccessTokenRights.Duplicate | AccessTokenRights.Query))
            {
                SidAndAttributes info = token.GetTokenUserSid();
                info.Sid.IsValidSid().Should().BeTrue();
                info.Attributes.Should().NotHaveFlag(SidAttributes.UseForDenyOnly);
                using (var restricted = token.CreateRestrictedToken(info.Sid))
                {
                    restricted.IsInvalid.Should().BeFalse();
                    info = restricted.GetTokenUserSid();
                    info.Attributes.Should().HaveFlag(SidAttributes.UseForDenyOnly);
                    info.Sid.IsValidSid().Should().BeTrue();
                    restricted.GetTokenStatistics().TokenType.Should().Be(TokenType.Primary);
                }
            }
        }

        [Fact]
        public void Impersonate_DisableUser()
        {
            ThreadRunner.Run((() =>
            {
                using (var token = Security.OpenProcessToken(AccessTokenRights.Duplicate | AccessTokenRights.Query))
                {
                    using (var restricted = Security.CreateRestrictedToken(token, Security.GetTokenUserSid(token)))
                    {
                        Security.ImpersonateLoggedOnUser(restricted);
                        Security.RevertToSelf();
                    }
                }
            }));
        }

        [Fact]
        public void Impersonate_Anonymous()
        {
            ThreadRunner.Run((() =>
            {
                Security.ImpersonateAnonymousToken();
                Security.RevertToSelf();
            }));
        }

        [Theory,
            InlineData(true),
            InlineData(false)]
        public void Impersonate_Anonymous_GetThreadToken(bool openAsSelf)
        {
            using (var token = Security.OpenProcessToken(AccessTokenRights.Read))
            {
                var privileges = token.GetTokenPrivileges();
            }

            ThreadRunner.Run((() =>
            {
                Security.ImpersonateAnonymousToken();
                SID sid;
                using (var threadToken = Security.OpenThreadToken(AccessTokenRights.Query, openAsSelf))
                {
                    threadToken.Should().NotBeNull();
                    threadToken.IsInvalid.Should().BeFalse();
                    sid = threadToken.GetTokenOwnerSid();
                    Action action = () => sid.LookupAccountSid();
                    action.Should().Throw<UnauthorizedAccessException>();

                    var privileges = threadToken.GetTokenPrivileges();
                }
                Security.RevertToSelf();

                var info = sid.LookupAccountSid();
                info.Name.Should().Be("ANONYMOUS LOGON");
                info.Usage.Should().Be(SidNameUse.WellKnownGroup);
                sid.Equals(Security.CreateWellKnownSid(WellKnownSID.Anonymous)).Should().BeTrue();
            }));
        }
    }
}
