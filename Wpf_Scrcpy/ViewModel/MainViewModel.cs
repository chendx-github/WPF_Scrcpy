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
using Wpf_Scrcpy.Views;

namespace Wpf_Scrcpy.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public List<COMMAND_data> datas1 = new List<COMMAND_data>();

        // 基本设置
        COMMAND_data _设备 = new COMMAND_data() { 命令 = "设备", 描述 = "使用 adb devices 列出了多个设备，则必须指定设备 序列号", 指令 = "--serial", 参数 = "", 启用 = true };
        public COMMAND_data 设备 { get => _设备; set => Set(ref _设备, value); }
        
        COMMAND_data _码率 = new COMMAND_data() { 命令 = "码率", 描述 = "默认编码器是 8Mbps，要更改视频编码器 (例如设为 1Mbps)", 指令 = "--video-bit-rate", 参数 = "8M", 启用 = true };
        public COMMAND_data 码率 { get => _码率; set => Set(ref _码率, value); }
        
        COMMAND_data _帧率 = new COMMAND_data() { 命令 = "帧率", 描述 = "", 指令 = "--max-fps", 参数 = "60", 启用 = true };
        public COMMAND_data 帧率 { get => _帧率; set => Set(ref _帧率, value); }
        
        COMMAND_data _分辨率 = new COMMAND_data() { 命令 = "分辨率", 描述 = "", 指令 = "--max-size", 参数 = "1080", 启用 = true };
        public COMMAND_data 分辨率 { get => _分辨率; set => Set(ref _分辨率, value); }
        
        COMMAND_data _画面裁剪 = new COMMAND_data() { 命令 = "画面裁剪", 描述 = "", 指令 = "--crop", 参数 = "1080:2000:0:0", 启用 = false };
        public COMMAND_data 画面裁剪 { get => _画面裁剪; set => Set(ref _画面裁剪, value); }
        
        COMMAND_data _锁定屏幕方向 = new COMMAND_data() { 命令 = "锁定屏幕方向", 描述 = "", 指令 = "--display-orientation", 参数 = 0, 启用 = false };
        public COMMAND_data 锁定屏幕方向 { get => _锁定屏幕方向; set => Set(ref _锁定屏幕方向, value); }
        
        COMMAND_data _缓冲区 = new COMMAND_data() { 命令 = "缓冲区", 描述 = "", 指令 = "--video-buffer", 参数 = "0", 启用 = true };
        public COMMAND_data 缓冲区 { get => _缓冲区; set => Set(ref _缓冲区, value); }
        
        COMMAND_data _保持常亮 = new COMMAND_data() { 命令 = "保持常亮", 描述 = "", 指令 = "--stay-awake", 参数 = "", 启用 = false };
        public COMMAND_data 保持常亮 { get => _保持常亮; set => Set(ref _保持常亮, value); }
        
        COMMAND_data _ExePath = new COMMAND_data() { 命令 = "ExePath", 描述 = "", 指令 = @"", 参数 = @"D:\sof\scrcpy-win64-v1.17\scrcpy.exe", 启用 = false };
        public COMMAND_data ExePath { get => _ExePath; set => Set(ref _ExePath, value); }

        COMMAND_data _编码器 = new COMMAND_data() { 命令 = "编码器", 描述 = "选择视频编解码器可能的值为：h264（默认值） h265 av1", 指令 = "--video-codec", 参数 = "h264", 启用 = true };
        public COMMAND_data 编码器 { get => _编码器; set => Set(ref _编码器, value); }

        COMMAND_data _UHID鼠标 = new COMMAND_data() { 命令 = "UHID鼠标", 描述 = "", 指令 = "--mouse=uhid", 参数 = "", 启用 = false };
        public COMMAND_data UHID鼠标 { get => _UHID鼠标; set => Set(ref _UHID鼠标, value); }

        COMMAND_data _UHID键盘 = new COMMAND_data() { 命令 = "UHID键盘", 描述 = "", 指令 = "--keyboard=uhid", 参数 = "", 启用 = false };
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
        public string 录制路径 { get => _录制路径; set => Set(ref _录制路径, value); }        private string _录制格式 = "mp4";
        public string 录制格式 { get => _录制格式; set => Set(ref _录制格式, value); }        private bool _启用音频 = true;
        public bool 启用音频 { get => _启用音频; set => Set(ref _启用音频, value); }

        COMMAND_data _音频码率 = new COMMAND_data() { 命令 = "音频码率", 描述 = "设置音频比特率", 指令 = "--audio-bit-rate", 参数 = "128K", 启用 = true };
        public COMMAND_data 音频码率 { get => _音频码率; set => Set(ref _音频码率, value); }        private string _statusMessage = "就绪";
        public string StatusMessage { get => _statusMessage; set => Set(ref _statusMessage, value); }

        private bool _显示详细日志 = false;
        public bool 显示详细日志 { get => _显示详细日志; set => Set(ref _显示详细日志, value); }

        private bool _启动时检查更新 = true;
        public bool 启动时检查更新 { get => _启动时检查更新; set => Set(ref _启动时检查更新, value); }private int _deviceCount = 0;
        public int DeviceCount { get => _deviceCount; set => Set(ref _deviceCount, value); }

        // 新增的快捷设置属性
        public COMMAND_data 全屏 = new COMMAND_data() { 命令 = "全屏", 描述 = "", 指令 = "--fullscreen", 参数 = "", 启用 = false };
        public COMMAND_data 窗口置顶 = new COMMAND_data() { 命令 = "窗口置顶", 描述 = "", 指令 = "--always-on-top", 参数 = "", 启用 = false };
        public COMMAND_data 关闭设备屏幕 = new COMMAND_data() { 命令 = "关闭设备屏幕", 描述 = "", 指令 = "--turn-screen-off", 参数 = "", 启用 = false };
        public COMMAND_data 显示触摸 = new COMMAND_data() { 命令 = "显示触摸", 描述 = "", 指令 = "--show-touches", 参数 = "", 启用 = false };

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
                显示触摸,
                音频码率
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
                    }                    // 处理设备选择
                    if (!string.IsNullOrEmpty(设备.参数))
                    {
                        strs1.Add($"--serial {设备.参数}");
                    }// 处理控制模式
                    if (!启用控制)
                    {
                        strs1.Add("--no-control");
                    }

                    // 处理录制
                    if (启用录制 && !string.IsNullOrEmpty(录制路径))
                    {
                        string recordFile = 录制路径;
                        if (!Path.HasExtension(recordFile))
                        {
                            recordFile += $".{录制格式}";
                        }
                        strs1.Add($"--record \"{recordFile}\"");
                        
                        // 添加录制格式参数
                        if (!string.IsNullOrEmpty(录制格式) && 录制格式 != "mp4")
                        {
                            strs1.Add($"--record-format {录制格式}");                        }
                    }

                    // 处理音频设置
                    if (!启用音频)
                    {
                        strs1.Add("--no-audio");
                    }
                    else
                    {
                        音频码率.启用 = true;
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
                    }                    // 检查scrcpy路径
                    if (string.IsNullOrEmpty(ExePath.参数) || !File.Exists(ExePath.参数))
                    {
                        StatusMessage = "请先设置scrcpy.exe路径";
                        MessageBox.Show("请先设置正确的scrcpy.exe路径", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    string scrcpyPath = ExePath.参数;
                    string workingDir = Path.GetDirectoryName(scrcpyPath);
                    string arguments = string.Join(" ", strs1);
                    
                    MyLog.MyLog.logclass.info($"执行命令: {scrcpyPath} {arguments}");
                    MyLog.MyLog.logclass.info($"工作目录: {workingDir}");
                      // 启动scrcpy进程
                    ProcessStartInfo startInfo = new ProcessStartInfo()
                    {
                        FileName = scrcpyPath,
                        Arguments = arguments,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true, // 隐藏命令行窗口
                        WindowStyle = ProcessWindowStyle.Hidden, // 确保窗口隐藏
                        WorkingDirectory = workingDir
                    };
                      Process scrcpyProcess = new Process();
                    scrcpyProcess.StartInfo = startInfo;                    scrcpyProcess.OutputDataReceived += (s, e) => 
                    {
                        if (!string.IsNullOrEmpty(e.Data))
                        {
                            MyLog.MyLog.logclass.info($"SCRCPY输出: {e.Data}");
                            // 如果用户选择显示详细日志，或者输出包含错误信息，在状态栏显示
                            if (显示详细日志 || e.Data.ToLower().Contains("error") || e.Data.ToLower().Contains("failed"))
                            {
                                Application.Current.Dispatcher.Invoke(() => 
                                {
                                    StatusMessage = e.Data.Contains("error") || e.Data.Contains("failed") 
                                        ? $"SCRCPY警告: {e.Data}" 
                                        : $"SCRCPY: {e.Data}";
                                });
                            }
                        }
                    };
                    scrcpyProcess.ErrorDataReceived += (s, e) => 
                    {
                        if (!string.IsNullOrEmpty(e.Data))
                        {
                            MyLog.MyLog.logclass.error($"SCRCPY错误: {e.Data}");
                            // 错误信息总是显示在状态栏
                            Application.Current.Dispatcher.Invoke(() => 
                            {
                                StatusMessage = $"SCRCPY错误: {e.Data}";
                            });
                        }
                    };
                    
                    // 监控进程退出
                    scrcpyProcess.EnableRaisingEvents = true;
                    scrcpyProcess.Exited += (s, e) => 
                    {
                        Application.Current.Dispatcher.Invoke(() => 
                        {
                            StatusMessage = "SCRCPY已退出";
                            MyLog.MyLog.logclass.info($"SCRCPY进程已退出，退出码: {scrcpyProcess.ExitCode}");
                        });
                    };
                    
                    try
                    {
                        scrcpyProcess.Start();
                        scrcpyProcess.BeginOutputReadLine();
                        scrcpyProcess.BeginErrorReadLine();
                        
                        StatusMessage = "scrcpy已启动";
                        SaveConfiguration();
                    }
                    catch (Exception startEx)
                    {
                        throw new Exception($"启动scrcpy失败: {startEx.Message}");
                    }
                }
                catch (Exception ex)
                {
                    StatusMessage = "启动失败: " + ex.Message;
                    MessageBox.Show($"启动失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            })).Value;            _getDevices = new RelayCommand(() =>
            {
                try
                {
                    StatusMessage = "正在扫描设备...";
                    var deviceList = new List<string>();
                    
                    // 首先尝试从scrcpy目录查找adb
                    string adbPath = "adb";
                    if (!string.IsNullOrEmpty(ExePath.参数) && File.Exists(ExePath.参数))
                    {
                        string scrcpyDir = Path.GetDirectoryName(ExePath.参数);
                        string localAdb = Path.Combine(scrcpyDir, "adb.exe");
                        if (File.Exists(localAdb))
                        {
                            adbPath = localAdb;
                            MyLog.MyLog.logclass.info($"使用本地ADB: {adbPath}");
                        }
                    }
                    else
                    {
                        // 如果没有设置scrcpy路径，提示用户
                        StatusMessage = "建议先设置scrcpy.exe路径以获得更好的兼容性";
                        MyLog.MyLog.logclass.info("使用系统PATH中的ADB");
                    }
                    
                    // 同步执行adb devices命令
                    ProcessStartInfo startInfo = new ProcessStartInfo()
                    {
                        FileName = adbPath,
                        Arguments = "devices",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    };
                    
                    using (Process process = new Process())
                    {
                        process.StartInfo = startInfo;
                        process.Start();
                        
                        string output = process.StandardOutput.ReadToEnd();
                        string error = process.StandardError.ReadToEnd();
                        
                        process.WaitForExit(10000); // 等待最多10秒
                        
                        if (process.ExitCode == 0)
                        {
                            MyLog.MyLog.logclass.info($"ADB输出: {output}");
                            
                            // 解析输出
                            string[] lines = output.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string line in lines)
                            {
                                // 跳过标题行"List of devices attached"
                                if (line.Contains("List of devices attached"))
                                    continue;
                                    
                                // 查找设备行，格式为: "设备序列号    device"
                                string trimmedLine = line.Trim();
                                if (trimmedLine.EndsWith("device") && !trimmedLine.EndsWith("unauthorized"))
                                {
                                    // 分割并取第一部分作为设备ID
                                    string[] parts = trimmedLine.Split(new[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    if (parts.Length >= 2 && parts[1] == "device")
                                    {
                                        string deviceId = parts[0];
                                        if (!string.IsNullOrEmpty(deviceId))
                                        {
                                            deviceList.Add(deviceId);
                                            MyLog.MyLog.logclass.info($"找到设备: {deviceId}");
                                        }
                                    }
                                }
                            }
                            
                            Devices = deviceList;
                            DeviceCount = deviceList.Count;
                            
                            if (deviceList.Count > 0)
                            {
                                StatusMessage = $"发现 {deviceList.Count} 个设备";
                                // 如果只有一个设备，自动选择它
                                if (deviceList.Count == 1 && string.IsNullOrEmpty(设备.参数))
                                {
                                    设备.参数 = deviceList[0];
                                    MyLog.MyLog.logclass.info($"自动选择设备: {deviceList[0]}");
                                }
                            }
                            else
                            {
                                StatusMessage = "未发现设备，请检查USB调试是否开启";
                                MessageBox.Show("未发现已连接的设备\n\n请确保:\n1. 设备已通过USB连接\n2. 已开启开发者选项\n3. 已开启USB调试\n4. 已授权电脑调试", 
                                                "未发现设备", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        else
                        {
                            MyLog.MyLog.logclass.error($"ADB执行失败，退出码: {process.ExitCode}");
                            MyLog.MyLog.logclass.error($"ADB错误输出: {error}");
                            StatusMessage = "ADB执行失败，请检查ADB是否安装";
                            MessageBox.Show($"ADB执行失败\n\n错误信息: {error}\n\n请确保:\n1. 已安装ADB工具或使用scrcpy自带的ADB\n2. ADB在系统PATH中或设置了正确的scrcpy路径\n3. USB驱动已正确安装", 
                                            "ADB错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    StatusMessage = "扫描设备失败";
                    MyLog.MyLog.logclass.error($"扫描设备异常: {ex.Message}");
                    MessageBox.Show($"扫描设备失败: {ex.Message}\n\n请确保:\n1. 已安装ADB工具\n2. 系统PATH中包含ADB路径\n3. 或设置了正确的scrcpy.exe路径", 
                                    "扫描设备异常", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    "更多信息请访问：https://github.com/Genymobile/scrcpy";                MessageBox.Show(helpText, "帮助", MessageBoxButton.OK, MessageBoxImage.Information);
            });            ShowAdvancedSettingsCommand = new RelayCommand(() =>
            {
                try
                {
                    // 直接创建高级设置窗口
                    var advancedWindow = new Views.AdvancedSettingsWindow();
                    advancedWindow.Owner = Application.Current.MainWindow;
                    
                    // 显示窗口并获取结果
                    bool? result = advancedWindow.ShowDialog();
                    
                    if (result == true)
                    {
                        // 用户点击了确定按钮，可以在这里处理高级设置的结果
                        StatusMessage = "高级设置已更新";
                        MyLog.MyLog.logclass.info("用户更新了高级设置");
                    }
                }
                catch (Exception ex)
                {
                    MyLog.MyLog.logclass.error($"打开高级设置失败: {ex.Message}");
                    MessageBox.Show($"打开高级设置失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });}

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
        public RelayCommand ShowAdvancedSettingsCommand { get; set; }
    }
}
