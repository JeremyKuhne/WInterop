// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using WInterop.Authentication;
using WInterop.Authorization.DataTypes;
using WInterop.SecurityManagement.DataTypes;
using Xunit;

namespace DesktopTests.Authentication
{
    public class AuthenticationTests
    {
        [Fact(Skip = "Need to conditionalize on admin access")]
        public void LsaOpenPolicy_StandardRead()
        {
            SafeLsaHandle handle = AuthenticationDesktopMethods.LsaOpenLocalPolicy(PolicyAccessRights.POLICY_READ);
            handle.IsInvalid.Should().BeFalse();
        }

        [Fact]
        public void LsaOpenPolicy_GenericRead()
        {
            Action action = () => AuthenticationDesktopMethods.LsaOpenLocalPolicy((PolicyAccessRights)GenericAccessRights.GENERIC_READ);
            action.ShouldThrow<UnauthorizedAccessException>();
        }
    }
}
