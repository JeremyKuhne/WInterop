// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi
{
    /// <summary>
    /// [FONTSIGNATURE]
    /// </summary>
    /// <remarks><see cref="https://msdn.microsoft.com/en-us/library/dd318064.aspx"/></remarks>
    public struct FontSignature
    {
        public UnicodeSubsetsOne UnicodeSubsetsOne;
        public UnicodeSubsetsTwo UnicodeSubsetsTwo;
        public UnicodeSubsetsThree UnicodeSubsetsThree;
        public UnicodeSubsetsFour UnicodeSubsetsFour;
        public CodePagesAnsi CodePagesAnsi;
        public CodePagesOem CodePagesOem;
    }
}
