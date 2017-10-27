using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.Storage.Pickers;
using Microsoft.Win32.SafeHandles;

namespace Tests.UniversalWindows
{
    class ExtensionTests
    {
        public void Foo()
        {
            var picker = new FileOpenPicker();
            using (SafeFileHandle handle = picker.PickSingleFileAsync().GetResults().CreateSafeFileHandle())
            {
                StreamReader reader = new StreamReader(new FileStream(handle, FileAccess.Read));
                // ...
            }
        }
    }
}
