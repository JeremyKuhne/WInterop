// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Security;

namespace SecurityTests;

public class SidStruct
{
    [Fact]
    public unsafe void SidSize()
    {
        sizeof(SecurityIdentifier).Should().Be(68);
    }

    [Fact]
    public unsafe void PassedAsInDoesNotCopy()
    {
        SecurityIdentifier sid = new();
        SecurityIdentifier* sp = &sid;

        static void CheckSid(in SecurityIdentifier insid, SecurityIdentifier* insp)
        {
            fixed (SecurityIdentifier* pinsid = &insid)
            {
                (pinsid == insp).Should().BeTrue();
            }
        }

        CheckSid(sid, sp);
    }

    [Fact]
    public void EmptySidEqualsSelf()
    {
        SecurityIdentifier sid = new();
        sid.Equals(sid).Should().BeTrue();
    }

    [Fact]
    public void EmptySidEqualsNewEmptySid()
    {
        SecurityIdentifier sid = new();
        sid.Equals(new SecurityIdentifier()).Should().BeTrue();
    }

    [Fact]
    public void EmptySidEqualsDefault()
    {
        SecurityIdentifier sid = new();
        sid.Equals(new SecurityIdentifier()).Should().BeTrue();
    }

    [Fact]
    public void KnownSidEqualsSelf()
    {
        SecurityIdentifier sid = Security.CreateWellKnownSid(WellKnownSID.IISUser);
        sid.Equals(sid).Should().BeTrue();
    }

    [Fact]
    public unsafe void KnownSidNotEqualsDefault()
    {
        SecurityIdentifier sid = Security.CreateWellKnownSid(WellKnownSID.IISUser);
        sid.Equals((SecurityIdentifier)default).Should().BeFalse();
    }

    [Fact]
    public void KnownSidNotEqualsOtherKnownSid()
    {
        SecurityIdentifier sid = Security.CreateWellKnownSid(WellKnownSID.IISUser);
        sid.Equals(Security.CreateWellKnownSid(WellKnownSID.Users)).Should().BeFalse();
    }
}
