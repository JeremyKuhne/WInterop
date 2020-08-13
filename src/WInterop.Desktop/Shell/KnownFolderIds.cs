// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Shell
{
    /// <summary>
    ///  [KNOWNFOLDERID]
    /// </summary>
    // https://msdn.microsoft.com/en-us/library/windows/desktop/dd378457.aspx
    public static class KnownFolderIds
    {
        /// <summary>
        ///  Account Pictures "%APPDATA%\Microsoft\Windows\AccountPictures"
        /// </summary>
        /// <remarks>
        ///  Windows 8 and higher
        /// </remarks>
        public static readonly Guid AccountPictures = new Guid("{008ca0b1-55b4-4c56-b8a8-4de4b299d3be}");

        /// <summary>
        ///  Virtual folder for Add New Programs / Get Programs
        /// </summary>
        public static readonly Guid AddNewPrograms = new Guid("{de61d971-5ebc-4f02-a3a9-6c82895e5c04}");

        /// <summary>
        ///  Per user Administrative Tools "%APPDATA%\Microsoft\Windows\Start Menu\Programs\Administrative Tools"
        /// </summary>
        public static readonly Guid AdminTools = new Guid("{724EF170-A42D-4FEF-9F26-B60E846FBA4F}");

        /// <summary>
        ///  Application Shortcuts "%LOCALAPPDATA%\Microsoft\Windows\Application Shortcuts"
        /// </summary>
        /// <remarks>
        ///  Windows 8 and higher
        /// </remarks>
        public static readonly Guid ApplicationShortcuts = new Guid("{A3918781-E5F2-4890-B3D9-A7E54332328C}");

        /// <summary>
        ///  Applications virtual folder
        /// </summary>
        /// <remarks>
        ///  Windows 8 and higher
        /// </remarks>
        public static readonly Guid AppsFolder = new Guid("{1e87508d-89c2-42f0-8a7e-645a0f50ca58}");

        /// <summary>
        ///  Application Updates virtual folder
        /// </summary>
        public static readonly Guid AppUpdates = new Guid("{a305ce99-f527-492b-8b1a-7e76fa98d6e4}");

        /// <summary>
        ///  Camera Roll folder
        /// </summary>
        /// <remarks>
        ///  Windows 8 and higher
        /// </remarks>
        public static readonly Guid CameraRoll = new Guid("{AB5FB87B-7CE2-4F83-915D-550846C9537B}");

        /// <summary>
        ///  Temporary Burn folder "%LOCALAPPDATA%\Microsoft\Windows\Burn\Burn"
        /// </summary>
        public static readonly Guid CDBurning = new Guid("{9E52AB10-F80D-49DF-ACB8-4330F5687855}");

        /// <summary>
        ///  Programs and Features (Add or Remove Programs) virtual folder
        /// </summary>
        public static readonly Guid ChangeRemovePrograms = new Guid("{df7266ac-9274-4867-8d55-3bd661de872d}");

        /// <summary>
        ///  Common Administrative Tools "%ALLUSERSPROFILE%\Microsoft\Windows\Start Menu\Programs\Administrative Tools"
        /// </summary>
        public static readonly Guid CommonAdminTools = new Guid("{D0384E7D-BAC3-4797-8F14-CBA229B392B5}");

        /// <summary>
        ///  OEM Links folder "%ALLUSERSPROFILE%\OEM Links"
        /// </summary>
        public static readonly Guid CommonOEMLinks = new Guid("{C1BAE2D0-10DF-4334-BEDD-7AA20B227A9D}");

        /// <summary>
        ///  Common Programs folder "%ALLUSERSPROFILE%\Microsoft\Windows\Start Menu\Programs"
        /// </summary>
        public static readonly Guid CommonPrograms = new Guid("{0139D44E-6AFE-49F2-8690-3DAFCAE6FFB8}");

        /// <summary>
        ///  Common Start Menu folder "%ALLUSERSPROFILE%\Microsoft\Windows\Start Menu"
        /// </summary>
        public static readonly Guid CommonStartMenu = new Guid("{A4115719-D62E-491D-AA7C-E74B8BE3B067}");

        /// <summary>
        ///  Common Startup folder "%ALLUSERSPROFILE%\Microsoft\Windows\Start Menu\Programs\StartUp"
        /// </summary>
        public static readonly Guid CommonStartup = new Guid("{82A5EA35-D9CD-47C5-9629-E15D2F714E6E}");

        /// <summary>
        ///  Common Templates folder "%ALLUSERSPROFILE%\Microsoft\Windows\Templates"
        /// </summary>
        public static readonly Guid CommonTemplates = new Guid("{B94237E7-57AC-4347-9151-B08C6C32D1F7}");

        /// <summary>
        ///  Computer virtual folder
        /// </summary>
        public static readonly Guid ComputerFolder = new Guid("{0AC0837C-BBF8-452A-850D-79D08E667CA7}");

        /// <summary>
        ///  Conflicts virtual folder
        /// </summary>
        public static readonly Guid ConflictFolder = new Guid("{4bfefb45-347d-4006-a5be-ac0cb0567192}");

        /// <summary>
        ///  Network Conections virtual folder
        /// </summary>
        public static readonly Guid ConnectionsFolder = new Guid("{6F0CD92B-2E97-45D1-88FF-B0D186B8DEDD}");

        /// <summary>
        ///  Contacts folder "%USERPROFILE%\Contacts"
        /// </summary>
        public static readonly Guid Contacts = new Guid("{56784854-C6CB-462b-8169-88E350ACB882}");

        /// <summary>
        ///  Control Panel virtual folder
        /// </summary>
        public static readonly Guid ControlPanelFolder = new Guid("{82A74AEB-AEB4-465C-A014-D097EE346D63}");

        /// <summary>
        ///  Cookies folder "%APPDATA%\Microsoft\Windows\Cookies"
        /// </summary>
        public static readonly Guid Cookies = new Guid("{2B0F765D-C0E9-4171-908E-08A611B84FF6}");

        /// <summary>
        ///  Desktop folder "%USERPROFILE%\Desktop"
        /// </summary>
        public static readonly Guid Desktop = new Guid("{B4BFCC3A-DB2C-424C-B029-7FE99A87C641}");

        /// <summary>
        ///  DeviceMetadataStore folder "%ALLUSERSPROFILE%\Microsoft\Windows\DeviceMetadataStore"
        /// </summary>
        public static readonly Guid DeviceMetadataStore = new Guid("{5CE4A5E9-E4EB-479D-B89F-130C02886155}");

        /// <summary>
        ///  Documents (My Documents) folder "%USERPROFILE%\Documents"
        /// </summary>
        public static readonly Guid Documents = new Guid("{FDD39AD0-238F-46AF-ADB4-6C85480369C7}");

        /// <summary>
        ///  Documents library folder "%APPDATA%\Microsoft\Windows\Libraries\Documents.library-ms"
        /// </summary>
        public static readonly Guid DocumentsLibrary = new Guid("{7B0DB17D-9CD2-4A93-9733-46CC89022E7C}");

        /// <summary>
        ///  Downloads folder "%USERPROFILE%\Downloads"
        /// </summary>
        public static readonly Guid Downloads = new Guid("{374DE290-123F-4565-9164-39C4925E467B}");

        /// <summary>
        ///  Favorites folder "%USERPROFILE%\Favorites"
        /// </summary>
        public static readonly Guid Favorites = new Guid("{1777F761-68AD-4D8A-87BD-30B759FA33DD}");

        /// <summary>
        ///  Fonts folder "%windir%\Fonts"
        /// </summary>
        public static readonly Guid Fonts = new Guid("{FD228CB7-AE11-4AE3-864C-16F3910AB8FE}");

        /// <summary>
        ///  Games virtual folder
        /// </summary>
        public static readonly Guid Games = new Guid("{CAC52C1A-B53D-4edc-92D7-6B2E8AC19434}");

        /// <summary>
        ///  GameExplorer folder "%LOCALAPPDATA%\Microsoft\Windows\GameExplorer"
        /// </summary>
        public static readonly Guid GameTasks = new Guid("{054FAE61-4DD8-4787-80B6-090220C4B700}");

        /// <summary>
        ///  History folder "%LOCALAPPDATA%\Microsoft\Windows\History"
        /// </summary>
        public static readonly Guid History = new Guid("{D9DC8A3B-B784-432E-A781-5A1130A75963}");

        /// <summary>
        ///  Homegroup virtual folder
        /// </summary>
        public static readonly Guid HomeGroup = new Guid("{52528A6B-B9E3-4ADD-B60D-588C2DBA842D}");

        /// <summary>
        ///  Homegroup current user virtual folder
        /// </summary>
        public static readonly Guid HomeGroupCurrentUser = new Guid("{9B74B6A3-0DFD-4f11-9E78-5F7800F2E772}");

        /// <summary>
        ///  ImplicitAppShortcuts folder "%APPDATA%\Microsoft\Internet Explorer\Quick Launch\User Pinned\ImplicitAppShortcuts"
        /// </summary>
        public static readonly Guid ImplicitAppShortcuts = new Guid("{BCB5256F-79F6-4CEE-B725-DC34E402FD46}");

        /// <summary>
        ///  Temporary Internet Files folder "%LOCALAPPDATA%\Microsoft\Windows\Temporary Internet Files"
        /// </summary>
        public static readonly Guid InternetCache = new Guid("{352481E8-33BE-4251-BA85-6007CAEDCF9D}");

        /// <summary>
        ///  The Internet virtual folder
        /// </summary>
        public static readonly Guid InternetFolder = new Guid("{4D9F7874-4E0C-4904-967B-40B0D20C3E4B}");

        /// <summary>
        ///  Libraries folder "%APPDATA%\Microsoft\Windows\Libraries"
        /// </summary>
        public static readonly Guid Libraries = new Guid("{1B3EA5DC-B587-4786-B4EF-BD1DC332AEAE}");

        /// <summary>
        ///  Links folder "%USERPROFILE%\Links"
        /// </summary>
        public static readonly Guid Links = new Guid("{bfb9d5e0-c6a9-404c-b2b2-ae6db6af4968}");

        /// <summary>
        ///  Local folder "%LOCALAPPDATA%" ("%USERPROFILE%\AppData\Local")
        /// </summary>
        public static readonly Guid LocalAppData = new Guid("{F1B32785-6FBA-4FCF-9D55-7B8E7F157091}");

        /// <summary>
        ///  LocalLow folder "%USERPROFILE%\AppData\LocalLow"
        /// </summary>
        public static readonly Guid LocalAppDataLow = new Guid("{A520A1A4-1780-4FF6-BD18-167343C5AF16}");

        /// <summary>
        ///  Fixed localized resources folder "%windir%\resources\0409" (per active codepage)
        /// </summary>
        public static readonly Guid LocalizedResourcesDir = new Guid("{2A00375E-224C-49DE-B8D1-440DF7EF3DDC}");

        /// <summary>
        ///  Music folder "%USERPROFILE%\Music"
        /// </summary>
        public static readonly Guid Music = new Guid("{4BD8D571-6D19-48D3-BE97-422220080E43}");

        /// <summary>
        ///  Music library folder "%APPDATA%\Microsoft\Windows\Libraries\Music.library-ms"
        /// </summary>
        public static readonly Guid MusicLibrary = new Guid("{2112AB0A-C86A-4FFE-A368-0DE96E47012E}");

        /// <summary>
        ///  Network shortcuts folder "%APPDATA%\Microsoft\Windows\Network Shortcuts"
        /// </summary>
        public static readonly Guid NetHood = new Guid("{C5ABBF53-E17F-4121-8900-86626FC2C973}");

        /// <summary>
        ///  Network virtual folder
        /// </summary>
        public static readonly Guid NetworkFolder = new Guid("{D20BEEC4-5CA8-4905-AE3B-BF251EA09B53}");

        /// <summary>
        ///  Original Images folder "%LOCALAPPDATA%\Microsoft\Windows Photo Gallery\Original Images"
        /// </summary>
        public static readonly Guid OriginalImages = new Guid("{2C36C0AA-5812-4b87-BFD0-4CD0DFB19B39}");

        /// <summary>
        ///  Slide Shows folder "%USERPROFILE%\Pictures\Slide Shows"
        /// </summary>
        public static readonly Guid PhotoAlbums = new Guid("{69D2CF90-FC33-4FB7-9A0C-EBB0F0FCB43C}");

        /// <summary>
        ///  Pictures library folder "%APPDATA%\Microsoft\Windows\Libraries\Pictures.library-ms"
        /// </summary>
        public static readonly Guid PicturesLibrary = new Guid("{A990AE9F-A03B-4E80-94BC-9912D7504104}");

        /// <summary>
        ///  Pictures folder "%USERPROFILE%\Pictures"
        /// </summary>
        public static readonly Guid Pictures = new Guid("{33E28130-4E1E-4676-835A-98395C3BC3BB}");

        /// <summary>
        ///  Playlists folder "%USERPROFILE%\Music\Playlists"
        /// </summary>
        public static readonly Guid Playlists = new Guid("{DE92C1C7-837F-4F69-A3BB-86E631204A23}");

        /// <summary>
        ///  Printers virtual folder
        /// </summary>
        public static readonly Guid PrintersFolder = new Guid("{76FC4E2D-D6AD-4519-A663-37BD56068185}");

        /// <summary>
        ///  Printer Shortcuts folder "%APPDATA%\Microsoft\Windows\Printer Shortcuts"
        /// </summary>
        public static readonly Guid PrintHood = new Guid("{9274BD8D-CFD1-41C3-B35E-B13F55A758F4}");

        /// <summary>
        ///  The root users profile folder "%USERPROFILE%" ("%SystemDrive%\Users\%USERNAME%")
        /// </summary>
        public static readonly Guid Profile = new Guid("{5E6C858F-0E22-4760-9AFE-EA3317B67173}");

        /// <summary>
        ///  ProgramData folder "%ALLUSERSPROFILE%" ("%ProgramData%", "%SystemDrive%\ProgramData")
        /// </summary>
        public static readonly Guid ProgramData = new Guid("{62AB5D82-FDC1-4DC3-A9DD-070D1D495D97}");

        /// <summary>
        ///  Program Files folder for the current process architecture "%ProgramFiles%" ("%SystemDrive%\Program Files")
        /// </summary>
        public static readonly Guid ProgramFiles = new Guid("{905e63b6-c1bf-494e-b29c-65b732d3d21a}");

        /// <summary>
        ///  64 bit Program Files folder (only available to 64 bit process)
        /// </summary>
        public static readonly Guid ProgramFilesX64 = new Guid("{6D809377-6AF0-444b-8957-A3773F02200E}");

        /// <summary>
        ///  32 bit Program Files folder (available to both 32/64 bit processes)
        /// </summary>
        public static readonly Guid ProgramFilesX86 = new Guid("{7C5A40EF-A0FB-4BFC-874A-C0F2E0B9FA8E}");

        /// <summary>
        ///  Common Program Files folder for the current process architecture "%ProgramFiles%\Common Files"
        /// </summary>
        public static readonly Guid ProgramFilesCommon = new Guid("{F7F1ED05-9F6D-47A2-AAAE-29D317C6F066}");

        /// <summary>
        ///  Common 64 bit Program Files folder (only available to 64 bit process)
        /// </summary>
        public static readonly Guid ProgramFilesCommonX64 = new Guid("{6365D5A7-0F0D-45E5-87F6-0DA56B6A4F7D}");

        /// <summary>
        ///  Common 32 bit Program Files folder (available to both 32/64 bit processes)
        /// </summary>
        public static readonly Guid ProgramFilesCommonX86 = new Guid("{DE974D24-D9C6-4D3E-BF91-F4455120B917}");

        /// <summary>
        ///  Start menu Programs folder "%APPDATA%\Microsoft\Windows\Start Menu\Programs"
        /// </summary>
        public static readonly Guid Programs = new Guid("{A77F5D77-2E2B-44C3-A6A2-ABA601054A51}");

        /// <summary>
        ///  Public user folder "%PUBLIC%" ("%SystemDrive%\Users\Public")
        /// </summary>
        public static readonly Guid Public = new Guid("{DFDF76A2-C82A-4D63-906A-5644AC457385}");

        /// <summary>
        ///  Public Desktop folder "%PUBLIC%\Desktop"
        /// </summary>
        public static readonly Guid PublicDesktop = new Guid("{C4AA340D-F20F-4863-AFEF-F87EF2E6BA25}");

        /// <summary>
        ///  Public Documents folder "%PUBLIC%\Documents"
        /// </summary>
        public static readonly Guid PublicDocuments = new Guid("{ED4824AF-DCE4-45A8-81E2-FC7965083634}");

        /// <summary>
        ///  Public Downloads folder "%PUBLIC%\Downloads"
        /// </summary>
        public static readonly Guid PublicDownloads = new Guid("{3D644C9B-1FB8-4f30-9B45-F670235F79C0}");

        /// <summary>
        ///  GameExplorer folder "%ALLUSERSPROFILE%\Microsoft\Windows\GameExplorer"
        /// </summary>
        public static readonly Guid PublicGameTasks = new Guid("{DEBF2536-E1A8-4c59-B6A2-414586476AEA}");

        /// <summary>
        ///  Public Libraries folder "%ALLUSERSPROFILE%\Microsoft\Windows\Libraries"
        /// </summary>
        public static readonly Guid PublicLibraries = new Guid("{48DAF80B-E6CF-4F4E-B800-0E69D84EE384}");

        /// <summary>
        ///  Public Music folder "%PUBLIC%\Music"
        /// </summary>
        public static readonly Guid PublicMusic = new Guid("{3214FAB5-9757-4298-BB61-92A9DEAA44FF}");

        /// <summary>
        ///  Public Pictures folder "%PUBLIC%\Pictures"
        /// </summary>
        public static readonly Guid PublicPictures = new Guid("{B6EBFB86-6907-413C-9AF7-4FC2ABF07CC5}");

        /// <summary>
        ///  Public Ringtones folder "%ALLUSERSPROFILE%\Microsoft\Windows\Ringtones"
        /// </summary>
        public static readonly Guid PublicRingtones = new Guid("{E555AB60-153B-4D17-9F04-A5FE99FC15EC}");

        /// <summary>
        ///  Public Account Pictures folder "%PUBLIC%\AccountPictures"
        /// </summary>
        public static readonly Guid PublicUserTiles = new Guid("{0482af6c-08f1-4c34-8c90-e17ec98b1e17}");

        /// <summary>
        ///  Public Videos folder "%PUBLIC%\Videos"
        /// </summary>
        public static readonly Guid PublicVideos = new Guid("{2400183A-6185-49FB-A2D8-4A392A602BA3}");

        /// <summary>
        ///  Quick Launch folder "%APPDATA%\Microsoft\Internet Explorer\Quick Launch"
        /// </summary>
        public static readonly Guid QuickLaunch = new Guid("{52a4f021-7b75-48a9-9f6b-4b87a210bc8f}");

        /// <summary>
        ///  Recent Items folder "%APPDATA%\Microsoft\Windows\Recent"
        /// </summary>
        public static readonly Guid Recent = new Guid("{AE50C081-EBD2-438A-8655-8A092E34987A}");

        /// <summary>
        ///  Recorded TV Library "%PUBLIC%\RecordedTV.library-ms"
        /// </summary>
        public static readonly Guid RecordedTVLibrary = new Guid("{1A6FDBA2-F42D-4358-A798-B74D745926C5}");

        /// <summary>
        ///  Recycle Bin virtual folder
        /// </summary>
        public static readonly Guid RecycleBinFolder = new Guid("{B7534046-3ECB-4C18-BE4E-64CD4CB7D6AC}");

        /// <summary>
        ///  Resources fixed folder "%windir%\Resources"
        /// </summary>
        public static readonly Guid ResourceDir = new Guid("{8AD10C31-2ADB-4296-A8F7-E4701232C972}");

        /// <summary>
        ///  Ringtones folder "%LOCALAPPDATA%\Microsoft\Windows\Ringtones"
        /// </summary>
        public static readonly Guid Ringtones = new Guid("{C870044B-F49E-4126-A9C3-B52A1FF411E8}");

        /// <summary>
        ///  Roaming user application data folder "%APPDATA%" ("%USERPROFILE%\AppData\Roaming")
        /// </summary>
        public static readonly Guid RoamingAppData = new Guid("{3EB685DB-65F9-4CF6-A03A-E3EF65729F3D}");

        /// <summary>
        ///  RoamedTileImages folder %LOCALAPPDATA%\Microsoft\Windows\RoamedTileImages
        /// </summary>
        /// <remarks>
        ///  Windows 8 and higher
        /// </remarks>
        public static readonly Guid RoamedTileImages = new Guid("{AAA8D5A5-F1D6-4259-BAA8-78E7EF60835E}");

        /// <summary>
        ///  RoamingTiles folder "%LOCALAPPDATA%\Microsoft\Windows\RoamingTiles"
        /// </summary>
        /// <remarks>
        ///  Windows 8 and higher
        /// </remarks>
        public static readonly Guid RoamingTiles = new Guid("{00BCFC5A-ED94-4e48-96A1-3F6217F21990}");

        /// <summary>
        ///  Sample Music folder "%PUBLIC%\Music\Sample Music"
        /// </summary>
        public static readonly Guid SampleMusic = new Guid("{B250C668-F57D-4EE1-A63C-290EE7D1AA1F}");

        /// <summary>
        ///  Sample Pictures folder "%PUBLIC%\Pictures\Sample Pictures"
        /// </summary>
        public static readonly Guid SamplePictures = new Guid("{C4900540-2379-4C75-844B-64E6FAF8716B}");

        /// <summary>
        ///  Sample Playlists folder "%PUBLIC%\Music\Sample Playlists"
        /// </summary>
        public static readonly Guid SamplePlaylists = new Guid("{15CA69B3-30EE-49C1-ACE1-6B5EC372AFB5}");

        /// <summary>
        ///  Sample Videos folder "%PUBLIC%\Videos\Sample Videos"
        /// </summary>
        public static readonly Guid SampleVideos = new Guid("{859EAD94-2E85-48AD-A71A-0969CB56A6CD}");

        /// <summary>
        ///  Saved Games folder "%USERPROFILE%\Saved Games"
        /// </summary>
        public static readonly Guid SavedGames = new Guid("{4C5C32FF-BB9D-43b0-B5B4-2D72E54EAAA4}");

        /// <summary>
        ///  Saved Pictures folder "%USERPROFILE%\Pictures\Saved Pictures"
        /// </summary>
        public static readonly Guid SavedPictures = new Guid("{3B193882-D3AD-4eab-965A-69829D1FB59F}");

        /// <summary>
        ///  Saved Pictures library folder "%APPDATA%\Microsoft\Windows\Libraries\SavedPictures.library-ms"
        /// </summary>
        public static readonly Guid SavedPicturesLibrary = new Guid("{E25B5812-BE88-4bd9-94B0-29233477B6C3}");

        /// <summary>
        ///  Searches folder "%USERPROFILE%\Searches"
        /// </summary>
        public static readonly Guid SavedSearches = new Guid("{7d1d3a04-debb-4115-95cf-2f29da2920da}");

        /// <summary>
        ///  Screenshots folder "%USERPROFILE%\Pictures\Screenshots"
        /// </summary>
        public static readonly Guid Screenshots = new Guid("{b7bede81-df94-4682-a7d8-57a52620b86f}");

        /// <summary>
        ///  Offline Files virtual folder
        /// </summary>
        public static readonly Guid SEARCH_CSC = new Guid("{ee32e446-31ca-4aba-814f-a5ebd2fd6d5e}");

        /// <summary>
        ///  Search History folder "%LOCALAPPDATA%\Microsoft\Windows\ConnectedSearch\History"
        /// </summary>
        public static readonly Guid SearchHistory = new Guid("{0D4C3DB6-03A3-462F-A0E6-08924C41B5D4}");

        /// <summary>
        ///  Search Results virtual folder
        /// </summary>
        public static readonly Guid SearchHome = new Guid("{190337d1-b8ca-4121-a639-6d472d16972a}");

        /// <summary>
        ///  Microsoft Office Outlook virtual folder
        /// </summary>
        public static readonly Guid SEARCH_MAPI = new Guid("{98ec0e18-2098-4d44-8644-66979315a281}");

        /// <summary>
        ///  Search Templates folder "%LOCALAPPDATA%\Microsoft\Windows\ConnectedSearch\Templates"
        /// </summary>
        public static readonly Guid SearchTemplates = new Guid("{7E636BFE-DFA9-4D5E-B456-D7B39851D8A9}");

        /// <summary>
        ///  SendTo folder "%APPDATA%\Microsoft\Windows\SendTo"
        /// </summary>
        public static readonly Guid SendTo = new Guid("{8983036C-27C0-404B-8F08-102D10DCFD74}");

        /// <summary>
        ///  Common Gadgets folder "%ProgramFiles%\Windows Sidebar\Gadgets"
        /// </summary>
        public static readonly Guid SidebarDefaultParts = new Guid("{7B396E54-9EC5-4300-BE0A-2482EBAE1A26}");

        /// <summary>
        ///  Gadgets folder "%LOCALAPPDATA%\Microsoft\Windows Sidebar\Gadgets"
        /// </summary>
        public static readonly Guid SidebarParts = new Guid("{A75D362E-50FC-4fb7-AC2C-A8BEAA314493}");

        /// <summary>
        ///  OneDrive folder "%USERPROFILE%\OneDrive"
        /// </summary>
        public static readonly Guid SkyDrive = new Guid("{A52BBA46-E9E1-435f-B3D9-28DAA648C0F6}");

        /// <summary>
        ///  OneDrive Camera Roll folder "%USERPROFILE%\OneDrive\Pictures\Camera Roll"
        /// </summary>
        public static readonly Guid SkyDriveCameraRoll = new Guid("{767E6811-49CB-4273-87C2-20F355E1085B}");

        /// <summary>
        ///  OneDrive Documents folder "%USERPROFILE%\OneDrive\Documents"
        /// </summary>
        public static readonly Guid SkyDriveDocuments = new Guid("{24D89E24-2F19-4534-9DDE-6A6671FBB8FE}");

        /// <summary>
        ///  OneDrive Pictures folder "%USERPROFILE%\OneDrive\Pictures"
        /// </summary>
        public static readonly Guid SkyDrivePictures = new Guid("{339719B5-8C47-4894-94C2-D8F77ADD44A6}");

        /// <summary>
        ///  Start Menu folder "%APPDATA%\Microsoft\Windows\Start Menu"
        /// </summary>
        public static readonly Guid StartMenu = new Guid("{625B53C3-AB48-4EC1-BA1F-A1EF4146FC19}");

        /// <summary>
        ///  Startup folder "%APPDATA%\Microsoft\Windows\Start Menu\Programs\StartUp"
        /// </summary>
        public static readonly Guid Startup = new Guid("{B97D20BB-F46A-4C97-BA10-5E3608430854}");

        /// <summary>
        ///  Sync Center virtual folder
        /// </summary>
        public static readonly Guid SyncManagerFolder = new Guid("{43668BF8-C14E-49B2-97C9-747784D784B7}");

        /// <summary>
        ///  Sync Results virtual folder
        /// </summary>
        public static readonly Guid SyncResultsFolder = new Guid("{289a9a43-be44-4057-a41b-587a76d7e7f9}");

        /// <summary>
        ///  Sync Setup virtual folder
        /// </summary>
        public static readonly Guid SyncSetupFolder = new Guid("{0F214138-B1D3-4a90-BBA9-27CBC0C5389A}");

        /// <summary>
        ///  System32 folder "%windir%\system32"
        /// </summary>
        public static readonly Guid System = new Guid("{1AC14E77-02E7-4E5D-B744-2EB1AE5198B7}");

        /// <summary>
        ///  X86 System32 folder "%windir%\system32" or "%windir%\syswow64"
        /// </summary>
        public static readonly Guid SystemX86 = new Guid("{D65231B0-B2F1-4857-A4CE-A8E7C6EA7D27}");

        /// <summary>
        ///  Templates folder "%APPDATA%\Microsoft\Windows\Templates"
        /// </summary>
        public static readonly Guid Templates = new Guid("{A63293E8-664E-48DB-A079-DF759E0509F7}");

        /// <summary>
        ///  User Pinned folder "%APPDATA%\Microsoft\Internet Explorer\Quick Launch\User Pinned"
        /// </summary>
        public static readonly Guid UserPinned = new Guid("{9E3995AB-1F9C-4F13-B827-48B24B6C7174}");

        /// <summary>
        ///  Users folder "%SystemDrive%\Users"
        /// </summary>
        public static readonly Guid UserProfiles = new Guid("{0762D272-C50A-4BB0-A382-697DCD729B80}");

        /// <summary>
        ///  User Programs folder "%LOCALAPPDATA%\Programs"
        /// </summary>
        public static readonly Guid UserProgramFiles = new Guid("{5CD7AEE2-2219-4A67-B85D-6C9CE15660CB}");

        /// <summary>
        ///  User Programs common folder "%LOCALAPPDATA%\Programs\Common"
        /// </summary>
        public static readonly Guid UserProgramFilesCommon = new Guid("{BCBD3057-CA5C-4622-B42D-BC56DB0AE516}");

        /// <summary>
        ///  User virtual folder
        /// </summary>
        public static readonly Guid UsersFiles = new Guid("{f3ce0f7c-4901-4acc-8648-d5d44b04ef8f}");

        /// <summary>
        ///  User libraries virtual folder
        /// </summary>
        public static readonly Guid UsersLibraries = new Guid("{A302545D-DEFF-464b-ABE8-61C8648D939B}");

        /// <summary>
        ///  Videos folder "%USERPROFILE%\Videos"
        /// </summary>
        public static readonly Guid Videos = new Guid("{18989B1D-99B5-455B-841C-AB7C74E4DDFC}");

        /// <summary>
        ///  Videos library folder "%APPDATA%\Microsoft\Windows\Libraries\Videos.library-ms"
        /// </summary>
        public static readonly Guid VideosLibrary = new Guid("{491E922F-5643-4AF4-A7EB-4E7A138D8174}");

        /// <summary>
        ///  Windows folder "%windir%"
        /// </summary>
        public static readonly Guid Windows = new Guid("{F38BF404-1D43-42F2-9305-67DE0B28FC23}");
    }
}
