// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using FluentAssertions;
using WInterop.SystemInformation;
using WInterop.SystemInformation.Types;
using Xunit;
using System.Diagnostics;

namespace Tests.SystemInformation
{
    public class SystemInformationTests
    {
        [Fact]
        public void IsProcessorFeaturePresent()
        {
            // We shouldn't be able to run on the original Pentium, so this should always be false.
            SystemInformationMethods.IsProcessorFeaturePresent(ProcessorFeature.PF_FLOATING_POINT_PRECISION_ERRATA).Should().BeFalse();

            // I don't think any platform doesn't support this
            SystemInformationMethods.IsProcessorFeaturePresent(ProcessorFeature.PF_COMPARE_EXCHANGE_DOUBLE).Should().BeTrue();
        }

        // [Fact]
        public void DumpProcessorFeatures()
        {
            foreach (ProcessorFeature feature in Enum.GetValues(typeof(ProcessorFeature)))
            {
                Debug.WriteLine($"{feature}: {SystemInformationMethods.IsProcessorFeaturePresent(feature)}");
            }
        }

        [Fact]
        public void CeipIsOptedIn()
        {
            // Can't really validate this, just make sure it doesn't blow up.
            SystemInformationMethods.CeipIsOptedIn();
        }
    }
}
