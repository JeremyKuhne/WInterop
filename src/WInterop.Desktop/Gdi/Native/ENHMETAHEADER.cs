// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;

namespace WInterop.Gdi.Native
{
    /// <summary>
    ///  [ENHMETAHEADER]
    /// </summary>
    public struct ENHMETAHEADER
    {
        public MetafileRecordType iType;            // Record typeEMR_HEADER
        public uint nSize;                          // Record size in bytes.  This may be greater
                                                    // than the sizeof(ENHMETAHEADER).
        public Rect rclBounds;                      // Inclusive-inclusive bounds in device units
        public Rect rclFrame;                       // Inclusive-inclusive Picture Frame of metafile in .01 mm units
        public MetafileSignature dSignature;        // Signature.  Must be ENHMETA_SIGNATURE.
        public uint nVersion;                       // Version number
        public uint nBytes;                         // Size of the metafile in bytes
        public uint nRecords;                       // Number of records in the metafile
        public ushort nHandles;                     // Number of handles in the handle table
                                                    // Handle index zero is reserved.
        public ushort sReserved;                    // Reserved.  Must be zero.
        public uint nDescription;                   // Number of chars in the unicode description string
                                                    // This is 0 if there is no description string
        public uint offDescription;                 // Offset to the metafile description record.
                                                    // This is 0 if there is no description string
        public uint nPalEntries;                    // Number of entries in the metafile palette.
        public Size szlDevice;                      // Size of the reference device in pels
        public Size szlMillimeters;                 // Size of the reference device in millimeters
        public uint   cbPixelFormat;                // Size of PIXELFORMATDESCRIPTOR information
                                                    // This is 0 if no pixel format is set
        public uint   offPixelFormat;               // Offset to PIXELFORMATDESCRIPTOR
                                                    // This is 0 if no pixel format is set
        public IntBoolean   bOpenGL;                 // TRUE if OpenGL commands are present in
                                                    // the metafile, otherwise FALSE
        public Size   szlMicrometers;               // Size of the reference device in micrometers
    }
}
