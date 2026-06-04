# ColorPicker

English | [简体中文](README.md)

A lightweight, direct screen color picker for Windows desktop.

![ColorPicker screenshot](file/screenshot.png)

## Highlights

- Pick the color of the pixel under the mouse in real time
- Display HEX, R, G, and B values
- Double-click to copy the HEX value or a single RGB channel value
- Edit RGB values manually after stopping live picking
- Fixes the legacy multi-monitor picking issue
- Uses Win32 single-pixel reading instead of frequent full-screen captures
- Targets .NET Framework 4.8 for a compatibility path from Windows 7 SP1 to Windows 11
- Includes baseline high DPI configuration for modern displays

## Quick Start

Click `Start(P)` to begin picking colors. Move the mouse and the current pixel color updates in real time.

Click `Stop(E)` to edit R, G, and B values manually. The preview panel updates with the edited color.

Copy behavior:

- Double-click the HEX field to copy the full color value, for example `#04A85F`
- Double-click the R / G / B fields to copy a single channel value

## Compatibility

Recommended operating systems:

- Windows 7 SP1
- Windows 10
- Windows 11

Runtime requirement:

- .NET Framework 4.8 or a later in-place .NET Framework 4.x update

Notes:

- Recent Windows 10 versions usually include .NET Framework 4.8.
- Windows 11 usually includes .NET Framework 4.8 or 4.8.1.
- Windows 7 SP1 requires .NET Framework 4.8 to be installed manually.

## Build

The project keeps the traditional WinForms project structure and is intended to be built with Visual Studio or Build Tools.

Recommended environment:

- Visual Studio 2019 / 2022
- .NET Framework 4.8 Developer Pack
- MSBuild

Build configuration:

- Target framework: `.NET Framework 4.8`
- Platform target: `x86`
- Output type: `WinExe`

## Modernization Notes

The first compatibility modernization pass has been completed:

- Upgraded from `.NET Framework 4.0 Client Profile` to `.NET Framework 4.8`
- Removed Client Profile
- Added `App.config` high DPI configuration
- Added `app.manifest` OS compatibility declaration
- Fixed multi-monitor coordinate handling
- Removed frequent `GC.Collect()`
- Fixed crashes caused by invalid RGB input

Roadmap and validation matrix:

[docs/HARNESS.md](docs/HARNESS.md)

## Release Notes

Release and maintenance notes are tracked in:

[docs/CHANGELOG.md](docs/CHANGELOG.md)

## Project History

The original project was created in 2014 and later added keyboard shortcuts, double-click copy, RGB display, and manual RGB editing. See Release Notes for the full history.

This maintained version focuses on modern Windows usage, especially multi-monitor support, high DPI behavior, performance, and build compatibility.

## License

Code in the ColorPicker project is licensed under the GPL.
