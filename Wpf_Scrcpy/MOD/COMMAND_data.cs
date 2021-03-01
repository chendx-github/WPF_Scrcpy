using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Scrcpy.MOD
{
    public class COMMAND_data
    {
        public string 命令 { get; set; }
        public string 描述 { get; set; }
        public string 指令 { get; set; }
        public dynamic 参数 { get; set; }
        public bool 启用 { get; set; }
    }
}
