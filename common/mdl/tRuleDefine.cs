using System;
using System.Collections.Generic;
using System.Text;

namespace common.mdl
{

    /// <summary>
    /// 规则定义表
    /// </summary>
    public class tRuleDefine : IModel
    {
        public Int64 rId { get; set; }
        /// <summary>
        /// 城市
        /// <summary>
        public string rCity { get; set; }
        /// <summary>
        /// 城市分类
        /// <summary>
        public string rCityClass { get; set; }
        /// <summary>
        /// 每月到店检查数量抽样总数（按城市或城市类别）
        /// <summary>
        public int? rDdPerMonth { get; set; }
        /// <summary>
        /// 每月远程检查数量抽样总数（按城市或城市类别）
        /// <summary>
        public int? rYcPerMonth { get; set; }
        /// <summary>
        /// 年份及月份例：201905
        /// <summary>
        public int? rYearMonth { get; set; }
    }
}
