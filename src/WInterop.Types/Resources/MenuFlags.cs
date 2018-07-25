using System;

namespace WInterop.Resources
{
    [Flags]
    public enum MenuFlags : uint
    {

        /// <summary>
        /// [MF_INSERT]
        /// </summary>
        Insert = 0x00000000,

        /// <summary>
        /// [MF_CHANGE]
        /// </summary>
        Change = 0x00000080,

        /// <summary>
        /// [MF_APPEND]
        /// </summary>
        Append = 0x00000100,

        /// <summary>
        /// [MF_DELETE]
        /// </summary>
        Delete = 0x00000200,

        /// <summary>
        /// [MF_REMOVE]
        /// </summary>
        Remove = 0x00001000,

        /// <summary>
        /// The specified ID is the menu ID. [MF_BYCOMMAND]
        /// IDs when creating menu items are always the ID.
        /// </summary>
        ByCommand = 0x00000000,

        /// <summary>
        /// The specified ID is the 0 based relative index. [MF_BYPOSITION]
        /// </summary>
        ByPosition = 0x00000400,

        /// <summary>
        /// Draws a horizontal dividing line menu. [MF_SEPARATOR]
        /// </summary>
        Separator = 0x00000800,

        /// <summary>
        /// Enabled and not grayed (default). [MF_ENABLED]
        /// </summary>
        Enabled = 0x00000000,

        /// <summary>
        /// Grayed and disabled. [MF_GRAYED]
        /// </summary>
        Grayed = 0x00000001,

        /// <summary>
        /// Disabled, but not grayed. [MF_DISABLED]
        /// </summary>
        Disabled = 0x00000002,

        /// <summary>
        /// Not checked (default). [MF_UNCHECKED]
        /// </summary>
        Unchecked = 0x00000000,

        /// <summary>
        /// Checked. [MF_CHECKED]
        /// </summary>
        Checked = 0x00000008,

        /// <summary>
        /// [MF_USECHECKBITMAPS]
        /// </summary>
        UseCheckBitmaps = 0x00000200,

        /// <summary>
        /// Menu content is a string. [MF_STRING]
        /// </summary>
        String = 0x00000000,

        /// <summary>
        /// Menu content is a bitmap. [MF_BITMAP]
        /// </summary>
        Bitmap = 0x00000004,

        /// <summary>
        /// Owner drawn. [MF_OWNERDRAW]
        /// </summary>
        OwnerDraw = 0x00000100,

        /// <summary>
        /// Menu opens a drop-down or submenu. [MF_POPUP]
        /// </summary>
        Popup = 0x00000010,

        /// <summary>
        /// Puts the menu on a new line (menu bar) or new column with column
        /// separator. [MF_MENUBARBREAK]
        /// </summary>
        MenuBarBreak = 0x00000020,

        /// <summary>
        /// Puts the menu on a new line (menu bar) or new column without column
        /// separator. [MF_MENUBREAK]
        /// </summary>
        MenuBreak = 0x00000040,

        /// <summary>
        /// [MF_UNHILITE]
        /// </summary>
        Unhilite = 0x00000000,

        /// <summary>
        /// [MF_HILITE]
        /// </summary>
        Hilite = 0x00000080,

        /// <summary>
        /// [MF_DEFAULT]
        /// </summary>
        Default = 0x00001000,

        /// <summary>
        /// [MF_SYSMENU]
        /// </summary>
        SystemMenu = 0x00002000,

        /// <summary>
        /// [MF_HELP]
        /// </summary>
        Help = 0x00004000,

        /// <summary>
        /// [MF_RIGHTJUSTIFY]
        /// </summary>
        RightJustify = 0x00004000,

        /// <summary>
        /// [MF_MOUSESELECT]
        /// </summary>
        MouseSelect = 0x00008000
    }
}
