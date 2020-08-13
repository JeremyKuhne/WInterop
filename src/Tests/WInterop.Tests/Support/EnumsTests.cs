// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using WInterop.Support;
using Xunit;

namespace SupportTests
{
    public class EnumsTests
    {
        [Fact]
        public void SetFlagTest_ByteEnum()
        {
            ByteEnum flags = default;

            // Set each flag and clear
            for (int i = 0; i < sizeof(ByteEnum) * 8; i++)
            {
                ByteEnum newFlag = (ByteEnum)(1 << i);
                flags.SetFlags(newFlag);
                flags.Should().Be(newFlag);
                flags.ClearFlags(newFlag);
                flags.Should().Be(0);
            }
        }

        [Fact]
        public void SetFlagTest_ShortEnum()
        {
            ShortEnum flags = default;

            // Set each flag and clear
            for (int i = 0; i < sizeof(ShortEnum) * 8; i++)
            {
                ShortEnum newFlag = (ShortEnum)(1 << i);
                flags.SetFlags(newFlag);
                flags.Should().Be(newFlag);
                flags.ClearFlags(newFlag);
                flags.Should().Be(0);
            }
        }

        [Fact]
        public void SetFlagTest_IntEnum()
        {
            IntEnum flags = default;

            // Set each flag and clear
            for (int i = 0; i < sizeof(IntEnum) * 8; i++)
            {
                IntEnum newFlag = (IntEnum)(1 << i);
                flags.SetFlags(newFlag);
                flags.Should().Be(newFlag);
                flags.ClearFlags(newFlag);
                flags.Should().Be(0);
            }
        }

        [Fact]
        public void SetFlagTest_LongEnum()
        {
            LongEnum flags = default;

            // Set each flag and clear
            for (int i = 0; i < sizeof(LongEnum) * 8; i++)
            {
                LongEnum newFlag = (LongEnum)(1 << i);
                flags.SetFlags(newFlag);
                flags.Should().Be(newFlag);
                flags.ClearFlags(newFlag);
                flags.Should().Be(0);
            }
        }


        public enum ByteEnum : byte { }
        public enum ShortEnum : short { }
        public enum IntEnum : int { }
        public enum LongEnum : long { }
    }
}
