// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security;

/// <summary>
///  [TOKEN_ELEVATION]
/// </summary>
/// <remarks>
/// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/bb530717.aspx"/>
/// </remarks>
public struct TokenElevation
{
    public IntBoolean TokenIsElevated;
}