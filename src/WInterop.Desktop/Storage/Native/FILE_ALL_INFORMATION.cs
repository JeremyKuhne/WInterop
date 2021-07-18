// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage.Native
{
    /// <summary>
    ///  Combo struct for all file information.
    /// </summary>
    /// <remarks>
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/hardware/ff545743.aspx"/>
    /// </remarks>
    public struct FILE_ALL_INFORMATION
    {
        public FileBasicInformation BasicInformation;
        public FileStandardInformation StandardInformation;
        public FileInternalInformation InternalInformation;
        public FileExtendedAttributeInformation EaInformation;
        public FileAccessInformation AccessInformation;
        public FilePositionInformation PositionInformation;
        public FileModeInformation ModeInformation;
        public FileAlignmentInformation AlignmentInformation;
        public FILE_NAME_INFORMATION NameInformation;
    }
}