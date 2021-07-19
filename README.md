# WInterop

Managed wrappers for Windows APIs

## What it is

A set of .NET 5.0 libraries with P/Invokes and wrapper methods for Windows APIs.

 - WInterop.Desktop.dll (Windows platform APIs (Win32, NT))
 - WInterop.GdiPlus.dll (GDI+ APIs)
 - WInterop.Direct2d.dll (Direct2d, DirectWrite)

It is expected to be practically useful both as a component and as a place to document how to P/Invoke. Unlike existing documentation sites the code has
version history and can be tested, which should be more practical and hopefully ultimately more robust.

WInterop is in an early stage of development. As it expands expect breaking changes. Contributions and feedback are welcome.

As the library targets .NET 5.0 it needs Visual Studio 16.7 or higher for development.

## Goals

- Provide a repository with useful Windows API P/Invokes and wrappers
- Provide wrappers that provide a more managed friendly front-end for the P/Invokes
- Document how to invoke specific APIs with the ability to test and view version history
- Provide utility code that makes it easier to write your own wrappers
- Expose all P/Invokes and data types publicly to allow for advanced/custom usage
- Make it easy to find implemented wrappers

## Thinking

The key concept driving this repo is "how can I program Win32 apps in a C# style?".

Windows Forms is great, but carries significant weight. This is primarily from its API being developed 25 years ago. It didn't have access to modern C#/.NET concepts (including generics), depended on GDI+, and had to support Windows 98.
The public model for Windows Forms starts at `Control`, which is a massive, complicated base class for interacting with Windows.

The idea in this repo is to allow you to both program directly to Win32 in a C# manner (see [HelloWin](https://github.com/JeremyKuhne/WInterop/blob/main/src/Samples/Petzold/5th/HelloWin/Program.cs))
and also provide progressive layers of helper functionality to take drudgery away (see [Windows101](https://github.com/JeremyKuhne/WInterop/blob/main/src/Samples/Tutorial/Windows101/Program.cs)).
The top "WinForms-like" layer can be seen in the [Controls](https://github.com/JeremyKuhne/WInterop/blob/main/src/Samples/Tutorial/Controls/Program.cs) sample.

Note that the API here is very much in flux, more so as you move up in layers. Part of the goals is to drive the API design by scaling up functionality and writing sample apps.

