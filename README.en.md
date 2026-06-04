# ColorPicker

English | [简体中文](README.md)

A lightweight screen color picker for Windows.

![ColorPicker screenshot](file/screenshot.png)

## Features

- Pick the color under the mouse in real time
- Display and copy HEX / RGB values
- Edit RGB values manually after stopping live picking
- Support multi-monitor color picking, including mixed DPI scaling
- Target .NET Framework 4.8

## Usage

Click `Start(P)` to start picking, and `Stop(E)` to stop.

Double-click the HEX, R, G, or B field to copy its value.

## Compatibility

- Windows 7 SP1 / Windows 10 / Windows 11
- Requires .NET Framework 4.8 or a later .NET Framework 4.x update
- Windows 7 SP1 requires .NET Framework 4.8 to be installed manually

## Build

Use Visual Studio 2019 / 2022 or Build Tools:

- Target framework: `.NET Framework 4.8`
- Platform target: `x86`
- Configuration: `Release`

## Release

GitHub Actions provides a manual release workflow:

[.github/workflows/release.yml](.github/workflows/release.yml)

Run `Build and Release`, select the workflow branch, and enter the version number. The ZIP contains only `ColorPicker.exe`.

## Docs

- [Changelog](docs/CHANGELOG.md)
- [Compatibility and validation checklist](docs/HARNESS.md)

## License

GPL
