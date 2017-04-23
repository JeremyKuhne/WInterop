// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using WInterop.Authentication;
using WInterop.Authorization;
using WInterop.Authorization.Types;
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
            SID sid = AuthorizationMethods.CreateWellKnownSid(WELL_KNOWN_SID_TYPE.WinBuiltinUsersSid);
            var rights = SecurityMethods.LsaEnumerateAccountRights(handle, ref sid);
            rights.Should().NotBeEmpty();
            rights.Should().Contain("SeChangeNotifyPrivilege");
        }

        [Fact(Skip = "Need to conditionalize on admin access")]
        public void EnumerateAccountRights_ReadRightsFails()
        {
            LsaHandle handle = AuthenticationMethods.LsaOpenLocalPolicy(PolicyAccessRights.POLICY_READ);
            SID sid = AuthorizationMethods.CreateWellKnownSid(WELL_KNOWN_SID_TYPE.WinBuiltinUsersSid);
            Action action = () => SecurityMethods.LsaEnumerateAccountRights(handle, ref sid);
            action.ShouldThrow<UnauthorizedAccessException>();
        }

        [Fact(Skip = "Need to conditionalize on admin access")]
        public void EnumerateAccountRights_BadSidFails()
        {
            LsaHandle handle = AuthenticationMethods.LsaOpenLocalPolicy(PolicyAccessRights.POLICY_READ);
            SID sid = new SID();
            Action action = () => SecurityMethods.LsaEnumerateAccountRights(handle, ref sid);
            action.ShouldThrow<ArgumentException>();
        }

        [Fact(Skip = "Need to conditionalize on admin access")]
        public void EnumerateAccountRights_NoRightsFails()
        {
            LsaHandle handle = AuthenticationMethods.LsaOpenLocalPolicy(PolicyAccessRights.POLICY_READ);
            SID sid = AuthorizationMethods.CreateWellKnownSid(WELL_KNOWN_SID_TYPE.WinBuiltinAnyPackageSid);
            SecurityMethods.LsaEnumerateAccountRights(handle, ref sid).Should().BeEmpty();
        }
    }
}
