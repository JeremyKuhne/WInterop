// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Errors;

namespace WInterop.Com
{
    public interface IDropSource
    {
        HResult QueryContinueDrag(bool escapePressed, KeyState keyState);
        HResult GiveFeedback(DropEffect dropEffect);
    }
}
