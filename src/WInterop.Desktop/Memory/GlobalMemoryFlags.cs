using System;

namespace WInterop.Memory
{
    [Flags]
    public enum GlobalMemoryFlags
    {
        /// <summary>
        /// [GMEM_FIXED]
        /// </summary>
        Fixed = 0x0000,

        /// <summary>
        /// [GMEM_MOVEABLE]
        /// </summary>
        Moveable = 0x0002,

        /// <summary>
        /// [GMEM_NOCOMPACT]
        /// </summary>
        NoCompact = 0x0010,

        /// <summary>
        /// [GMEM_NODISCARD]
        /// </summary>
        NoDiscard = 0x0020,

        /// <summary>
        /// [GMEM_ZEROINIT]
        /// </summary>
        ZeroInit = 0x0040,

        /// <summary>
        /// [GMEM_MODIFY]
        /// </summary>
        Modify = 0x0080,

        /// <summary>
        /// [GMEM_DISCARDABLE]
        /// </summary>
        Discardable = 0x0100,

        /// <summary>
        /// [GMEM_NOT_BANKED]
        /// </summary>
        NotBanked = 0x1000,

        /// <summary>
        /// [GMEM_SHARE]
        /// </summary>
        Share = 0x2000,

        /// <summary>
        /// [GMEM_DDESHARE]
        /// </summary>
        DdeShare = 0x2000,

        /// <summary>
        /// [GMEM_NOTIFY]
        /// </summary>
        Notify = 0x4000,

        /// <summary>
        /// [GMEM_LOWER]
        /// </summary>
        Lower = NotBanked,

        /// <summary>
        /// [GMEM_INVALID_HANDLE]
        /// </summary>
        InvalidHandle = 0x8000,

        /// <summary>
        /// [GHND]
        /// </summary>
        Handle = Moveable | ZeroInit,

        /// <summary>
        /// [GPTR]
        /// </summary>
        Pointer = Fixed | ZeroInit,
    }
}
