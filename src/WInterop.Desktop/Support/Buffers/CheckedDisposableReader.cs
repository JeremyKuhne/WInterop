// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Support.Buffers
{
    public class CheckedDisposableReader : CheckedReader, IDisposable
    {
        private readonly IDisposable? _disposable;

        public CheckedDisposableReader(ISizedBuffer buffer) : base(buffer)
        {
            _disposable = buffer as IDisposable;
        }

        public CheckedDisposableReader(IBuffer buffer, ulong byteCapacity)
            : base(buffer, byteCapacity)
        {
            _disposable = buffer as IDisposable;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _disposable?.Dispose();
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
    }
}
