// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.IO;
using System.Text;
using Tests.Support;
using WInterop.Compression;
using WInterop.Compression.Types;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.Types;
using WInterop.Support;
using Xunit;

namespace DesktopTests.Compression
{
    public class LzTests
    {
        [Theory,
            InlineData(true),
            InlineData(false)]
        public void ReadStream_ExpandedSmaller(bool useCreateFile)
        {
            // Try a file that is bigger compressed
            using (var cleaner = new TestFileCleaner())
            {
                string path = cleaner.CreateTestFile(CompressedFile1);

                using (var lzStream = new LzStream(path, useCreateFile))
                {
                    lzStream.UncompressedName.Should().Be(path);

                    using (var reader = new StreamReader(lzStream, Encoding.ASCII))
                    {
                        string result = reader.ReadToEnd();
                        result.Should().Be(CompressedContent1);
                    }
                }
            }
        }

        [Theory,
            InlineData(true),
            InlineData(false)]
        public void ReadStream_ExpandedLarger(bool useCreateFile)
        {
            // Try a file that is bigger uncompressed
            using (var cleaner = new TestFileCleaner())
            {
                string path = cleaner.CreateTestFile(CompressedFile2);

                using (var lzStream = new LzStream(path, useCreateFile))
                {
                    lzStream.UncompressedName.Should().Be(path);

                    using (var reader = new StreamReader(lzStream, Encoding.ASCII))
                    {
                        string result = reader.ReadToEnd();
                        result.Should().Be(CompressedContent2);
                    }
                }
            }
        }

        [Theory,
            InlineData(true),
            InlineData(false)]
        public void CopyFile(bool useCreateFile)
        {
            using (var cleaner = new TestFileCleaner())
            {
                string source = cleaner.CreateTestFile(CompressedFile2);
                string destination = cleaner.GetTestPath();

                CompressionMethods.CopyFile(source, destination, false, useCreateFile).Should().Be(563);

                FileHelper.ReadAllText(destination).Should().Be(CompressedContent2);
            }
        }

        [Theory,
            InlineData(true),
            InlineData(false)]
        public void CopyFile_OverExisting(bool useCreateFile)
        {
            using (var cleaner = new TestFileCleaner())
            {
                string source = cleaner.CreateTestFile(CompressedFile2);
                string destination = cleaner.CreateTestFile($"CopyFile_OverExisting({useCreateFile})");

                CompressionMethods.CopyFile(source, destination, overwrite: true, useCreateFile: useCreateFile).Should().Be(563);
                FileHelper.ReadAllText(destination).Should().Be(CompressedContent2);
            }
        }

        [Theory,
            InlineData(true),
            InlineData(false)]
        public void CopyFile_NotOverExisting(bool useCreateFile)
        {
            using (var cleaner = new TestFileCleaner())
            {
                string source = cleaner.CreateTestFile(CompressedFile2);
                string destination = cleaner.CreateTestFile($"CopyFile_NotOverExisting({useCreateFile})");

                Action action = () => CompressionMethods.CopyFile(source, destination, overwrite: false, useCreateFile: useCreateFile);
                action.ShouldThrow<IOException>().And.HResult.Should().Be((int)ErrorMacros.HRESULT_FROM_WIN32(WindowsError.ERROR_FILE_EXISTS));
            }
        }

        [Fact]
        public unsafe void CheckHeader()
        {
            fixed (byte* b = CompressedFile1)
            {
                LzxHeader header = *(LzxHeader*)b;
                header.GetSignature().Should().ContainInOrder(LzxHeader.LzxSignature);
                header.GetUncompressedSize().Should().Be((uint)CompressedContent1.Length);
                header.algorithm.Should().Be((byte)'A');
                header.extensionChar.Should().Be(0x00);
            }
        }

        [Fact]
        public unsafe void ValidHeader()
        {
            fixed (byte* b = CompressedFile1)
            {
                LzxHeader header = *(LzxHeader*)b;
                header.IsHeaderValid().Should().BeTrue();
            }
        }

        [Fact]
        public unsafe void InvalidHeader()
        {
            for (int i = 0; i < 9; i++)
            {
                byte[] data = new byte[CompressedFile1.Length];
                CompressedFile1.CopyTo(data, 0);
                data[i] = 0xCC;

                fixed (byte* b = data)
                {
                    LzxHeader header = *(LzxHeader*)b;
                    header.IsHeaderValid().Should().BeFalse($"byte {i} was modified");
                }
            }
        }

        [Theory, MemberData(nameof(ExpandedTestData))]
        public void ExpandedName(string compressedName, byte character, string expandedName)
        {
            using (var cleaner = new TestFileCleaner())
            {
                byte[] data = new byte[CompressedFile1.Length];
                CompressedFile1.CopyTo(data, 0);
                data[9] = character;
                string path = Paths.Combine(cleaner.TempFolder, compressedName);
                FileHelper.WriteAllBytes(path, data);
                Path.GetFileName(CompressionMethods.GetExpandedName(path)).Should().Be(expandedName);
            }
        }

        [Theory, MemberData(nameof(ExpandedTestData))]
        public void ExpandedNameEx(string compressedName, byte character, string expandedName)
        {
            using (var cleaner = new TestFileCleaner())
            {
                byte[] data = new byte[CompressedFile1.Length];
                CompressedFile1.CopyTo(data, 0);
                data[9] = character;
                string path = Paths.Combine(cleaner.TempFolder, compressedName);
                FileHelper.WriteAllBytes(path, data);
                Path.GetFileName(CompressionMethods.GetExpandedNameEx(path)).Should().Be(expandedName);
            }
        }

        public static TheoryData<string, byte, string> ExpandedTestData
        {
            get
            {
                return new TheoryData<string, byte, string>
                {
                    { "Foo", 0x00, "Foo" },
                    { "Foo", (byte)'B', "Foo" },
                    { "Foo.a", (byte)'F', "Foo.a" },
                    { "Foo._", 0x00, "Foo" },
                    { "Foo._", (byte)'_', "Foo._" },
                    { "Foo._", (byte)'C', "Foo.C" },
                    { "Foo._um", (byte)'g', "Foo._um" },
                    { "Foo._", (byte)'d', "Foo.D" },
                    { "FOo.tx_", (byte)'E', "FOo.txE" },
                    { "FoO.Appl_", (byte)'~', "FoO.Appl~" }
                };
            }
        }

        [Fact]
        public void ExpandedName_NotLzFile()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string path = cleaner.CreateTestFile("ExpandedName_NotLzFile");
                CompressionMethods.GetExpandedName(path).Should().Be(path);
            }
        }

        [Fact]
        public void ExpandedName_LongPath()
        {
            // Unfortunately GetExpandedNameW doesn't fail properly. It calls the A version
            // and accidentally ignores the errors returned, copying garbage into the
            // returned string.

            using (var cleaner = new TestFileCleaner())
            {
                string path = PathGenerator.CreatePathOfLength(cleaner.TempFolder, 160);
                FileHelper.WriteAllBytes(path, CompressedFile1);
                Action action = () => CompressionMethods.GetExpandedName(path);
                action.ShouldThrow<WInteropIOException>().WithMessage("BadValue");
            }
        }

        [Fact]
        public void OpenFile_LongPath()
        {
            // The OpenFile api only supports 128 character paths.
            using (var cleaner = new TestFileCleaner())
            {
                string path = PathGenerator.CreatePathOfLength(cleaner.TempFolder, 160);
                FileHelper.WriteAllBytes(path, CompressedFile1);
                Action action = () => CompressionMethods.LzOpenFile(path);
                action.ShouldThrow<WInteropIOException>().WithMessage("BadInHandle");
            }
        }

        [Fact]
        public void CreateFile_LongPath()
        {
            // Unlike OpenFile, CreateFile handles > 128 character paths.
            using (var cleaner = new TestFileCleaner())
            {
                string path = PathGenerator.CreatePathOfLength(cleaner.TempFolder, 160);
                FileHelper.WriteAllBytes(path, CompressedFile1);
                using (var handle = CompressionMethods.LzCreateFile(path))
                {
                    handle.RawHandle.Should().BeGreaterThan(0);
                }
            }
        }

        [Fact]
        public void CreateFile_OverMaxPathLongPath()
        {
            // Unlike OpenFile, CreateFile handles > 128 character paths. Unfortunately it is
            // constrained by MAX_PATH internal buffers, so it cant go over 260.
            using (var cleaner = new TestFileCleaner())
            {
                string path = @"\\?\" + PathGenerator.CreatePathOfLength(cleaner.TempFolder, 300);
                FileHelper.WriteAllBytes(path, CompressedFile1);
                Action action = () => CompressionMethods.LzCreateFile(path);
                action.ShouldThrow<WInteropIOException>().WithMessage("BadValue");
            }
        }

        // [Fact]
        public unsafe void ExpandAll()
        {
            string path = @"P:\TEmp\DDK\WIN30DDK";
            string targetRoot = Path.Combine(Path.GetDirectoryName(path), "Expanded", Path.GetFileName(path));
            foreach (var file in Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories))
            {
                string target = Path.Combine(targetRoot, file.Substring(path.Length + 1));
                Directory.CreateDirectory(Path.GetDirectoryName(target));
                CompressionMethods.CopyFile(file, target);
            }
        }


        // COMPRESS.EXE from the Windows Server 2003 resource kit doesn't get the expanded size in the header
        // correct (when running on Win10 RS2 at least). Not sure why this is, or why expand.exe seems to be
        // ok with the bad size while the API throws a fit (-3).

        private const string CompressedContent1 = "This is a test file that will test file compression. \r\n";

        private static byte[] CompressedFile1 =
        {
            0x53, 0x5A, 0x44, 0x44, 0x88, 0xF0, 0x27, 0x33, 0x41, 0x00, 0x37, 0x00, 0x00, 0x00, 0xDF, 0x54,
            0x68, 0x69, 0x73, 0x20, 0xF2, 0xF0, 0x61, 0x20, 0xFF, 0x74, 0x65, 0x73, 0x74, 0x20, 0x66, 0x69,
            0x6C, 0xFF, 0x65, 0x20, 0x74, 0x68, 0x61, 0x74, 0x20, 0x77, 0xF7, 0x69, 0x6C, 0x6C, 0xF9, 0xF8,
            0x63, 0x6F, 0x6D, 0x70, 0xFF, 0x72, 0x65, 0x73, 0x73, 0x69, 0x6F, 0x6E, 0x2E, 0x07, 0x20, 0x0D,
            0x0A
        };

        private const string CompressedContent2 =
        "Four score and seven years ago our fathers brought forth upon this continent, a new nation, conceived in Liberty, and dedicated to the proposition that all men are created equal."
        + "\r\n\r\n"
        + "Now we are engaged in a great civil war, testing whether that nation, or any nation so conceived, and so dedicated, can long endure. "
        + "We are met on a great battle-field of that war. "
        + "We have come to dedicate a portion of that field, as a final resting place for those who here gave their lives, that that nation might live. "
        + "It is altogether fitting and proper that we should do this.";

        private static byte[] CompressedFile2 =
        {
            0x53, 0x5A, 0x44, 0x44, 0x88, 0xF0, 0x27, 0x33, 0x41, 0x00, 0x33, 0x02, 0x00, 0x00, 0xFF, 0x46,
            0x6F, 0x75, 0x72, 0x20, 0x73, 0x63, 0x6F, 0xFF, 0x72, 0x65, 0x20, 0x61, 0x6E, 0x64, 0x20, 0x73,
            0xFF, 0x65, 0x76, 0x65, 0x6E, 0x20, 0x79, 0x65, 0x61, 0x7F, 0x72, 0x73, 0x20, 0x61, 0x67, 0x6F,
            0x20, 0xF1, 0xF1, 0xDF, 0x66, 0x61, 0x74, 0x68, 0x65, 0x08, 0x00, 0x62, 0x72, 0xFF, 0x6F, 0x75,
            0x67, 0x68, 0x74, 0x20, 0x66, 0x6F, 0xFF, 0x72, 0x74, 0x68, 0x20, 0x75, 0x70, 0x6F, 0x6E, 0xFF,
            0x20, 0x74, 0x68, 0x69, 0x73, 0x20, 0x63, 0x6F, 0xFF, 0x6E, 0x74, 0x69, 0x6E, 0x65, 0x6E, 0x74,
            0x2C, 0xFF, 0x20, 0x61, 0x20, 0x6E, 0x65, 0x77, 0x20, 0x6E, 0xBF, 0x61, 0x74, 0x69, 0x6F, 0x6E,
            0x2C, 0x32, 0x01, 0x63, 0xFF, 0x65, 0x69, 0x76, 0x65, 0x64, 0x20, 0x69, 0x6E, 0xFF, 0x20, 0x4C,
            0x69, 0x62, 0x65, 0x72, 0x74, 0x79, 0xFC, 0x3C, 0x00, 0xFC, 0xF0, 0x64, 0x65, 0x64, 0x69, 0x63,
            0x61, 0xED, 0x74, 0x53, 0x00, 0x74, 0x6F, 0x2D, 0x00, 0x65, 0x20, 0x70, 0x3F, 0x72, 0x6F, 0x70,
            0x6F, 0x73, 0x69, 0x46, 0x01, 0x2D, 0x00, 0xFF, 0x61, 0x74, 0x20, 0x61, 0x6C, 0x6C, 0x20, 0x6D,
            0xBA, 0x02, 0x00, 0x61, 0xF8, 0xF0, 0x63, 0x72, 0x65, 0x6B, 0x02, 0x65, 0xFF, 0x71, 0x75, 0x61,
            0x6C, 0x2E, 0x0D, 0x0A, 0x0D, 0x3F, 0x0A, 0x4E, 0x6F, 0x77, 0x20, 0x77, 0xF9, 0xF0, 0xF8, 0xF0,
            0xDF, 0x65, 0x6E, 0x67, 0x61, 0x67, 0x53, 0x03, 0x61, 0x20, 0xFD, 0x67, 0x95, 0x01, 0x20, 0x63,
            0x69, 0x76, 0x69, 0x6C, 0xFF, 0x20, 0x77, 0x61, 0x72, 0x2C, 0x20, 0x74, 0x65, 0x7D, 0x73, 0x36,
            0x00, 0x67, 0x20, 0x77, 0x68, 0x65, 0x15, 0x01, 0xAC, 0x82, 0x03, 0x44, 0x05, 0x6F, 0x72, 0xFA,
            0xF0, 0x79, 0x43, 0x04, 0x20, 0x83, 0x73, 0x6F, 0x4B, 0x07, 0x60, 0x03, 0xFA, 0x00, 0x66, 0x06,
            0x4A, 0x00, 0x61, 0xEF, 0x6E, 0x20, 0x6C, 0x6F, 0xD4, 0x00, 0x65, 0x6E, 0x64, 0xBF, 0x75, 0x72,
            0x65, 0x2E, 0x20, 0x57, 0xAB, 0x03, 0x6D, 0xE7, 0x65, 0x74, 0x20, 0x2B, 0x00, 0xBC, 0x05, 0x62,
            0x61, 0x74, 0xFF, 0x74, 0x6C, 0x65, 0x2D, 0x66, 0x69, 0x65, 0x6C, 0x8F, 0x64, 0x20, 0x6F, 0x66,
            0x82, 0x03, 0xCA, 0x00, 0x29, 0x12, 0x68, 0x3B, 0x61, 0x76, 0x92, 0x00, 0x6F, 0x6D, 0x65, 0x6F,
            0x01, 0x66, 0x05, 0x02, 0x3D, 0x00, 0x70, 0x24, 0x00, 0x7F, 0x01, 0x4E, 0x15, 0x48, 0x12, 0x3C,
            0x00, 0x09, 0x00, 0xBE, 0x85, 0x10, 0x6E, 0x61, 0x6C, 0x20, 0x72, 0xD0, 0x04, 0x70, 0xCF, 0x6C,
            0x61, 0x63, 0x65, 0x22, 0x01, 0x2D, 0x00, 0x6F, 0x73, 0xED, 0x65, 0xD6, 0x00, 0x6F, 0x20, 0x16,
            0x00, 0x65, 0x20, 0x67, 0xBC, 0x5F, 0x11, 0x15, 0x00, 0x69, 0x72, 0x20, 0x6C, 0x51, 0x00, 0x73,
            0x38, 0xCD, 0x00, 0x84, 0x01, 0xDF, 0x08, 0x20, 0x6D, 0x69, 0x1F, 0x01, 0xC4, 0x11, 0xDF, 0x2E,
            0x20, 0x49, 0x74, 0x20, 0x30, 0x00, 0x61, 0x6C, 0x77, 0x74, 0x6F, 0x67, 0xD9, 0x03, 0x66, 0x69,
            0x74, 0xD2, 0x02, 0xF0, 0xFB, 0xF1, 0x77, 0x01, 0xDC, 0x05, 0xAA, 0x00, 0x73, 0x68, 0x6F, 0x75,
            0x3A, 0x4B, 0x10, 0x64, 0x71, 0x01, 0x69, 0x73, 0x2E
        };
    }
}
