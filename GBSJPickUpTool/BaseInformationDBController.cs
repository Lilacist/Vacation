using DistanceUpdateTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartAssembly;
using System.Runtime.InteropServices;
using System.Data;

namespace GBSJPickUpTool
{
    public partial class DBControllerSet {
        public class BaseInformationDBController :BaseDBController
        {
            private static readonly string dbnamestring = "BaseInfo.sdb";
            private static readonly string tablestring = "CREATE TABLE IF NOT EXISTS INFORMATION(IDNUM VARCHAR(20), NAME VARCHAR(20), DEPART VARCHAR(20), POST VARCHAR(20),SEX VARCHAR(20),HOME VARCHAR(20), IFMARRIED VARCHAR(20), PONAME VARCHAR(20), POHOME VARCHAR(20))";
            public BaseInformationDBController() : base(dbnamestring, tablestring) { }
            private void DbInserInformation(string IDnum, string Name, string Department, string Post, string Sex, string Homeland,string ifmarried,string PoName,string PoHomeland)
            {
                string sql = "INSERT INTO INFORMATION (IDNUM, NAME, DEPART, POST, SEX, HOME, IFMARRIED,PONAME,POHOME) values ('" + IDnum + "', '" + Name + "', '" + Department + "', '" + Post + "', '" + Sex + "', '" + Homeland + "', '" + ifmarried + "', '" + PoName + "', '" + PoHomeland + "')";
                BaseExecuteWithoutReturnValue(sql);
            }
            private void DbClear()
            {
                string sql = "DELETE FROM INFORMATION";
                BaseExecuteWithoutReturnValue(sql);
                return;
            }

            public void InserPersonInformation(string IDnum, string Name, string Department, string Post, string Sex, string Homeland, string ifmarried, string PoName, string PoHomeland)
            {
                DbInserInformation(IDnum, Name, Department, Post, Sex, Homeland,ifmarried,PoName,PoHomeland);
            }
            public void Clear()
            {
                DbClear();
            }
        }
    }
    
}
