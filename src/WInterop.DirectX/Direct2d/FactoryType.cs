// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    /// <summary>
    ///  [D2D1_FACTORY_TYPE]
    /// </summary>
    /// <remarks>
    /// <see cref="https://docs.microsoft.com/en-us/windows/desktop/api/d2d1/ne-d2d1-d2d1_factory_type"/>
    /// </remarks>
    public enum FactoryType : uint
    {
        /// <summary>
        ///  [D2D1_FACTORY_TYPE_SINGLE_THREADED]
        /// </summary>
        SingleThreaded = D2D1_FACTORY_TYPE.D2D1_FACTORY_TYPE_SINGLE_THREADED,

        /// <summary>
        ///  [D2D1_FACTORY_TYPE_MULTI_THREADED]
        /// </summary>
        MultiThreaded = D2D1_FACTORY_TYPE.D2D1_FACTORY_TYPE_MULTI_THREADED
    }
}
