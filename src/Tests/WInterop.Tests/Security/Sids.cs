// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System.Diagnostics;
using WInterop.Security;
using Xunit;

namespace SecurityTests;

public class Sids
{
    [Fact]
    public void CreateWellKnownSid_Everyone()
    {
        SID sid = Security.CreateWellKnownSid(WellKnownSID.World);
        sid.IsValidSid().Should().BeTrue();
        sid.Revision.Should().Be(1);
        sid.IdentifierAuthority.Should().Be(IdentifierAuthority.World);

        sid.GetSidSubAuthorityCount().Should().Be(1);
        sid.GetSidSubAuthority(0).Should().Be(0);

        sid.IsWellKnownSid(WellKnownSID.World).Should().BeTrue();
        sid.ConvertSidToString().Should().Be("S-1-1-0");

        AccountSidInformation info = sid.LookupAccountSid();
        info.Name.Should().Be("Everyone");
        info.DomainName.Should().Be("");
        info.Usage.Should().Be(SidNameUse.WellKnownGroup);
    }

    [Fact]
    public void IsValidSid_GoodSid()
    {
        SID sid = Security.CreateWellKnownSid(WellKnownSID.IISUser);
        sid.IsValidSid().Should().BeTrue();
    }

    // [Fact]
    private void DumpAllWellKnownSids()
    {
        foreach (WellKnownSID type in Enum.GetValues(typeof(WellKnownSID)))
        {
            Debug.WriteLine(@"/// <summary>");
            try
            {
                SID sid = Security.CreateWellKnownSid(type);
                AccountSidInformation info = sid.LookupAccountSid();
                Debug.WriteLine($"///  {info.Name} ({sid.ConvertSidToString()}) [{info.Usage}]");
            }
            catch
            {
                Debug.WriteLine($"///  Unable to retrieve");
            }
            Debug.WriteLine(@"/// </summary>");
            Debug.WriteLine($"{type} = {(int)type},");
            Debug.WriteLine("");
        }
    }

    [Fact]
    public void IsValidSid_BadSid()
    {
        SID sid = new();
        sid.IsValidSid().Should().BeFalse();
    }
}
