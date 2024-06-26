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
        COMMAND_data _码率 = new COMMAND_data() { 命令 = "码率", 描述 = "默认码率是 8Mbps。要改变视频的码率 (例如改为 1Mbps)：", 指令 = "-b", 参数 = "6M", 启用 = true };
        public COMMAND_data 码率 { get => _码率; set => Set(ref _码率, value); }
        COMMAND_data _帧率 = new COMMAND_data() { 命令 = "帧率", 描述 = "", 指令 = "--max-fps", 参数 = "100", 启用 = true };
        public COMMAND_data 帧率 { get => _帧率; set => Set(ref _帧率, value); }
        COMMAND_data _分辨率 = new COMMAND_data() { 命令 = "分辨率", 描述 = "", 指令 = "-m", 参数 = "512", 启用 = true };
        public COMMAND_data 分辨率 { get => _分辨率; set => Set(ref _分辨率, value); }
        COMMAND_data _画面裁剪 = new COMMAND_data() { 命令 = "画面裁剪", 描述 = "", 指令 = "--crop", 参数 = "1080:2000:0:0", 启用 = false };
        public COMMAND_data 画面裁剪 { get => _画面裁剪; set => Set(ref _画面裁剪, value); }
        COMMAND_data _锁定屏幕方向 = new COMMAND_data() { 命令 = "锁定屏幕方向", 描述 = "", 指令 = "--lock-video-orientation", 参数 = 0, 启用 = false };
        public COMMAND_data 锁定屏幕方向 { get => _锁定屏幕方向; set => Set(ref _锁定屏幕方向, value); }
        COMMAND_data _缓冲区 = new COMMAND_data() { 命令 = "缓冲区", 描述 = "", 指令 = "--display-buffer", 参数 = "50", 启用 = true };
        public COMMAND_data 缓冲区 { get => _缓冲区; set => Set(ref _缓冲区, value); }
        COMMAND_data _保持常亮 = new COMMAND_data() { 命令 = "保持常亮", 描述 = "", 指令 = "-w", 参数 = "", 启用 = false };
        public COMMAND_data 保持常亮 { get => _保持常亮; set => Set(ref _保持常亮, value); }
        COMMAND_data _ExePath = new COMMAND_data() { 命令 = "ExePath", 描述 = "", 指令 = @"", 参数 = @"D:\sof\scrcpy-win64-v1.17\scrcpy.exe", 启用 = false };
        public COMMAND_data ExePath  { get => _ExePath; set => Set(ref _ExePath, value);}

        COMMAND_data _编码器 = new COMMAND_data() { 命令 = "编码器", 描述 = "可以选择视频编解码器。可能的值为：h264（默认值） h265 av1", 指令 = "--video-codec", 参数 = "h264", 启用 = true };

        public COMMAND_data 编码器 { get => _编码器; set => Set(ref _编码器, value); }

        COMMAND_data _UHID鼠标 = new COMMAND_data() { 命令 = "UHID键盘", 描述 = "", 指令 = "-M", 参数 = "", 启用 = false };
        public COMMAND_data UHID鼠标 { get => _UHID鼠标; set => Set(ref _UHID鼠标, value); }


        COMMAND_data _UHID键盘 = new COMMAND_data() { 命令 = "UHID键盘", 描述 = "", 指令 = "-K", 参数 = "", 启用 = true };
        public COMMAND_data UHID键盘 { get => _UHID键盘; set => Set(ref _UHID键盘, value); }

        public COMMAND_data 屏幕录制 = new COMMAND_data() { 命令 = "屏幕录制", 描述 = "scrcpy --no-display --record file.mp4\r\nscrcpy -Nr file.mkv\r\n# 按 Ctrl+C 停止录制", 指令 = "-r", 参数 = "", 启用 = false };
        public COMMAND_data SSH_隧道 = new COMMAND_data() { 命令 = "SSH_隧道", 描述 = "ssh -CN -L5037:localhost:5037 -R27183:localhost:27183 your_remote_computer", 指令 = "-CN", 参数 = "", 启用 = false };
        public COMMAND_data 关闭设备屏幕 = new COMMAND_data() { 命令 = "关闭设备屏幕", 描述 = "", 指令 = "--turn-screen-off", 参数 = "", 启用 = true };
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
        string config_file = "config.json";
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
                UHID鼠标,
                UHID键盘,
                只读,
                全屏,
                保持窗口在最前,
                无边框,
                标题,
                位置和大小,
                旋转,
                缓冲区,
                ExePath
            });
            if (File.Exists(config_file))
            {
                try
                {
                    var a001 = 序列化.序列化.json文件反序列化<List<COMMAND_data>>(config_file);
                    var config1 = new TypeAdapterConfig() { };
                    config1.ForType<COMMAND_data, COMMAND_data>().Ignore(s => s.指令,s => s.启用);
                    datas1.ForEach(s =>
                    {
                        a001.Find(s1 => s1.命令 == s.命令)?.Adapt(s, config1);
                    });
                }
                catch (Exception)
                {
                }

            }
            datas1.Find(s => s.命令 == "ExePath").启用 = false;
            _E_Submit = new Lazy<RelayCommand>(() => new RelayCommand(() =>
            {

                List<string> strs1 = new List<string>();
                foreach (var item in datas1)
                {
                    if (item.启用)
                    {
                        if (item == 设备 && item.参数 == "")
                        {
                            continue;
                        }
                        //if (new string[] { "编码器", "缓冲区" }.Contains(item.命令) && !string.IsNullOrEmpty(item.参数))
                        //{
                        //    strs1.Add($"{item.指令}={item.参数}");
                        //}
                        else
                        {
                            strs1.Add($"{item.指令} {item.参数}");
                        }
                    }
                }
                string cmd_str = $"{Path.GetFileNameWithoutExtension(ExePath.参数)} {string.Join(" ", strs1)}";
                MyLog.MyLog.logclass.info($"提交指令 scrcpy {cmd_str}");
                cmdHelp.cmdPorcess(cmd_str, (s, s1) =>
                {
                    MyLog.MyLog.logclass.info(s1.Data);
                }, $@"cd /d {Path.GetDirectoryName(ExePath.参数)}");
                序列化.序列化.序列化为json文件(config_file, datas1);
            })).Value;
            _getDevices = new RelayCommand(() =>
            {
                _Devices1 = new List<string>();
                string cmd_str = "adb devices";
                cmdHelp.cmdPorcess(cmd_str, (s, s1) =>
                {
                    string str = s1.Data;
                    MyLog.MyLog.logclass.info(str);
                    if (str != null && Regex.IsMatch(str, @"\tdevice$"))
                    {
                        _Devices1.Add(str.Replace("\tdevice", ""));
                        Devices = _Devices1;
                    }
                }, $@"cd /d {Path.GetDirectoryName(ExePath.参数)}");
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