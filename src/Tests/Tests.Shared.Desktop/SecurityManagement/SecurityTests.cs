// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using WInterop.Authentication;
using WInterop.Authorization;
using WInterop.SecurityManagement;
using WInterop.SecurityManagement.Types;
using Xunit;

namespace DesktopTests.SecurityManagement
{
    public class SecurityTests
    {
        [Fact(Skip = "Need to conditionalize on admin access")]
        public void EnumerateAccountRights_UserGroup()
        {
            LsaHandle handle = AuthenticationMethods.LsaOpenLocalPolicy(PolicyAccessRights.POLICY_EXECUTE);
            SID sid = Authorization.CreateWellKnownSid(WellKnownSID.Users);
            var rights = SecurityMethods.LsaEnumerateAccountRights(handle, in sid);
            rights.Should().NotBeEmpty();
            rights.Should().Contain("SeChangeNotifyPrivilege");
        }

        [Fact(Skip = "Need to conditionalize on admin access")]
        public void EnumerateAccountRights_ReadRightsFails()
        {
            LsaHandle handle = AuthenticationMethods.LsaOpenLocalPolicy(PolicyAccessRights.POLICY_READ);
            SID sid = Authorization.CreateWellKnownSid(WellKnownSID.Users);
            Action action = () => SecurityMethods.LsaEnumerateAccountRights(handle, in sid);
            action.Should().Throw<UnauthorizedAccessException>();
        }

        [Fact(Skip = "Need to conditionalize on admin access")]
        public void EnumerateAccountRights_BadSidFails()
        {
            LsaHandle handle = AuthenticationMethods.LsaOpenLocalPolicy(PolicyAccessRights.POLICY_READ);
            SID sid = new SID();
            Action action = () => SecurityMethods.LsaEnumerateAccountRights(handle, in sid);
            action.Should().Throw<ArgumentException>();
        }

        [Fact(Skip = "Need to conditionalize on admin access")]
        public void EnumerateAccountRights_NoRightsFails()
        {
            LsaHandle handle = AuthenticationMethods.LsaOpenLocalPolicy(PolicyAccessRights.POLICY_READ);
            SID sid = Authorization.CreateWellKnownSid(WellKnownSID.AllApplicationPackages);
            SecurityMethods.LsaEnumerateAccountRights(handle, in sid).Should().BeEmpty();
        }
    }
}
