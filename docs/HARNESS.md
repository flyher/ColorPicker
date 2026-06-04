# ColorPicker 兼容性改造 Harness

## 目标

本文档用于记录 ColorPicker 源码审查结论，并作为后续逐步改造、验证和发布的执行清单。

当前项目是一个 Visual Studio 2010 风格的 WinForms 程序，已按低风险维护路线升级为 .NET Framework 4.8。程序功能简单，主要改造目标是提升 Windows 7 SP1 到 Windows 11 的兼容性。

## 当前项目状态

- 工程类型：WinForms 桌面程序
- 解决方案格式：Visual Studio 2010
- 目标框架：.NET Framework 4.8
- 平台目标：x86
- 主要代码文件：
  - `ColorPicker/Main.cs`
  - `ColorPicker/Main.Designer.cs`
  - `ColorPicker/Program.cs`
  - `ColorPicker/ColorPicker.csproj`
  - `ColorPicker/App.config`
  - `ColorPicker/app.manifest`

## .NET Framework 系统自带情况

| Windows 版本 | 系统自带 .NET Framework | 该系统最高可支持 |
|---|---:|---:|
| Windows XP | 无 | XP: 1.0 / XP SP2: 3.5 / XP SP3: 4.0.3 |
| Windows Vista | 3.0 | 4.6 |
| Windows 7 | 3.5 | 4.8 |
| Windows 8 | 4.5 | 4.6.1 |
| Windows 8.1 | 4.5.1 | 4.8 |
| Windows 10 1507 | 4.6 | 4.6.2 |
| Windows 10 22H2 | 4.8 | 4.8.1 |
| Windows 11 21H2 | 4.8 | 4.8.1 |
| Windows 11 22H2 及更新 | 4.8.1 | 4.8.1 |

参考资料：

- Microsoft .NET Framework installation guide: https://learn.microsoft.com/en-us/dotnet/framework/install/on-windows-11
- Microsoft .NET Framework system requirements: https://learn.microsoft.com/en-us/dotnet/framework/get-started/system-requirements
- Microsoft .NET Framework lifecycle: https://learn.microsoft.com/en-us/lifecycle/products/microsoft-net-framework

## 已发现问题

### 1. 多显示器取色错误（已修复）

位置：`ColorPicker/Main.cs`

原始逻辑使用：

- `Control.MousePosition` 获取全局鼠标坐标
- `Screen.PrimaryScreen.Bounds` 只截取主屏
- `Bitmap.GetPixel(pt.X, pt.Y)` 使用全局坐标直接访问主屏截图

问题：

- 鼠标在副屏时，坐标可能超出主屏截图范围。
- 副屏在主屏左侧或上方时，坐标可能为负数。
- 会触发 `ArgumentOutOfRangeException` 或取到错误颜色。

README 中也记录了该已知 bug。

### 2. 截图取色性能差（已修复）

位置：`ColorPicker/Main.cs`

原始逻辑每 50ms 创建一张主屏大小的 `Bitmap`，再执行整屏 `CopyFromScreen`，最后只读取一个像素。

问题：

- 4K 或多屏环境下内存分配和 GDI 压力较大。
- 高频创建大 Bitmap 会造成 UI 卡顿。
- 每次 tick 调用 `GC.Collect()` 会进一步造成停顿。

建议：

- 使用 Win32 `GetDC/GetPixel` 直接读取鼠标所在像素。
- 或只复制鼠标位置的 1x1 区域。
- 删除手动 `GC.Collect()`。

### 3. RGB 输入缺少校验（已修复）

位置：`ColorPicker/Main.cs`

原始逻辑使用 `Convert.ToInt32`，并只检查文本长度。

问题：

- 输入非数字会抛异常。
- 输入 `999` 这种长度合法但范围非法的值，会在 `Color.FromArgb` 抛异常。
- 负数也没有被拦截。

建议：

- 改用 `int.TryParse`。
- 将 RGB 限制在 `0-255`。
- 非法输入时保留当前颜色或提示用户。

### 4. 目标框架过旧（已修复）

位置：`ColorPicker/ColorPicker.csproj`

原始目标框架为 `.NET Framework 4.0 Client Profile`。

问题：

- .NET Framework 4.0 已结束支持。
- 现代 Windows 自带的是 .NET Framework 4.8 或 4.8.1。
- 旧 Client Profile 对新工具链、DPI、系统兼容配置支持较弱。

建议：

- 最小改造路线：升级到 `.NET Framework 4.8`。当前已完成。
- 现代化路线：迁移到 `net8.0-windows` 或更新的 Windows Desktop SDK。

### 5. 高 DPI 支持不足（已初步配置）

位置：`ColorPicker/Main.Designer.cs`

原始代码使用 `AutoScaleMode.Font`，没有 app manifest 或 app.config 中的 DPI aware 配置。当前已添加 `App.config` 和 `app.manifest`。

问题：

- 高分屏下 UI 可能模糊。
- 多显示器不同缩放比例时，坐标与像素映射可能不准确。

建议：

- .NET Framework 路线：添加 app.config / manifest，启用高 DPI 支持。当前已完成基础配置，仍需真机验证。
- 现代 .NET 路线：在 `Program.cs` 中调用 `Application.SetHighDpiMode(...)`。

### 6. 构建链偏旧

当前机器测试情况：

- 找到 `dotnet.exe`
- 但没有 .NET SDK
- `dotnet build ColorPicker.sln` 无法执行
- PATH 中没有找到 `msbuild`

说明：

- Runtime 用于运行程序。
- SDK / Developer Pack / MSBuild 用于编译程序。
- 当前项目需要 Visual Studio 或 Build Tools 才能可靠构建。

## 推荐兼容策略

### 推荐路线 A：低风险维护路线

适用目标：

- 主要兼容 Windows 10 / Windows 11
- 保留 WinForms
- 尽量少改现有代码
- 希望程序尽量免安装运行

改造内容：

1. 升级目标框架到 .NET Framework 4.8。
2. 去掉 Client Profile。
3. 平台目标从 x86 调整为 AnyCPU 或 x64。
4. 修复多屏取色。
5. 优化取色性能。
6. 添加 RGB 输入校验。
7. 添加 DPI aware 配置。

这是本项目最建议采用的路线。

### 推荐路线 B：现代化路线

适用目标：

- 希望长期维护。
- 希望使用现代 .NET SDK。
- 可以接受发布包更大。

改造内容：

1. 迁移到 SDK-style csproj。
2. 目标框架改为 `net8.0-windows` 或更新版本。
3. 使用 WinForms Windows Desktop SDK。
4. 发布为 self-contained，随程序携带运行时。
5. 使用现代高 DPI API。

注意：

- 现代 .NET Runtime 通常不是系统自带。
- 若不使用 self-contained 发布，需要用户另装运行时。

### 不推荐路线：继续兼容 XP/Vista

原因：

- 系统已过生命周期。
- 需要锁定过旧 .NET Framework。
- 高 DPI、多屏、TLS、安全更新和现代工具链都会受限。
- 对取色器这种桌面小工具，维护成本高于收益。

## 分阶段执行计划

### 阶段 1：修复运行时功能问题

目标：

- 不大改工程结构。
- 先让取色功能在现代多屏环境下稳定。

任务：

- [x] 修复多显示器取色坐标问题。
- [x] 避免每次取色截整屏。
- [x] 删除 `GC.Collect()`。
- [x] 修复 RGB 输入异常。
- [ ] 验证主屏、副屏、负坐标副屏场景。

验收标准：

- 鼠标位于任意显示器时都能取色。
- 快速移动鼠标时 UI 不明显卡顿。
- RGB 输入非法值时程序不崩溃。

### 阶段 2：升级 .NET Framework 兼容性

目标：

- 提升 Windows 10 / Windows 11 兼容性。

任务：

- [x] 将目标框架升级到 .NET Framework 4.8。
- [x] 移除 Client Profile。
- [x] 保持平台目标为 x86，以兼容 32 位 Windows 7。
- [x] 清理无用 using。
- [ ] 使用 Visual Studio / Build Tools 编译 Release。

验收标准：

- Release 构建成功。
- 在 Windows 10 / Windows 11 可直接运行。
- 不再依赖 .NET Framework 4.0 Client Profile。

### 阶段 3：增加高 DPI 支持

目标：

- 改善高分屏和多缩放显示器表现。

任务：

- [x] 添加 app.config 和 app manifest。
- [x] 声明 Windows 7 / Windows 8 / Windows 8.1 / Windows 10 / Windows 11 兼容。
- [x] 启用 DPI aware 基础配置。
- [ ] 测试 100%、125%、150%、200% 缩放。

验收标准：

- UI 不模糊或明显改善。
- 多显示器不同缩放比例下取色坐标正确。

### 阶段 4：发布与回归验证

目标：

- 形成可交付版本。

任务：

- [ ] 生成 Release 包。
- [ ] 记录运行环境要求。
- [ ] 添加简单使用说明。
- [ ] 回归测试复制色值、快捷键、RGB 手动输入、开始/停止取色。

验收标准：

- 程序可独立分发。
- README 中说明支持系统和依赖。
- 常见功能可正常使用。

## 验证矩阵

| 测试项 | Windows 10 | Windows 11 | 备注 |
|---|---|---|---|
| 单主屏取色 | 待测 | 待测 | 基础功能 |
| 双屏，副屏在右侧 | 待测 | 待测 | 常见场景 |
| 双屏，副屏在左侧 | 待测 | 待测 | 负坐标场景 |
| 双屏，缩放比例不同 | 待测 | 待测 | DPI 场景 |
| RGB 输入 0-255 | 待测 | 待测 | 正常输入 |
| RGB 输入非法字符 | 待测 | 待测 | 不应崩溃 |
| 双击复制 HEX | 待测 | 待测 | 剪贴板 |
| 双击复制 R/G/B | 待测 | 待测 | 剪贴板 |

## 下一步建议

下一步需要使用 Visual Studio 或 Build Tools 编译 Release，并在 Windows 7 SP1、Windows 10、Windows 11 上执行验证矩阵。
