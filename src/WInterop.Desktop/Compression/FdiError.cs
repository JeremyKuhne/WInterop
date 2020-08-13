// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Compression
{
    public enum FdiError : int
    {
        /// <summary>
        ///  No error. [FDIERROR_NONE]
        /// </summary>
        None,

        /// <summary>
        ///  Cabinet not found. [FDIERROR_CABINET_NOT_FOUND]
        /// </summary>
        CabinetNotFound,

        /// <summary>
        ///  Cabinet file does not have the correct format. [FDIERROR_NOT_A_CABINET]
        /// </summary>
        NotACabinet,

        /// <summary>
        ///  Cabinet file has an unknown version number. [FDIERROR_UNKNOWN_CABINET_VERSION]
        /// </summary>
        UnknownCabinetVersion,

        /// <summary>
        ///  Cabinet file is corrupt. [FDIERROR_CORRUPT_CABINET]
        /// </summary>
        CorruptCabinent,

        /// <summary>
        ///  Could not allocate enough memory. [FDIERROR_ALLOC_FAIL]
        /// </summary>
        AllocFail,

        /// <summary>
        ///  Unknown compression type in a cabinet folder. [FDIERROR_BAD_COMPR_TYPE]
        /// </summary>
        BadCompressionType,

        /// <summary>
        ///  Failure decompressing data from a cabinet file. [FDIERROR_MDI_FAIL]
        /// </summary>
        MdiFail,

        /// <summary>
        ///  Failure writing to target file. [FDIERROR_TARGET_FILE]
        /// </summary>
        TargetFile,

        /// <summary>
        ///  Cabinets in a set do not have the same RESERVE sizes. [FDIERROR_RESERVE_MISMATCH]
        /// </summary>
        ReserveMismatch,

        /// <summary>
        ///  Cabinet returned on fdintNEXT_CABINET is incorrect. [FDIERROR_WRONG_CABINET]
        /// </summary>
        WrongCabinet,

        /// <summary>
        ///  FDI aborted. [FDIERROR_USER_ABORT]
        /// </summary>
        UserAbort,

        /// <summary>
        ///  Unexpected end of file. [FDIERROR_EOF]
        /// </summary>
        EndOfFile,
    }
}
