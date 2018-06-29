using System;
using WInterop.Desktop.Console;

namespace CoreConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            uint inputCodePage = ConsoleMethods.GetConsoleInputCodePage();
            uint ouputCodePage = ConsoleMethods.GetConsoleOuputCodePage();

        }
    }
}
