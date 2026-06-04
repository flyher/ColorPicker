# ColorPicker

[English](README.en.md) | 简体中文

一个轻量的 Windows 屏幕取色器。

![ColorPicker screenshot](file/screenshot.png)

## 功能

- 实时读取鼠标所在像素颜色
- 显示并复制 HEX / RGB 色值
- 停止取色后可手动输入 RGB 调色
- 支持多显示器取色
- 面向 .NET Framework 4.8

## 使用

点击 `Start(P)` 开始取色，点击 `Stop(E)` 停止取色。

双击 HEX、R、G、B 输入框可复制对应值。

## 兼容性

- Windows 7 SP1 / Windows 10 / Windows 11
- 需要 .NET Framework 4.8 或更高的 .NET Framework 4.x
- Windows 7 SP1 需要手动安装 .NET Framework 4.8

## 构建

建议使用 Visual Studio 2019 / 2022 或 Build Tools：

- Target framework: `.NET Framework 4.8`
- Platform target: `x86`
- Configuration: `Release`

## 发布

GitHub Actions 提供手动发布流水线：

[.github/workflows/release.yml](.github/workflows/release.yml)

运行 `Build and Release`，选择 workflow 分支并输入 release tag。ZIP 内仅包含 `ColorPicker.exe`。

## 文档

- [发布日志](docs/CHANGELOG.md)
- [兼容性与验证清单](docs/HARNESS.md)

## License

GPL
