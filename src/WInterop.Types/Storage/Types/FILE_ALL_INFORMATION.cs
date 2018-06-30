// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage.Types
{
    /// <summary>
    /// Combo struct for all file information.
    /// </summary>
    /// <remarks>
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/hardware/ff545743.aspx"/>
    /// </remarks>
    public struct FILE_ALL_INFORMATION
    {
        public FILE_BASIC_INFORMATION BasicInformation;
        public FILE_STANDARD_INFORMATION StandardInformation;
        public FILE_INTERNAL_INFORMATION InternalInformation;
        public FILE_EA_INFORMATION EaInformation;
        public FILE_ACCESS_INFORMATION AccessInformation;
        public FILE_POSITION_INFORMATION PositionInformation;
        public FILE_MODE_INFORMATION ModeInformation;
        public FILE_ALIGNMENT_INFORMATION AlignmentInformation;
        public FILE_NAME_INFORMATION NameInformation;
    }
}
