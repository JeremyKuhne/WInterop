# WInterop

Managed wrappers for Windows APIs

## What it is

A set of portable libraries with P/Invokes and wrapper methods for Windows APIs. Currently the library set is split into WInterop.dll (Windows Store accessible APIs)
and WInterop.Desktop.dll (APIs that aren't available to Windows Store applications).

It is expected to be practically useful both as a component and as a place to document how to P/Invoke. Unlike existing documentation sites the code has version history
and can be tested, which should be more practical and hopefully ultimately more robust.

WInterop is in an early stage of development. As it expands expect breaking changes. Contributions and feedback are welcome.

## Goals

- Provide a repository with useful Windows API P/Invokes and wrappers
- Provide wrappers that provide a more managed friendly front-end for the P/Invokes
- Make it easy to write code that needs direct API access by consuming the package directly
- Document how to invoke specific APIs with the ability to test and view version history
- Provide utility code that makes it easier to write your own wrappers
- Expose all P/Invokes and data types publicly to allow for advanced/custom usage
- Make it easy to know what APIs are available on all platforms (including WinRT)
- Make it easy to find implemented wrappers
- Ease writing code that will run against different runtimes on Windows (.NET, .NET Core)
- Allow targeting Windows 7 and above (including WinRT) without writing separate libraries


