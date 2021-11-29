// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using WInterop.Security;
using Xunit;

namespace SecurityTests;

public class LsaTests
{
    [Fact(Skip = "Need to conditionalize on admin access")]
    public void LsaOpenPolicy_StandardRead()
    {
        LsaHandle handle = Security.LsaOpenLocalPolicy(PolicyAccessRights.Read);
        handle.IsInvalid.Should().BeFalse();
    }

    [Fact]
    public void LsaOpenPolicy_GenericRead()
    {
        Action action = () => Security.LsaOpenLocalPolicy((PolicyAccessRights)GenericAccessRights.Read);
        action.Should().Throw<UnauthorizedAccessException>();
    }
}
