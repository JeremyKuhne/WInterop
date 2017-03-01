// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using WInterop.Authentication;
using WInterop.Authorization;
using WInterop.Authorization.DataTypes;
using WInterop.SecurityManagement;
using WInterop.SecurityManagement.DataTypes;
using Xunit;

namespace DesktopTests.SecurityManagement
{
    public class SecurityTests
    {
        [Fact]
        public void EnumerateAccountRights_UserGroup()
        {
            SafeLsaHandle handle = AuthenticationDesktopMethods.LsaOpenLocalPolicy(PolicyAccessRights.POLICY_EXECUTE);
            SID sid = AuthorizationDesktopMethods.CreateWellKnownSid(WELL_KNOWN_SID_TYPE.WinBuiltinUsersSid);
            var rights = SecurityDesktopMethods.LsaEnumerateAccountRights(handle, ref sid);
            rights.Should().NotBeEmpty();
            rights.Should().Contain("SeChangeNotifyPrivilege");
        }

        [Fact]
        public void EnumerateAccountRights_ReadRightsFails()
        {
            SafeLsaHandle handle = AuthenticationDesktopMethods.LsaOpenLocalPolicy(PolicyAccessRights.POLICY_READ);
            SID sid = AuthorizationDesktopMethods.CreateWellKnownSid(WELL_KNOWN_SID_TYPE.WinBuiltinUsersSid);
            Action action = () => SecurityDesktopMethods.LsaEnumerateAccountRights(handle, ref sid);
            action.ShouldThrow<UnauthorizedAccessException>();
        }

        [Fact]
        public void EnumerateAccountRights_BadSidFails()
        {
            SafeLsaHandle handle = AuthenticationDesktopMethods.LsaOpenLocalPolicy(PolicyAccessRights.POLICY_READ);
            SID sid = new SID();
            Action action = () => SecurityDesktopMethods.LsaEnumerateAccountRights(handle, ref sid);
            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void EnumerateAccountRights_NoRightsFails()
        {
            SafeLsaHandle handle = AuthenticationDesktopMethods.LsaOpenLocalPolicy(PolicyAccessRights.POLICY_READ);
            SID sid = AuthorizationDesktopMethods.CreateWellKnownSid(WELL_KNOWN_SID_TYPE.WinBuiltinAnyPackageSid);
            SecurityDesktopMethods.LsaEnumerateAccountRights(handle, ref sid).Should().BeEmpty();
        }
    }
}
