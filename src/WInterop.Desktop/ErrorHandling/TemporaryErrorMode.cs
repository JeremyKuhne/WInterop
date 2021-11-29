// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Errors;

/// <summary>
///  Simple helper class to temporarily enable thread error flag modes if necessary.
/// </summary>
public struct TemporaryErrorMode : IDisposable
{
    private bool _restoreOldMode;
    private readonly ErrorMode _oldMode;

    private TemporaryErrorMode(ErrorMode modesToEnable)
    {
        _oldMode = Error.GetThreadErrorMode();
        if ((_oldMode & modesToEnable) != modesToEnable)
        {
            _oldMode = Error.SetThreadErrorMode(_oldMode | modesToEnable);
            _restoreOldMode = true;
        }
        else
        {
            _restoreOldMode = false;
        }
    }

    /// <summary>
    ///  Set the given error mode flags if needed. Use in using statement to clear flags when done.
    /// </summary>
    public static IDisposable EnableMode(ErrorMode modesToEnable)
    {
        return new TemporaryErrorMode(modesToEnable);
    }

    public void Dispose()
    {
        if (_restoreOldMode)
        {
            _restoreOldMode = false;
            Error.SetThreadErrorMode(_oldMode);
        }
    }
}