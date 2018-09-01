// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Shell
{
    /// <summary>
    /// [FOLDERTYPEID]
    /// </summary>
    // https://msdn.microsoft.com/en-us/library/windows/desktop/bb762581.aspx
    public static class FolderTypeIds
    {
        // FOLDERTYPEID_Invalid:              {57807898-8c4f-4462-bb63-71042380b109}
        public static Guid Invalid = new Guid("{57807898-8c4f-4462-bb63-71042380b109}");

        // FOLDERTYPEID_Generic:              {5c4f28b5-f869-4e84-8e60-f11db97c5cc7}
        public static Guid Generic = new Guid("{5c4f28b5-f869-4e84-8e60-f11db97c5cc7}");

        // FOLDERTYPEID_GenericSearchResults: {7fde1a1e-8b31-49a5-93b8-6be14cfa4943}
        public static Guid GenericSearchResults = new Guid("{5c4f28b5-f869-4e84-8e60-f11db97c5cc7}");

        // FOLDERTYPEID_GenericLibrary:       {5f4eab9a-6833-4f61-899d-31cf46979d49}
        public static Guid GenericLibrary = new Guid("{5f4eab9a-6833-4f61-899d-31cf46979d49}");

        // FOLDERTYPEID_Documents:            {7d49d726-3c21-4f05-99aa-fdc2c9474656}
        public static Guid Documents = new Guid("{7d49d726-3c21-4f05-99aa-fdc2c9474656}");

        // FOLDERTYPEID_Pictures:             {b3690e58-e961-423b-b687-386ebfd83239}
        public static Guid Pictures = new Guid("{b3690e58-e961-423b-b687-386ebfd83239}");

        // FOLDERTYPEID_Music:                {94d6ddcc-4a68-4175-a374-bd584a510b78}
        public static Guid Music = new Guid("{94d6ddcc-4a68-4175-a374-bd584a510b78}");

        // FOLDERTYPEID_Videos:               {5fa96407-7e77-483c-ac93-691d05850de8}
        public static Guid Videos = new Guid("{5fa96407-7e77-483c-ac93-691d05850de8}");

        // FOLDERTYPEID_UserFiles:            {CD0FC69B-71E2-46e5-9690-5BCD9F57AAB3}
        public static Guid UserFiles = new Guid("{CD0FC69B-71E2-46e5-9690-5BCD9F57AAB3}");

        // FOLDERTYPID_UsersLibraries         {C4D98F09-6124-4fe0-9942-826416082DA9}
        public static Guid UsersLibraries = new Guid("{C4D98F09-6124-4fe0-9942-826416082DA9}");

        // FOLDERTYPEID_OtherUsers,           {B337FD00-9DD5-4635-A6D4-DA33FD102B7A}
        public static Guid OtherUsers = new Guid("{B337FD00-9DD5-4635-A6D4-DA33FD102B7A}");

        // {7F2F5B96-FF74-41da-AFD8-1C78A5F3AEA2}
        public static Guid PublishedItems = new Guid("{7F2F5B96-FF74-41da-AFD8-1C78A5F3AEA2}");

        // FOLDERTYPEID_Communications:       {91475fe5-586b-4eba-8d75-d17434b8cdf6}
        public static Guid Communications = new Guid("{91475fe5-586b-4eba-8d75-d17434b8cdf6}");

        // FOLDERTYPEID_Contacts:             {de2b70ec-9bf7-4a93-bd3d-243f7881d492}
        public static Guid Contacts = new Guid("{de2b70ec-9bf7-4a93-bd3d-243f7881d492}");

        // FOLDERTYPEID_StartMenu:            {ef87b4cb-f2ce-4785-8658-4ca6c63e38c6}
        public static Guid StartMenu = new Guid("{ef87b4cb-f2ce-4785-8658-4ca6c63e38c6}");

        // FOLDERTYPEID_RecordedTV:           {5557a28f-5da6-4f83-8809-c2c98a11a6fa}
        public static Guid RecordedTV = new Guid("{5557a28f-5da6-4f83-8809-c2c98a11a6fa}");

        // FOLDERTYPEID_SavedGames:           {d0363307-28cb-4106-9f23-2956e3e5e0e7}
        public static Guid SavedGames = new Guid("{d0363307-28cb-4106-9f23-2956e3e5e0e7}");

        // FOLDERTYPEID_OpenSearch:           {8faf9629-1980-46ff-8023-9dceab9c3ee3}
        public static Guid OpenSearch = new Guid("{8faf9629-1980-46ff-8023-9dceab9c3ee3}");

        // FOLDERTYPEID_SearchConnector:      {982725ee-6f47-479e-b447-812bfa7d2e8f}
        public static Guid SearchConnector = new Guid("{982725ee-6f47-479e-b447-812bfa7d2e8f}");

        // FOLDERTYPEID_AccountPictures:      {db2a5d8f-06e6-4007-aba6-af877d526ea6}
        public static Guid AccountPictures = new Guid("{db2a5d8f-06e6-4007-aba6-af877d526ea6}");

        // foldertypes that do not have top views are below

        // FOLDERTYPEID_Games Folder          {b689b0d0-76d3-4cbb-87f7-585d0e0ce070}
        public static Guid Games = new Guid("{b689b0d0-76d3-4cbb-87f7-585d0e0ce070}");

        // category view of control panel
        // FOLDERTYPEID_ControlPanelCategory: {de4f0660-fa10-4b8f-a494-068b20b22307}
        public static Guid ControlPanelCategory = new Guid("{de4f0660-fa10-4b8f-a494-068b20b22307}");

        // classic-mode control panel
        // FOLDERTYPEID_ControlPanelClassic:  {0c3794f3-b545-43aa-a329-c37430c58d2a}
        public static Guid ControlPanelClassic = new Guid("{0c3794f3-b545-43aa-a329-c37430c58d2a}");

        // prnfldr
        // FOLDERTYPEID_Printers:             {2c7bbec6-c844-4a0a-91fa-cef6f59cfda1}
        public static Guid Printers = new Guid("{2c7bbec6-c844-4a0a-91fa-cef6f59cfda1}");

        // bbckfldr
        // FOLDERTYPEID_RecycleBin:           {d6d9e004-cd87-442b-9d57-5e0aeb4f6f72}
        public static Guid RecycleBin = new Guid("{d6d9e004-cd87-442b-9d57-5e0aeb4f6f72}");

        // software explorer for ARP
        // FOLDERTYPEID_SoftwareExplorer:     {d674391b-52d9-4e07-834e-67c98610f39d}
        public static Guid SoftwareExplorer = new Guid("{d674391b-52d9-4e07-834e-67c98610f39d}");

        // ZIP folders
        // FOLDERTYPEID_CompressedFolder:     {80213e82-bcfd-4c4f-8817-bb27601267a9}
        public static Guid CompressedFolder = new Guid("{80213e82-bcfd-4c4f-8817-bb27601267a9}");

        // NetworkExplorerFolder
        // FOLDERTYPEID_NetworkExplorer:      {25CC242B-9A7C-4f51-80E0-7A2928FEBE42}
        public static Guid NetworkExplorer = new Guid("{25CC242B-9A7C-4f51-80E0-7A2928FEBE42}");

        // searches folder
        // FOLDERTYPEID_Searches:             {0b0ba2e3-405f-415e-a6ee-cad625207853}
        public static Guid Searches = new Guid("{0b0ba2e3-405f-415e-a6ee-cad625207853}");

        // search home
        // FOLDERTYPEID_SearchHome:           {834d8a44-0974-4ed6-866e-f203d80b3810}
        public static Guid SearchHome = new Guid("{834d8a44-0974-4ed6-866e-f203d80b3810}");

        // FOLDERTYPEID_StorageProviderGeneric:   {4F01EBC5-2385-41f2-A28E-2C5C91FB56E0}
        public static Guid StorageProviderGeneric = new Guid("{4F01EBC5-2385-41f2-A28E-2C5C91FB56E0}");

        // FOLDERTYPEID_StorageProviderDocuments: {DD61BD66-70E8-48dd-9655-65C5E1AAC2D1}
        public static Guid StorageProviderDocuments = new Guid("{DD61BD66-70E8-48dd-9655-65C5E1AAC2D1}");

        // FOLDERTYPEID_StorageProviderPictures:  {71D642A9-F2B1-42cd-AD92-EB9300C7CC0A}
        public static Guid StorageProviderPictures = new Guid("{71D642A9-F2B1-42cd-AD92-EB9300C7CC0A}");

        // FOLDERTYPEID_StorageProviderMusic:     {672ECD7E-AF04-4399-875C-0290845B6247}
        public static Guid StorageProviderMusic = new Guid("{672ECD7E-AF04-4399-875C-0290845B6247}");

        // FOLDERTYPEID_StorageProviderVideos:    {51294DA1-D7B1-485b-9E9A-17CFFE33E187}
        public static Guid StorageProviderVideos = new Guid("{51294DA1-D7B1-485b-9E9A-17CFFE33E187}");

    }
}
