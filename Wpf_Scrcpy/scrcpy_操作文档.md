# scrcpy 3.3.1 操作文档

> **作者：** MiniMax Agent  
> **版本：** scrcpy 3.3.1  
> **项目地址：** https://github.com/Genymobile/scrcpy  
> **文档更新时间：** 2025-06-21

## 目录

- [概述](#概述)
- [基本用法](#基本用法)
- [连接选项](#连接选项)
- [视频配置](#视频配置)
- [音频配置](#音频配置)
- [相机选项](#相机选项)
- [显示配置](#显示配置)
- [输入控制](#输入控制)
- [录制选项](#录制选项)
- [窗口设置](#窗口设置)
- [高级功能](#高级功能)
- [快捷键列表](#快捷键列表)
- [环境变量](#环境变量)
- [退出状态码](#退出状态码)

## 概述

scrcpy是一个功能强大的Android设备屏幕镜像工具，支持通过USB或TCP/IP连接，提供低延迟、高质量的屏幕镜像和控制功能。

## 基本用法

```bash
# 基本镜像命令
scrcpy.exe

# 显示帮助信息
scrcpy.exe -h
scrcpy.exe --help

# 显示版本信息
scrcpy.exe -v
scrcpy.exe --version
```

## 连接选项

### 设备选择

| 选项 | 描述 |
|------|------|
| `-s, --serial=serial` | 指定设备序列号（多设备连接时必需） |
| `-d, --select-usb` | 使用USB设备（当只有一个USB设备时） |
| `-e, --select-tcpip` | 使用TCP/IP设备（当只有一个TCP/IP设备时） |

### TCP/IP连接

| 选项 | 描述 |
|------|------|
| `--tcpip[=[+]ip[:port]]` | 配置并连接TCP/IP设备<br>- 无地址：自动查找当前设备IP<br>- 有地址：连接到指定地址<br>- `+`前缀：强制重连 |
| `-p, --port=port[:port]` | 设置客户端监听的TCP端口范围<br>默认：27183:27199 |
| `--tunnel-host=ip` | 设置adb隧道的IP地址<br>默认：localhost |
| `--tunnel-port=port` | 设置adb隧道的TCP端口<br>默认：0（自动） |
| `--force-adb-forward` | 不尝试使用"adb reverse"连接设备 |

### ADB管理

| 选项 | 描述 |
|------|------|
| `--kill-adb-on-close` | scrcpy终止时杀死adb进程 |

## 视频配置

### 基本视频设置

| 选项 | 描述 |
|------|------|
| `-m, --max-size=value` | 限制视频宽度和高度的最大值<br>默认：0（无限制） |
| `-b, --video-bit-rate=value` | 设置视频比特率（支持K、M后缀）<br>默认：8M（8000000） |
| `--max-fps=value` | 限制屏幕捕获帧率<br>（Android 10+官方支持） |
| `--video-buffer=ms` | 添加视频帧显示前的缓冲延迟<br>默认：0（无缓冲） |

### 编码器设置

| 选项 | 描述 |
|------|------|
| `--video-codec=name` | 选择视频编解码器<br>可选：h264、h265、av1<br>默认：h264 |
| `--video-encoder=name` | 使用特定的MediaCodec视频编码器 |
| `--video-codec-options=key[:type]=value[,...]` | 设置设备视频编码器选项<br>类型：int、long、float、string |

### 视频源

| 选项 | 描述 |
|------|------|
| `--video-source=source` | 选择视频源<br>- display：显示屏<br>- camera：相机（Android 12+）<br>默认：display |

### 视频处理

| 选项 | 描述 |
|------|------|
| `--angle=degrees` | 按自定义角度旋转视频内容（顺时针） |
| `--no-video` | 禁用视频转发 |
| `--no-video-playback` | 禁用计算机上的视频播放 |
| `--no-mipmaps` | 禁用mipmap生成（用于改善缩放质量） |
| `--no-downsize-on-error` | MediaCodec错误时不自动降低分辨率重试 |

## 音频配置

### 基本音频设置

| 选项 | 描述 |
|------|------|
| `--no-audio` | 禁用音频转发 |
| `--no-audio-playback` | 禁用计算机上的音频播放 |
| `--require-audio` | 音频捕获失败时使scrcpy失败 |

### 音频编码

| 选项 | 描述 |
|------|------|
| `--audio-bit-rate=value` | 设置音频比特率（支持K、M后缀）<br>默认：128K（128000） |
| `--audio-codec=name` | 选择音频编解码器<br>可选：opus、aac、flac、raw<br>默认：opus |
| `--audio-encoder=name` | 使用特定的MediaCodec音频编码器 |
| `--audio-codec-options=key[:type]=value[,...]` | 设置设备音频编码器选项 |

### 音频源

| 选项 | 描述 |
|------|------|
| `--audio-source=source` | 选择音频源<br>- output：完整音频输出<br>- playback：音频播放<br>- mic：麦克风<br>- mic-unprocessed：未处理麦克风<br>- mic-camcorder：录像麦克风<br>- mic-voice-recognition：语音识别麦克风<br>- mic-voice-communication：语音通信麦克风<br>- voice-call：语音通话<br>- voice-call-uplink：语音通话上行<br>- voice-call-downlink：语音通话下行<br>- voice-performance：实时性能音频<br>默认：output |

### 音频缓冲

| 选项 | 描述 |
|------|------|
| `--audio-buffer=ms` | 配置音频缓冲延迟（毫秒）<br>默认：50 |
| `--audio-output-buffer=ms` | 配置SDL音频输出缓冲区大小<br>默认：5 |
| `--audio-dup` | 复制音频（捕获并保持设备播放）<br>仅适用于--audio-source=playback |

## 相机选项

### 相机选择

| 选项 | 描述 |
|------|------|
| `--camera-id=id` | 指定要镜像的设备相机ID |
| `--camera-facing=facing` | 按朝向选择设备相机<br>可选：front、back、external |
| `--list-cameras` | 列出设备相机 |

### 相机配置

| 选项 | 描述 |
|------|------|
| `--camera-size=<width>x<height>` | 指定明确的相机捕获尺寸 |
| `--camera-ar=ar` | 按宽高比选择相机尺寸（±10%）<br>可选：sensor、4:3、1.6等 |
| `--camera-fps=value` | 指定相机捕获帧率<br>默认：30fps |
| `--camera-high-speed` | 启用高速相机捕获模式 |
| `--list-camera-sizes` | 列出有效的相机捕获尺寸 |

## 显示配置

### 显示选择

| 选项 | 描述 |
|------|------|
| `--display-id=id` | 指定要镜像的设备显示器ID<br>默认：0 |
| `--list-displays` | 列出设备显示器 |

### 显示方向

| 选项 | 描述 |
|------|------|
| `--display-orientation=value` | 设置初始显示方向<br>可选：0、90、180、270、flip0-270<br>默认：0 |
| `--capture-orientation=value` | 设置捕获视频方向<br>支持@前缀锁定旋转 |
| `--orientation=value` | 等同于同时设置display-orientation和record-orientation |

### 新显示器

| 选项 | 描述 |
|------|------|
| `--new-display[=[<width>x<height>][/<dpi>]]` | 创建指定分辨率和密度的新显示器<br>示例：<br>- `--new-display=1920x1080`<br>- `--new-display=1920x1080/420`<br>- `--new-display=/240` |

### 虚拟显示器

| 选项 | 描述 |
|------|------|
| `--no-vd-destroy-content` | 禁用虚拟显示器"移除时销毁内容"标志 |
| `--no-vd-system-decorations` | 禁用虚拟显示器系统装饰标志 |

### 屏幕裁剪

| 选项 | 描述 |
|------|------|
| `--crop=width:height:x:y` | 在服务器上裁剪设备屏幕<br>值以设备自然方向表示 |

## 输入控制

### 基本控制

| 选项 | 描述 |
|------|------|
| `-n, --no-control` | 禁用设备控制（只读镜像） |
| `--shortcut-mod=key[+...][,...]` | 指定scrcpy快捷键修饰符<br>可选：lctrl、rctrl、lalt、ralt、lsuper、rsuper<br>默认：lalt,lsuper |

### 键盘输入

| 选项 | 描述 |
|------|------|
| `-K` | 等同于--keyboard=uhid，OTG模式下为--keyboard=aoa |
| `--keyboard=mode` | 选择键盘输入发送方式<br>- disabled：禁用<br>- sdk：Android系统API<br>- uhid：物理HID键盘模拟<br>- aoa：AOAv2协议模拟 |
| `--no-key-repeat` | 按住键时不转发重复键事件 |
| `--raw-key-events` | 为所有输入键注入键事件，忽略文本事件 |
| `--prefer-text` | 将字母字符和空格作为文本事件注入 |

### 鼠标输入

| 选项 | 描述 |
|------|------|
| `-M` | 等同于--mouse=uhid，OTG模式下为--mouse=aoa |
| `--mouse=mode` | 选择鼠标输入发送方式<br>选项同keyboard |
| `--mouse-bind=xxxx[:xxxx]` | 配置辅助点击绑定<br>字符：+（转发）、-（忽略）、b（返回）、h（主页）、s（应用切换）、n（通知面板）<br>默认：SDK鼠标为'bhsn:++++'，AOA/UHID为'++++:bhsn' |
| `--no-mouse-hover` | 不转发鼠标悬停事件 |

### 游戏手柄

| 选项 | 描述 |
|------|------|
| `-G` | 等同于--gamepad=uhid，OTG模式下为--gamepad=aoa |
| `--gamepad=mode` | 选择游戏手柄输入发送方式<br>- disabled：禁用<br>- uhid：UHID内核模块模拟<br>- aoa：AOAv2协议模拟 |

### OTG模式

| 选项 | 描述 |
|------|------|
| `--otg` | 运行OTG模式：模拟物理键盘和鼠标<br>此模式下不需要adb，禁用镜像<br>仅适用于USB连接 |

### 剪贴板

| 选项 | 描述 |
|------|------|
| `--no-clipboard-autosync` | 禁用剪贴板自动同步 |
| `--legacy-paste` | 在Ctrl+v时将计算机剪贴板文本作为键事件序列注入 |

## 录制选项

### 基本录制

| 选项 | 描述 |
|------|------|
| `-r, --record=file.mp4` | 录制屏幕到文件 |
| `--record-format=format` | 强制录制格式<br>可选：mp4、mkv、m4a、mka、opus、aac、flac、wav |
| `--record-orientation=value` | 设置录制方向<br>可选：0、90、180、270 |

### 录制控制

| 选项 | 描述 |
|------|------|
| `--time-limit=seconds` | 设置最大镜像时间（秒） |

## 窗口设置

### 窗口状态

| 选项 | 描述 |
|------|------|
| `-f, --fullscreen` | 以全屏模式启动 |
| `--always-on-top` | 使scrcpy窗口始终置顶 |
| `--no-window` | 禁用scrcpy窗口（暗示--no-video-playback） |
| `--window-borderless` | 禁用窗口装饰（无边框窗口） |

### 窗口位置和大小

| 选项 | 描述 |
|------|------|
| `--window-title=text` | 设置自定义窗口标题 |
| `--window-x=value` | 设置初始窗口水平位置<br>默认：auto |
| `--window-y=value` | 设置初始窗口垂直位置<br>默认：auto |
| `--window-width=value` | 设置初始窗口宽度<br>默认：0（自动） |
| `--window-height=value` | 设置初始窗口高度<br>默认：0（自动） |

## 高级功能

### 设备管理

| 选项 | 描述 |
|------|------|
| `-t, --show-touches` | 启动时启用"显示触摸"，退出时恢复 |
| `-w, --stay-awake` | 设备插电时保持scrcpy运行期间设备唤醒 |
| `-S, --turn-screen-off` | 立即关闭设备屏幕 |
| `--no-power-on` | 启动时不为设备供电 |
| `--power-off-on-close` | 关闭scrcpy时关闭设备屏幕 |
| `--screen-off-timeout=seconds` | 设置scrcpy运行时的屏幕关闭超时 |

### 应用管理

| 选项 | 描述 |
|------|------|
| `--list-apps` | 列出设备上安装的Android应用 |
| `--start-app=name` | 启动Android应用<br>- `?`前缀：模糊匹配应用名<br>- `+`前缀：启动前强制停止<br>示例：`--start-app=+?firefox` |

### 文件传输

| 选项 | 描述 |
|------|------|
| `--push-target=path` | 设置拖放文件到设备的目标目录<br>默认：/sdcard/Download/ |

### 系统设置

| 选项 | 描述 |
|------|------|
| `--disable-screensaver` | scrcpy运行时禁用屏保 |
| `--no-cleanup` | 禁用退出时的清理（移除服务器二进制文件、恢复设备状态） |

### IME设置

| 选项 | 描述 |
|------|------|
| `--display-ime-policy=value` | 设置IME显示位置策略<br>- local：本地显示<br>- fallback：后备显示<br>- hide：隐藏 |

### 性能和调试

| 选项 | 描述 |
|------|------|
| `--print-fps` | 启动FPS计数器，在控制台打印帧率日志 |
| `-V, --verbosity=value` | 设置日志级别<br>可选：verbose、debug、info、warn、error<br>默认：info |
| `--list-encoders` | 列出设备上可用的视频和音频编码器 |

### 渲染设置

| 选项 | 描述 |
|------|------|
| `--render-driver=name` | 请求SDL使用指定的渲染驱动<br>可选：direct3d、opengl、opengles2、opengles、metal、software |

### V4L2（仅Linux）

| 选项 | 描述 |
|------|------|
| `--v4l2-sink=/dev/videoN` | 输出到v4l2loopback设备<br>仅Linux可用 |
| `--v4l2-buffer=ms` | V4L2推送帧前添加缓冲延迟<br>默认：0<br>仅Linux可用 |

### 其他

| 选项 | 描述 |
|------|------|
| `-N, --no-playback` | 禁用计算机上的视频和音频播放 |
| `--pause-on-exit[=mode]` | 配置退出时暂停<br>可选：true、false、if-error<br>默认：false |

## 快捷键列表

> **说明：** MOD是快捷键修饰符，默认为左Alt或左Super，可通过--shortcut-mod配置。

### 显示控制

| 快捷键 | 功能 |
|--------|------|
| `MOD+f` | 切换全屏模式 |
| `MOD+Left` | 向左旋转显示 |
| `MOD+Right` | 向右旋转显示 |
| `MOD+Shift+Left/Right` | 水平翻转显示 |
| `MOD+Shift+Up/Down` | 垂直翻转显示 |
| `MOD+z` | 暂停或重新暂停显示 |
| `MOD+Shift+z` | 取消暂停显示 |
| `MOD+Shift+r` | 重置视频捕获/编码 |

### 窗口操作

| 快捷键 | 功能 |
|--------|------|
| `MOD+g` | 调整窗口为1:1（像素完美） |
| `MOD+w` 或 双击黑边 | 调整窗口移除黑边 |

### 设备操作

| 快捷键 | 功能 |
|--------|------|
| `MOD+h` 或 中键点击 | 点击HOME |
| `MOD+b` 或 `MOD+Backspace` 或 右键（屏幕开启时） | 点击BACK |
| `MOD+s` 或 第4键点击 | 点击APP_SWITCH |
| `MOD+m` | 点击MENU |
| `MOD+Up` | 点击VOLUME_UP |
| `MOD+Down` | 点击VOLUME_DOWN |
| `MOD+p` | 点击POWER（开/关屏幕） |
| 右键（屏幕关闭时） | 唤醒 |
| `MOD+o` | 关闭设备屏幕（保持镜像） |
| `MOD+Shift+o` | 打开设备屏幕 |
| `MOD+r` | 旋转设备屏幕 |

### 通知和状态

| 快捷键 | 功能 |
|--------|------|
| `MOD+n` 或 第5键点击 | 展开通知面板 |
| `MOD+Shift+n` | 收起通知面板 |

### 剪贴板操作

| 快捷键 | 功能 |
|--------|------|
| `MOD+c` | 复制到剪贴板（Android 7+） |
| `MOD+x` | 剪切到剪贴板（Android 7+） |
| `MOD+v` | 复制计算机剪贴板到设备然后粘贴（Android 7+） |
| `MOD+Shift+v` | 将计算机剪贴板文本作为键事件序列注入 |

### 其他功能

| 快捷键 | 功能 |
|--------|------|
| `MOD+k` | 打开设备上的键盘设置（仅HID键盘） |
| `MOD+i` | 启用/禁用FPS计数器 |

### 手势操作

| 操作 | 功能 |
|------|------|
| `Ctrl+点击拖动` | 从屏幕中心缩放和旋转 |
| `Shift+点击拖动` | 垂直倾斜（双指滑动） |
| `Ctrl+Shift+点击拖动` | 水平倾斜（双指滑动） |

### 文件操作

| 操作 | 功能 |
|------|------|
| 拖放APK文件 | 从计算机安装APK |
| 拖放非APK文件 | 推送文件到设备（参见--push-target） |

## 环境变量

| 变量 | 描述 |
|------|------|
| `ADB` | adb可执行文件路径 |
| `ANDROID_SERIAL` | 未指定选择器时使用的设备序列号 |
| `SCRCPY_ICON_PATH` | 程序图标路径 |
| `SCRCPY_SERVER_PATH` | 服务器二进制文件路径 |

## 退出状态码

| 状态码 | 描述 |
|--------|------|
| 0 | 正常程序终止 |
| 1 | 启动失败 |
| 2 | 运行时设备断开连接 |

## 使用示例

### 基本连接

```bash
# 基本镜像
scrcpy.exe

# 连接指定设备
scrcpy.exe -s 1234567890abcdef

# TCP/IP连接
scrcpy.exe --tcpip=192.168.1.100:5555

# 仅显示，不控制
scrcpy.exe -n
```

### 质量设置

```bash
# 高质量镜像
scrcpy.exe -b 15M -m 1920

# 低延迟设置
scrcpy.exe --video-buffer=0 --audio-buffer=20

# 录制屏幕
scrcpy.exe -r recording.mp4
```

### 窗口配置

```bash
# 全屏启动
scrcpy.exe -f

# 自定义窗口
scrcpy.exe --window-title="我的设备" --window-width=800 --window-height=600

# 始终置顶
scrcpy.exe --always-on-top
```

### 高级用法

```bash
# OTG模式（无需ADB）
scrcpy.exe --otg

# 新虚拟显示器
scrcpy.exe --new-display=1920x1080/420

# 启动特定应用
scrcpy.exe --start-app=com.android.chrome

# 相机镜像
scrcpy.exe --video-source=camera --camera-facing=front
```

---

**提示：** 更多详细信息和最新功能，请访问 [scrcpy GitHub项目页面](https://github.com/Genymobile/scrcpy)。