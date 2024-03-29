﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Tests.Support;

/// <summary>
///  This will lock until disposed to assist in tests that change the current directory.
///  Current directory is process wide- try to avoid changing the directory at all, or use this wrapper.
/// </summary>
public sealed class TempCurrentDirectory : IDisposable
{
    private readonly string _priorDirectory;
    private static readonly object s_tempDirectoryLock = new();

    public TempCurrentDirectory(string directory = null)
    {
        Monitor.Enter(s_tempDirectoryLock);
        _priorDirectory = Environment.CurrentDirectory;

        if (directory is not null)
        {
            Environment.CurrentDirectory = directory;
        }
    }

    public void Dispose()
    {
        if (Environment.CurrentDirectory != _priorDirectory && Directory.Exists(_priorDirectory))
        {
            Environment.CurrentDirectory = _priorDirectory;
        }

        Monitor.Exit(s_tempDirectoryLock);
    }

    public override bool Equals(object obj)
        => obj is TempCurrentDirectory directory && _priorDirectory == directory._priorDirectory;

    public override int GetHashCode() => _priorDirectory.GetHashCode();
}
