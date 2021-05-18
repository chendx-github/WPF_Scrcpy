using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Wpf_Scrcpy.MOD;
using Wpf_Scrcpy.Mothod;

namespace Wpf_Scrcpy.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

        public List<COMMAND_data> datas1 = new List<COMMAND_data>();

        COMMAND_data _设备 = new COMMAND_data() { 命令 = "设备", 描述 = "如果 adb devices 列出了多个设备，您必须指定设备的 序列号 ：", 指令 = "-s", 参数 = "", 启用 = true };
        public COMMAND_data 设备 { get => _设备; set => Set(ref _设备, value); }
        COMMAND_data _码率 = new COMMAND_data() { 命令 = "码率", 描述 = "默认码率是 8Mbps。要改变视频的码率 (例如改为 1Mbps)：", 指令 = "-b", 参数 = "1M", 启用 = true };
        public COMMAND_data 码率 { get => _码率; set => Set(ref _码率, value); }
        COMMAND_data _帧率 = new COMMAND_data() { 命令 = "帧率", 描述 = "", 指令 = "--max-fps", 参数 = "10", 启用 = true };
        public COMMAND_data 帧率 { get => _帧率; set => Set(ref _帧率, value); }
        COMMAND_data _分辨率 = new COMMAND_data() { 命令 = "分辨率", 描述 = "", 指令 = "-m", 参数 = "512", 启用 = true };
        public COMMAND_data 分辨率 { get => _分辨率; set => Set(ref _分辨率, value); }
        public COMMAND_data 画面裁剪 = new COMMAND_data() { 命令 = "画面裁剪", 描述 = "", 指令 = "--crop", 参数 = "", 启用 = false };
        COMMAND_data _锁定屏幕方向 = new COMMAND_data() { 命令 = "锁定屏幕方向", 描述 = "", 指令 = "--lock-video-orientation", 参数 = 0, 启用 = false };
        public COMMAND_data 锁定屏幕方向 { get => _锁定屏幕方向; set => Set(ref _锁定屏幕方向, value); }
        public COMMAND_data 编码器 = new COMMAND_data() { 命令 = "编码器", 描述 = "一些设备内置了多种编码器，但是有的编码器会导致问题或崩溃。可以手动选择其它编码器：\r\n\r\nscrcpy --encoder OMX.qcom.video.encoder.avc\r\n要列出可用的编码器，可以指定一个不存在的编码器名称，错误信息中会包含所有的编码器：", 指令 = "--encoder", 参数 = "", 启用 = false };
        public COMMAND_data 屏幕录制 = new COMMAND_data() { 命令 = "屏幕录制", 描述 = "scrcpy --no-display --record file.mp4\r\nscrcpy -Nr file.mkv\r\n# 按 Ctrl+C 停止录制", 指令 = "-r", 参数 = "", 启用 = false };
        public COMMAND_data SSH_隧道 = new COMMAND_data() { 命令 = "SSH_隧道", 描述 = "ssh -CN -L5037:localhost:5037 -R27183:localhost:27183 your_remote_computer", 指令 = "-CN", 参数 = "", 启用 = false };
        public COMMAND_data 关闭设备屏幕 = new COMMAND_data() { 命令 = "关闭设备屏幕", 描述 = "", 指令 = "--turn-screen-off", 参数 = "", 启用 = true };
        public COMMAND_data 保持常亮 = new COMMAND_data() { 命令 = "保持常亮", 描述 = "", 指令 = "-w", 参数 = "", 启用 = false };
        public COMMAND_data 只读 = new COMMAND_data() { 命令 = "只读", 描述 = "", 指令 = "-n", 参数 = "", 启用 = false };
        public COMMAND_data 全屏 = new COMMAND_data() { 命令 = "全屏", 描述 = "", 指令 = "-f", 参数 = "", 启用 = false };
        public COMMAND_data 保持窗口在最前 = new COMMAND_data() { 命令 = "保持窗口在最前", 描述 = "", 指令 = "--always-on-top", 参数 = "", 启用 = false };
        COMMAND_data _无边框 = new COMMAND_data() { 命令 = "无边框", 描述 = "", 指令 = "--window-borderless", 参数 = "", 启用 = false };
        public COMMAND_data 无边框 { get => _无边框; set => Set(ref _无边框, value); }
        public COMMAND_data 标题 = new COMMAND_data() { 命令 = "标题", 描述 = "", 指令 = "--window-title", 参数 = "", 启用 = false };
        public COMMAND_data 位置和大小 = new COMMAND_data() { 命令 = "位置和大小", 描述 = "", 指令 = "--window-x 100 --window-y 100 --window-width 800 --window-height 600", 参数 = "", 启用 = false };
        public COMMAND_data 旋转 = new COMMAND_data() { 命令 = "旋转", 描述 = "0: 无旋转\r\n1: 逆时针旋转 90°\r\n2: 旋转 180°\r\n3: 顺时针旋转 90°", 指令 = "--rotation", 参数 = "", 启用 = false };

        List<string> _Devices = new List<string>();
        List<string> _Devices1 = new List<string>();
        public List<string> Devices { get => _Devices; set => Set(ref _Devices, value); }
        string _device = "";
        public string device { get => _device; set => Set(ref _device, value); }
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
                编码器,
                屏幕录制,
                SSH_隧道,
                关闭设备屏幕,
                保持常亮,
                只读,
                全屏,
                保持窗口在最前,
                无边框,
                标题,
                位置和大小,
                旋转,
            });
            _E_Submit = new Lazy<RelayCommand>(() => new RelayCommand(() =>
            {

                List<string> strs1 = new List<string>();
                foreach (var item in datas1)
                {
                    if (item.启用)
                    {
                        if(item == 设备 && item.参数 == "")
                        {
                            continue;
                        }
                        strs1.Add($"{item.指令} {item.参数}");
                    }
                }
                string cmd_str = $"scrcpy {string.Join(" ", strs1)}";
                MyLog.MyLog.logclass.info($"提交指令 scrcpy {cmd_str}");
                cmdHelp.cmdPorcess(cmd_str, (s, s1) =>
                {
                    MyLog.MyLog.logclass.info(s1.Data);
                }, @"cd /d D:\sof\scrcpy-win64-v1.17");
            })).Value;
            _getDevices = new RelayCommand(() =>
            {
                _Devices1 = new List<string>();
                string cmd_str = "adb devices";
                cmdHelp.cmdPorcess(cmd_str, (s, s1) =>
                {
                    string str = s1.Data;
                    MyLog.MyLog.logclass.info(str);
                    if(str!= null && Regex.IsMatch(str, @"\tdevice$"))
                    {
                        _Devices1.Add(str.Replace("\tdevice", ""));
                        Devices = _Devices1;
                    }
                }, @"cd /d D:\sof\scrcpy-win64-v1.17");
            });
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"
            }
        }

        RelayCommand _E_Submit = null;
        public RelayCommand E_Submit { get => _E_Submit; set => Set(ref _E_Submit, value); }
        RelayCommand _getDevices = null;
        public RelayCommand getDevices { get => _getDevices; set => Set(ref _getDevices, value); }

    }
}