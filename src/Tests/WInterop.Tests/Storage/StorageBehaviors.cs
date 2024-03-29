﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using TerraFX.Interop.Windows;
using Tests.Support;
using WInterop;
using WInterop.Errors;
using WInterop.Storage;
using WInterop.Support;

namespace StorageTests;

public partial class FileManagementBehaviors
{
    [Theory,
        // Basic dot space handling
        InlineData(@"C:\", @"C:\"),
        InlineData(@"C:\ ", @"C:\"),
        InlineData(@"C:\.", @"C:\"),
        InlineData(@"C:\..", @"C:\"),
        InlineData(@"C:\...", @"C:\"),
        InlineData(@"C:\ .", @"C:\"),
        InlineData(@"C:\ ..", @"C:\"),
        InlineData(@"C:\ ...", @"C:\"),
        InlineData(@"C:\. ", @"C:\"),
        InlineData(@"C:\.. ", @"C:\"),
        InlineData(@"C:\... ", @"C:\"),
        InlineData(@"C:\.\", @"C:\"),
        InlineData(@"C:\..\", @"C:\"),
        InlineData(@"C:\...\", @"C:\...\"),
        InlineData(@"C:\ \", @"C:\ \"),
        InlineData(@"C:\ .\", @"C:\ \"),
        InlineData(@"C:\ ..\", @"C:\ ..\"),
        InlineData(@"C:\ ...\", @"C:\ ...\"),
        InlineData(@"C:\. \", @"C:\. \"),
        InlineData(@"C:\.. \", @"C:\.. \"),
        InlineData(@"C:\... \", @"C:\... \"),
        InlineData(@"C:\A \", @"C:\A \"),
        InlineData(@"C:\A \B", @"C:\A \B"),

        // Same as above with prefix
        InlineData(@"\\?\C:\", @"\\?\C:\"),
        InlineData(@"\\?\C:\ ", @"\\?\C:\"),
        InlineData(@"\\?\C:\.", @"\\?\C:"),           // Changes behavior, without \\?\, returns C:\
        InlineData(@"\\?\C:\..", @"\\?\"),            // Changes behavior, without \\?\, returns C:\
        InlineData(@"\\?\C:\...", @"\\?\C:\"),
        InlineData(@"\\?\C:\ .", @"\\?\C:\"),
        InlineData(@"\\?\C:\ ..", @"\\?\C:\"),
        InlineData(@"\\?\C:\ ...", @"\\?\C:\"),
        InlineData(@"\\?\C:\. ", @"\\?\C:\"),
        InlineData(@"\\?\C:\.. ", @"\\?\C:\"),
        InlineData(@"\\?\C:\... ", @"\\?\C:\"),
        InlineData(@"\\?\C:\.\", @"\\?\C:\"),
        InlineData(@"\\?\C:\..\", @"\\?\"),           // Changes behavior, without \\?\, returns C:\
        InlineData(@"\\?\C:\...\", @"\\?\C:\...\"),

        // How deep can we go with prefix
        InlineData(@"\\?\C:\..\..", @"\\?\"),
        InlineData(@"\\?\C:\..\..\..", @"\\?\"),

        // Pipe tests
        InlineData(@"\\.\pipe", @"\\.\pipe"),
        InlineData(@"\\.\pipe\", @"\\.\pipe\"),
        InlineData(@"\\?\pipe", @"\\?\pipe"),
        InlineData(@"\\?\pipe\", @"\\?\pipe\"),

        // Basic dot space handling with UNCs
        InlineData(@"\\Server\Share\", @"\\Server\Share\"),
        InlineData(@"\\Server\Share\ ", @"\\Server\Share\"),
        InlineData(@"\\Server\Share\.", @"\\Server\Share"),                      // UNCs can eat trailing separator
        InlineData(@"\\Server\Share\..", @"\\Server\Share"),                     // UNCs can eat trailing separator
        InlineData(@"\\Server\Share\..\foo", @"\\Server\Share\foo"),
        InlineData(@"\\Server\Share\...", @"\\Server\Share\"),
        InlineData(@"\\Server\Share\ .", @"\\Server\Share\"),
        InlineData(@"\\Server\Share\ ..", @"\\Server\Share\"),
        InlineData(@"\\Server\Share\ ...", @"\\Server\Share\"),
        InlineData(@"\\Server\Share\. ", @"\\Server\Share\"),
        InlineData(@"\\Server\Share\.. ", @"\\Server\Share\"),
        InlineData(@"\\Server\Share\... ", @"\\Server\Share\"),
        InlineData(@"\\Server\Share\.\", @"\\Server\Share\"),
        InlineData(@"\\Server\Share\..\", @"\\Server\Share\"),
        InlineData(@"\\Server\Share\...\", @"\\Server\Share\...\"),

        // Slash direction makes no difference
        InlineData(@"//Server\Share\", @"\\Server\Share\"),
        InlineData(@"//Server\Share\ ", @"\\Server\Share\"),
        InlineData(@"//Server\Share\.", @"\\Server\Share"),                      // UNCs can eat trailing separator
        InlineData(@"//Server\Share\..", @"\\Server\Share"),                     // UNCs can eat trailing separator
        InlineData(@"//Server\Share\...", @"\\Server\Share\"),
        InlineData(@"//Server\Share\ .", @"\\Server\Share\"),
        InlineData(@"//Server\Share\ ..", @"\\Server\Share\"),
        InlineData(@"//Server\Share\ ...", @"\\Server\Share\"),
        InlineData(@"//Server\Share\. ", @"\\Server\Share\"),
        InlineData(@"//Server\Share\.. ", @"\\Server\Share\"),
        InlineData(@"//Server\Share\... ", @"\\Server\Share\"),
        InlineData(@"//Server\Share\.\", @"\\Server\Share\"),
        InlineData(@"//Server\Share\..\", @"\\Server\Share\"),
        InlineData(@"//Server\Share\...\", @"\\Server\Share\...\"),

        // Slash count breaks rooting
        InlineData(@"\\\Server\Share\", @"\\\Server\Share\"),
        InlineData(@"\\\Server\Share\ ", @"\\\Server\Share\"),
        InlineData(@"\\\Server\Share\.", @"\\\Server\Share"),                     // UNCs can eat trailing separator
        InlineData(@"\\\Server\Share\..", @"\\\Server"),                          // Paths without 2 initial slashes will not root the share
        InlineData(@"\\\Server\Share\...", @"\\\Server\Share\"),
        InlineData(@"\\\Server\Share\ .", @"\\\Server\Share\"),
        InlineData(@"\\\Server\Share\ ..", @"\\\Server\Share\"),
        InlineData(@"\\\Server\Share\ ...", @"\\\Server\Share\"),
        InlineData(@"\\\Server\Share\. ", @"\\\Server\Share\"),
        InlineData(@"\\\Server\Share\.. ", @"\\\Server\Share\"),
        InlineData(@"\\\Server\Share\... ", @"\\\Server\Share\"),
        InlineData(@"\\\Server\Share\.\", @"\\\Server\Share\"),
        InlineData(@"\\\Server\Share\..\", @"\\\Server\"),                       // Paths without 2 initial slashes will not root the share
        InlineData(@"\\\Server\Share\...\", @"\\\Server\Share\...\"),

        // Inital slash count is always kept
        InlineData(@"\\\\Server\Share\", @"\\\Server\Share\"),
        InlineData(@"\\\\\Server\Share\", @"\\\Server\Share\"),

        // Extended paths root to \\?\
        InlineData(@"\\?\UNC\Server\Share\", @"\\?\UNC\Server\Share\"),
        InlineData(@"\\?\UNC\Server\Share\ ", @"\\?\UNC\Server\Share\"),
        InlineData(@"\\?\UNC\Server\Share\.", @"\\?\UNC\Server\Share"),
        InlineData(@"\\?\UNC\Server\Share\..", @"\\?\UNC\Server"),               // Extended UNCs can eat into Server\Share
        InlineData(@"\\?\UNC\Server\Share\...", @"\\?\UNC\Server\Share\"),
        InlineData(@"\\?\UNC\Server\Share\ .", @"\\?\UNC\Server\Share\"),
        InlineData(@"\\?\UNC\Server\Share\ ..", @"\\?\UNC\Server\Share\"),
        InlineData(@"\\?\UNC\Server\Share\ ...", @"\\?\UNC\Server\Share\"),
        InlineData(@"\\?\UNC\Server\Share\. ", @"\\?\UNC\Server\Share\"),
        InlineData(@"\\?\UNC\Server\Share\.. ", @"\\?\UNC\Server\Share\"),
        InlineData(@"\\?\UNC\Server\Share\... ", @"\\?\UNC\Server\Share\"),
        InlineData(@"\\?\UNC\Server\Share\.\", @"\\?\UNC\Server\Share\"),
        InlineData(@"\\?\UNC\Server\Share\..\", @"\\?\UNC\Server\"),             // Extended UNCs can eat into Server\Share
        InlineData(@"\\?\UNC\Server\Share\...\", @"\\?\UNC\Server\Share\...\"),

        // How deep can we go with prefix
        InlineData(@"\\?\UNC\Server\Share\..\..", @"\\?\UNC"),
        InlineData(@"\\?\UNC\Server\Share\..\..\..", @"\\?\"),
        InlineData(@"\\?\UNC\Server\Share\..\..\..\..", @"\\?\"),

        // Root slash behavior
        InlineData(@"C:/", @"C:\"),
        InlineData(@"C:/..", @"C:\"),
        InlineData(@"//Server/Share", @"\\Server\Share"),
        InlineData(@"//Server/Share/..", @"\\Server\Share"),
        InlineData(@"//Server//Share", @"\\Server\Share"),
        InlineData(@"//Server//Share/..", @"\\Server\"),                         // Double slash shares normalize but don't root correctly
        InlineData(@"//Server\\Share/..", @"\\Server\"),
        InlineData(@"//?/", @"\\?\"),
        // InlineData(@"\??\", @"D:\??\")                                       // \??\ will return the current directory's drive if passed to GetFullPathName
        // InlineData(@"/??/", @"D:\??\")
        InlineData(@"//./", @"\\.\"),

        // Legacy device behavior
        InlineData(@"CON", @"\\.\CON"),
        // InlineData(@"CON:Alt", @"\\.\CON"),              // Changed recently to CWD\CON:Alt
        InlineData(@"LPT9", @"\\.\LPT9"),
        // InlineData(@"prn.json", @"\\.\prn"),             // Changed recently to CWD\prn.json
        // InlineData(@"C:\foo\prn.json", @"\\.\prn"),      // Changed recently to C:\foo\prn.json
        // InlineData(@"C:\CON", @"\\.\CON"),               // Changed recently to C:\CON
        InlineData(@"\\.\C:\CON", @"\\.\C:\CON"),

        InlineData(@"C:\A\B\.\..\C", @"C:\A\C")
        ]
    public void ValidateKnownFixedBehaviors(string value, string expected)
    {
        Storage.GetFullPathName(value).Should().Be(expected, $"source was {value}");
    }

    [Fact]
    public unsafe void FindFirstFileBehaviors()
    {
        using (var cleaner = new TestFileCleaner())
        {
            HANDLE handle;
            WIN32_FIND_DATAW findData;
            fixed (char* p = cleaner.TempFolder)
            {
                handle = TerraFXWindows.FindFirstFileW((ushort*)p, &findData);
            }

            try
            {
                IsValid(handle).Should().BeTrue("root location exists");
            }
            finally
            {
                if (IsValid(handle))
                {
                    TerraFXWindows.FindClose(handle);
                }
            }

            fixed (char* p = cleaner.GetTestPath())
            {
                handle = TerraFXWindows.FindFirstFileW((ushort*)p, &findData);
            }
            WindowsError error = Error.GetLastError();

            try
            {
                IsValid(handle).Should().BeFalse("non-existant file");
                error.Should().Be(WindowsError.ERROR_FILE_NOT_FOUND);
            }
            finally
            {
                if (IsValid(handle))
                {
                    TerraFXWindows.FindClose(handle);
                }
            }

            fixed (char* p = Path.Join(cleaner.GetTestPath(), "NotHere"))
            handle = TerraFXWindows.FindFirstFileW((ushort*)p, &findData);
            error = Error.GetLastError();

            try
            {
                IsValid(handle).Should().BeFalse("non-existant subdir");
                error.Should().Be(WindowsError.ERROR_PATH_NOT_FOUND);
            }
            finally
            {
                if (IsValid(handle))
                {
                    TerraFXWindows.FindClose(handle);
                }
            }
        }

        static bool IsValid(HANDLE handle)
        {
            return handle != HANDLE.NULL && handle != HANDLE.INVALID_VALUE;
        }
    }

    [Fact]
    public unsafe void GetFileAttributesBehavior_Basic()
    {
        using var cleaner = new TestFileCleaner();

        WIN32_FILE_ATTRIBUTE_DATA attributeData;
        fixed (char* p = cleaner.TempFolder)
        {

            bool success = TerraFXWindows.GetFileAttributesExW(
                (ushort*)p,
                GET_FILEEX_INFO_LEVELS.GetFileExInfoStandard,
                &attributeData);
            success.Should().BeTrue("root location exists");
        }

        fixed (char* p = cleaner.GetTestPath())
        {
            bool success = TerraFXWindows.GetFileAttributesExW(
                (ushort*)p,
                GET_FILEEX_INFO_LEVELS.GetFileExInfoStandard,
                &attributeData);
            WindowsError error = Error.GetLastError();
            success.Should().BeFalse("non-existant file");
            error.Should().Be(WindowsError.ERROR_FILE_NOT_FOUND);
        }

        fixed (char* p = Path.Join(cleaner.GetTestPath(), "NotHere"))
        {
            bool success = TerraFXWindows.GetFileAttributesExW(
                (ushort*)p,
                GET_FILEEX_INFO_LEVELS.GetFileExInfoStandard,
                &attributeData);

            WindowsError error = Error.GetLastError();
            success.Should().BeFalse("non-existant subdir");
            error.Should().Be(WindowsError.ERROR_PATH_NOT_FOUND);
        }
    }

    [Fact]
    public unsafe void GetFileAttributesBehavior_BadCharactersOnNonExistantPath()
    {
        string tempPath = Path.GetTempPath();
        Win32FileAttributeData attributeData = default;

        bool success;
        fixed (char* p = tempPath)
        {
            success = TerraFXWindows.GetFileAttributesExW(
                (ushort*)p,
                GET_FILEEX_INFO_LEVELS.GetFileExInfoStandard,
                &attributeData);
        }

        success.Should().BeTrue("can get temp folder attributes");

        // Try with a bad, non-existent subdir name
        fixed (char* p = Path.Join(tempPath, @"""*"""))
        {
            success = TerraFXWindows.GetFileAttributesExW(
                (ushort*)p,
                GET_FILEEX_INFO_LEVELS.GetFileExInfoStandard,
                &attributeData);
        }

        WindowsError error = Error.GetLastError();
        success.Should().BeFalse("non-existant subdir");
        error.Should().Be(WindowsError.ERROR_INVALID_NAME);

        // Try with a nested nonexistant subdir, with a bad subdir name
        fixed (char* p = Path.Join(tempPath, Path.GetRandomFileName(), @"""*"""))
        {
            success = TerraFXWindows.GetFileAttributesExW(
                (ushort*)p,
                GET_FILEEX_INFO_LEVELS.GetFileExInfoStandard,
                &attributeData);
        }

        error = Error.GetLastError();
        success.Should().BeFalse("non-existant subdir");
        error.Should().Be(WindowsError.ERROR_PATH_NOT_FOUND);
    }

    [Fact(Skip = "Failing on RS5")]
    public unsafe void GetFileAttributesBehavior_DeletedFile()
    {
        using var cleaner = new TestFileCleaner();
        string path = cleaner.CreateTestFile(nameof(GetFileAttributesBehavior_DeletedFile));
        using var handle = Storage.CreateFile(path, CreationDisposition.OpenExisting, shareMode: ShareModes.All);
        handle.IsInvalid.Should().BeFalse();
        Storage.FileExists(path).Should().BeTrue();
        Storage.DeleteFile(path);

        // With the file deleted and the handle still open the file will still physically exist.
        // Trying to access the file via a handle at this point will fail with access denied.

        Action action = () => Storage.FileExists(path);
        action.Should().Throw<UnauthorizedAccessException>();

        action = () => Storage.CreateFile(path, CreationDisposition.OpenExisting, shareMode: ShareModes.All,
            desiredAccess: DesiredAccess.ReadAttributes);
        action.Should().Throw<UnauthorizedAccessException>();

        Win32FindData findData;
        HANDLE findHandle;
        fixed (char* p = path)
        {
            // Find file will work at this point.
            findHandle = TerraFXWindows.FindFirstFileW((ushort*)p, (WIN32_FIND_DATAW*)&findData);
            findHandle.Should().NotBe(HANDLE.NULL);
        }

        try
        {
            // This is failing with a corrupted name
            findData.FileName.CreateString().Should().Be(Paths.GetLastSegment(path));
        }
        finally
        {
            TerraFXWindows.FindClose(findHandle);
        }
    }

    [Fact]
    public void GetFullPathNameLongPathBehaviors()
    {
        // ERROR_FILENAME_EXCED_RANGE (206)
        // GetFullPathName will fail if the passed in patch is longer than short.MaxValue - 2, even if the path will normalize below that value.
        // Storage.GetFullPathName(PathGenerator.CreatePathOfLength(@"C:\..\..\..\..", short.MaxValue - 2));

        // ERROR_INVALID_NAME (123)
        // GetFullPathName will fail if the passed in path normalizes over short.MaxValue - 2
        // Storage.GetFullPathName(new string('a', short.MaxValue - 2));

        Storage.GetFullPathName(PathGenerator.CreatePathOfLength(Storage.GetTempPath(), short.MaxValue - 2));

        // Works
        // NativeMethods.File.GetFullPathName(PathGenerator.CreatePathOfLength(@"C:\", short.MaxValue - 2));
    }

    [Fact(Skip = "Needs updated for Windows 10 1903, which no longer holds names until handles are closed.")]
    public void LockedFileDirectoryDeletion()
    {
        using var cleaner = new TestFileCleaner();
        string directory = cleaner.GetTestPath();
        Storage.CreateDirectory(directory);
        Storage.DirectoryExists(directory).Should().BeTrue();
        string file = cleaner.CreateTestFile(nameof(LockedFileDirectoryDeletion), directory);
        using (var handle = Storage.CreateFile(file, CreationDisposition.OpenExisting, DesiredAccess.GenericRead, ShareModes.ReadWrite | ShareModes.Delete))
        {
            handle.IsInvalid.Should().BeFalse();

            // Mark the file for deletion
            Storage.DeleteFile(file);

            // RemoveDirectory API call will throw
            Action action = () => Storage.RemoveDirectory(directory);
            action.Should().Throw<WInteropIOException>().And.HResult.Should().Be((int)WindowsError.ERROR_DIR_NOT_EMPTY.ToHResult());

            // Opening the directory for deletion will succeed, but have no impact
            using var directoryHandle = Storage.CreateFile(
                directory,
                CreationDisposition.OpenExisting,
                DesiredAccess.ListDirectory | DesiredAccess.Delete,
                ShareModes.ReadWrite | ShareModes.Delete,
                AllFileAttributes.None,
                FileFlags.BackupSemantics | FileFlags.DeleteOnClose);
            directoryHandle.IsInvalid.Should().BeFalse();
        }

        // File will be gone now that the handle is closed
        Storage.FileExists(file).Should().BeFalse();

        // But the directory will still exist as it doesn't respect DeleteOnClose with an open handle when it is closed
        Storage.DirectoryExists(directory).Should().BeTrue();

        // Create a handle to the directory again with DeleteOnClose and it will actually delete the directory
        using (var directoryHandle = Storage.CreateFile(
            directory,
            CreationDisposition.OpenExisting,
            DesiredAccess.ListDirectory | DesiredAccess.Delete,
            ShareModes.ReadWrite | ShareModes.Delete,
            AllFileAttributes.None,
            FileFlags.BackupSemantics | FileFlags.DeleteOnClose))
        {
            directoryHandle.IsInvalid.Should().BeFalse();
        }
        Storage.DirectoryExists(directory).Should().BeFalse();
    }


    [Fact]
    public void LockedFileDirectoryDeletion2()
    {
        using var cleaner = new TestFileCleaner();
        string directory = cleaner.GetTestPath();
        Storage.CreateDirectory(directory);
        Storage.DirectoryExists(directory).Should().BeTrue();
        string file = cleaner.CreateTestFile(nameof(LockedFileDirectoryDeletion2), directory);

        SafeFileHandle directoryHandle = null;
        using (var handle = Storage.CreateFile(file, CreationDisposition.OpenExisting, DesiredAccess.GenericRead, ShareModes.ReadWrite | ShareModes.Delete))
        {
            handle.IsInvalid.Should().BeFalse();

            // Mark the file for deletion
            Storage.DeleteFile(file);

            // Open the directory handle
            directoryHandle = Storage.CreateFile(
                directory,
                CreationDisposition.OpenExisting,
                DesiredAccess.ListDirectory | DesiredAccess.Delete,
                ShareModes.ReadWrite | ShareModes.Delete,
                AllFileAttributes.None,
                FileFlags.BackupSemantics | FileFlags.DeleteOnClose);
        }

        try
        {
            // File will be gone now that the handle is closed
            Storage.FileExists(file).Should().BeFalse();

            directoryHandle.Close();

            // The directory will not exist as the open handle was closed before it was closed
            Storage.DirectoryExists(directory).Should().BeFalse();

        }
        finally
        {
            directoryHandle?.Close();
        }
    }
}
