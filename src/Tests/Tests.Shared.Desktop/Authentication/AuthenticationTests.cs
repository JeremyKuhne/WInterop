// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using WInterop.Authentication;
using WInterop.Authorization.Types;
using WInterop.SecurityManagement.Types;
using Xunit;

namespace DesktopTests.Authentication
{
    public class AuthenticationTests
    {
        [Fact(Skip = "Need to conditionalize on admin access")]
        public void LsaOpenPolicy_StandardRead()
        {
            LsaHandle handle = AuthenticationMethods.LsaOpenLocalPolicy(PolicyAccessRights.POLICY_READ);
            handle.IsInvalid.Should().BeFalse();
        }

        [Fact]
        public void LsaOpenPolicy_GenericRead()
        {
            Action action = () => AuthenticationMethods.LsaOpenLocalPolicy((PolicyAccessRights)GenericAccessRights.Read);
            action.Should().Throw<UnauthorizedAccessException>();
        }
    }
}
