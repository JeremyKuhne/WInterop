// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Compression
{
    public enum FciError : int
    {
        /// <summary>
        ///  No error. [FCIERR_NONE]
        /// </summary>
        None,

        /// <summary>
        ///  Failure opening file to be stored in cabinet.
        ///  erf.erfTyp has C run-time *errno* value. [FCIERR_OPEN_SRC]
        /// </summary>
        OpenSource,

        /// <summary>
        ///  Failure reading file to be stored in cabinet.
        ///  erf.erfTyp has C run-time *errno* value. [FCIERR_READ_SRC]
        /// </summary>
        ReadSource,

        /// <summary>
        ///  Out of memory in FCI. [FCIERR_ALLOC_FAIL]
        /// </summary>
        AllocFail,

        /// <summary>
        ///  Could not create a temporary file.
        ///  erf.erfTyp has C run-time *errno* value. [FCIERR_TEMP_FILE]
        /// </summary>
        TempFile,

        /// <summary>
        ///  Unknown compression type. [FCIERR_BAD_COMPR_TYPE]
        /// </summary>
        BadCompressionType,

        /// <summary>
        ///  Could not create cabinet file.
        ///  erf.erfTyp has C run-time *errno* value. [FCIERR_CAB_FILE]
        /// </summary>
        CabinetFile,

        /// <summary>
        ///  Client requested abort. [FCIERR_USER_ABORT]
        /// </summary>
        UserAbort,

        /// <summary>
        ///  Failure compressing data. [FCIERR_MCI_FAIL]
        /// </summary>
        MciFail,

        /// <summary>
        ///  Data-size or file-count exceeded CAB format limits.
        ///  i.e. Total-bytes (uncompressed) in a CAB-folder exceeded 0x7FFF8000 (~ 2GB)
        ///  or, CAB size (compressed) exceeded 0x7FFFFFFF
        ///  or, File-count in CAB exceeded 0xFFFF
        ///  [FCIERR_CAB_FORMAT_LIMIT]
        /// </summary>
        CabinetFormatLimit
    }
}
