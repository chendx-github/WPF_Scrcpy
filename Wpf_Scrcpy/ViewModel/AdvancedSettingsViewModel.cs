using GalaSoft.MvvmLight;
using System;

namespace Wpf_Scrcpy.ViewModel
{
    public class AdvancedSettingsViewModel : ViewModelBase
    {
        // 音频设置
        private string _audioCodec = "opus";
        public string AudioCodec
        {
            get => _audioCodec;
            set => Set(ref _audioCodec, value);
        }

        private string _audioBitRate = "128K";
        public string AudioBitRate
        {
            get => _audioBitRate;
            set => Set(ref _audioBitRate, value);
        }

        private string _audioSource = "output";
        public string AudioSource
        {
            get => _audioSource;
            set => Set(ref _audioSource, value);
        }

        private string _audioBuffer = "50";
        public string AudioBuffer
        {
            get => _audioBuffer;
            set => Set(ref _audioBuffer, value);
        }

        private bool _noAudio = false;
        public bool NoAudio
        {
            get => _noAudio;
            set => Set(ref _noAudio, value);
        }        private bool _audioDup = false;
        public bool AudioDup
        {
            get => _audioDup;
            set => Set(ref _audioDup, value);
        }

        private bool _requireAudio = false;
        public bool RequireAudio
        {
            get => _requireAudio;
            set => Set(ref _requireAudio, value);
        }

        // 相机设置
        private string _cameraFacing = "back";
        public string CameraFacing
        {
            get => _cameraFacing;
            set => Set(ref _cameraFacing, value);
        }

        private string _cameraSize = "";
        public string CameraSize
        {
            get => _cameraSize;
            set => Set(ref _cameraSize, value);
        }

        private string _cameraFps = "30";
        public string CameraFps
        {
            get => _cameraFps;
            set => Set(ref _cameraFps, value);
        }

        private bool _cameraHighSpeed = false;
        public bool CameraHighSpeed
        {
            get => _cameraHighSpeed;
            set => Set(ref _cameraHighSpeed, value);
        }

        // 网络设置
        private string _tcpIpAddress = "";
        public string TcpIpAddress
        {
            get => _tcpIpAddress;
            set => Set(ref _tcpIpAddress, value);
        }

        private string _portRange = "27183:27199";
        public string PortRange
        {
            get => _portRange;
            set => Set(ref _portRange, value);
        }

        private string _tunnelHost = "localhost";
        public string TunnelHost
        {
            get => _tunnelHost;
            set => Set(ref _tunnelHost, value);
        }

        private bool _forceAdbForward = false;
        public bool ForceAdbForward
        {
            get => _forceAdbForward;
            set => Set(ref _forceAdbForward, value);
        }

        private bool _killAdbOnClose = false;
        public bool KillAdbOnClose
        {
            get => _killAdbOnClose;
            set => Set(ref _killAdbOnClose, value);
        }

        // 输入设置
        private string _keyboardMode = "sdk";
        public string KeyboardMode
        {
            get => _keyboardMode;
            set => Set(ref _keyboardMode, value);
        }

        private string _mouseMode = "sdk";
        public string MouseMode
        {
            get => _mouseMode;
            set => Set(ref _mouseMode, value);
        }

        private string _gamepadMode = "disabled";
        public string GamepadMode
        {
            get => _gamepadMode;
            set => Set(ref _gamepadMode, value);
        }

        private bool _noKeyRepeat = false;
        public bool NoKeyRepeat
        {
            get => _noKeyRepeat;
            set => Set(ref _noKeyRepeat, value);
        }

        private bool _rawKeyEvents = false;
        public bool RawKeyEvents
        {
            get => _rawKeyEvents;
            set => Set(ref _rawKeyEvents, value);
        }

        private bool _noClipboardAutosync = false;
        public bool NoClipboardAutosync
        {
            get => _noClipboardAutosync;
            set => Set(ref _noClipboardAutosync, value);
        }

        private bool _legacyPaste = false;
        public bool LegacyPaste
        {
            get => _legacyPaste;
            set => Set(ref _legacyPaste, value);
        }

        // 显示设置
        private string _displayId = "0";
        public string DisplayId
        {
            get => _displayId;
            set => Set(ref _displayId, value);
        }

        private string _newDisplay = "";
        public string NewDisplay
        {
            get => _newDisplay;
            set => Set(ref _newDisplay, value);
        }

        private string _renderDriver = "";
        public string RenderDriver
        {
            get => _renderDriver;
            set => Set(ref _renderDriver, value);
        }

        private bool _noMipmaps = false;
        public bool NoMipmaps
        {
            get => _noMipmaps;
            set => Set(ref _noMipmaps, value);
        }

        // 性能设置
        private string _videoBuffer = "0";
        public string VideoBuffer
        {
            get => _videoBuffer;
            set => Set(ref _videoBuffer, value);
        }

        private string _timeLimit = "";
        public string TimeLimit
        {
            get => _timeLimit;
            set => Set(ref _timeLimit, value);
        }

        private bool _printFps = false;
        public bool PrintFps
        {
            get => _printFps;
            set => Set(ref _printFps, value);
        }

        private bool _disableScreensaver = false;
        public bool DisableScreensaver
        {
            get => _disableScreensaver;
            set => Set(ref _disableScreensaver, value);
        }        private bool _otgMode = false;
        public bool OtgMode
        {
            get => _otgMode;
            set => Set(ref _otgMode, value);
        }

        // 音频输出缓冲设置
        private string _audioOutputBuffer = "5";
        public string AudioOutputBuffer
        {
            get => _audioOutputBuffer;
            set => Set(ref _audioOutputBuffer, value);
        }

        // 窗口设置
        private string _windowTitle = "";
        public string WindowTitle
        {
            get => _windowTitle;
            set => Set(ref _windowTitle, value);
        }

        private string _windowWidth = "0";
        public string WindowWidth
        {
            get => _windowWidth;
            set => Set(ref _windowWidth, value);
        }

        private string _windowHeight = "0";
        public string WindowHeight
        {
            get => _windowHeight;
            set => Set(ref _windowHeight, value);
        }

        private string _windowX = "auto";
        public string WindowX
        {
            get => _windowX;
            set => Set(ref _windowX, value);
        }

        private string _windowY = "auto";
        public string WindowY
        {
            get => _windowY;
            set => Set(ref _windowY, value);
        }

        // 电源和屏幕管理
        private bool _powerOffOnClose = false;
        public bool PowerOffOnClose
        {
            get => _powerOffOnClose;
            set => Set(ref _powerOffOnClose, value);
        }

        private bool _noPowerOn = false;
        public bool NoPowerOn
        {
            get => _noPowerOn;
            set => Set(ref _noPowerOn, value);
        }

        private string _screenOffTimeout = "";
        public string ScreenOffTimeout
        {
            get => _screenOffTimeout;
            set => Set(ref _screenOffTimeout, value);
        }

        // 文件传输设置
        private string _pushTarget = "/sdcard/Download/";
        public string PushTarget
        {
            get => _pushTarget;
            set => Set(ref _pushTarget, value);
        }

        // 显示方向设置
        private string _displayOrientation = "0";
        public string DisplayOrientation
        {
            get => _displayOrientation;
            set => Set(ref _displayOrientation, value);
        }

        private string _captureOrientation = "";
        public string CaptureOrientation
        {
            get => _captureOrientation;
            set => Set(ref _captureOrientation, value);
        }

        // 裁剪设置
        private string _cropArea = "";
        public string CropArea
        {
            get => _cropArea;
            set => Set(ref _cropArea, value);
        }

        // 视频编码器设置
        private string _videoEncoder = "";
        public string VideoEncoder
        {
            get => _videoEncoder;
            set => Set(ref _videoEncoder, value);
        }

        private string _videoCodecOptions = "";
        public string VideoCodecOptions
        {
            get => _videoCodecOptions;
            set => Set(ref _videoCodecOptions, value);
        }

        // 音频编码器设置
        private string _audioEncoder = "";
        public string AudioEncoder
        {
            get => _audioEncoder;
            set => Set(ref _audioEncoder, value);
        }

        private string _audioCodecOptions = "";
        public string AudioCodecOptions
        {
            get => _audioCodecOptions;
            set => Set(ref _audioCodecOptions, value);
        }

        // 快捷键修饰符设置
        private string _shortcutMod = "lalt,lsuper";
        public string ShortcutMod
        {
            get => _shortcutMod;
            set => Set(ref _shortcutMod, value);
        }

        // V4L2设置 (Linux only)
        private string _v4l2Sink = "";
        public string V4l2Sink
        {
            get => _v4l2Sink;
            set => Set(ref _v4l2Sink, value);
        }

        private string _v4l2Buffer = "0";
        public string V4l2Buffer
        {
            get => _v4l2Buffer;
            set => Set(ref _v4l2Buffer, value);
        }

        // 日志级别设置
        private string _verbosity = "info";
        public string Verbosity
        {
            get => _verbosity;
            set => Set(ref _verbosity, value);
        }

        // 系统级设置
        private bool _noCleanup = false;
        public bool NoCleanup
        {
            get => _noCleanup;
            set => Set(ref _noCleanup, value);
        }

        private bool _noDownsizeOnError = false;
        public bool NoDownsizeOnError
        {
            get => _noDownsizeOnError;
            set => Set(ref _noDownsizeOnError, value);
        }        public void ResetToDefaults()
        {
            // 音频设置默认值
            AudioCodec = "opus";
            AudioBitRate = "128K";
            AudioSource = "output";
            AudioBuffer = "50";            AudioOutputBuffer = "5";
            NoAudio = false;
            AudioDup = false;
            RequireAudio = false;

            // 相机设置默认值
            CameraFacing = "back";
            CameraSize = "";
            CameraFps = "30";
            CameraHighSpeed = false;

            // 网络设置默认值
            TcpIpAddress = "";
            PortRange = "27183:27199";
            TunnelHost = "localhost";
            ForceAdbForward = false;
            KillAdbOnClose = false;

            // 输入设置默认值
            KeyboardMode = "sdk";
            MouseMode = "sdk";
            GamepadMode = "disabled";
            NoKeyRepeat = false;
            RawKeyEvents = false;
            NoClipboardAutosync = false;
            LegacyPaste = false;

            // 显示设置默认值
            DisplayId = "0";
            NewDisplay = "";
            RenderDriver = "";
            NoMipmaps = false;
            DisplayOrientation = "0";
            CaptureOrientation = "";
            CropArea = "";

            // 窗口设置默认值
            WindowTitle = "";
            WindowWidth = "0";
            WindowHeight = "0";
            WindowX = "auto";
            WindowY = "auto";

            // 性能设置默认值
            VideoBuffer = "0";
            TimeLimit = "";
            PrintFps = false;
            DisableScreensaver = false;
            OtgMode = false;

            // 编码器设置默认值
            VideoEncoder = "";
            VideoCodecOptions = "";
            AudioEncoder = "";
            AudioCodecOptions = "";

            // 电源管理默认值
            PowerOffOnClose = false;
            NoPowerOn = false;
            ScreenOffTimeout = "";

            // 文件传输默认值
            PushTarget = "/sdcard/Download/";

            // 快捷键默认值
            ShortcutMod = "lalt,lsuper";

            // V4L2设置默认值
            V4l2Sink = "";
            V4l2Buffer = "0";

            // 日志级别默认值
            Verbosity = "info";

            // 系统设置默认值
            NoCleanup = false;
            NoDownsizeOnError = false;
        }
    }
}
