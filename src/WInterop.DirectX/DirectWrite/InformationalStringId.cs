// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.DirectWrite
{
    /// <summary>
    /// The informational string enumeration identifies a string in a font. [DWRITE_INFORMATIONAL_STRING_ID]
    /// </summary>
    public enum InformationalStringId : uint
    {
        /// <summary>
        /// Unspecified name ID.
        /// </summary>
        None,

        /// <summary>
        /// Copyright notice provided by the font.
        /// </summary>
        CopyrightNotice,

        /// <summary>
        /// String containing a version number.
        /// </summary>
        VersionStrings,

        /// <summary>
        /// Trademark information provided by the font.
        /// </summary>
        Trademark,

        /// <summary>
        /// Name of the font manufacturer.
        /// </summary>
        Manufacturer,

        /// <summary>
        /// Name of the font designer.
        /// </summary>
        Designer,

        /// <summary>
        /// URL of font designer (with protocol, e.g., http://, ftp://).
        /// </summary>
        DesignerUrl,

        /// <summary>
        /// Description of the font. Can contain revision information, usage recommendations, history, features, etc.
        /// </summary>
        Description,

        /// <summary>
        /// URL of font vendor (with protocol, e.g., http://, ftp://). If a unique serial number is embedded in the URL, it can be used to register the font.
        /// </summary>
        VendorUrl,

        /// <summary>
        /// Description of how the font may be legally used, or different example scenarios for licensed use. This field should be written in plain language, not legalese.
        /// </summary>
        LicenseDescription,

        /// <summary>
        /// URL where additional licensing information can be found.
        /// </summary>
        LicenseInfoUrl,

        /// <summary>
        /// GDI-compatible family name. Because GDI allows a maximum of four fonts per family, fonts in the same family may have different GDI-compatible family names
        /// (e.g., "Arial", "Arial Narrow", "Arial Black").
        /// </summary>
        Win32FamilyNames,

        /// <summary>
        /// GDI-compatible subfamily name.
        /// </summary>
        Win32SubfamilyName,

        /// <summary>
        /// Typographic family name preferred by the designer. This enables font designers to group more than four fonts in a single family without losing compatibility with
        /// GDI. This name is typically only present if it differs from the GDI-compatible family name.
        /// </summary>
        TypographicFamilyNames,

        /// <summary>
        /// Typographic subfamily name preferred by the designer. This name is typically only present if it differs from the GDI-compatible subfamily name. 
        /// </summary>
        TypographicSubfamilyNames,

        /// <summary>
        /// Sample text. This can be the font name or any other text that the designer thinks is the best example to display the font in.
        /// </summary>
        SampleText,

        /// <summary>
        /// The full name of the font, e.g. "Arial Bold", from name id 4 in the name table.
        /// </summary>
        FullName,

        /// <summary>
        /// The postscript name of the font, e.g. "GillSans-Bold" from name id 6 in the name table.
        /// </summary>
        PostscriptName,

        /// <summary>
        /// The postscript CID findfont name, from name id 20 in the name table.
        /// </summary>
        PostscriptCidName,

        /// <summary>
        /// Family name for the weight-stretch-style model.
        /// </summary>
        WeightStretchStyleFamilyName,

        /// <summary>
        /// Script/language tag to identify the scripts or languages that the font was
        /// primarily designed to support. See DWRITE_FONT_PROPERTY_ID_DESIGN_SCRIPT_LANGUAGE_TAG
        /// for a longer description.
        /// </summary>
        DesignScriptLanguageTag,

        /// <summary>
        /// Script/language tag to identify the scripts or languages that the font declares
        /// it is able to support.
        /// </summary>
        SupportedScriptLanguageTag
    }
}
