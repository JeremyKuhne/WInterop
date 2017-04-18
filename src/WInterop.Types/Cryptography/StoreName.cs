// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Cryptography.DataTypes
{
    /// <summary>
    /// Specifies a name of a X.509 certificate store. Aligns with System.Security.Cryptography.X509Certificates.StoreName values.
    /// (Names are aligned with the actual system strings, however.)
    /// </summary>
    public enum StoreName
    {
        /// <summary>
        /// The certificate store for other users.
        /// </summary>
        AddressBook = 1,

        /// <summary>
        /// The certificate store for third-party certificate authorities (CAs).
        /// </summary>
        AuthRoot = 2,

        /// <summary>
        /// The certificate store for intermediate certificate authorities (CAs).
        /// </summary>
        CA = 3,

        /// <summary>
        /// The certificate store for revoked certificates.
        /// </summary>
        Disallowed = 4,

        /// <summary>
        /// The certificate store for personal certificates.
        /// </summary>
        My = 5,

        /// <summary>
        /// The certificate store for trusted root certificate authorities (CAs).
        /// </summary>
        Root = 6,

        /// <summary>
        /// The certificate store for directly trusted people and resources.
        /// </summary>
        TrustedPeople = 7,

        /// <summary>
        /// The certificate store for directly trusted publishers.
        /// </summary>
        TrustedPublisher = 8,

        /// <summary>
        /// The certificate store for Software Publisher Certificates. (Not defined by .NET.)
        /// </summary>
        SPC = 9
    }
}
