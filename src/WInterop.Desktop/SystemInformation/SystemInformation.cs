// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Errors;
using WInterop.Support.Buffers;
using WInterop.SystemInformation.Native;

namespace WInterop.SystemInformation;

public static partial class SystemInformation
{
    /// <summary>
    ///  Get the current user name.
    /// </summary>
    public static string GetUserName()
    {
        return BufferHelper.BufferInvoke((StringBuffer buffer) =>
        {
            GetUserName(buffer);
            return buffer.ToString();
        });
    }

    public static void GetUserName(StringBuffer buffer)
    {
        uint sizeInChars = buffer.CharCapacity;
        while (!SystemInformationImports.GetUserNameW(buffer, ref sizeInChars))
        {
            Error.ThrowIfLastErrorNot(WindowsError.ERROR_INSUFFICIENT_BUFFER);
            buffer.EnsureCharCapacity(sizeInChars);
        }

        // Returned size includes the null
        buffer.Length = sizeInChars - 1;
    }

    /// <summary>
    ///  Returns the suite mask for the OS which defines the "edition" of Windows.
    /// </summary>
    public static SuiteMask GetSuiteMask() => SystemInformationImports.RtlGetSuiteMask();

    /// <summary>
    ///  Gets the user name in the specified format. Returns null for formats that aren't mapped.
    /// </summary>
    public static string? GetUserName(ExtendedNameFormat format)
    {
        return BufferHelper.BufferInvoke((StringBuffer buffer) =>
        {
            uint size = buffer.CharCapacity;
            while (!SystemInformationImports.GetUserNameExW(format, buffer, ref size))
            {
                WindowsError error = Error.GetLastError();
                switch (error)
                {
                    case WindowsError.ERROR_NONE_MAPPED:
                        return null;
                    case WindowsError.ERROR_MORE_DATA:
                        buffer.EnsureCharCapacity(size);
                        break;
                    default:
                        throw error.GetException();
                }
            }

            buffer.Length = size;
            return buffer.ToString();
        });
    }

    /// <summary>
    ///  Get the computer name in the specified format.
    /// </summary>
    public static string GetComputerName(ComputerNameFormat format)
    {
        return BufferHelper.BufferInvoke((StringBuffer buffer) =>
        {
            uint size = buffer.CharCapacity;
            while (!SystemInformationImports.GetComputerNameExW(format, buffer, ref size))
            {
                Error.ThrowIfLastErrorNot(WindowsError.ERROR_MORE_DATA);
                buffer.EnsureCharCapacity(size);
            }

            buffer.Length = size;
            return buffer.ToString();
        });
    }

    /// <summary>
    ///  Expand environment variables in the given string.
    /// </summary>
    public static string ExpandEnvironmentVariables(string value)
    {
        return BufferHelper.BufferInvoke((StringBuffer buffer) =>
        {
            uint size;
            while ((size = SystemInformationImports.ExpandEnvironmentStringsW(value, buffer, buffer.CharCapacity))
                > buffer.CharCapacity)
            {
                buffer.EnsureCharCapacity(size);
            }

            if (size == 0)
                Error.ThrowLastError();

            buffer.Length = size - 1;
            return buffer.ToString();
        });
    }

    /// <summary>
    ///  Returns true if the specified processor feature is present.
    /// </summary>
    public static bool IsProcessorFeaturePresent(ProcessorFeature feature)
        => SystemInformationImports.IsProcessorFeaturePresent(feature);

    /// <summary>
    ///  Returns true if the user is currently opted in for the Customer Experience Improvement Program.
    /// </summary>
    public static bool CeipIsOptedIn() => SystemInformationImports.CeipIsOptedIn();

    /// <summary>
    ///  Returns the performance counter frequency in counts per second.
    /// </summary>
    public static long QueryPerformanceFrequency()
    {
        SystemInformationImports.QueryPerformanceFrequency(out long frequency);
        return frequency;
    }

    /// <summary>
    ///  Returns the current performance counter value in counts.
    /// </summary>
    public static long QueryPerformanceCounter()
    {
        SystemInformationImports.QueryPerformanceCounter(out long counts);
        return counts;
    }

    /// <summary>
    ///  Returns the local system time.
    /// </summary>
    public static SystemTime GetLocalTime()
    {
        SystemInformationImports.GetLocalTime(out SystemTime time);
        return time;
    }

    /// <summary>
    ///  Gets the NetBIOS computer name.
    /// </summary>
    public static string GetComputerName()
    {
        return BufferHelper.BufferInvoke((StringBuffer buffer) =>
        {
            uint size = buffer.CharCapacity;
            while (!SystemInformationImports.GetComputerNameW(buffer, ref size))
            {
                Error.ThrowIfLastErrorNot(WindowsError.ERROR_BUFFER_OVERFLOW);
                buffer.EnsureCharCapacity(size);
            }

            buffer.Length = size;
            return buffer.ToString();
        });
    }

    /// <summary>
    ///  Gets the real version info without lies.
    /// </summary>
    public static unsafe OsVersionInfo GetVersionInfo()
    {
        OsVersionInfo info = new() { OSVersionInfoSize = (uint)sizeof(OsVersionInfo) };
        SystemInformationImports.RtlGetVersion(ref info).ThrowIfFailed();
        return info;
    }
}