using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
namespace Vacation
{
    public partial class DistanceForm : MaterialForm
    {
        DBControllerSet.DistanceDBController distanceclr;
        public DistanceForm()
        {
            InitializeComponent();
            comboBox1.SelectedItem = "湖北";
            comboBox2.SelectedItem = "广水";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem)
            {
                case "北京":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "北京" });
                    break;
                case "上海":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "上海" });
                    break;
                case "重庆":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "重庆","江津","合川","永川","南川" });
                    break;
                case "天津":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "天津" });
                    break;
                case "广东":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "东莞", "广州", "中山", "深圳", "惠州", "江门", "珠海", "汕头", "佛山", "湛江", "河源", "肇庆","清远", "潮州", "韶关", "揭阳", "阳江", "梅州", "云浮", "茂名", "汕尾","番禺","花都","增城","从化","乐昌","南雄","潮阳","澄海","顺德","南海","三水","高明","台山","新会","开平","鹤山","恩平","廉江","雷州","吴川","高州","化州","信宜","高要","四会","惠阳","兴宁","陆丰","阳春","英德","连州","普宁","罗定" , "海丰", "博罗" });
                    break;
                case "山东":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "济南", "青岛", "临沂", "济宁", "菏泽", "烟台", "淄博", "泰安", "潍坊", "日照", "威海", "滨州", "东营","聊城", "德州", "莱芜", "枣庄","章丘","胶州","即墨","平度","胶南","莱西","滕州","龙口","莱阳","莱州","蓬莱","招远","栖霞","海阳","青州","诸城","寿光","安丘","高密","曲阜","邹城","兖州","肥城","文登","禹城","临清","昌邑","新泰","乐陵","荣成","乳山", "长岛", "冠县", "微山", "泗水", "平邑", "沂水", "梁山", "武城", "定陶", "莒县", "宁阳" });
                    break;
                case "江苏":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "苏州", "徐州", "盐城", "无锡", "南京", "南通", "连云港", "常州", "镇江", "扬州", "淮安", "泰州", "宿迁","江阴","宜兴","锡山", "新沂", "邳州", "溧阳", "金坛", "武进", "常熟", "张家港", "昆山", "吴江", "太仓", "海门", "通州", "启东", "如皋", "东台", "吴县", "大丰", "仪征", "高邮", "江都", "丹阳", "扬中", "句容", "兴化", "靖江", "泰兴", "姜堰", "灌云" ,"泗洪", "东海", "雎宁", "江宁" });
                    break;
                case "河南":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "郑州", "南阳", "新乡", "安阳", "洛阳", "信阳", "平顶山", "周口", "商丘", "开封", "焦作", "驻马店", "濮阳","三门峡", "漯河", "许昌", "鹤壁","济源", "巩义", "荥阳", "新密", "新郑", "登封", "偃师", "舞钢", "汝州", "林州", "卫辉", "辉县", "沁阳", "孟州", "禹州", "长葛", "义马", "灵宝", "邓州", "永城","项城","夏邑", "上蔡" ,"民权" ,"太康","唐河" ,"正阳" ,"西华" ,"汝南" ,"商水", "郸城", "鄢陵", "泌阳", "固始", "柘城", "遂平", "沈丘", "商城" });
                    break;
                case "河北":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "石家庄", "唐山", "保定", "邯郸", "邢台", "沧州", "秦皇岛", "张家口", "衡水", "廊坊", "承德","辛集", "藁城", "晋州", "新乐", "鹿泉", "遵化", "丰南", "迁安", "武安", "南宫", "沙河", "涿州", "定州", "高碑店", "泊头", "任丘", "黄骅", "安国", "霸州", "三河", "冀州", "深州", "河间", "阜平", "临漳", "阜城", "万全", "定兴", "平乡", "馆陶", "博野", "雄县", "灵寿", "河任", "高阳", "蓉城" });
                    break;
                case "浙江":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "温州", "宁波", "杭州", "台州", "嘉兴", "金华", "湖州", "绍兴", "舟山", "丽水", "衢州","萧山","建德","富阳", "余杭", "临安", "余姚", "慈溪", "奉化", "瑞安", "乐清", "海宁", "平湖", "桐乡", "诸暨", "上虞", "嵊州", "兰溪", "义乌", "东阳", "永康", "江山", "温岭", "临海", "龙泉", "常山", "三门" });
                    break;
                case "陕西":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "西安", "咸阳", "宝鸡", "汉中", "渭南", "安康", "榆林", "商洛", "延安", "铜川","兴平","韩城","华阴", "商州" , "三原", "子洲", "绥德", "南郑", "宜川", "富平", "城固", "杨凌", "白河", "户县", "吴旗", "合阳" });
                    break;
                case "湖南":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "长沙", "邵阳", "常德", "衡阳", "株洲", "湘潭", "永州", "岳阳", "怀化", "郴州", "娄底", "益阳", "张家界", "湘西","浏阳","醴陵","湘乡", "韶山", "耒阳", "常宁", "武冈", "汩罗", "临湘", "津市", "沅江", "资兴", "洪江", "冷水江", "涟源", "吉首", "澧县", "淑浦", "新化", "嘉禾", "隆回", "石门", "双峰", "邵东", "桑植", "桃源", "攸县", "溆浦", "汉寿", "茶陵", "凤凰", "武涉", "龙山", "望城", "昭阳", "保靖"});
                    break;
                case "福建":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "漳州", "厦门", "泉州", "福州", "莆田", "宁德", "三明", "南平", "龙岩","福清","长乐","永安", "石狮", "晋江", "南安", "龙海", "邵武", "武夷山", "建瓯", "建阳", "漳平", "福安", "福鼎" , "永春", "永泰", "漳浦", "连江" });
                    break;
                case "云南":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "昆明", "红河", "大理", "文山", "德宏", "曲靖", "昭通", "楚雄", "保山", "玉溪", "丽江", "临沧", "思茅", "西双版纳", "怒江", "迪庆","安宁","宣威",  "个旧", "开远", "景洪", "瑞丽", "潞西", "施甸", "双百" });
                    break;
                case "四川":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "成都", "绵阳", "广元", "达州", "南充", "德阳", "广安", "阿坝", "巴中", "遂宁", "内江", "凉山", "攀枝花", "乐山", "自贡", "泸州", "雅安","宜宾", "资阳", "眉山", "甘孜","都江堰","彭州","邛崃","崇州", "广汉", "什邡", "绵竹", "江油", "峨眉山", "阆中", "华蓥", "万源", "西昌","简阳", "岳池", "渠县", "仪陇", "盐亭", "蓬溪", "中江", "双流", "南部", "隆昌", "蓬安", "营山", "泸县", "蒲江", "剑阁"});
                    break;
                case "安徽":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "芜湖", "合肥", "六安", "宿州", "阜阳", "安庆", "马鞍山", "蚌埠", "淮北", "淮南","宣城", "黄山", "铜陵", "亳州", "池州", "巢湖", "滁州", "临泉", "怀远", "颖上", "全椒", "霍山", "肥东", "舒城", "太和", "当涂" });
                    break;
                case "海南":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "三亚", "海口", "文昌", "东方", "昌江", "陵水", "乐东", "保亭", "五指山", "澄迈", "万宁", "临高", "白沙", "定安", "琼中", "屯昌", "通什", "琼海", "儋州", "琼山" });
                    break;
                case "江西":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "南昌", "赣州","九江", "新余", "抚州", "宜春", "景德镇", "萍乡", "鹰潭","乐平","瑞昌","贵溪","瑞金", "南康", "丰城", "樟树", "高安", "上饶", "德兴", "吉安", "井冈山", "临川" , "玉山", "永修", "进贤", "广丰", "奉新", "遂川", "信丰", "宜丰" });
                    break;
                case "湖北":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "武汉", "宜昌", "襄樊", "荆州", "黄冈", "孝感", "十堰", "咸宁", "黄石", "仙桃", "天门", "随州", "荆门","鄂州", "神农架","广水","大冶","丹江口","枝城", "当阳", "枝江", "老河口", "枣阳", "宜城", "钟祥", "应城", "安陆", "汉川", "石首", "洪湖", "松滋", "麻城", "武穴", "赤壁", "恩施", "利川", "潜江", "襄阳" ,"宣恩", "红安", "宜都", "郧县", "公安", "嘉鱼", "通城", "云梦", "浠水", "大治", "大悟", "黄陂", "巴东", "通山", "团风", "蕲春", "黄梅" });
                    break;
                case "山西":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "太原", "大同", "运城", "长治", "晋城", "忻州", "临汾", "吕梁", "晋中", "阳泉", "朔州","古交","潞城", "高平", "原平", "孝义", "离石", "汾阳", "榆次", "介休", "侯马", "霍州", "永济", "河津" , "广灵", "文水", "临猗", "盂县", "夏县", "洪洞", "闻喜", "柳林", "五寨", "保德" });
                    break;
                case "辽宁":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "大连", "沈阳", "丹东", "辽阳", "葫芦岛", "锦州", "朝阳", "营口", "鞍山", "抚顺", "阜新", "盘锦", "本溪", "铁岭","新民","瓦房店","普兰店", "庄河", "海城", "东港", "凤城", "凌海", "北宁", "大石桥", "灯塔", "铁法","开原", "凌源", "北票", "兴城", "绥中" });
                    break;
                case "黑龙江":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "齐齐哈尔", "哈尔滨", "大庆", "佳木斯", "双鸭山", "牡丹江", "鸡西", "黑河", "绥化", "鹤岗", "伊春", "大兴安岭", "七台河","阿城","双城","尚志","五常","讷河", "虎林", "密山", "铁力", "同江", "富锦", "绥芬河", "海林", "宁安", "穆棱", "北安", "五大连池", "安达", "肇东", "海伦", "泰来", "青冈", "塔河" });
                    break;
                case "贵州":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "贵阳", "黔东南", "黔南", "遵义", "黔西南", "毕节", "铜仁", "安顺", "六盘水","桐城","天长","明光", "首市", "宣州", "宁国", "贵池","清镇","赤水","仁怀", "兴义", "凯里", "都匀", "福泉","六枝" ,"贵定", "织金", "江口", "榕江", "龙里", "大方" });
                    break;
                case "甘肃":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "兰州", "天水", "庆阳", "武威", "酒泉", "张掖", "陇南", "白银", "定西", "平凉", "嘉峪关", "临夏", "金昌", "甘南","玉门","敦煌","西峰", "合作", "正宁", "崇信", "民乐", "永昌" });
                    break;
                case "青海":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "西宁", "海西", "海东", "海北", "果洛", "玉树", "黄南","格尔木","德令哈","平安", "互助"});
                    break;
                case "吉林":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "吉林", "长春", "白山", "延边", "白城", "松原", "辽源", "通化", "四平","九台","榆树","德惠","蛟河", "桦甸", "舒兰", "磐石", "公主岭", "双辽", "河口", "集安", "临江", "洮南", "梅市", "图们", "敦化", "珲春", "大安", "延吉", "龙井","和龙" });
                    break;
                case "台湾":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "台北", "高雄", "台中", "新竹", "基隆", "台南", "嘉义"});
                    break;
                case "宁夏回族自治区":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "银川", "吴忠", "中卫", "石嘴山", "固原","青铜峡","灵武" , "泾源","隆德" });
                    break;
                case "西藏自治区":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "拉萨", "山南", "林芝", "日喀则", "阿里", "昌都", "那曲" });
                    break;
                case "新疆维吾尔自治区":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "乌鲁木齐", "伊犁", "昌吉", "石河子", "哈密", "阿克苏", "巴音郭楞", "喀什", "塔城", "克拉玛依", "阿勒泰", "吐鲁番", "阿拉尔", "博尔塔拉", "五家渠", "克孜勒苏", "图木舒克","阜康", "米泉", "博乐", "库尔勒", "阿图什", "和田", "奎屯", "伊宁", "乌苏", "奇台" });
                    break;
                case "内蒙古自治区":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "赤峰", "包头", "通辽", "呼和浩特", "鄂尔多斯", "乌海", "呼伦贝尔", "兴安", "巴彦淖尔", "乌兰察布","锡林郭勒","阿拉善","霍林郭勒","海拉尔", "满洲里", "扎兰屯", "牙克石", "额尔古纳", "乌兰浩特", "二连浩特", "锡林浩特", "根河", "集宁", "丰镇", "东胜", "临河" , "巴盟" });
                    break;
                case "广西壮族自治区":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "贵港", "玉林", "北海", "南宁", "柳州", "桂林", "梧州", "钦州", "来宾", "崇左", "防城港","岑溪","东兴","桂平", "北流", "凭祥", "合山", "贺州", "百色", "河池", "宜州", "融水", "灌阳", "保德" });
                    break;
                case "澳门特别行政区":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "澳门" });
                    break;
                case "香港特别行政区":
                    comboBox2.Items.Clear();
                    comboBox2.Items.AddRange(new object[] { "香港" });
                    break;
                default:break;
            }
        }

        private void DistanceForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox4.SelectedItem)
            {
                case "北京":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "北京" });
                    break;
                case "上海":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "上海" });
                    break;
                case "重庆":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "重庆", "江津", "合川", "永川", "南川" });
                    break;
                case "天津":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "天津" });
                    break;
                case "广东":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "东莞", "广州", "中山", "深圳", "惠州", "江门", "珠海", "汕头", "佛山", "湛江", "河源", "肇庆", "清远", "潮州", "韶关", "揭阳", "阳江", "梅州", "云浮", "茂名", "汕尾", "番禺", "花都", "增城", "从化", "乐昌", "南雄", "潮阳", "澄海", "顺德", "南海", "三水", "高明", "台山", "新会", "开平", "鹤山", "恩平", "廉江", "雷州", "吴川", "高州", "化州", "信宜", "高要", "四会", "惠阳", "兴宁", "陆丰", "阳春", "英德", "连州", "普宁", "罗定", "海丰", "博罗" });
                    break;
                case "山东":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "济南", "青岛", "临沂", "济宁", "菏泽", "烟台", "淄博", "泰安", "潍坊", "日照", "威海", "滨州", "东营", "聊城", "德州", "莱芜", "枣庄", "章丘", "胶州", "即墨", "平度", "胶南", "莱西", "滕州", "龙口", "莱阳", "莱州", "蓬莱", "招远", "栖霞", "海阳", "青州", "诸城", "寿光", "安丘", "高密", "曲阜", "邹城", "兖州", "肥城", "文登", "禹城", "临清", "昌邑", "新泰", "乐陵", "荣成", "乳山", "长岛", "冠县", "微山", "泗水", "平邑", "沂水", "梁山", "武城", "定陶", "莒县", "宁阳" });
                    break;
                case "江苏":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "苏州", "徐州", "盐城", "无锡", "南京", "南通", "连云港", "常州", "镇江", "扬州", "淮安", "泰州", "宿迁", "江阴", "宜兴", "锡山", "新沂", "邳州", "溧阳", "金坛", "武进", "常熟", "张家港", "昆山", "吴江", "太仓", "海门", "通州", "启东", "如皋", "东台", "吴县", "大丰", "仪征", "高邮", "江都", "丹阳", "扬中", "句容", "兴化", "靖江", "泰兴", "姜堰", "灌云", "泗洪", "东海", "雎宁", "江宁" });
                    break;
                case "河南":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "郑州", "南阳", "新乡", "安阳", "洛阳", "信阳", "平顶山", "周口", "商丘", "开封", "焦作", "驻马店", "濮阳", "三门峡", "漯河", "许昌", "鹤壁", "济源", "巩义", "荥阳", "新密", "新郑", "登封", "偃师", "舞钢", "汝州", "林州", "卫辉", "辉县", "沁阳", "孟州", "禹州", "长葛", "义马", "灵宝", "邓州", "永城", "项城", "夏邑", "上蔡", "民权", "太康", "唐河", "正阳", "西华", "汝南", "商水", "郸城", "鄢陵", "泌阳", "固始", "柘城", "遂平", "沈丘", "商城" });
                    break;
                case "河北":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "石家庄", "唐山", "保定", "邯郸", "邢台", "沧州", "秦皇岛", "张家口", "衡水", "廊坊", "承德", "辛集", "藁城", "晋州", "新乐", "鹿泉", "遵化", "丰南", "迁安", "武安", "南宫", "沙河", "涿州", "定州", "高碑店", "泊头", "任丘", "黄骅", "安国", "霸州", "三河", "冀州", "深州", "河间", "阜平", "临漳", "阜城", "万全", "定兴", "平乡", "馆陶", "博野", "雄县", "灵寿", "河任", "高阳", "蓉城" });
                    break;
                case "浙江":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "温州", "宁波", "杭州", "台州", "嘉兴", "金华", "湖州", "绍兴", "舟山", "丽水", "衢州", "萧山", "建德", "富阳", "余杭", "临安", "余姚", "慈溪", "奉化", "瑞安", "乐清", "海宁", "平湖", "桐乡", "诸暨", "上虞", "嵊州", "兰溪", "义乌", "东阳", "永康", "江山", "温岭", "临海", "龙泉", "常山", "三门" });
                    break;
                case "陕西":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "西安", "咸阳", "宝鸡", "汉中", "渭南", "安康", "榆林", "商洛", "延安", "铜川", "兴平", "韩城", "华阴", "商州", "三原", "子洲", "绥德", "南郑", "宜川", "富平", "城固", "杨凌", "白河", "户县", "吴旗", "合阳" });
                    break;
                case "湖南":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "长沙", "邵阳", "常德", "衡阳", "株洲", "湘潭", "永州", "岳阳", "怀化", "郴州", "娄底", "益阳", "张家界", "湘西", "浏阳", "醴陵", "湘乡", "韶山", "耒阳", "常宁", "武冈", "汩罗", "临湘", "津市", "沅江", "资兴", "洪江", "冷水江", "涟源", "吉首", "澧县", "淑浦", "新化", "嘉禾", "隆回", "石门", "双峰", "邵东", "桑植", "桃源", "攸县", "溆浦", "汉寿", "茶陵", "凤凰", "武涉", "龙山", "望城", "昭阳", "保靖" });
                    break;
                case "福建":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "漳州", "厦门", "泉州", "福州", "莆田", "宁德", "三明", "南平", "龙岩", "福清", "长乐", "永安", "石狮", "晋江", "南安", "龙海", "邵武", "武夷山", "建瓯", "建阳", "漳平", "福安", "福鼎", "永春", "永泰", "漳浦", "连江" });
                    break;
                case "云南":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "昆明", "红河", "大理", "文山", "德宏", "曲靖", "昭通", "楚雄", "保山", "玉溪", "丽江", "临沧", "思茅", "西双版纳", "怒江", "迪庆", "安宁", "宣威", "个旧", "开远", "景洪", "瑞丽", "潞西", "施甸", "双百" });
                    break;
                case "四川":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "成都", "绵阳", "广元", "达州", "南充", "德阳", "广安", "阿坝", "巴中", "遂宁", "内江", "凉山", "攀枝花", "乐山", "自贡", "泸州", "雅安", "宜宾", "资阳", "眉山", "甘孜", "都江堰", "彭州", "邛崃", "崇州", "广汉", "什邡", "绵竹", "江油", "峨眉山", "阆中", "华蓥", "万源", "西昌", "简阳", "岳池", "渠县", "仪陇", "盐亭", "蓬溪", "中江", "双流", "南部", "隆昌", "蓬安", "营山", "泸县", "蒲江", "剑阁" });
                    break;
                case "安徽":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "芜湖", "合肥", "六安", "宿州", "阜阳", "安庆", "马鞍山", "蚌埠", "淮北", "淮南", "宣城", "黄山", "铜陵", "亳州", "池州", "巢湖", "滁州", "临泉", "怀远", "颖上", "全椒", "霍山", "肥东", "舒城", "太和", "当涂" });
                    break;
                case "海南":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "三亚", "海口", "文昌", "东方", "昌江", "陵水", "乐东", "保亭", "五指山", "澄迈", "万宁", "临高", "白沙", "定安", "琼中", "屯昌", "通什", "琼海", "儋州", "琼山" });
                    break;
                case "江西":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "南昌", "赣州", "九江", "新余", "抚州", "宜春", "景德镇", "萍乡", "鹰潭", "乐平", "瑞昌", "贵溪", "瑞金", "南康", "丰城", "樟树", "高安", "上饶", "德兴", "吉安", "井冈山", "临川", "玉山", "永修", "进贤", "广丰", "奉新", "遂川", "信丰", "宜丰" });
                    break;
                case "湖北":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "武汉", "宜昌", "襄樊", "荆州", "黄冈", "孝感", "十堰", "咸宁", "黄石", "仙桃", "天门", "随州", "荆门", "鄂州", "神农架", "广水", "大冶", "丹江口", "枝城", "当阳", "枝江", "老河口", "枣阳", "宜城", "钟祥", "应城", "安陆", "汉川", "石首", "洪湖", "松滋", "麻城", "武穴", "赤壁", "恩施", "利川", "潜江", "襄阳", "宣恩", "红安", "宜都", "郧县", "公安", "嘉鱼", "通城", "云梦", "浠水", "大治", "大悟", "黄陂", "巴东", "通山", "团风", "蕲春", "黄梅" });
                    break;
                case "山西":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "太原", "大同", "运城", "长治", "晋城", "忻州", "临汾", "吕梁", "晋中", "阳泉", "朔州", "古交", "潞城", "高平", "原平", "孝义", "离石", "汾阳", "榆次", "介休", "侯马", "霍州", "永济", "河津", "广灵", "文水", "临猗", "盂县", "夏县", "洪洞", "闻喜", "柳林", "五寨", "保德" });
                    break;
                case "辽宁":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "大连", "沈阳", "丹东", "辽阳", "葫芦岛", "锦州", "朝阳", "营口", "鞍山", "抚顺", "阜新", "盘锦", "本溪", "铁岭", "新民", "瓦房店", "普兰店", "庄河", "海城", "东港", "凤城", "凌海", "北宁", "大石桥", "灯塔", "铁法", "开原", "凌源", "北票", "兴城", "绥中" });
                    break;
                case "黑龙江":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "齐齐哈尔", "哈尔滨", "大庆", "佳木斯", "双鸭山", "牡丹江", "鸡西", "黑河", "绥化", "鹤岗", "伊春", "大兴安岭", "七台河", "阿城", "双城", "尚志", "五常", "讷河", "虎林", "密山", "铁力", "同江", "富锦", "绥芬河", "海林", "宁安", "穆棱", "北安", "五大连池", "安达", "肇东", "海伦", "泰来", "青冈", "塔河" });
                    break;
                case "贵州":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "贵阳", "黔东南", "黔南", "遵义", "黔西南", "毕节", "铜仁", "安顺", "六盘水", "桐城", "天长", "明光", "首市", "宣州", "宁国", "贵池", "清镇", "赤水", "仁怀", "兴义", "凯里", "都匀", "福泉", "六枝", "贵定", "织金", "江口", "榕江", "龙里", "大方" });
                    break;
                case "甘肃":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "兰州", "天水", "庆阳", "武威", "酒泉", "张掖", "陇南", "白银", "定西", "平凉", "嘉峪关", "临夏", "金昌", "甘南", "玉门", "敦煌", "西峰", "合作", "正宁", "崇信", "民乐", "永昌" });
                    break;
                case "青海":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "西宁", "海西", "海东", "海北", "果洛", "玉树", "黄南", "格尔木", "德令哈", "平安", "互助" });
                    break;
                case "吉林":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "吉林", "长春", "白山", "延边", "白城", "松原", "辽源", "通化", "四平", "九台", "榆树", "德惠", "蛟河", "桦甸", "舒兰", "磐石", "公主岭", "双辽", "河口", "集安", "临江", "洮南", "梅市", "图们", "敦化", "珲春", "大安", "延吉", "龙井", "和龙" });
                    break;
                case "台湾":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "台北", "高雄", "台中", "新竹", "基隆", "台南", "嘉义" });
                    break;
                case "宁夏回族自治区":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "银川", "吴忠", "中卫", "石嘴山", "固原", "青铜峡", "灵武", "泾源", "隆德" });
                    break;
                case "西藏自治区":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "拉萨", "山南", "林芝", "日喀则", "阿里", "昌都", "那曲" });
                    break;
                case "新疆维吾尔自治区":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "乌鲁木齐", "伊犁", "昌吉", "石河子", "哈密", "阿克苏", "巴音郭楞", "喀什", "塔城", "克拉玛依", "阿勒泰", "吐鲁番", "阿拉尔", "博尔塔拉", "五家渠", "克孜勒苏", "图木舒克", "阜康", "米泉", "博乐", "库尔勒", "阿图什", "和田", "奎屯", "伊宁", "乌苏", "奇台" });
                    break;
                case "内蒙古自治区":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "赤峰", "包头", "通辽", "呼和浩特", "鄂尔多斯", "乌海", "呼伦贝尔", "兴安", "巴彦淖尔", "乌兰察布", "锡林郭勒", "阿拉善", "霍林郭勒", "海拉尔", "满洲里", "扎兰屯", "牙克石", "额尔古纳", "乌兰浩特", "二连浩特", "锡林浩特", "根河", "集宁", "丰镇", "东胜", "临河", "巴盟" });
                    break;
                case "广西壮族自治区":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "贵港", "玉林", "北海", "南宁", "柳州", "桂林", "梧州", "钦州", "来宾", "崇左", "防城港", "岑溪", "东兴", "桂平", "北流", "凭祥", "合山", "贺州", "百色", "河池", "宜州", "融水", "灌阳", "保德" });
                    break;
                case "澳门特别行政区":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "澳门" });
                    break;
                case "香港特别行政区":
                    comboBox3.Items.Clear();
                    comboBox3.Items.AddRange(new object[] { "香港" });
                    break;
                default: break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            distanceclr = new DBControllerSet.DistanceDBController();
            try
            {
                int km = (Convert.ToInt32(distanceclr.GetDis(comboBox2.Text, comboBox3.Text)) / 1000);
                int day;
                label9.Text =km.ToString() + "KM";
                if (km < 100) day = 0;
                else if (km < 300) day = 1;
                else if (km < 600) day = 2;
                else if (km < 1200) day = 3;
                else if (km < 2400) day = 4;
                else day = 5;
                label10.Text = day + "天";
            }
            catch
            {
                label9.Text = "数据库缺失";
                label10.Text = "∞天";
            }
            return;
        }
    }
}

