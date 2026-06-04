# Compatibility Harness

用于记录 ColorPicker 的兼容性决策、已完成改造和验证清单。

## 当前状态

- UI: WinForms
- Target framework: `.NET Framework 4.8`
- Platform target: `x86`
- Supported OS: Windows 7 SP1 / Windows 10 / Windows 11
- Build tool: Visual Studio / Build Tools + MSBuild

## 兼容性决策

- 选择 `.NET Framework 4.8`，覆盖 Windows 7 SP1 到 Windows 11。
- 保持 `x86`，兼容 32 位 Windows 7 和 64 位 Windows。
- 不继续支持 XP / Vista。
- 现代 .NET self-contained 发布暂不采用，避免扩大改造范围。

## 已完成

- [x] 从 `.NET Framework 4.0 Client Profile` 升级到 `.NET Framework 4.8`
- [x] 删除 Client Profile
- [x] 在程序启动时声明 DPI aware，改善高 DPI 模糊
- [x] 移除自定义 `app.manifest` 嵌入，避免 side-by-side 启动错误
- [x] 修复多显示器取色坐标问题
- [x] 取色改为 Win32 `GetDC/GetPixel` 单像素读取
- [x] 删除高频整屏截图和 `GC.Collect()`
- [x] 修复 RGB 非法输入崩溃
- [x] 清理已跟踪的 `.suo`、`obj`、zip 构建产物
- [x] 新增 GitHub Actions 手动发布流水线

## 待验证

- [ ] 使用 Visual Studio / Build Tools 编译 `Release|x86`
- [ ] Windows 7 SP1 + .NET Framework 4.8 运行验证
- [ ] Windows 10 运行验证
- [ ] Windows 11 运行验证
- [ ] 单屏取色
- [ ] 多屏取色，副屏在右侧
- [ ] 多屏取色，副屏在左侧或上方
- [ ] 100% / 125% / 150% / 200% DPI 缩放
- [ ] RGB 合法输入：`0-255`
- [ ] RGB 非法输入：非数字、负数、大于 255
- [ ] 双击复制 HEX / R / G / B
- [ ] GitHub Actions 发布 ZIP 到 Release

## 发布产物

GitHub Release 应包含：

- `ColorPicker-<tag>-<sha>.zip`
- ZIP 内仅包含 `ColorPicker.exe`
- Release notes 中的最后 commit 链接
- GitHub Actions runner 链接
- ZIP 的 MD5 和 SHA256

## 参考

- .NET Framework on Windows 11: https://learn.microsoft.com/en-us/dotnet/framework/install/on-windows-11
- .NET Framework system requirements: https://learn.microsoft.com/en-us/dotnet/framework/get-started/system-requirements
- .NET Framework lifecycle: https://learn.microsoft.com/en-us/lifecycle/products/microsoft-net-framework
