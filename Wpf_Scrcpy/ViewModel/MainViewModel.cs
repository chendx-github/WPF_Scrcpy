using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Wpf_Scrcpy.MOD;
using Wpf_Scrcpy.Mothod;
using Mapster.Models;
using Mapster;
using System.Linq;
using Microsoft.Win32;
using System.Windows;

namespace Wpf_Scrcpy.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public List<COMMAND_data> datas1 = new List<COMMAND_data>();

        // 基本设置
        COMMAND_data _设备 = new COMMAND_data() { 命令 = "设备", 描述 = "使用 adb devices 列出了多个设备，则必须指定设备 序列号", 指令 = "-s", 参数 = "", 启用 = true };
        public COMMAND_data 设备 { get => _设备; set => Set(ref _设备, value); }
        
        COMMAND_data _码率 = new COMMAND_data() { 命令 = "码率", 描述 = "默认编码器是 8Mbps，要更改视频编码器 (例如设为 1Mbps)", 指令 = "-b", 参数 = "8M", 启用 = true };
        public COMMAND_data 码率 { get => _码率; set => Set(ref _码率, value); }
        
        COMMAND_data _帧率 = new COMMAND_data() { 命令 = "帧率", 描述 = "", 指令 = "--max-fps", 参数 = "60", 启用 = true };
        public COMMAND_data 帧率 { get => _帧率; set => Set(ref _帧率, value); }
        
        COMMAND_data _分辨率 = new COMMAND_data() { 命令 = "分辨率", 描述 = "", 指令 = "-m", 参数 = "1080", 启用 = true };
        public COMMAND_data 分辨率 { get => _分辨率; set => Set(ref _分辨率, value); }
        
        COMMAND_data _画面裁剪 = new COMMAND_data() { 命令 = "画面裁剪", 描述 = "", 指令 = "--crop", 参数 = "1080:2000:0:0", 启用 = false };
        public COMMAND_data 画面裁剪 { get => _画面裁剪; set => Set(ref _画面裁剪, value); }
        
        COMMAND_data _锁定屏幕方向 = new COMMAND_data() { 命令 = "锁定屏幕方向", 描述 = "", 指令 = "--display-orientation", 参数 = 0, 启用 = false };
        public COMMAND_data 锁定屏幕方向 { get => _锁定屏幕方向; set => Set(ref _锁定屏幕方向, value); }
        
        COMMAND_data _缓冲区 = new COMMAND_data() { 命令 = "缓冲区", 描述 = "", 指令 = "--video-buffer", 参数 = "0", 启用 = true };
        public COMMAND_data 缓冲区 { get => _缓冲区; set => Set(ref _缓冲区, value); }
        
        COMMAND_data _保持常亮 = new COMMAND_data() { 命令 = "保持常亮", 描述 = "", 指令 = "-w", 参数 = "", 启用 = false };
        public COMMAND_data 保持常亮 { get => _保持常亮; set => Set(ref _保持常亮, value); }
        
        COMMAND_data _ExePath = new COMMAND_data() { 命令 = "ExePath", 描述 = "", 指令 = @"", 参数 = @"D:\sof\scrcpy-win64-v1.17\scrcpy.exe", 启用 = false };
        public COMMAND_data ExePath { get => _ExePath; set => Set(ref _ExePath, value); }

        COMMAND_data _编码器 = new COMMAND_data() { 命令 = "编码器", 描述 = "选择视频编解码器可能的值为：h264（默认值） h265 av1", 指令 = "--video-codec", 参数 = "h264", 启用 = true };
        public COMMAND_data 编码器 { get => _编码器; set => Set(ref _编码器, value); }

        COMMAND_data _UHID鼠标 = new COMMAND_data() { 命令 = "UHID鼠标", 描述 = "", 指令 = "-M", 参数 = "", 启用 = false };
        public COMMAND_data UHID鼠标 { get => _UHID鼠标; set => Set(ref _UHID鼠标, value); }

        COMMAND_data _UHID键盘 = new COMMAND_data() { 命令 = "UHID键盘", 描述 = "", 指令 = "-K", 参数 = "", 启用 = false };
        public COMMAND_data UHID键盘 { get => _UHID键盘; set => Set(ref _UHID键盘, value); }

        COMMAND_data _无边框 = new COMMAND_data() { 命令 = "无边框", 描述 = "", 指令 = "--window-borderless", 参数 = "", 启用 = false };
        public COMMAND_data 无边框 { get => _无边框; set => Set(ref _无边框, value); }        // 新增属性
        private string _connectionType = "usb";
        public string ConnectionType 
        { 
            get => _connectionType; 
            set 
            {
                Set(ref _connectionType, value);
                IsWifiConnection = value == "wifi" || value == "tcpip";
            }
        }

        private string _ipAddress = "";
        public string IpAddress { get => _ipAddress; set => Set(ref _ipAddress, value); }

        private string _port = "5555";
        public string Port { get => _port; set => Set(ref _port, value); }

        private bool _isWifiConnection = false;
        public bool IsWifiConnection { get => _isWifiConnection; set => Set(ref _isWifiConnection, value); }

        private bool _启用控制 = true;
        public bool 启用控制 { get => _启用控制; set => Set(ref _启用控制, value); }

        private bool _启用录制 = false;
        public bool 启用录制 { get => _启用录制; set => Set(ref _启用录制, value); }

        private string _录制路径 = "";
        public string 录制路径 { get => _录制路径; set => Set(ref _录制路径, value); }

        private string _录制格式 = "mp4";
        public string 录制格式 { get => _录制格式; set => Set(ref _录制格式, value); }

        private string _statusMessage = "就绪";
        public string StatusMessage { get => _statusMessage; set => Set(ref _statusMessage, value); }

        private int _deviceCount = 0;
        public int DeviceCount { get => _deviceCount; set => Set(ref _deviceCount, value); }

        // 新增的快捷设置属性
        public COMMAND_data 全屏 = new COMMAND_data() { 命令 = "全屏", 描述 = "", 指令 = "-f", 参数 = "", 启用 = false };
        public COMMAND_data 窗口置顶 = new COMMAND_data() { 命令 = "窗口置顶", 描述 = "", 指令 = "--always-on-top", 参数 = "", 启用 = false };
        public COMMAND_data 关闭设备屏幕 = new COMMAND_data() { 命令 = "关闭设备屏幕", 描述 = "", 指令 = "-S", 参数 = "", 启用 = false };
        public COMMAND_data 显示触摸 = new COMMAND_data() { 命令 = "显示触摸", 描述 = "", 指令 = "-t", 参数 = "", 启用 = false };

        List<string> _Devices = new List<string>();
        public List<string> Devices { get => _Devices; set => Set(ref _Devices, value); }        string config_file = "config.json";

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            
            datas1.AddRange(new[]{
                设备,
                码率,
                帧率,
                分辨率,
                画面裁剪,
                锁定屏幕方向,
                缓冲区,
                保持常亮,
                UHID鼠标,
                UHID键盘,
                无边框,
                编码器,
                ExePath,
                全屏,
                窗口置顶,
                关闭设备屏幕,
                显示触摸
            });

            InitializeCommands();
            LoadConfiguration();
        }

        private void InitializeCommands()
        {
            _E_Submit = new Lazy<RelayCommand>(() => new RelayCommand(() =>
            {
                try
                {
                    StatusMessage = "正在启动...";
                    List<string> strs1 = new List<string>();

                    // 处理连接方式
                    if (ConnectionType == "wifi" && !string.IsNullOrEmpty(IpAddress))
                    {
                        strs1.Add($"--tcpip={IpAddress}:{Port}");
                    }

                    // 处理设备选择
                    if (!string.IsNullOrEmpty(设备.参数))
                    {
                        strs1.Add($"-s {设备.参数}");
                    }

                    // 处理控制模式
                    if (!启用控制)
                    {
                        strs1.Add("-n");
                    }                    // 处理录制
                    if (启用录制 && !string.IsNullOrEmpty(录制路径))
                    {
                        string recordFile = 录制路径;
                        if (!Path.HasExtension(recordFile))
                        {
                            recordFile += $".{录制格式}";
                        }
                        strs1.Add($"-r \"{recordFile}\"");
                    }

                    foreach (var item in datas1)
                    {
                        if (item.启用)
                        {
                            if (item == 设备 && string.IsNullOrEmpty(item.参数))
                            {
                                continue;
                            }

                            if (!string.IsNullOrEmpty(item.参数.ToString()))
                            {
                                strs1.Add($"{item.指令} {item.参数}");
                            }
                            else
                            {
                                strs1.Add(item.指令);
                            }
                        }
                    }

                    string cmd_str = $"{Path.GetFileNameWithoutExtension(ExePath.参数)} {string.Join(" ", strs1)}";
                    MyLog.MyLog.logclass.info($"提交指令 scrcpy {cmd_str}");
                    
                    cmdHelp.cmdPorcess(cmd_str, (s, s1) =>
                    {
                        MyLog.MyLog.logclass.info(s1.Data);
                    }, $@"cd /d {Path.GetDirectoryName(ExePath.参数)}");
                    
                    StatusMessage = "已启动";
                    SaveConfiguration();
                }
                catch (Exception ex)
                {
                    StatusMessage = "启动失败: " + ex.Message;
                    MessageBox.Show($"启动失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            })).Value;

            _getDevices = new RelayCommand(() =>
            {
                try
                {
                    StatusMessage = "正在扫描设备...";
                    var deviceList = new List<string>();
                    string cmd_str = "adb devices";
                    
                    cmdHelp.cmdPorcess(cmd_str, (s, s1) =>
                    {
                        string str = s1.Data;
                        MyLog.MyLog.logclass.info(str);
                        if (str != null && Regex.IsMatch(str, @"\tdevice$"))
                        {
                            deviceList.Add(str.Replace("\tdevice", ""));
                        }
                    }, $@"cd /d {Path.GetDirectoryName(ExePath.参数)}");
                    
                    Devices = deviceList;
                    DeviceCount = deviceList.Count;
                    StatusMessage = $"发现 {deviceList.Count} 个设备";
                }
                catch (Exception ex)
                {
                    StatusMessage = "扫描设备失败";
                    MessageBox.Show($"扫描设备失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

            BrowseExePathCommand = new RelayCommand(() =>
            {
                var dialog = new OpenFileDialog()
                {
                    Title = "选择 scrcpy.exe 文件",
                    Filter = "可执行文件 (*.exe)|*.exe|所有文件 (*.*)|*.*",
                    CheckFileExists = true
                };

                if (dialog.ShowDialog() == true)
                {
                    ExePath.参数 = dialog.FileName;
                }
            });

            BrowseRecordPathCommand = new RelayCommand(() =>
            {
                var dialog = new SaveFileDialog()
                {
                    Title = "选择录制文件保存位置",
                    Filter = "MP4 文件 (*.mp4)|*.mp4|MKV 文件 (*.mkv)|*.mkv|所有文件 (*.*)|*.*",
                    DefaultExt = 录制格式
                };

                if (dialog.ShowDialog() == true)
                {
                    录制路径 = dialog.FileName;
                }            });

            ShowHelpCommand = new RelayCommand(() =>
            {
                var helpText = "Scrcpy 使用说明：\n\n" +
                    "1. 设备连接：\n" +
                    "   - USB连接：直接连接设备，启用USB调试\n" +
                    "   - WiFi连接：设备与电脑在同一网络，输入设备IP地址\n\n" +
                    "2. 常用快捷键：\n" +
                    "   - Ctrl+F: 全屏\n" +
                    "   - Ctrl+G: 调整窗口大小\n" +
                    "   - Ctrl+H: 返回键\n" +
                    "   - Ctrl+S: 切换应用\n" +
                    "   - Ctrl+M: 菜单键\n" +
                    "   - Ctrl+P: 电源键\n\n" +
                    "3. 录制功能：\n" +
                    "   - 勾选启用录制，选择保存路径\n" +
                    "   - 支持 MP4、MKV 等格式\n\n" +
                    "4. 高级设置：\n" +
                    "   - 音频设置、相机控制、网络配置等\n" +
                    "   - 点击'高级设置'按钮进行配置\n\n" +
                    "更多信息请访问：https://github.com/Genymobile/scrcpy";

                MessageBox.Show(helpText, "帮助", MessageBoxButton.OK, MessageBoxImage.Information);
            });        }

        private void LoadConfiguration()
        {
            if (File.Exists(config_file))
            {
                try
                {
                    var a001 = 序列化.序列化.json文件反序列化<List<COMMAND_data>>(config_file);
                    var config1 = new TypeAdapterConfig() { };
                    config1.ForType<COMMAND_data, COMMAND_data>().Ignore(s => s.指令, s => s.描述);
                    datas1.ForEach(s =>
                    {
                        a001.Find(s1 => s1.命令 == s.命令)?.Adapt(s, config1);
                    });
                }                catch (Exception)
                {
                    StatusMessage = "配置加载失败";
                }
            }
            datas1.Find(s => s.命令 == "ExePath").启用 = false;
        }

        private void SaveConfiguration()
        {
            try
            {
                序列化.序列化.序列化为json文件(config_file, datas1);
            }
            catch (Exception ex)
            {
                MyLog.MyLog.logclass.error($"保存配置失败: {ex.Message}");
            }
        }

        // Commands
        RelayCommand _E_Submit = null;
        public RelayCommand E_Submit { get => _E_Submit; set => Set(ref _E_Submit, value); }
        
        RelayCommand _getDevices = null;
        public RelayCommand getDevices { get => _getDevices; set => Set(ref _getDevices, value); }        public RelayCommand BrowseExePathCommand { get; set; }
        public RelayCommand BrowseRecordPathCommand { get; set; }
        public RelayCommand ShowHelpCommand { get; set; }
    }
}
