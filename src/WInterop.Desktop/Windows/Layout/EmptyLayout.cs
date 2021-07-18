// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;

namespace WInterop.Windows
{
    public class EmptyLayout : ILayoutHandler
    {
        public void Layout(Rectangle bounds) { }
        private EmptyLayout() { }
        public static EmptyLayout Instance { get; } = new EmptyLayout();
    }
}