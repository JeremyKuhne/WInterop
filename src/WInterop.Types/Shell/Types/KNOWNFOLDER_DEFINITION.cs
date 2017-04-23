// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using System.Threading;
using WInterop.FileManagement.Types;

namespace WInterop.Shell.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/bb773325.aspx
    [StructLayout(LayoutKind.Sequential)]
    public class KNOWNFOLDER_DEFINITION : IDisposable
    {
        private KF_CATEGORY category;
        private IntPtr pszName;
        private IntPtr pszDescription;
        private Guid fidParent;
        private IntPtr pszRelativePath;
        private IntPtr pszParsingName;
        private IntPtr pszTooltip;
        private IntPtr pszLocalizedName;
        private IntPtr pszIcon;
        private IntPtr pszSecurity;
        private FileAttributes dwAttributes;
        private KF_DEFINITION_FLAGS kfdFlags;
        private Guid ftidType;

        // Can't use a bool here as the class will no longer be blittable
        private int _disposed;

        private bool Disposed
        {
            get { return _disposed != 0; }
        }

        public KNOWNFOLDER_DEFINITION()
        {
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Interlocked.CompareExchange(ref _disposed, 0, 1) == 0)
            {
                // This API doesn't care if the pointer is already zero
                Marshal.FreeCoTaskMem(Interlocked.Exchange(ref pszName, IntPtr.Zero));
                Marshal.FreeCoTaskMem(Interlocked.Exchange(ref pszDescription, IntPtr.Zero));
                Marshal.FreeCoTaskMem(Interlocked.Exchange(ref pszRelativePath, IntPtr.Zero));
                Marshal.FreeCoTaskMem(Interlocked.Exchange(ref pszParsingName, IntPtr.Zero));
                Marshal.FreeCoTaskMem(Interlocked.Exchange(ref pszTooltip, IntPtr.Zero));
                Marshal.FreeCoTaskMem(Interlocked.Exchange(ref pszLocalizedName, IntPtr.Zero));
                Marshal.FreeCoTaskMem(Interlocked.Exchange(ref pszIcon, IntPtr.Zero));
                Marshal.FreeCoTaskMem(Interlocked.Exchange(ref pszSecurity, IntPtr.Zero));
            }
        }

        ~KNOWNFOLDER_DEFINITION()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public KF_CATEGORY Category
        {
            get { return Disposed ? 0 : category; }
        }

        public string Name
        {
            get { return Disposed ? null : Marshal.PtrToStringUni(pszName); }
        }

        public string Description
        {
            get { return Disposed ? null : Marshal.PtrToStringUni(pszDescription); }
        }

        public Guid Parent
        {
            get { return Disposed ? Guid.Empty : fidParent; }
        }

        public string RelativePath
        {
            get { return Disposed ? null : Marshal.PtrToStringUni(pszRelativePath); }
        }

        public string ParsingName
        {
            get { return Disposed ? null : Marshal.PtrToStringUni(pszParsingName); }
        }

        public string Tooltip
        {
            get { return Disposed ? null : Marshal.PtrToStringUni(pszTooltip); }
        }

        public string LocalizedName
        {
            get { return Disposed ? null : Marshal.PtrToStringUni(pszLocalizedName); }
        }

        public string Icon
        {
            get { return Disposed ? null : Marshal.PtrToStringUni(pszIcon); }
        }

        public string Security
        {
            get { return Disposed ? null : Marshal.PtrToStringUni(pszSecurity); }
        }

        public FileAttributes Attributes
        {
            get { return Disposed ? 0 : dwAttributes; }
        }

        public KF_DEFINITION_FLAGS Flags
        {
            get { return Disposed ? 0 : kfdFlags; }
        }

        public Guid FolderTypeId
        {
            get { return Disposed ? Guid.Empty : ftidType; }
        }
    }
}
