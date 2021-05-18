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

        COMMAND_data _�豸 = new COMMAND_data() { ���� = "�豸", ���� = "��� adb devices �г��˶���豸��������ָ���豸�� ���к� ��", ָ�� = "-s", ���� = "", ���� = true };
        public COMMAND_data �豸 { get => _�豸; set => Set(ref _�豸, value); }
        COMMAND_data _���� = new COMMAND_data() { ���� = "����", ���� = "Ĭ�������� 8Mbps��Ҫ�ı���Ƶ������ (�����Ϊ 1Mbps)��", ָ�� = "-b", ���� = "1M", ���� = true };
        public COMMAND_data ���� { get => _����; set => Set(ref _����, value); }
        COMMAND_data _֡�� = new COMMAND_data() { ���� = "֡��", ���� = "", ָ�� = "--max-fps", ���� = "10", ���� = true };
        public COMMAND_data ֡�� { get => _֡��; set => Set(ref _֡��, value); }
        COMMAND_data _�ֱ��� = new COMMAND_data() { ���� = "�ֱ���", ���� = "", ָ�� = "-m", ���� = "512", ���� = true };
        public COMMAND_data �ֱ��� { get => _�ֱ���; set => Set(ref _�ֱ���, value); }
        public COMMAND_data ����ü� = new COMMAND_data() { ���� = "����ü�", ���� = "", ָ�� = "--crop", ���� = "", ���� = false };
        COMMAND_data _������Ļ���� = new COMMAND_data() { ���� = "������Ļ����", ���� = "", ָ�� = "--lock-video-orientation", ���� = 0, ���� = false };
        public COMMAND_data ������Ļ���� { get => _������Ļ����; set => Set(ref _������Ļ����, value); }
        public COMMAND_data ������ = new COMMAND_data() { ���� = "������", ���� = "һЩ�豸�����˶��ֱ������������еı������ᵼ�����������������ֶ�ѡ��������������\r\n\r\nscrcpy --encoder OMX.qcom.video.encoder.avc\r\nҪ�г����õı�����������ָ��һ�������ڵı��������ƣ�������Ϣ�л�������еı�������", ָ�� = "--encoder", ���� = "", ���� = false };
        public COMMAND_data ��Ļ¼�� = new COMMAND_data() { ���� = "��Ļ¼��", ���� = "scrcpy --no-display --record file.mp4\r\nscrcpy -Nr file.mkv\r\n# �� Ctrl+C ֹͣ¼��", ָ�� = "-r", ���� = "", ���� = false };
        public COMMAND_data SSH_��� = new COMMAND_data() { ���� = "SSH_���", ���� = "ssh -CN -L5037:localhost:5037 -R27183:localhost:27183 your_remote_computer", ָ�� = "-CN", ���� = "", ���� = false };
        public COMMAND_data �ر��豸��Ļ = new COMMAND_data() { ���� = "�ر��豸��Ļ", ���� = "", ָ�� = "--turn-screen-off", ���� = "", ���� = true };
        public COMMAND_data ���ֳ��� = new COMMAND_data() { ���� = "���ֳ���", ���� = "", ָ�� = "-w", ���� = "", ���� = false };
        public COMMAND_data ֻ�� = new COMMAND_data() { ���� = "ֻ��", ���� = "", ָ�� = "-n", ���� = "", ���� = false };
        public COMMAND_data ȫ�� = new COMMAND_data() { ���� = "ȫ��", ���� = "", ָ�� = "-f", ���� = "", ���� = false };
        public COMMAND_data ���ִ�������ǰ = new COMMAND_data() { ���� = "���ִ�������ǰ", ���� = "", ָ�� = "--always-on-top", ���� = "", ���� = false };
        COMMAND_data _�ޱ߿� = new COMMAND_data() { ���� = "�ޱ߿�", ���� = "", ָ�� = "--window-borderless", ���� = "", ���� = false };
        public COMMAND_data �ޱ߿� { get => _�ޱ߿�; set => Set(ref _�ޱ߿�, value); }
        public COMMAND_data ���� = new COMMAND_data() { ���� = "����", ���� = "", ָ�� = "--window-title", ���� = "", ���� = false };
        public COMMAND_data λ�úʹ�С = new COMMAND_data() { ���� = "λ�úʹ�С", ���� = "", ָ�� = "--window-x 100 --window-y 100 --window-width 800 --window-height 600", ���� = "", ���� = false };
        public COMMAND_data ��ת = new COMMAND_data() { ���� = "��ת", ���� = "0: ����ת\r\n1: ��ʱ����ת 90��\r\n2: ��ת 180��\r\n3: ˳ʱ����ת 90��", ָ�� = "--rotation", ���� = "", ���� = false };

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
                �豸,
                ����,
                ֡��,
                �ֱ���,
                ����ü�,
                ������Ļ����,
                ������,
                ��Ļ¼��,
                SSH_���,
                �ر��豸��Ļ,
                ���ֳ���,
                ֻ��,
                ȫ��,
                ���ִ�������ǰ,
                �ޱ߿�,
                ����,
                λ�úʹ�С,
                ��ת,
            });
            _E_Submit = new Lazy<RelayCommand>(() => new RelayCommand(() =>
            {

                List<string> strs1 = new List<string>();
                foreach (var item in datas1)
                {
                    if (item.����)
                    {
                        if(item == �豸 && item.���� == "")
                        {
                            continue;
                        }
                        strs1.Add($"{item.ָ��} {item.����}");
                    }
                }
                string cmd_str = $"scrcpy {string.Join(" ", strs1)}";
                MyLog.MyLog.logclass.info($"�ύָ�� scrcpy {cmd_str}");
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