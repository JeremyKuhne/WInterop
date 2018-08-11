// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    public enum KeyboardType : int
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724336.aspx
        // http://archives.miloush.net/michkap/archive/2011/01/07/10112915.html

        /// <summary>
        /// AT&T '301' & '302'; Olivetti 83-key; PC-XT 84-key; etc.
        /// </summary>
        IbmPcXt = 1,

        /// <summary>
        /// Olivetti M24 102-key.
        /// </summary>
        OlivettiM24 = 2,

        /// <summary>
        /// HP Vectra (DIN); Olivetti 86-key; etc.
        /// </summary>
        HpVectra = 3,

        /// <summary>
        /// Enhanced 101/102-key; Olivetti A; etc. (default)
        /// </summary>
        Enhanced = 4,

        /// <summary>
        /// Nokia (Ericsson) type 5 (1050, etc.).
        /// </summary>
        EricssonType5 = 5,

        /// <summary>
        /// Nokia (Ericsson) type 6 (9140).
        /// </summary>
        EricssonType6 = 6,

        /// <summary>
        /// Japanese IBM type 002 keyboard.
        /// </summary>
        JapaneseIbm = 7,

        /// <summary>
        /// Japanese OADG (106) keyboard.
        /// </summary>
        JapaneseOadg = 8,

        /// <summary>
        /// Korean 101 (type A) keyboard.
        /// </summary>
        Korean101A = 10,

        /// <summary>
        /// Korean 101 (type B) keyboard.
        /// </summary>
        Korean101B = 11,

        /// <summary>
        /// Korean 101 (type C) keyboard.
        /// </summary>
        Korean101C = 12,

        /// <summary>
        /// Korean 103 keyboard.
        /// </summary>
        Korean103 = 13,

        /// <summary>
        /// Japanese AX keyboard.
        /// </summary>
        JapaneseAx = 16,

        /// <summary>
        /// Fujitsu FMR JIS keyboard.
        /// </summary>
        FujitsuJis = 20,

        /// <summary>
        /// Fujitsu FMR OYAYUBI keyboard.
        /// </summary>
        FujitsuFmrOyayubi = 21,

        /// <summary>
        /// Fujitsu FMV OYAYUBI keyboard.
        /// </summary>
        FujitsuFmvOyayubi = 22,

        /// <summary>
        /// NEC PC-9800 Normal Keyboard.
        /// </summary>
        NecPc9800Normal = 30,

        /// <summary>
        /// NEC PC-9800 Document processor Keyboard. Not supported on Windows 2000 or higher.
        /// </summary>
        NecPc9800Document = 31,

        /// <summary>
        /// NEC PC-9800 106 Keyboard. Same as JapaneseOadg (type 8).
        /// </summary>
        NecPc9800_106 = 32,

        /// <summary>
        /// NEC PC-9800 for Hydra: PC-9800 Keyboard on Windows 2000 and up.
        /// NEC PC-98NX for Hydra: PC-9800 Keyboard.
        /// </summary>
        NecPc9800Hydra = 33,

        /// <summary>
        /// NEC PC-9800 for Hydra: PC-9800 Keyboard on Windows NT 3.51/4.0.
        /// </summary>
        NecPc9800HydraNt = 34,

        /// <summary>
        /// NEC PC-9800 for Hydra: PC-9800 Keyboard on Windows 9x.
        /// </summary>
        NecPc9800Hydra9x = 37,

        /// <summary>
        /// DEC LK411-JJ (JIS  layout) keyboard.
        /// </summary>
        DecJis = 40,

        /// <summary>
        /// DEC LK411-AJ (ANSI layout) keyboard.
        /// </summary>
        DecAnsi = 41
    }
}
