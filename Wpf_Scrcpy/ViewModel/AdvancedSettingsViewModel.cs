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
        }

        private bool _audioDup = false;
        public bool AudioDup
        {
            get => _audioDup;
            set => Set(ref _audioDup, value);
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
        }

        private bool _otgMode = false;
        public bool OtgMode
        {
            get => _otgMode;
            set => Set(ref _otgMode, value);
        }

        public void ResetToDefaults()
        {
            // 音频设置默认值
            AudioCodec = "opus";
            AudioBitRate = "128K";
            AudioSource = "output";
            AudioBuffer = "50";
            NoAudio = false;
            AudioDup = false;

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

            // 性能设置默认值
            VideoBuffer = "0";
            TimeLimit = "";
            PrintFps = false;
            DisableScreensaver = false;
            OtgMode = false;
        }
    }
}
