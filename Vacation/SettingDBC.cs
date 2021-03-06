﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Vacation
{
    partial class DBControllerSet 
    {
        class SettingDBC : BaseDBController
        {
            private static readonly string dbnamestring = "Setting.sdb";
            private static readonly string tablestring = "CREATE TABLE IF NOT EXISTS SETTING(DATEALARM VARCHAR(20), RATEALARM VARCHAR(20))";
            public SettingDBC() : base(dbnamestring, tablestring) { }
            private bool DbIfExist()
            {
                string sql = "SELECT * FROM SETTING";
                return BaseIfExist(sql);
            }
            private void DbClear()
            {
                string sql = "DELETE FROM SETTING";
                BaseExecuteWithoutReturnValue(sql);
                return;
            }
            private void write(int date, int rate)
            {
                string sql = "INSERT INTO SETTING(DATEALARM,RATEALARM)values ('" + date + "', '" + rate + "')";
                BaseExecuteWithoutReturnValue(sql);
                return;
            }
            public void rewrite(int date, int rate)
            {
                DbClear();
                write(date, rate);
                return;
            }
            public bool ifsettingexist()
            {
                return DbIfExist();
            }
        }
    }
}
