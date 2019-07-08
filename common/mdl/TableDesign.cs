using System;
using System.Collections.Generic;
using System.Text;

namespace common.mdl
{
    public class TableDesign : IModel
    {
        public string 表名 { get; set; }
        public Int16 序号 { get; set; }
        public string 列名 { get; set; }
        public string 列说明 { get; set; }
        public string 数据类型 { get; set; }
        public Int16 长度 { get; set; }
        public int 小数 { get; set; }
        public string 标识 { get; set; }
        public string 主键 { get; set; }
        public string 空值 { get; set; }
        public string 默认值 { get; set; }
    }
}
