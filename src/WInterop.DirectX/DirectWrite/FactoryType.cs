// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.DirectWrite;

/// <summary>
///  Specifies the type of DirectWrite factory object. [DWRITE_FACTORY_TYPE]
/// </summary>
public enum FactoryType : uint
{
    /// <summary>
    ///  Shared factory allow for re-use of cached font data across multiple in process components.
    ///  Such factories also take advantage of cross process font caching components for better performance.
    ///  [DWRITE_FACTORY_TYPE_SHARED]
    /// </summary>
    Shared,

    /// <summary>
    ///  Objects created from the isolated factory do not interact with internal DirectWrite state from other
    ///  components. [DWRITE_FACTORY_TYPE_ISOLATED]
    /// </summary>
    Isolated
}
