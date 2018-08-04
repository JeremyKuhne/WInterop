// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Shell.Types
{
    public enum ASSOCSTR
    {
        /// <summary>
        /// Command string associated with a Shell verb. [ASSOCSTR_COMMAND]
        /// </summary>
        Command = 1,

        /// <summary>
        /// An executable from a Shell verb command string. [ASSOCSTR_EXECUTABLE]
        /// </summary>
        /// <remarks>
        /// For example, this string is found as the (Default) value for a subkey such as
        /// HKEY_CLASSES_ROOT\ApplicationName\shell\Open\command. If the command uses Rundll.exe,
        /// set the ASSOCF_REMAPRUNDLL flag in the flags parameter of IQueryAssociations::GetString
        /// to retrieve the target executable.
        /// </remarks>
        Executable,

        /// <summary>
        /// The friendly name of a document type. [ASSOCSTR_FRIENDLYDOCNAME]
        /// </summary>
        FriendlyDocName,

        /// <summary>
        /// The friendly name of an executable file. [ASSOCSTR_FRIENDLYAPPNAME]
        /// </summary>
        FriendlyAppName,

        /// <summary>
        /// Ignore information in the open subkey. [ASSOCSTR_NOOPEN]
        /// </summary>
        NoOpen,

        /// <summary>
        /// Look under the ShellNew subkey. [ASSOCSTR_SHELLNEWVALUE]
        /// </summary>
        ShellNewValue,

        /// <summary>
        /// Template for DDE commands. [ASSOCSTR_DDECOMMAND]
        /// </summary>
        DdeCommand,

        /// <summary>
        /// DDE command used to create a process. [ASSOCSTR_DDEIFEXEC]
        /// </summary>
        DdeIfExec,

        /// <summary>
        /// Application name in a DDE broadcast. [ASSOCSTR_DDEAPPLICATION]
        /// </summary>
        DdeApplication,

        /// <summary>
        /// Topic name in a DDE broadcast. [ASSOCSTR_DDETOPIC]
        /// </summary>
        DdeTopic,

        /// <summary>
        /// InfoTip registry value. [ASSOCSTR_INFOTIP]
        /// </summary>
        /// <remarks>
        /// Returns an info tip for an item, or list of properties in the form of an
        /// IPropertyDescriptionList from which to create an info tip, such as when
        /// hovering the cursor over a file name. The list of properties can be parsed
        /// with PSGetPropertyDescriptionListFromString.
        /// </remarks>
        InfoTip,

        /// <summary>
        /// QuickTip registry value. [ASSOCSTR_QUICKTIP]
        /// </summary>
        /// <remarks>
        /// Like INFOTIP, but always is IPropertyDescriptionList.
        /// </remarks>
        QuickTip,

        /// <summary>
        /// TitleInfo registry value. [ASSOCSTR_TILEINFO]
        /// </summary>
        /// <remarks>
        /// Always IPropertyDescriptionList.
        /// </remarks>
        TitleInfo,

        /// <summary>
        /// General type of a MIME file association. [ASSOCSTR_CONTENTTYPE]
        /// </summary>
        ContentType,

        /// <summary>
        /// Path to the icon resources for the association. [ASSOCSTR_DEFAULTICON]
        /// </summary>
        DefaultIcon,

        /// <summary>
        /// Gets the CLSID of a specific shell extensions IID. [ASSOCSTR_SHELLEXTENSION]
        /// </summary>
        ShellExtension,

        /// <summary>
        /// Gets the target CLSID of IDropTarget. [ASSOCSTR_DROPTARGET]
        /// </summary>
        DropTarget,

        /// <summary>
        /// Gets the IExecuteCommand object's CLSID. [ASSOCSTR_DELEGATEEXECUTE]
        /// </summary>
        DelegateExecute,

        /// <summary>
        /// Introduced in Windows 8. [ASSOCSTR_SUPPORTED_URI_PROTOCOLS]
        /// </summary>
        SupportedUriProtocols,

        /// <summary>
        /// Returns the ProgID provided by the app associated with the file type
        /// or URI scheme. Configured by users in default program settings.
        /// [ASSOCSTR_PROGID]
        /// </summary>
        ProgId,

        /// <summary>
        /// Returns the AppUserModelId of the app associated with the file type
        /// or URI scheme. Configured by users in default program settings.
        /// [ASSOCSTR_APPID]
        /// </summary>
        AppId,

        /// <summary>
        /// Returns the publisher of the app associated with the file type
        /// or URI scheme. Configured by users in default program settings.
        /// [ASSOCSTR_APPPUBLISHER]
        /// </summary>
        AppPublisher,

        /// <summary>
        /// Returns the icon referenceof the app associated with the file type
        /// or URI scheme. Configured by users in default program settings.
        /// [ASSOCSTR_APPICONREFERENCE]
        /// </summary>
        AppIconReference
    }
}
