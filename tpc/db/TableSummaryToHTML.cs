using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Chloe;

namespace tpc.db
{
    public class TableSummaryToHTML
    {
        IDbContext _sqlcontext;
        public TableSummaryToHTML(IDbContext sqlcontext)
        {
            _sqlcontext = sqlcontext;
        }

        public string GetCity()
        {
            return _sqlcontext.Query<common.mdl.tRuleDefine>().Select(r => r.rCity).First();
        }
    }
}