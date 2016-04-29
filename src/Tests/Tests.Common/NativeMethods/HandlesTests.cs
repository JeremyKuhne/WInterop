// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System.IO;
using System.Linq;
using WInterop.Utility;
using Xunit;

namespace WInterop.Tests.NativeMethodsTests
{
    public class HandlesTests
    {
        [Fact]
        public void GetHandleTypeBasic()
        {
            string tempPath = NativeMethods.FileManagement.GetTempPath();
            using (var directory = NativeMethods.FileManagement.CreateFile(tempPath, FileAccess.Read, FileShare.ReadWrite, FileMode.Open,
                FileManagement.FileAttributes.NONE, FileManagement.FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
            {
                string name = NativeMethods.Handles.GetObjectTypeName(directory.DangerousGetHandle());
                name.Should().Be("File");
            }
        }

        [Fact]
        public void GetHandleNameBasic()
        {
            string tempPath = NativeMethods.FileManagement.GetTempPath();
            using (var directory = NativeMethods.FileManagement.CreateFile(tempPath, FileAccess.Read, FileShare.ReadWrite, FileMode.Open,
                FileManagement.FileAttributes.NONE, FileManagement.FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
            {
                // This will give back the NT path (\Device\HarddiskVolumen...)
                string name = NativeMethods.Handles.GetObjectName(directory.DangerousGetHandle());

                // Skip past the C:\
                Paths.AddTrailingSeparator(name).Should().EndWith(tempPath.Substring(3));
            }
        }
    }
}
