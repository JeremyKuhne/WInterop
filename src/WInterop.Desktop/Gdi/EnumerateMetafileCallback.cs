// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi
{
    public delegate bool EnumerateMetafileCallback(ref MetafileRecord record, nint callbackParameter);
}