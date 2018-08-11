using System;
using System.Collections.Generic;
using System.Text;

namespace WInterop.Windows
{
    public enum SizeType
    {
        /// <summary>
        /// [SIZE_RESTORED]
        /// </summary>
        Restored = 0,

        /// <summary>
        /// [SIZE_MINIMIZED]
        /// </summary>
        Minimized = 1,

        /// <summary>
        /// [SIZE_MAXIMIZED]
        /// </summary>
        Maximized = 2,

        /// <summary>
        /// [SIZE_MAXSHOW]
        /// </summary>
        MaxShow = 3,

        /// <summary>
        /// [SIZE_MAXHIDE]
        /// </summary>
        MaxHide = 4
    }
}
