// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using WInterop.Security;
using Xunit;

namespace SecurityTests
{
    public class SidStruct
    {
        [Fact]
        public unsafe void SidSize()
        {
            sizeof(SID).Should().Be(68);
        }

        [Fact]
        public unsafe void PassedAsInDoesNotCopy()
        {
            SID sid = new SID();
            SID* sp = &sid;

            void CheckSid(in SID insid, SID* insp)
            {
                fixed (SID* pinsid = &insid)
                {
                    (pinsid == insp).Should().BeTrue();
                }
            }

            CheckSid(sid, sp);
        }

        [Fact]
        public void EmptySidEqualsSelf()
        {
            SID sid = new SID();
            sid.Equals(sid).Should().BeTrue();
        }

        [Fact]
        public void EmptySidEqualsNewEmptySid()
        {
            SID sid = new SID();
            sid.Equals(new SID()).Should().BeTrue();
        }

        [Fact]
        public void EmptySidEqualsDefault()
        {
            SID sid = new SID();
            sid.Equals(new SID()).Should().BeTrue();
        }

        [Fact]
        public void KnownSidEqualsSelf()
        {
            SID sid = Security.CreateWellKnownSid(WellKnownSID.IISUser);
            sid.Equals(sid).Should().BeTrue();
        }

        [Fact]
        public unsafe void KnownSidNotEqualsDefault()
        {
            SID sid = Security.CreateWellKnownSid(WellKnownSID.IISUser);
            sid.Equals(default).Should().BeFalse();
        }

        [Fact]
        public void KnownSidNotEqualsOtherKnownSid()
        {
            SID sid = Security.CreateWellKnownSid(WellKnownSID.IISUser);
            sid.Equals(Security.CreateWellKnownSid(WellKnownSID.Users)).Should().BeFalse();
        }
    }
}
