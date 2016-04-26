// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Tests.NativeMethodTests
{
    using ErrorHandling;
    using FluentAssertions;
    using System;
    using System.IO;
    using Xunit;

    public class FileManagementTests
    {
        [Fact]
        public void GetTempPathBasic()
        {
            NativeMethods.FileManagement.GetTempPath().Should().NotBeNullOrWhiteSpace();
        }

#if DESKTOP
        [Fact]
        public void GetShortPathBasic()
        {
            string tempPath = NativeMethods.FileManagement.GetTempPath();
            NativeMethods.FileManagement.GetShortPathName(tempPath).Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void GetLongPathBasic()
        {
            string tempPath = NativeMethods.FileManagement.GetTempPath();
            NativeMethods.FileManagement.GetLongPathName(tempPath).Should().NotBeNullOrWhiteSpace();
        }
#endif

        [Fact]
        public void GetFullPathBasic()
        {
            string tempPath = NativeMethods.FileManagement.GetTempPath();
            NativeMethods.FileManagement.GetFullPathName(tempPath).Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void GetTempFileNameBasic()
        {
            string tempPath = NativeMethods.FileManagement.GetTempPath();
            string tempFileName = NativeMethods.FileManagement.GetTempFileName(tempPath, "tfn");
            try
            {
                tempFileName.Should().StartWith(tempPath);
            }
            finally
            {
                NativeMethods.FileManagement.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void DeleteFileBasic()
        {
            string tempPath = NativeMethods.FileManagement.GetTempPath();
            string tempFileName = NativeMethods.FileManagement.GetTempFileName(tempPath, "tfn");
            try
            {
                File.Exists(tempFileName).Should().BeTrue();
            }
            finally
            {
                NativeMethods.FileManagement.DeleteFile(tempFileName);
                File.Exists(tempFileName).Should().BeFalse();
            }
        }

        [Fact]
        public void CreateFileBasic()
        {
            string tempPath = NativeMethods.FileManagement.GetTempPath();
            string tempFileName = NativeMethods.FileManagement.GetTempFileName(tempPath, "tfn");
            try
            {
                using (var file = NativeMethods.FileManagement.CreateFile(tempFileName, FileAccess.Read, FileShare.ReadWrite, FileMode.Open))
                {
                    file.IsInvalid.Should().BeFalse();
                }
            }
            finally
            {
                NativeMethods.FileManagement.DeleteFile(tempFileName);
            }
        }

        [Fact]
        public void CreateFileCreateTempFile()
        {
            string tempPath = NativeMethods.FileManagement.GetTempPath();
            string tempFileName = Path.Combine(tempPath, Path.GetRandomFileName());
            try
            {
                using (var file = NativeMethods.FileManagement.CreateFile(tempFileName, FileAccess.Read, FileShare.ReadWrite, FileMode.Create))
                {
                    file.IsInvalid.Should().BeFalse();
                    File.Exists(tempFileName).Should().BeTrue();
                }
            }
            finally
            {
                NativeMethods.FileManagement.DeleteFile(tempFileName);
            }
        }
    }
}
