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
using Tests.Shared.Support;
using WInterop.ProcessAndThreads;
using WInterop.ProcessAndThreads.Types;

namespace AuthorizationTests
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

            Authorization.IsProcessElevated().Should().Be(runningAsAdmin);
        }

        [Fact]
        public void CreateWellKnownSid_Everyone()
        {
            SID sid = Authorization.CreateWellKnownSid(WellKnownSID.World);
            sid.IsValidSid().Should().BeTrue();
            sid.Revision.Should().Be(1);
            sid.IdentifierAuthority.Should().Be(IdentifierAuthority.World);

            sid.GetSidSubAuthorityCount().Should().Be(1);
            sid.GetSidSubAuthority(0).Should().Be(0);

            sid.IsWellKnownSid(WellKnownSID.World).Should().BeTrue();
            sid.ConvertSidToString().Should().Be("S-1-1-0");

            AccountSidInformation info = sid.LookupAccountSid();
            info.Name.Should().Be("Everyone");
            info.DomainName.Should().Be("");
            info.Usage.Should().Be(SidNameUse.WellKnownGroup);
        }

        [Fact]
        public void IsValidSid_GoodSid()
        {
            SID sid = Authorization.CreateWellKnownSid(WellKnownSID.IISUser);
            sid.IsValidSid().Should().BeTrue();
        }

        // [Fact]
        private void DumpAllWellKnownSids()
        {
            foreach (WellKnownSID type in Enum.GetValues(typeof(WellKnownSID)))
            {
                Debug.WriteLine(@"/// <summary>");
                try
                {
                    SID sid = Authorization.CreateWellKnownSid(type);
                    AccountSidInformation info = sid.LookupAccountSid();
                    Debug.WriteLine($"/// {info.Name} ({sid.ConvertSidToString()}) [{info.Usage}]");
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
            sid.IsValidSid().Should().BeFalse();
        }

        [Fact]
        public void GetTokenUserSid_ForCurrentProcess()
        {
            SID sid;
            using (var token = Authorization.OpenProcessToken(AccessTokenRights.Read))
            {
                token.IsInvalid.Should().BeFalse();
                sid = token.GetTokenUserSid();
            }
            sid.IsValidSid().Should().BeTrue();

            AccountSidInformation info = sid.LookupAccountSid();
            info.Name.Should().Be(SystemInformationMethods.GetUserName());
        }

        [Fact]
        public void GetTokenGroupSids_ForCurrentProcess()
        {
            using (var token = Authorization.OpenProcessToken(AccessTokenRights.Read))
            {
                token.IsInvalid.Should().BeFalse();
                List<GroupSidInformation> groupSids = token.GetTokenGroupSids().ToList();
                groupSids.Should().NotBeEmpty();
                groupSids.Should().Contain((group) => group.Sid.LookupAccountSid(null).Name.Equals("Everyone"));
                TokenStatistics stats = token.GetTokenStatistics();
                groupSids.Count.Should().Be((int)stats.GroupCount);
            }
        }

        [Fact]
        public void GetTokenStatistics_ForCurrentProcess()
        {
            using (var token = Authorization.OpenProcessToken(AccessTokenRights.Read))
            {
                TokenStatistics stats = token.GetTokenStatistics();
                stats.TokenType.Should().Be(TokenType.Primary);
            }
        }

        [Fact]
        public void GetTokenPrivileges_ForCurrentProcess()
        {
            using (var token = Authorization.OpenProcessToken(AccessTokenRights.Read))
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
            using (var token = Authorization.OpenProcessToken(AccessTokenRights.Duplicate))
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
            using (var token = Authorization.OpenProcessToken(AccessTokenRights.Read))
            {
                token.IsInvalid.Should().BeFalse();
                sid = token.GetTokenOwnerSid();
            }
            sid.IsValidSid().Should().BeTrue();

            AccountSidInformation info = sid.LookupAccountSid();
            info.Name.Should().Be(SystemInformationMethods.GetUserName());
        }

        [Fact]
        public void GetTokenPrimaryGroupSid_ForCurrentProcess()
        {
            SID sid;
            using (var token = Authorization.OpenProcessToken(AccessTokenRights.Read))
            {
                token.IsInvalid.Should().BeFalse();
                sid = token.GetTokenPrimaryGroupSid();
            }
            sid.IsValidSid().Should().BeTrue();

            AccountSidInformation info = sid.LookupAccountSid();
            info.Name.Should().Be(SystemInformationMethods.GetUserName());
        }

        [Fact]
        public void IsPrivilegeEnabled_ForCurrentProcess()
        {
            using (var token = Authorization.OpenProcessToken(AccessTokenRights.Read))
            {
                token.IsInvalid.Should().BeFalse();
                token.IsPrivilegeEnabled(Privilege.ChangeNotify).Should().BeTrue();
                token.IsPrivilegeEnabled(Privilege.Backup).Should().BeFalse();
            }
        }

        [Fact]
        public void AreAllPrivilegesEnabled_ForCurrentProcess()
        {
            using (var token = Authorization.OpenProcessToken(AccessTokenRights.Read))
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
            Authorization.GetDomainName().Should().NotBeNull();
        }

        [Theory,
            InlineData(true),
            InlineData(false)]
        public void GetThreadToken(bool openAsSelf)
        {
            // Unless we're impersonating we shouldn't get a token for the thread
            using (var token = Authorization.OpenThreadToken(AccessTokenRights.Query, openAsSelf))
            {
                token.Should().BeNull();
            }
        }

        [Fact]
        public void DuplicateProcessToken_NoDuplicateRights()
        {
            using (var token = Authorization.OpenProcessToken(AccessTokenRights.Query))
            {
                Action action = () => token.DuplicateToken();
                action.Should().Throw<UnauthorizedAccessException>("didn't ask for duplicate rights");
            }
        }

        [Fact]
        public void DuplicateProcessToken()
        {
            using (var token = Authorization.OpenProcessToken(AccessTokenRights.Duplicate | AccessTokenRights.Query))
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
            ThreadRunner.Run(() =>
            {
                using (var token = Authorization.OpenThreadToken(AccessTokenRights.Query, openAsSelf: true))
                {
                    token.Should().BeNull();
                }

                using (var token = Authorization.OpenProcessToken(AccessTokenRights.Duplicate | AccessTokenRights.Query))
                {
                    using (var duplicate = token.DuplicateToken(AccessTokenRights.Query | AccessTokenRights.Impersonate, ImpersonationLevel.Impersonation))
                    {
                        using (ThreadHandle thread = Threads.GetCurrentThread())
                        {
                            thread.SetThreadToken(duplicate);

                            using (var threadToken = Authorization.OpenThreadToken(AccessTokenRights.Query, openAsSelf: false))
                            {
                                threadToken.Should().BeNull();
                            }
                        }
                    }
                }
            });
        }
    }
}
