// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using WInterop.Authentication;
using WInterop.Security;
using Xunit;

namespace SecurityTests
{
    public class EnumerateRights
    {
        [Fact(Skip = "Need to conditionalize on admin access")]
        public void EnumerateAccountRights_UserGroup()
        {
            LsaHandle handle = Authentication.LsaOpenLocalPolicy(PolicyAccessRights.POLICY_EXECUTE);
            SID sid = Authorization.CreateWellKnownSid(WellKnownSID.Users);
            var rights = Security.LsaEnumerateAccountRights(handle, in sid);
            rights.Should().NotBeEmpty();
            rights.Should().Contain("SeChangeNotifyPrivilege");
        }

        [Fact(Skip = "Need to conditionalize on admin access")]
        public void EnumerateAccountRights_ReadRightsFails()
        {
            LsaHandle handle = Authentication.LsaOpenLocalPolicy(PolicyAccessRights.POLICY_READ);
            SID sid = Authorization.CreateWellKnownSid(WellKnownSID.Users);
            Action action = () => Security.LsaEnumerateAccountRights(handle, in sid);
            action.Should().Throw<UnauthorizedAccessException>();
        }

        [Fact(Skip = "Need to conditionalize on admin access")]
        public void EnumerateAccountRights_BadSidFails()
        {
            LsaHandle handle = Authentication.LsaOpenLocalPolicy(PolicyAccessRights.POLICY_READ);
            SID sid = new SID();
            Action action = () => Security.LsaEnumerateAccountRights(handle, in sid);
            action.Should().Throw<ArgumentException>();
        }

        [Fact(Skip = "Need to conditionalize on admin access")]
        public void EnumerateAccountRights_NoRightsFails()
        {
            LsaHandle handle = Authentication.LsaOpenLocalPolicy(PolicyAccessRights.POLICY_READ);
            SID sid = Authorization.CreateWellKnownSid(WellKnownSID.AllApplicationPackages);
            Security.LsaEnumerateAccountRights(handle, in sid).Should().BeEmpty();
        }
    }
}
