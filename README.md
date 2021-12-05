# WInterop

Managed wrappers for Windows APIs

## What it is

A set of .NET 6.0 libraries light-weight wrapper methods for Windows APIs.

 - WInterop.Desktop.dll (Windows platform APIs (Win32, NT))
 - WInterop.GdiPlus.dll (GDI+ APIs)
 - WInterop.Direct2d.dll (Direct2d, DirectWrite)

It is expected to be practically useful both as a component and as a place to document how to utilize various Windows
technologies. Initially this was a repository of P/Invokes as well, but now there are automatically generated low-level
wrappers that make this repetitive. This library uses [TerraFX](https://github.com/terrafx/terrafx.interop.windows) as
the P/Invoke / COM function pointer layer.

WInterop is in an early stage of development. As it expands expect breaking changes. Contributions and feedback are welcome.

If using Visual Studio you'll need 17.0 or later as this library targets .NET 6.0.

## Goals

- Provide wrappers that provide a more managed friendly front-end for Windows APIs
- Document how to invoke specific APIs with the ability to test
- Provide utility code that makes it easier to write your own wrappers
- Make it easy to find implemented wrappers

## Thinking

The key concept driving this repo is "how can I program Win32 apps in a C# style?".

Windows Forms is great, but carries significant weight. This is primarily from its API being developed over 25 years ago.
It didn't have access to modern C#/.NET concepts (including generics), depended on GDI+, and had to support Windows 98.
The public model for Windows Forms starts at `Control`, which is a massive, complicated base class for interacting with Windows.

The idea in this repo is to allow you to both program directly to Win32 in a C# manner (see [HelloWin](https://github.com/JeremyKuhne/WInterop/blob/main/src/Samples/Petzold/5th/HelloWin/Program.cs))
and also provide progressive layers of helper functionality to take drudgery away (see [Windows101](https://github.com/JeremyKuhne/WInterop/blob/main/src/Samples/Tutorial/Windows101/Program.cs)).
The top "WinForms-like" layer can be seen in the [Controls](https://github.com/JeremyKuhne/WInterop/blob/main/src/Samples/Tutorial/Controls/Program.cs) sample.

Note that the API here is very much in flux, more so as you move up in layers. Part of the goals is to drive the API design by scaling up functionality and writing sample apps.

