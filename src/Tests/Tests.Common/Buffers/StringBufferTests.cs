// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Tests.Buffers
{
    using FluentAssertions;
    using System;
    using System.Reflection;
    using WInterop.Buffers;
    using Xunit;

    public class StringBufferTests
    {
        const string testString = "The quick brown fox jumped over the lazy dog.";

        [Theory
            InlineData(0)
            InlineData(1)
            InlineData(10)
            ]
        public void ConstructWithInitialCapacity(uint capacity)
        {
            using (var buffer = new StringBuffer(capacity))
            {
                buffer.CharCapacity.Should().Be(capacity);
            }
        }

        [Fact]
        public void ConstructFromEmptyString()
        {
            using (var buffer = new StringBuffer(""))
            {
                buffer.ByteCapacity.Should().Be(0);
                buffer.CharCapacity.Should().Be(0);
            }
        }

        [Theory
            InlineData("a")
            InlineData("Test")
            ]
        public unsafe void ConstructFromString(string testString)
        {
            using (var buffer = new StringBuffer(testString))
            {
                buffer.Length.Should().Be((uint)testString.Length);
                buffer.CharCapacity.Should().Be((uint)testString.Length + 1);

                for (int i = 0; i < testString.Length; i++)
                {
                    buffer[(uint)i].Should().Be(testString[i]);
                }

                ((char*)buffer.DangerousGetHandle().ToPointer())[testString.Length].Should().Be('\0', "should be null terminated");

                buffer.ToString().Should().Be(testString);
            }
        }

        [Theory
            InlineData("a")
            InlineData("foo")
            ]
        public void ReduceLength(string testString)
        {
            using (var buffer = new StringBuffer(testString))
            {
                buffer.CharCapacity.Should().Be((uint)testString.Length + 1);

                for (int i = 1; i <= testString.Length; i++)
                {
                    buffer.Length = (uint)(testString.Length - i);
                    buffer.ToString().Should().Be(testString.Substring(0, testString.Length - i));
                    buffer.CharCapacity.Should().Be((uint)testString.Length + 1, "shouldn't reduce capacity when dropping length");
                }
            }
        }

        [Fact]
        public void CanIndexChar()
        {
            using (var buffer = new StringBuffer())
            {
                buffer.Length = 1;
                buffer[0] = 'Q';
                buffer[0].Should().Be('Q');
            }
        }

        [Fact]
        public void GetOverIndexThrowsArgumentOutOfRange()
        {
            using (var buffer = new StringBuffer())
            {
                Action action = () => { char c = buffer[0]; };
                action.ShouldThrow<ArgumentOutOfRangeException>();
            }
        }

        [Fact]
        public void SetOverIndexThrowsArgumentOutOfRange()
        {
            using (var buffer = new StringBuffer())
            {
                Action action = () => { buffer[0] = 'Q'; };
                action.ShouldThrow<ArgumentOutOfRangeException>();
            }
        }

        [Theory
            InlineData(0) // 4294967295
            InlineData(1) // 4294967296
            ]
        public void CharCapacityHasUintMax(uint plusValue)
        {
            if (Utility.Environment.Is64BitProcess)
            {
                using (var buffer = new StringBuffer())
                {
                    var length = typeof(NativeBuffer).GetField("_byteCapacity", BindingFlags.NonPublic | BindingFlags.Instance);

                    ulong setValue = (ulong)uint.MaxValue * 2 + plusValue;
                    length.SetValue(buffer, setValue);

                    buffer.CharCapacity.Should().Be(uint.MaxValue);
                }
            }
        }

        [Theory
            InlineData(0, 0)
            InlineData(1, 0)
            InlineData(2, 1)
            InlineData(3, 1)
            ]
        public void CharCapacityFromByte(ulong byteCapacity, uint charCapacity)
        {
            using (var buffer = new StringBuffer())
            {
                buffer.EnsureByteCapacity(byteCapacity);
                buffer.CharCapacity.Should().Be(charCapacity);
            }
        }

        [Theory
            InlineData(0)
            InlineData(1)
            InlineData(2)
            ]
        public void EnsureCharCapacity(uint charCapacity)
        {
            using (var buffer = new StringBuffer())
            {
                buffer.EnsureCharCapacity(charCapacity);
                buffer.CharCapacity.Should().Be(charCapacity);
            }
        }

        [Theory
            InlineData(@"Foo", @"Foo", true)
            InlineData(@"Foo", @"foo", false)
            InlineData(@"Foobar", @"Foo", true)
            InlineData(@"Foobar", @"foo", false)
            InlineData(@"Fo", @"Foo", false)
            InlineData(@"Fo", @"foo", false)
            InlineData(@"", @"", true)
            InlineData(@"", @"f", false)
            InlineData(@"f", @"", true)
            ]
        public void StartsWithOrdinal(string source, string value, bool expected)
        {
            using (var buffer = new StringBuffer(source))
            {
                buffer.StartsWithOrdinal(value).Should().Be(expected);
            }
        }

        [Fact]
        public void StartsWithNullThrows()
        {
            using (var buffer = new StringBuffer())
            {
                Action action = () => buffer.StartsWithOrdinal(null);
                action.ShouldThrow<ArgumentNullException>();
            }
        }

        [Fact]
        public void SubStringEqualsNegativeCountThrows()
        {
            using (var buffer = new StringBuffer())
            {
                Action action = () => buffer.SubStringEquals("", startIndex: 0, count: -2);
                action.ShouldThrow<ArgumentOutOfRangeException>();
            }
        }

        [Fact]
        public void SubStringEqualsOverSizeCountThrows()
        {
            using (var buffer = new StringBuffer())
            {
                Action action = () => buffer.SubStringEquals("", startIndex: 0, count: 1);
                action.ShouldThrow<ArgumentOutOfRangeException>();
            }
        }

        [Fact]
        public void SubStringEqualsOverSizeCountWithIndexThrows()
        {
            using (var buffer = new StringBuffer("A"))
            {
                Action action = () => buffer.SubStringEquals("", startIndex: 1, count: 1);
                action.ShouldThrow<ArgumentOutOfRangeException>();
            }
        }

        [Theory
            InlineData(@"", null, 0, 0, false)
            InlineData(@"", @"", 0, 0, true)
            InlineData(@"", @"", 0, -1, true)
            InlineData(@"A", @"", 0, -1, false)
            InlineData(@"", @"A", 0, -1, false)
            InlineData(@"Foo", @"Foo", 0, -1, true)
            InlineData(@"Foo", @"foo", 0, -1, false)
            InlineData(@"Foo", @"Foo", 1, -1, false)
            InlineData(@"Foo", @"Food", 0, -1, false)
            InlineData(@"Food", @"Foo", 0, -1, false)
            InlineData(@"Food", @"Foo", 0, 3, true)
            InlineData(@"Food", @"ood", 1, 3, true)
            InlineData(@"Food", @"ooD", 1, 3, false)
            InlineData(@"Food", @"ood", 1, 2, false)
            InlineData(@"Food", @"Food", 0, 3, false)
        ]
        public void SubStringEquals(string source, string value, int startIndex, int count, bool expected)
        {
            using (var buffer = new StringBuffer(source))
            {
                buffer.SubStringEquals(value, startIndex: (uint)startIndex, count: count).Should().Be(expected);
            }
        }

        [Theory
            InlineData(@"", @"", 0, -1, @"")
            InlineData(@"", @"", 0, 0, @"")
            InlineData(@"", @"A", 0, -1, @"A")
            InlineData(@"", @"A", 0, 0, @"")
            InlineData(@"", @"Aa", 0, -1, @"Aa")
            InlineData(@"", @"Aa", 0, 0, @"")
            InlineData(@"", "Aa\0", 0, -1, "Aa\0")
            InlineData(@"", "Aa\0", 0, 3, "Aa\0")
            InlineData(@"", @"AB", 0, -1, @"AB")
            InlineData(@"", @"AB", 0, 1, @"A")
            InlineData(@"", @"AB", 1, 1, @"B")
            InlineData(@"", @"AB", 1, -1, @"B")
            InlineData(@"", @"ABC", 1, -1, @"BC")
            InlineData(null, @"", 0, -1, @"")
            InlineData(null, @"", 0, 0, @"")
            InlineData(null, @"A", 0, -1, @"A")
            InlineData(null, @"A", 0, 0, @"")
            InlineData(null, @"Aa", 0, -1, @"Aa")
            InlineData(null, @"Aa", 0, 0, @"")
            InlineData(null, "Aa\0", 0, -1, "Aa\0")
            InlineData(null, "Aa\0", 0, 3, "Aa\0")
            InlineData(null, @"AB", 0, -1, @"AB")
            InlineData(null, @"AB", 0, 1, @"A")
            InlineData(null, @"AB", 1, 1, @"B")
            InlineData(null, @"AB", 1, -1, @"B")
            InlineData(null, @"ABC", 1, -1, @"BC")
            InlineData(@"Q", @"", 0, -1, @"Q")
            InlineData(@"Q", @"", 0, 0, @"Q")
            InlineData(@"Q", @"A", 0, -1, @"QA")
            InlineData(@"Q", @"A", 0, 0, @"Q")
            InlineData(@"Q", @"Aa", 0, -1, @"QAa")
            InlineData(@"Q", @"Aa", 0, 0, @"Q")
            InlineData(@"Q", "Aa\0", 0, -1, "QAa\0")
            InlineData(@"Q", "Aa\0", 0, 3, "QAa\0")
            InlineData(@"Q", @"AB", 0, -1, @"QAB")
            InlineData(@"Q", @"AB", 0, 1, @"QA")
            InlineData(@"Q", @"AB", 1, 1, @"QB")
            InlineData(@"Q", @"AB", 1, -1, @"QB")
            InlineData(@"Q", @"ABC", 1, -1, @"QBC")
            ]
        public void AppendTests(string source, string value, int startIndex, int count, string expected)
        {
            // From string
            using (var buffer = new StringBuffer(source))
            {
                buffer.Append(value, startIndex, count);
                buffer.ToString().Should().Be(expected);
            }

            // From buffer
            using (var buffer = new StringBuffer(source))
            using (var valueBuffer = new StringBuffer(value))
            {
                if (count == -1)
                    buffer.Append(valueBuffer, (uint)startIndex);
                else
                    buffer.Append(valueBuffer, (uint)startIndex, (uint)count);
                buffer.ToString().Should().Be(expected);
            }
        }

        [Fact]
        public void AppendNullStringThrows()
        {
            using (var buffer = new StringBuffer())
            {
                Action action = () => buffer.Append((string)null);
                action.ShouldThrow<ArgumentNullException>();
            }
        }

        [Fact]
        public void AppendNullStringBufferIndexThrows()
        {
            using (var buffer = new StringBuffer())
            {
                Action action = () => buffer.Append((StringBuffer)null, 0);
                action.ShouldThrow<ArgumentNullException>();
            }
        }

        [Fact]
        public void AppendNullStringBufferIndexCountThrows()
        {
            using (var buffer = new StringBuffer())
            {
                Action action = () => buffer.Append((StringBuffer)null, 0, 0);
                action.ShouldThrow<ArgumentNullException>();
            }
        }

        [Fact]
        public void AppendNegativeIndexThrows()
        {
            using (var buffer = new StringBuffer())
            {
                Action action = () => buffer.Append("a", startIndex: -1);
                action.ShouldThrow<ArgumentOutOfRangeException>();
            }
        }

        [Fact]
        public void AppendOverIndexThrows()
        {
            using (var buffer = new StringBuffer())
            {
                Action action = () => buffer.Append("", startIndex: 1);
                action.ShouldThrow<ArgumentOutOfRangeException>();
            }
        }

        [Fact]
        public void AppendOverCountThrows()
        {
            using (var buffer = new StringBuffer())
            {
                Action action = () => buffer.Append("", startIndex: 0, count: 1);
                action.ShouldThrow<ArgumentOutOfRangeException>();
            }
        }

        [Fact]
        public void AppendOverCountWithIndexThrows()
        {
            using (var buffer = new StringBuffer())
            {
                Action action = () => buffer.Append("A", startIndex: 1, count: 1);
                action.ShouldThrow<ArgumentOutOfRangeException>();
            }
        }

        [Fact]
        public void SubStringIndexOverLengthThrows()
        {
            using (var buffer = new StringBuffer())
            {
                Action action = () => buffer.SubString(startIndex: 1);
                action.ShouldThrow<ArgumentOutOfRangeException>().And.ParamName.Should().Be("startIndex");

                buffer.Append("a");
                action.ShouldThrow<ArgumentOutOfRangeException>().And.ParamName.Should().Be("startIndex");

                buffer.Append("b");
                action.ShouldNotThrow();
            }
        }

        [Fact]
        public void SubStringNegativeCountThrows()
        {
            using (var buffer = new StringBuffer())
            {
                Action action = () => buffer.SubString(startIndex: 0, count: -2);
                action.ShouldThrow<ArgumentOutOfRangeException>().And.ParamName.Should().Be("count");
            }
        }

        [Fact]
        public void SubStringCountOverLengthThrows()
        {
            using (var buffer = new StringBuffer())
            {
                Action action = () => buffer.SubString(startIndex: 0, count: 1);
                action.ShouldThrow<ArgumentOutOfRangeException>().And.ParamName.Should().Be("count");
            }
        }

        [Fact]
        public void SubStringImplicitCountOverMaxStringThrows()
        {
            using (var buffer = new StringBuffer())
            {
                var length = buffer.GetType().GetField("_length", BindingFlags.NonPublic | BindingFlags.Instance);

                length.SetValue(buffer, (uint)int.MaxValue + 1);
                Action action = () => buffer.SubString(startIndex: 0, count: -1);
                action.ShouldThrow<ArgumentOutOfRangeException>().And.ParamName.Should().Be("count");
            }
        }

        [Fact]
        public void SubStringIndexPlusCountCombinedOutOfRangeThrows()
        {
            using (var buffer = new StringBuffer("a"))
            {
                Action action = () => buffer.SubString(startIndex: 1, count: 1);
                action.ShouldThrow<ArgumentOutOfRangeException>().And.ParamName.Should().Be("startIndex");

                buffer.Append("b");
                action.ShouldNotThrow<ArgumentOutOfRangeException>();

                action = () => buffer.SubString(startIndex: 1, count: 2);
                action.ShouldThrow<ArgumentOutOfRangeException>().And.ParamName.Should().Be("count");
            }
        }

        [Theory
            InlineData(@"", 0, -1, @"")
            InlineData(@"A", 0, -1, @"A")
            InlineData(@"AB", 0, -1, @"AB")
            InlineData(@"AB", 0, 1, @"A")
            InlineData(@"AB", 1, 1, @"B")
            InlineData(@"AB", 1, -1, @"B")
            InlineData(@"", 0, 0, @"")
            InlineData(@"A", 0, 0, @"")
        ]
        public void SubStringTest(string source, int startIndex, int count, string expected)
        {
            using (var buffer = new StringBuffer(source))
            {
                buffer.SubString(startIndex: (uint)startIndex, count: count).Should().Be(expected);
            }
        }

        [Fact]
        public void ToStringOverSizeThrowsOverflow()
        {
            using (var buffer = new StringBuffer())
            {
                var length = buffer.GetType().GetField("_length", BindingFlags.NonPublic | BindingFlags.Instance);

                length.SetValue(buffer, (uint)int.MaxValue + 1);
                Action action = () => buffer.ToString();
                action.ShouldThrow<OverflowException>();
            }
        }

        [Fact]
        public unsafe void SetLengthToFirstNullNoNull()
        {
            using (var buffer = new StringBuffer("A"))
            {
                // Wipe out the last null
                ((char*)buffer.DangerousGetHandle().ToPointer())[buffer.Length] = 'B';
                buffer.SetLengthToFirstNull();
                buffer.Length.Should().Be(1);
            }
        }

        [Fact]
        public unsafe void SetLengthToFirstNullEmptyBuffer()
        {
            using (var buffer = new StringBuffer())
            {
                buffer.SetLengthToFirstNull();
                buffer.Length.Should().Be(0);
            }
        }

        [Theory
            InlineData(@"", 0, 0)
            InlineData(@"Foo", 3, 3)
            InlineData("\0", 1, 0)
            InlineData("Foo\0Bar", 7, 3)
            ]
        public unsafe void SetLengthToFirstNullTests(string content, uint startLength, uint endLength)
        {
            using (var buffer = new StringBuffer(content))
            {
                // With existing content
                buffer.Length.Should().Be(startLength);
                buffer.SetLengthToFirstNull();
                buffer.Length.Should().Be(endLength);

                // Clear the buffer & manually copy in
                buffer.Length = 0;
                fixed (char* contentPointer = content)
                {
                    Buffer.MemoryCopy(contentPointer, buffer.DangerousGetHandle().ToPointer(), (long)buffer.CharCapacity * 2, content.Length * sizeof(char));
                }

                buffer.Length.Should().Be(0);
                buffer.SetLengthToFirstNull();
                buffer.Length.Should().Be(endLength);
            }
        }

        [Theory
            InlineData("foo bar", ' ')
            InlineData("foo\0bar", '\0')
            InlineData("foo\0bar", ' ')
            InlineData("foobar", ' ')
            InlineData("foo bar ", ' ')
            InlineData("foobar ", ' ')
            InlineData("foobar ", 'b')
            InlineData(" ", ' ')
            InlineData("", ' ')
            InlineData(null, ' ')
            ]
        public void Split(string content, char splitChar)
        {
            // We want equivalence with built-in string behavior
            using (var buffer = new StringBuffer(content))
            {
                buffer.Split(splitChar).ShouldAllBeEquivalentTo(content?.Split(splitChar) ?? new string[] { "" });
            }
        }

        [Theory
            InlineData("foo", new char[] { }, "foo")
            InlineData("foo", null, "foo")
            InlineData("foo", new char[] { 'b' }, "foo")
            InlineData("", new char[] { }, "")
            InlineData("", null, "")
            InlineData("", new char[] { 'b' }, "")
            InlineData("foo", new char[] { 'o' }, "f")
            InlineData("foo", new char[] { 'o', 'f' }, "")
            // Add a couple cases to try and get the trim to walk off the front of the buffer.
            InlineData("foo", new char[] { 'o', 'f', '\0' }, "")
            InlineData("foo", new char[] { 'o', 'f', '\u9000' }, "")
            ]
        public void TrimEnd(string content, char[] trimChars, string expected)
        {
            // We want equivalence with built-in string behavior
            using (var buffer = new StringBuffer(content))
            {
                buffer.TrimEnd(trimChars);
                buffer.ToString().Should().Be(expected);
            }
        }

        [Theory
            InlineData("foo bar", new char[] { ' ' })
            InlineData("foo bar", new char[] { })
            InlineData("foo bar", null)
            InlineData("foo\0bar", new char[] { '\0' })
            InlineData("foo\0bar", new char[] { ' ' })
            InlineData("foobar", new char[] { ' ' })
            InlineData("foo bar ", new char[] { ' ' })
            InlineData("foobar ", new char[] { ' ' })
            InlineData("foobar ", new char[] { ' ', 'b' })
            InlineData(" ", new char[] { ' ' })
            InlineData("", new char[] { ' ' })
            InlineData(null, new char[] { ' ' })
            ]
        public void SplitParams(string content, char[] splitChars)
        {
            // We want equivalence with built-in string behavior
            using (var buffer = new StringBuffer(content))
            {
                buffer.Split(splitChars).ShouldAllBeEquivalentTo(content?.Split(splitChars) ?? new string[] { "" });
            }
        }

        [Theory
            InlineData(null, ' ', false)
            InlineData("", ' ', false)
            InlineData("foo", 'F', false)
            InlineData("foo", '\0', false)
            InlineData("foo", 'f', true)
            InlineData("foo", 'o', true)
            InlineData("foo\0", '\0', true)
            ]
        public void ContainsTests(string content, char value, bool expected)
        {
            using (var buffer = new StringBuffer(content))
            {
                buffer.Contains(value).Should().Be(expected);
            }
        }

        [Theory
            InlineData(null, null, false)
            InlineData(null, new char[0], false)
            InlineData(null, new char[] { ' ' }, false)
            InlineData("", new char[] { ' ' }, false)
            InlineData("foo", new char[] { 'F' }, false)
            InlineData("foo", new char[] { '\0' }, false)
            InlineData("foo", new char[] { 'f' }, true)
            InlineData("foo", new char[] { 'o' }, true)
            InlineData("foo\0", new char[] { '\0' }, true)
            ]
        public void ContainsParamsTests(string content, char[] values, bool expected)
        {
            using (var buffer = new StringBuffer(content))
            {
                buffer.Contains(values).Should().Be(expected);
            }
        }

        [Theory
            InlineData(@"Foo", @"Bar", 0, 0, 3, "Bar")
            InlineData(@"Foo", @"Bar", 0, 0, -1, "Bar")
            InlineData(@"Foo", @"Bar", 3, 0, 3, "FooBar")
            InlineData(@"", @"Bar", 0, 0, 3, "Bar")
            InlineData(@"Foo", @"Bar", 1, 0, 3, "FBar")
            InlineData(@"Foo", @"Bar", 1, 1, 2, "Far")
            ]
        public void CopyFromString(string content, string source, uint bufferIndex, int sourceIndex, int count, string expected)
        {
            using (var buffer = new StringBuffer(content))
            {
                buffer.CopyFrom(bufferIndex, source, sourceIndex, count);
                buffer.ToString().Should().Be(expected);
            }
        }

        [Fact]
        public void CopyFromStringThrowsOnNull()
        {
            using (var buffer = new StringBuffer())
            {
                Action action = () => { buffer.CopyFrom(0, null); };
                action.ShouldThrow<ArgumentNullException>();
            }
        }

        [Fact]
        public void CopyFromStringThrowsIndexingBeyondBufferLength()
        {
            using (var buffer = new StringBuffer())
            {
                Action action = () => { buffer.CopyFrom(1, ""); };
                action.ShouldThrow<ArgumentOutOfRangeException>();
            }
        }

        [Theory
            InlineData("", 0, 1)
            InlineData("", 1, 0)
            InlineData("", 1, -1)
            InlineData("", 2, 0)
            InlineData("Foo", 3, 1)
            InlineData("Foo", 4, 0)
            ]
        public void CopyFromStringThrowsIndexingBeyondStringLength(string value, int index, int count)
        {
            using (var buffer = new StringBuffer())
            {
                Action action = () => { buffer.CopyFrom(0, value, index, count); };
                action.ShouldThrow<ArgumentOutOfRangeException>();
            }
        }

        [Theory
            InlineData(@"Foo", @"Bar", 0, 0, 3, "Bar")
            InlineData(@"Foo", @"Bar", 0, 0, 0, "Foo")
            InlineData(@"Foo", @"Bar", 3, 0, 3, "FooBar")
            InlineData(@"", @"Bar", 0, 0, 3, "Bar")
            InlineData(@"Foo", @"Bar", 1, 0, 3, "FBar")
            InlineData(@"Foo", @"Bar", 1, 1, 2, "Far")
            ]
        public void CopyToBufferString(string destination, string content, uint destinationIndex, uint bufferIndex, uint count, string expected)
        {
            using (var buffer = new StringBuffer(content))
            using (var destinationBuffer = new StringBuffer(destination))
            {
                buffer.CopyTo(bufferIndex, destinationBuffer, destinationIndex, count);
                destinationBuffer.ToString().Should().Be(expected);
            }
        }

        [Fact]
        public void CopyToBufferOutOfRangeThrows()
        {
            using (var buffer = new StringBuffer())
            using (var destinationBuffer = new StringBuffer())
            {
                Action action = () => buffer.CopyTo(0, destinationBuffer, 1, 0);
                action.ShouldThrow<ArgumentOutOfRangeException>().And.ParamName.Should().Be("destinationIndex");
            }
        }

        [Fact]
        public void CopyToBufferThrowsOnNull()
        {
            using (var buffer = new StringBuffer())
            {
                Action action = () => { buffer.CopyTo(0, null, 0, 0); };
                action.ShouldThrow<ArgumentNullException>();
            }
        }

        [Theory
            InlineData("", 0, 1)
            InlineData("", 1, 0)
            InlineData("", 2, 0)
            InlineData("Foo", 3, 1)
            InlineData("Foo", 4, 0)
            ]
        public void CopyToBufferThrowsIndexingBeyondSourceBufferLength(string source, uint index, uint count)
        {
            using (var buffer = new StringBuffer(source))
            using (var targetBuffer = new StringBuffer())
            {
                Action action = () => { buffer.CopyTo(index, targetBuffer, 0, count); };
                action.ShouldThrow<ArgumentOutOfRangeException>();
            }
        }

        [Fact]
        public void AppendChar()
        {
            string testString = "Fo\0o";

            using (var buffer = new StringBuffer())
            {
                for (int i = 0; i < testString.Length; i++)
                {
                    buffer.Append(testString[i]);
                    buffer.Length.Should().Be((uint)i + 1);
                    buffer.ToString().Should().Be(testString.Substring(0, i + 1));
                }
            }
        }

        [Theory
            InlineData("", 'a', 0, 1, false)
            InlineData("", 'a', 1, 1, false)
            InlineData("a", 'a', 0, 0, true)
            InlineData("a", 'a', 1, 2, false)
            InlineData("aa", 'a', 0, 0, true)
            InlineData("aa", 'a', 1, 1, true)
            InlineData("ab", 'b', 0, 1, true)
            ]
        public void IndexOfTests(string source, char value, uint skip, uint expectedIndex, bool expectedValue)
        {
            using (var buffer = new StringBuffer(source))
            {
                uint index;
                buffer.IndexOf(value, out index, skip).Should().Be(expectedValue);
                index.Should().Be(expectedIndex);
            }
        }

        [Theory
            InlineData("", 'z', 0, "")
            InlineData("", 'a', 1, "a")
            InlineData("", 'b', 2, "bb")
            InlineData("", 'c', 3, "ccc")
            InlineData("", 'd', 4, "dddd")
            InlineData("", 'e', 5, "eeeee")
            InlineData("", 'f', 6, "ffffff")
            InlineData("", 'g', 7, "ggggggg")
            InlineData("", 'h', 8, "hhhhhhhh")
            InlineData("", 'i', 9, "iiiiiiiii")
            InlineData("", 'j', 10, "jjjjjjjjjj")
            InlineData("", 'k', 11, "kkkkkkkkkkk")
            InlineData("y", 'z', 0, "y")
            InlineData("y", 'a', 1, "ya")
            InlineData("y", 'b', 2, "ybb")
            ]
        public void AppendCharCountTests(string initialBuffer, char value, uint count, string expected)
        {
            using (var buffer = new StringBuffer(initialBuffer))
            {
                buffer.Append(value, count);
                buffer.ToString().Should().Be(expected);
            }
        }

        // [Fact]
        public void AppendCharCountPerf()
        {
            using (var buffer = new StringBuffer())
            {
                for (int i = 0; i < 100000; i++)
                {
                    buffer.Length = 0;
                    buffer.Append('a', 100000);
                }
            }
        }
    }
}
