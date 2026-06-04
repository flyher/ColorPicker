# Changelog

## Unreleased

### Added

- 新增英文 README、兼容性清单和发布日志文档。
- 新增 GitHub Actions 手动发布流水线，构建 EXE、压缩 ZIP 并发布到 GitHub Release。
- 新增 `App.config` 高 DPI 配置和 `app.manifest` Windows 兼容声明。
- Release notes 自动包含 build ref、commit、runner 信息，以及 ZIP 的 MD5 / SHA256。

### Changed

- 从 `.NET Framework 4.0 Client Profile` 升级到 `.NET Framework 4.8`。
- 删除 Client Profile，保持 `x86` 平台目标。
- 取色方式从整屏截图改为 Win32 `GetDC/GetPixel` 单像素读取。
- 精简 README，仅保留使用、兼容性、构建、发布和文档入口。
- 将 GitHub Actions 的 `build_ref` 改为可选，默认构建手动触发页面选择的分支。

### Fixed

- 修复多显示器取色问题，包括副屏负坐标场景。
- 修复 RGB 非数字、负数或大于 255 时可能崩溃的问题。
- 开启窗体 `KeyPreview`，提升 `P` / `E` 快捷键在不同焦点状态下的可用性。

### Optimized

- 删除高频 `GC.Collect()`，避免取色过程中 UI 卡顿。
- 避免每 50ms 创建整屏 `Bitmap`，降低内存分配和 GDI 压力。
- 清理无用 `using`，并移除已跟踪的 `.suo`、`obj`、zip 构建产物。

## 2017-05-01

### Added

- Stop 状态下支持 R、G、B 输入色值调色。

## 2017-03-04

### Known Issues

- 取多屏颜色时程序错误。
- 原说明建议将需要取色的地方移到主屏。

该问题已在当前维护版本中修复。

## 2016-03-10

### Added

- 增加 R、G、B 色值显示。

## 2014-07-01

### Fixed

- 修正某些颜色、某些青色取色失败的问题。
- 原因是取色数值首位为 0 时会被忽略，例如 `#04A85F` 会丢失首位 `0`。

## 2014-04-14

### Added

- 增加取色快捷键。
- 增加双击复制代码。

### Known Issues

- 某些蓝色和某些青色取色失败。

该问题已于 2014-07-01 修复。

## 2014-02-27

### Added

- 创建 ColorPicker 项目。
