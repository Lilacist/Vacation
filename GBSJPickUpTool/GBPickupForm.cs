using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GBSJPickUpTool
{
    public partial class GBPickupForm : MaterialForm
    {
        static readonly string inputline_backup = "北京 上海 重庆 江津 合川 永川 南川 天津 东莞 广州 中山 深圳 惠州 江门 珠海 汕头 佛山 湛江 河源 肇庆 清远 潮州 韶关 揭阳 阳江 梅州 云浮 茂名 汕尾 番禺 花都 增城 从化 乐昌 南雄 潮阳 澄海 顺德 南海 三水 高明 台山 新会 开平 鹤山 恩平 廉江 雷州 吴川 高州 化州 信宜 高要 四会 惠阳 兴宁 陆丰 阳春 英德 连州 普宁 罗定 济南 青岛 临沂 济宁 菏泽 烟台 淄博 泰安 潍坊 日照 威海 滨州 东营 聊城 德州 莱芜 枣庄 章丘 胶州 即墨 平度 胶南 莱西 滕州 龙口 莱阳 莱州 蓬莱 招远 栖霞 海阳 青州 诸城 寿光 安丘 高密 曲阜 邹城 兖州 肥城 文登 禹城 临清 昌邑 新泰 乐陵 荣成 乳山 苏州 徐州 盐城 无锡 南京 南通 连云港 常州 镇江 扬州 淮安 泰州 宿迁 江阴 宜兴 锡山 新沂 邳州 溧阳 金坛 武进 常熟 张家港 昆山 吴江 太仓 海门 通州 启东 如皋 东台 吴县 大丰 仪征 高邮 江都 丹阳 扬中 句容 兴化 靖江 泰兴 姜堰 郑州 南阳 新乡 安阳 洛阳 信阳 平顶山 周口 商丘 开封 焦作 驻马店 濮阳 三门峡 漯河 许昌 鹤壁 济源 巩义 荥阳 新密 新郑 登封 偃师 舞钢 汝州 林州 卫辉 辉县 沁阳 孟州 禹州 长葛 义马 灵宝 邓州 永城 项城 石家庄 唐山 保定 邯郸 邢台 沧州 秦皇岛 张家口 衡水 廊坊 承德 辛集 藁城 晋州 新乐 鹿泉 遵化 丰南 迁安 武安 南宫 沙河 涿州 定州 高碑店 泊头 任丘 黄骅 安国 霸州 三河 冀州 深州 河间 温州 宁波 杭州 台州 嘉兴 金华 湖州 绍兴 舟山 丽水 衢州 萧山 建德 富阳 余杭 临安 余姚 慈溪 奉化 瑞安 乐清 海宁 平湖 桐乡 诸暨 上虞 嵊州 兰溪 义乌 东阳 永康 江山 温岭 临海 龙泉 西安 咸阳 宝鸡 汉中 渭南 安康 榆林 商洛 延安 铜川 兴平 韩城 华阴 商州 长沙 邵阳 常德 衡阳 株洲 湘潭 永州 岳阳 怀化 郴州 娄底 益阳 张家界 湘西 浏阳 醴陵 湘乡 韶山 耒阳 常宁 武冈 汩罗 临湘 津市 沅江 资兴 洪江 冷水江 涟源 吉首 漳州 厦门 泉州 福州 莆田 宁德 三明 南平 龙岩 福清 长乐 永安 石狮 晋江 南安 龙海 邵武 武夷山 建瓯 建阳 漳平 福安 福鼎 昆明 红河 大理 文山 德宏 曲靖 昭通 楚雄 保山 玉溪 丽江 临沧 思茅 西双版纳 怒江 迪庆 安宁 宣威 个旧 开远 景洪 瑞丽 潞西 成都 绵阳 广元 达州 南充 德阳 广安 阿坝 巴中 遂宁 内江 凉山 攀枝花 乐山 自贡 泸州 雅安 宜宾 资阳 眉山 甘孜 都江堰 彭州 邛崃 崇州 广汉 什邡 绵竹 江油 峨眉山 阆中 华蓥 万源 西昌 简阳 芜湖 合肥 六安 宿州 阜阳 安庆 马鞍山 蚌埠 淮北 淮南 宣城 黄山 铜陵 亳州 池州 巢湖 滁州 三亚 海口 文昌 东方 昌江 陵水 乐东 保亭 五指山 澄迈 万宁 临高 白沙 定安 琼中 屯昌 通什 琼海 儋州 琼山 南昌 赣州 九江 新余 抚州 宜春 景德镇 萍乡 鹰潭 乐平 瑞昌 贵溪 瑞金 南康 丰城 樟树 高安 上饶 德兴 吉安 井冈山 临川 武汉 宜昌 襄樊 荆州 黄冈 孝感 十堰 咸宁 黄石 仙桃 天门 随州 荆门 鄂州 神农架 广水 大冶 丹江口 枝城 当阳 枝江 老河口 枣阳 宜城 钟祥 应城 安陆 汉川 石首 洪湖 松滋 麻城 武穴 赤壁 恩施 利川 潜江 太原 大同 运城 长治 晋城 忻州 临汾 吕梁 晋中 阳泉 朔州 古交 潞城 高平 原平 孝义 离石 汾阳 榆次 介休 侯马 霍州 永济 河津 大连 吉林 沈阳 丹东 辽阳 葫芦岛 锦州 朝阳 营口 鞍山 抚顺 阜新 盘锦 本溪 铁岭 新民 瓦房店 普兰店 庄河 海城 东港 凤城 凌海 北宁 大石桥 灯塔 铁法 开原 凌源 北票 兴城 齐齐哈尔 哈尔滨 大庆 佳木斯 双鸭山 牡丹江 鸡西 黑河 绥化 鹤岗 伊春 大兴安岭 七台河 阿城 双城 尚志 五常 讷河 虎林 密山 铁力 同江 富锦 绥芬河 海林 宁安 穆棱 北安 五大连池 安达 肇东 海伦 贵阳 黔东南 黔南 遵义 黔西南 毕节 铜仁 安顺 六盘水 桐城 天长 明光 首市 宣州 宁国 贵池 清镇 赤水 仁怀 兴义 凯里 都匀 福泉 兰州 天水 庆阳 武威 酒泉 张掖 陇南 白银 定西 平凉 嘉峪关 临夏 金昌 甘南 玉门 敦煌 西峰 合作 西宁 海西 海东 海北 果洛 玉树 黄南 格尔木 德令哈 长春 白山 延边 白城 松原 辽源 通化 四平 九台 榆树 德惠 蛟河 桦甸 舒兰 磐石 公主岭 双辽 河口 集安 临江 洮南 梅市 图们 敦化 珲春 大安 延吉 龙井 和龙 台北 高雄 台中 新竹 基隆 台南 嘉义 银川 吴忠 中卫 石嘴山 固原 青铜峡 灵武 拉萨 山南 林芝 日喀则 阿里 昌都 那曲 乌鲁木齐 伊犁 昌吉 石河子 哈密 阿克苏 巴音郭楞 喀什 塔城 克拉玛依 阿勒泰 吐鲁番 阿拉尔 博尔塔拉 五家渠 克孜勒苏 图木舒克 阜康 米泉 博乐 库尔勒 阿图什 和田 奎屯 伊宁 乌苏 赤峰 包头 通辽 呼和浩特 鄂尔多斯 乌海 呼伦贝尔 兴安 巴彦淖尔 乌兰察布 锡林郭勒 阿拉善 霍林郭勒 海拉尔 满洲里 扎兰屯 牙克石 额尔古纳 乌兰浩特 二连浩特 锡林浩特 根河 集宁 丰镇 东胜 临河 贵港 玉林 北海 南宁 柳州 桂林 梧州 钦州 来宾 崇左 防城港 岑溪 东兴 桂平 北流 凭祥 合山 贺州 百色 河池 宜州 澳门 香港 奇台 岳池 三门 夏邑 襄阳 广灵 阜平 子洲 绥德 临泉 三原 宣恩 玉山 澧县 淑浦 永春 新化 施甸 红安 怀远 上蔡 嘉禾 宜都 郧县 民权 颖上 隆回 长岛 太康 冠县 石门 正宁 巴盟 公安 嘉鱼 全椒 南郑 永泰 双峰 通城 唐河 永修 宜川 漳浦 临漳 崇信 云梦 渠县 六枝 阜城 微山 浠水 富平 仪陇 正阳 大治 灌云 泗洪 万全 盐亭 蓬溪 城固 文水 定兴 泰来 大方 杨凌 进贤 邵东 桑植 大悟 连江 临猗 广丰 奉新 海丰 西华 桃源 攸县 平乡 民乐 黄陂 中江 泗水 溆浦 霍山 遂川 巴东 博罗 盂县 信丰 双流 东海 夏县 汉寿 洪洞 茶陵 馆陶 平邑 汝南 沂水 肥东 永昌 舒城 商水 闻喜 平安 南部 郸城 雎宁 泾源 常山 白河 隆德 凤凰 博野 贵定 梁山 武涉 绥中 太和 织金 江口 武城 雄县 柳林 泸县 蒲江 融水 灵寿 五寨 通山 定陶 隆昌 蓬安 龙山 户县 宜丰 鄢陵 保德 泌阳 江宁 望城 昭阳 吴旗 保靖 灌阳 榕江 青冈 固始 双百 当涂 柘城 任县 高阳 龙里 莒县 塔河 宁阳 遂平 团风 蕲春 营山 沈丘 互助 蓉城 商城 剑阁 合阳 黄梅 保德";
        static string[] DataBase_city = inputline_backup.Split(' ');
        private Thread Th_progress = null;
        private Thread Temp = null;
        string[,] arr;
        static List<string> Lname;
        int person_sum;
        int sb;
        public string ServerAddress { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Port { get; set; }
        public string ServerName { get; set; }
        public GBPickupForm()
        {
            InitializeComponent();
        }
        private void Regpo(string Id, string name, string poname, string pohome)
        {
            int res = -1;
            Parallel.For(0, person_sum, (int i, ParallelLoopState status) =>
            {
                if (arr[i, 0] == Id && arr[i, 1] == name)
                {
                    res = i;
                    status.Break();
                }
            });
            if (res == -1) { Lname.Insert(sb++, name); return; }
            else
            {
                arr[res, 6] = "1";
                arr[res, 7] = poname;
                arr[res, 8] = pohome;
            }
        }
        private void SetProgressBar(int value)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            try
            {
                Th_progress = new Thread(() =>
                {
                    this.CrossThreadCalls(() =>
                    {
                        progressBar1.Value = value;
                        label1.Text = value.ToString() + "%";
                    });
                    return;
                });
                Th_progress.IsBackground = true;
                Th_progress.Start();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString());
                return;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OracleTest test = new OracleTest();
            if (test.ShowDialog() == DialogResult.OK)
            {
                ServerAddress = test.ServerAddress;
                User = test.User;
                Password = test.Password;
                Port = test.Port;
                ServerName = test.ServerName;
                button1.Enabled = true;
                MessageBox.Show("连接成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            GC.Collect();
        }
        private void SetTextBox(string value)
        {
            lock (this.richTextBox1)
            {
                try { if (this.richTextBox1 != null && this.richTextBox1.IsDisposed != true) this.richTextBox1?.AppendText(value); } catch (Exception err) { MessageBox.Show(err.Message); }
            }
        }
        private string GetStandardCity(string rawcity)
        {
            if (rawcity == null || rawcity == "" || rawcity == "NULL" || rawcity == "null") return "NULL";
            string res = "";
            Parallel.For(0, DataBase_city.Length, (int i, ParallelLoopState status) =>
            {
                if (rawcity.Contains(DataBase_city[i]))
                {
                    res = DataBase_city[i];
                    status.Break();
                }
            });
            if (res == "") return "ERR";
            return res;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Temp = new Thread(() =>
            {
                button1.Enabled = false;
                DBControllerSet.BaseInformationDBController baseInformation = new DBControllerSet.BaseInformationDBController();
                baseInformation.Clear();
                sb = 0;
                Lname = new List<string>();
                Lname.Clear();
                SetProgressBar(10);
                OracleHelper oracle = new OracleHelper(User, Password, Port, ServerName, ServerAddress);
                DataTable dt = new DataTable();
                DataTable dtpo = new DataTable();
                oracle.ExecuteDataTable("SELECT * FROM A_基本情况", ref dt);
                oracle.ExecuteDataTable("SELECT * FROM F_配偶情况", ref dtpo);
                person_sum = dt.Rows.Count;
                SetProgressBar(20);
                DataTableReader tableReader = dt.CreateDataReader();
                DataTableReader potableReader = dtpo.CreateDataReader();
                arr = new string[person_sum, 9];
                arr.Initialize();
                int person_num = 0;
                while (tableReader.Read() && person_num < person_sum)
                {
                    string IDnum = tableReader.GetValue(0).ToString();
                    string Name = Regex.Replace(tableReader.GetValue(2).ToString(), @"\s", "");
                    string Department = tableReader.GetValue(3).ToString();
                    string Post = tableReader.GetValue(4).ToString();
                    string Sex = (tableReader.GetValue(7).ToString() == "1" ? "男" : "女");
                    string Homeland = GetStandardCity(tableReader.GetValue(9).ToString());
                    if (Homeland == "ERR")
                    {
                        SelectForm select = new SelectForm();
                        select.name = Name;
                        select.rawplace = tableReader.GetValue(9).ToString();
                        if (select.ShowDialog() == DialogResult.OK)
                        {
                            Homeland = select.toplace;
                        }
                    }
                    arr[person_num, 0] = IDnum;
                    arr[person_num, 1] = Name;
                    arr[person_num, 2] = Department;
                    arr[person_num, 3] = Post;
                    arr[person_num, 4] = Sex;
                    arr[person_num, 5] = Homeland;
                    arr[person_num, 6] = "0";
                    arr[person_num, 7] = "NULL";
                    arr[person_num, 8] = "NULL";
                    SetTextBox(IDnum + " " + Name + " " + Department + " " + Post + " " + Sex + " " + Homeland + "\r\n");
                    person_num++;
                }
                SetProgressBar(30);
                while (potableReader.Read())
                {
                    string IDnum = potableReader.GetValue(0).ToString();
                    string Name = Regex.Replace(potableReader.GetValue(1).ToString(), @"\s", "");
                    string PoName = Regex.Replace(potableReader.GetValue(2).ToString(), @"\s", "");
                    string PoHomeland = GetStandardCity(potableReader.GetValue(8).ToString());
                    if (PoHomeland == "ERR")
                    {
                        SelectForm select = new SelectForm();
                        select.name = Name;
                        select.rawplace = potableReader.GetValue(8).ToString();
                        if (select.ShowDialog() == DialogResult.OK)
                        {
                            PoHomeland = select.toplace;
                        }
                    }
                    Regpo(IDnum, Name, PoName, PoHomeland);
                    SetTextBox(IDnum + " " + Name + " " + PoName + " " + PoHomeland + "\r\n");
                }
                SetProgressBar(40);
                if (Lname.Count != 0)
                {
                    SetTextBox("以下人员配偶未导入：\r\n");
                    for (int i = 0; i < Lname.Count; i++)
                    {
                        SetTextBox(Lname[i] + "\r\n");
                    }
                }
                for (person_num = 0; person_num < person_sum; person_num++)
                {
                    SetTextBox("正在写入" + arr[person_num, 1] + "相关数据\r\n");
                    SetProgressBar((40 + ((person_num * 60) / person_sum)));
                    baseInformation.InserPersonInformation(arr[person_num, 0], arr[person_num, 1], arr[person_num, 2], arr[person_num, 3], arr[person_num, 4], arr[person_num, 5], arr[person_num, 6], arr[person_num, 7], arr[person_num, 8]);
                }
                SetTextBox("数据写入完成！");
                SetProgressBar(100);
                this.button2.Visible = this.button2.Enabled = true;
                this.button1.Visible = this.button1.Enabled = true;
            });
            Temp.IsBackground = true;
            Temp.Start();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.ScrollToCaret();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool pass = false;
            string savepath = "";
            string BaseDBPath = Application.StartupPath + "\\DataBase\\BaseInfo.sdb";
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowNewFolderButton = true;
            dialog.Description = "请选择导出的文件夹";
            dialog.SelectedPath = Application.StartupPath;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                savepath = dialog.SelectedPath.ToString();
                pass = true;
            }
            else
            {
                MessageBox.Show("没有选择文件夹", "未选择", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (pass)
            {
                DirectoryInfo destDirectory = new DirectoryInfo(savepath);
                string fileName = Path.GetFileName(BaseDBPath);
                if (!File.Exists(BaseDBPath))
                {
                    return;
                }
                if (!destDirectory.Exists)
                {
                    destDirectory.Create();
                }
                File.Copy(BaseDBPath, destDirectory.FullName + @"\" + fileName, true);
                MessageBox.Show("文件已保存", "导出完成", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}