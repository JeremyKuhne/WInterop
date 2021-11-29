// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Errors;
using WInterop.Support;
using WInterop.Windows.Native;

namespace WInterop.Windows;

public static partial class Windows
{
    public static unsafe string GetCurrentThemeName()
    {
        Span<char> themeName = stackalloc char[Paths.MaxPath];
        fixed (char* theme = themeName)
        {
            WindowsImports.GetCurrentThemeName(theme, Paths.MaxPath, null, 0, null, 0).ThrowIfFailed();
        }

        return themeName.SliceAtNull().ToString();
    }

    public static bool IsAppThemed() => WindowsImports.IsAppThemed();

    public static bool IsCompositionActive() => WindowsImports.IsCompositionActive();

    public static bool IsThemeActive() => WindowsImports.IsThemeActive();
}