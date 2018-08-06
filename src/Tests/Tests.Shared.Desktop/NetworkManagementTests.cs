// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Xunit;
using FluentAssertions;
using System.Linq;
using WInterop.Network;

namespace NetworkTests
{
    public partial class Basic
    {
        [Fact]
        public void BasicGetLocalGroupNames()
        {
            string[] knownLocalGroups = { "Administrators", "Guests", "Users" };
            var localGroups = Network.EnumerateLocalGroups();
            localGroups.Should().Contain(knownLocalGroups);
            knownLocalGroups.Should().BeSubsetOf(localGroups);
        }

        [Fact]
        public void BasicGetLocalGroupMembers()
        {
            string[] knownMembers = { "Authenticated Users", "INTERACTIVE" };
            var members = Network.EnumerateGroupUsers("Users");
            members.Select(m => m.Name).Should().Contain(knownMembers);
            knownMembers.Should().BeSubsetOf(members.Select(m => m.Name));
        }

        [Fact(Skip = "Need to conditionalize this on admin rights.")]
        public void AddLocalGroup()
        {
            Network.AddLocalGroup("TestGroup", "This group is for testing");
        }

        [Fact]
        public void GetLocalUserNames()
        {
            var localUsers = Network.EnumerateUsers();
        }

        [Fact]
        public void GetUserInfo()
        {
            foreach (string user in Network.EnumerateUsers())
            {
                var info = Network.GetUserInfo(user);
            }
        }
    }
}
