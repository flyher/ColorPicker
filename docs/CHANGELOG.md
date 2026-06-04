# Changelog

## 2026-06-04

- 升级到 `.NET Framework 4.8`，删除 Client Profile，保持 `x86` 平台目标。
- 修复多显示器取色问题，包括副屏负坐标场景。
- 取色改为 Win32 `GetDC/GetPixel` 单像素读取，避免高频整屏截图。
- 删除高频 `GC.Collect()`，降低取色时的卡顿风险。
- 修复 RGB 非数字、负数或大于 255 时可能崩溃的问题。
- 开启窗体 `KeyPreview`，提升 `P` / `E` 快捷键可用性。
- 移除自定义 `app.manifest` 嵌入，修复 side-by-side 启动错误。
- 在程序启动时声明 DPI aware，改善高 DPI 下文字发糊问题。
- 新增中文 / 英文 README、兼容性清单和发布日志文档。
- 新增 GitHub Actions 手动发布流水线，构建 EXE、压缩 ZIP 并发布到 GitHub Release。
- Release notes 包含构建分支、最后 commit 链接、runner 链接，以及 ZIP 的 MD5 / SHA256。
- ZIP 包保持单文件分发，仅包含 `ColorPicker.exe`。
- GitHub Actions 官方 actions 更新到 `actions/checkout@v6.0.3` 和 `actions/upload-artifact@v7.0.1`。
- 清理已跟踪的 `.suo`、`obj`、zip 构建产物。

## 2017-05-01

- Stop 状态下支持 R、G、B 输入色值调色。

## 2017-03-04

- 已知问题：取多屏颜色时程序错误，只能将需要取色的位置移到主屏。

## 2016-03-10

- 增加 R、G、B 色值显示。

## 2014-07-01

- 修正某些颜色、某些青色取色失败的问题。
- 原因是取色数值首位为 0 时会被忽略，例如 `#04A85F` 会丢失首位 `0`。

## 2014-04-14

- 增加取色快捷键。
- 增加双击复制代码。
- 已知问题：某些蓝色和某些青色取色失败。

## 2014-02-27

- 创建 ColorPicker 项目。
