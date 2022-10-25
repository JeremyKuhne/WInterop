// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows;

/// <summary>
///  Reserved IDs for system objects.
/// </summary>
public enum ObjectIdentifier : int
{
    // https://msdn.microsoft.com/library/windows/desktop/dd373606.aspx

    /// <summary>
    ///  The window itself rather than a child object. [OBJID_WINDOW]
    /// </summary>
    Window = unchecked((int)0x00000000),

    /// <summary>
    ///  System menu. [OBJID_SYSMENU]
    /// </summary>
    SystemMenu = unchecked((int)0xFFFFFFFF),

    /// <summary>
    ///  Title bar. [OBJID_TITLEBAR]
    /// </summary>
    TitleBar = unchecked((int)0xFFFFFFFE),

    /// <summary>
    ///  Menu bar. [OBJID_MENU]
    /// </summary>
    Menu = unchecked((int)0xFFFFFFFD),

    /// <summary>
    ///  The window's client area. In most cases, the operating system controls
    ///  the frame elements and the client object contains all elements that are
    ///  controlled by the application. Servers only process the WM_GETOBJECT
    ///  messages in which the lParam is OBJID_CLIENT, OBJID_WINDOW, or a custom
    ///  object identifier. [OBJID_CLIENT]
    /// </summary>
    Client = unchecked((int)0xFFFFFFFC),

    /// <summary>
    ///  The window's vertical scroll bar. [OBJID_VSCROLL]
    /// </summary>
    VScroll = unchecked((int)0xFFFFFFFB),

    /// <summary>
    ///  The window's horizontal scroll bar. [OBJID_HSCROLL]
    /// </summary>
    HScroll = unchecked((int)0xFFFFFFFA),

    /// <summary>
    ///  The window's size grip: an optional frame component located at the lower-right
    ///  corner of the window frame. [OBJID_SIZEGRIP]
    /// </summary>
    SizeGrip = unchecked((int)0xFFFFFFF9),

    /// <summary>
    ///  The text insertion bar (caret) in the window. [OBJID_CARET]
    /// </summary>
    Caret = unchecked((int)0xFFFFFFF8),

    /// <summary>
    ///  The mouse pointer. There is only one mouse pointer in the system, and it is not
    ///  a child of any window. [OBJID_CURSOR]
    /// </summary>
    Cursor = unchecked((int)0xFFFFFFF7),

    /// <summary>
    ///  An alert that is associated with a window or an application. System provided
    ///  message boxes are the only UI elements that send events with this object identifier.
    ///  Server applications cannot use the AccessibleObjectFromX functions with this
    ///  object identifier.  This is a known issue with Microsoft Active Accessibility.
    ///  [OBJID_ALERT]
    /// </summary>
    Alert = unchecked((int)0xFFFFFFF6),

    /// <summary>
    ///  A sound object. Sound objects do not have screen locations or children, but they
    ///  do have name and state attributes. They are children of the application that
    ///  is playing the sound. [OBJID_SOUND]
    /// </summary>
    Sound = unchecked((int)0xFFFFFFF5),

    /// <summary>
    ///  An object identifier that Oleacc.dll uses internally. For more information,
    ///  see Appendix F: https://learn.microsoft.com/windows/win32/winauto/appendix-f--object-identifier-values-for-objid-queryclassnameidx.
    ///  [OBJID_QUERYCLASSNAMEIDX]
    /// </summary>
    QueryClassNameIdx = unchecked((int)0xFFFFFFF4),

    /// <summary>
    ///  In response to this object identifier, third-party applications can expose their
    ///  own object model. Third-party applications can return any COM interface in response
    ///  to this object identifier. [OBJID_NATIVEOM]
    /// </summary>
    NativeOM = unchecked((int)0xFFFFFFF0),
}