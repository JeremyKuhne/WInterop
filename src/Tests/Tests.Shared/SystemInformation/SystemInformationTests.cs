// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using FluentAssertions;
using WInterop.SystemInformation;
using Xunit;
using System.Diagnostics;

namespace SystemInformationTests
{
    public partial class Basic
    {
        [Fact]
        public void IsProcessorFeaturePresent()
        {
            // We shouldn't be able to run on the original Pentium, so this should always be false.
            SystemInformation.IsProcessorFeaturePresent(ProcessorFeature.FloatingPointPrecisionErrata).Should().BeFalse();

            // I don't think any platform doesn't support this
            SystemInformation.IsProcessorFeaturePresent(ProcessorFeature.CompareExchangeDouble).Should().BeTrue();
        }

        // [Fact]
        private void DumpProcessorFeatures()
        {
            foreach (ProcessorFeature feature in Enum.GetValues(typeof(ProcessorFeature)))
            {
                Debug.WriteLine($"{feature}: {SystemInformation.IsProcessorFeaturePresent(feature)}");
            }
        }

        [Fact]
        public void CeipIsOptedIn()
        {
            // Can't really validate this, just make sure it doesn't blow up.
            SystemInformation.CeipIsOptedIn();
        }

        [Fact]
        public void GetComputerName()
        {
            SystemInformation.GetComputerName().Should().Be(Environment.GetEnvironmentVariable("COMPUTERNAME"));
        }
    }
}
