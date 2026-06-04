# Changelog

本文件用于保留 ColorPicker 每次发布或维护改造的记录。

## Unreleased

### Added

- 新增 `docs/HARNESS.md`，记录源码审查结论、兼容性策略、阶段性改造计划和验证矩阵。
- 新增 `docs/CHANGELOG.md`，用于长期保留发布日志。
- 新增 `README.en.md` 英文说明文档，并在中英文 README 顶部互相标注入口。
- 新增 `ColorPicker/App.config`，配置 .NET Framework 4.8 运行时和 WinForms 高 DPI 支持。
- 新增 `ColorPicker/app.manifest`，声明 Windows 7 / Windows 8 / Windows 8.1 / Windows 10 / Windows 11 兼容。
- README 增加现代化项目说明、兼容系统、构建方式和维护文档入口。

### Changed

- 项目目标框架从 `.NET Framework 4.0 Client Profile` 升级到 `.NET Framework 4.8`。
- 删除 `TargetFrameworkProfile=Client`，改用完整 .NET Framework 4.8。
- 平台目标保持 `x86`，用于兼容 32 位 Windows 7 SP1 以及 64 位 Windows。
- 屏幕取色方式从整屏截图改为 Win32 `GetDC/GetPixel` 单像素读取。
- README 改为更适合现代开源项目展示的结构。

### Fixed

- 修复多显示器环境下只能在主屏取色的问题。
- 修复副屏在主屏左侧或上方时，鼠标坐标可能为负数导致取色异常的问题。
- 修复 RGB 输入非数字、负数或大于 255 时可能崩溃的问题。
- 开启窗体 `KeyPreview`，提升 `P` / `E` 快捷键在不同焦点状态下的可用性。

### Optimized

- 删除高频 `GC.Collect()`，避免取色过程中 UI 卡顿。
- 避免每 50ms 创建整屏 `Bitmap`，降低内存分配和 GDI 压力。
- 清理部分旧模板遗留的无用 `using`。

### Verification

- 已完成 `ColorPicker.csproj`、`App.config`、`app.manifest` XML 解析检查。
- 当前环境未安装 .NET SDK / MSBuild，尚未完成 Release 构建验证。

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
