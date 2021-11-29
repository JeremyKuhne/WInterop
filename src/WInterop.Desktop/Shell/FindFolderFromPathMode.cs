// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Shell;

/// <summary>
///  [FFFP_MODE]
/// </summary>
// https://msdn.microsoft.com/en-us/library/windows/desktop/bb762505.aspx
public enum FindFolderFromPathMode : uint
{
    ExactMatch = 0,
    NearestParentMatch = 1
}