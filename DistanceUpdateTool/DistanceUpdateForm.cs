using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DistanceUpdateTool
{
    public partial class DistanceUpdateForm : MaterialForm
    {
        string inputline_backup = "北京 上海 重庆 江津 合川 永川 南川 天津 东莞 广州 中山 深圳 惠州 江门 珠海 汕头 佛山 湛江 河源 肇庆 清远 潮州 韶关 揭阳 阳江 梅州 云浮 茂名 汕尾 番禺 花都 增城 从化 乐昌 南雄 潮阳 澄海 顺德 南海 三水 高明 台山 新会 开平 鹤山 恩平 廉江 雷州 吴川 高州 化州 信宜 高要 四会 惠阳 兴宁 陆丰 阳春 英德 连州 普宁 罗定 济南 青岛 临沂 济宁 菏泽 烟台 淄博 泰安 潍坊 日照 威海 滨州 东营 聊城 德州 莱芜 枣庄 章丘 胶州 即墨 平度 胶南 莱西 滕州 龙口 莱阳 莱州 蓬莱 招远 栖霞 海阳 青州 诸城 寿光 安丘 高密 曲阜 邹城 兖州 肥城 文登 禹城 临清 昌邑 新泰 乐陵 荣成 乳山 苏州 徐州 盐城 无锡 南京 南通 连云港 常州 镇江 扬州 淮安 泰州 宿迁 江阴 宜兴 锡山 新沂 邳州 溧阳 金坛 武进 常熟 张家港 昆山 吴江 太仓 海门 通州 启东 如皋 东台 吴县 大丰 仪征 高邮 江都 丹阳 扬中 句容 兴化 靖江 泰兴 姜堰 郑州 南阳 新乡 安阳 洛阳 信阳 平顶山 周口 商丘 开封 焦作 驻马店 濮阳 三门峡 漯河 许昌 鹤壁 济源 巩义 荥阳 新密 新郑 登封 偃师 舞钢 汝州 林州 卫辉 辉县 沁阳 孟州 禹州 长葛 义马 灵宝 邓州 永城 项城 石家庄 唐山 保定 邯郸 邢台 沧州 秦皇岛 张家口 衡水 廊坊 承德 辛集 藁城 晋州 新乐 鹿泉 遵化 丰南 迁安 武安 南宫 沙河 涿州 定州 高碑店 泊头 任丘 黄骅 安国 霸州 三河 冀州 深州 河间 温州 宁波 杭州 台州 嘉兴 金华 湖州 绍兴 舟山 丽水 衢州 萧山 建德 富阳 余杭 临安 余姚 慈溪 奉化 瑞安 乐清 海宁 平湖 桐乡 诸暨 上虞 嵊州 兰溪 义乌 东阳 永康 江山 温岭 临海 龙泉 西安 咸阳 宝鸡 汉中 渭南 安康 榆林 商洛 延安 铜川 兴平 韩城 华阴 商州 长沙 邵阳 常德 衡阳 株洲 湘潭 永州 岳阳 怀化 郴州 娄底 益阳 张家界 湘西 浏阳 醴陵 湘乡 韶山 耒阳 常宁 武冈 汩罗 临湘 津市 沅江 资兴 洪江 冷水江 涟源 吉首 漳州 厦门 泉州 福州 莆田 宁德 三明 南平 龙岩 福清 长乐 永安 石狮 晋江 南安 龙海 邵武 武夷山 建瓯 建阳 漳平 福安 福鼎 昆明 红河 大理 文山 德宏 曲靖 昭通 楚雄 保山 玉溪 丽江 临沧 思茅 西双版纳 怒江 迪庆 安宁 宣威 个旧 开远 景洪 瑞丽 潞西 成都 绵阳 广元 达州 南充 德阳 广安 阿坝 巴中 遂宁 内江 凉山 攀枝花 乐山 自贡 泸州 雅安 宜宾 资阳 眉山 甘孜 都江堰 彭州 邛崃 崇州 广汉 什邡 绵竹 江油 峨眉山 阆中 华蓥 万源 西昌 简阳 芜湖 合肥 六安 宿州 阜阳 安庆 马鞍山 蚌埠 淮北 淮南 宣城 黄山 铜陵 亳州 池州 巢湖 滁州 三亚 海口 文昌 东方 昌江 陵水 乐东 保亭 五指山 澄迈 万宁 临高 白沙 定安 琼中 屯昌 通什 琼海 儋州 琼山 南昌 赣州 九江 新余 抚州 宜春 景德镇 萍乡 鹰潭 乐平 瑞昌 贵溪 瑞金 南康 丰城 樟树 高安 上饶 德兴 吉安 井冈山 临川 武汉 宜昌 襄樊 荆州 黄冈 孝感 十堰 咸宁 黄石 仙桃 天门 随州 荆门 鄂州 神农架 广水 大冶 丹江口 枝城 当阳 枝江 老河口 枣阳 宜城 钟祥 应城 安陆 汉川 石首 洪湖 松滋 麻城 武穴 赤壁 恩施 利川 潜江 太原 大同 运城 长治 晋城 忻州 临汾 吕梁 晋中 阳泉 朔州 古交 潞城 高平 原平 孝义 离石 汾阳 榆次 介休 侯马 霍州 永济 河津 大连 吉林 沈阳 丹东 辽阳 葫芦岛 锦州 朝阳 营口 鞍山 抚顺 阜新 盘锦 本溪 铁岭 新民 瓦房店 普兰店 庄河 海城 东港 凤城 凌海 北宁 大石桥 灯塔 铁法 开原 凌源 北票 兴城 齐齐哈尔 哈尔滨 大庆 佳木斯 双鸭山 牡丹江 鸡西 黑河 绥化 鹤岗 伊春 大兴安岭 七台河 阿城 双城 尚志 五常 讷河 虎林 密山 铁力 同江 富锦 绥芬河 海林 宁安 穆棱 北安 五大连池 安达 肇东 海伦 贵阳 黔东南 黔南 遵义 黔西南 毕节 铜仁 安顺 六盘水 桐城 天长 明光 首市 宣州 宁国 贵池 清镇 赤水 仁怀 兴义 凯里 都匀 福泉 兰州 天水 庆阳 武威 酒泉 张掖 陇南 白银 定西 平凉 嘉峪关 临夏 金昌 甘南 玉门 敦煌 西峰 合作 西宁 海西 海东 海北 果洛 玉树 黄南 格尔木 德令哈 长春 白山 延边 白城 松原 辽源 通化 四平 九台 榆树 德惠 蛟河 桦甸 舒兰 磐石 公主岭 双辽 河口 集安 临江 洮南 梅市 图们 敦化 珲春 大安 延吉 龙井 和龙 台北 高雄 台中 新竹 基隆 台南 嘉义 银川 吴忠 中卫 石嘴山 固原 青铜峡 灵武 拉萨 山南 林芝 日喀则 阿里 昌都 那曲 乌鲁木齐 伊犁 昌吉 石河子 哈密 阿克苏 巴音郭楞 喀什 塔城 克拉玛依 阿勒泰 吐鲁番 阿拉尔 博尔塔拉 五家渠 克孜勒苏 图木舒克 阜康 米泉 博乐 库尔勒 阿图什 和田 奎屯 伊宁 乌苏 赤峰 包头 通辽 呼和浩特 鄂尔多斯 乌海 呼伦贝尔 兴安 巴彦淖尔 乌兰察布 锡林郭勒 阿拉善 霍林郭勒 海拉尔 满洲里 扎兰屯 牙克石 额尔古纳 乌兰浩特 二连浩特 锡林浩特 根河 集宁 丰镇 东胜 临河 贵港 玉林 北海 南宁 柳州 桂林 梧州 钦州 来宾 崇左 防城港 岑溪 东兴 桂平 北流 凭祥 合山 贺州 百色 河池 宜州 澳门 香港 奇台 岳池 三门 夏邑 襄阳 广灵 阜平 子洲 绥德 临泉 三原 宣恩 玉山 澧县 淑浦 永春 新化 施甸 红安 怀远 上蔡 嘉禾 宜都 郧县 民权 颖上 隆回 长岛 太康 冠县 石门 正宁 巴盟 公安 嘉鱼 全椒 南郑 永泰 双峰 通城 唐河 永修 宜川 漳浦 临漳 崇信 云梦 渠县 六枝 阜城 微山 浠水 富平 仪陇 正阳 大治 灌云 泗洪 万全 盐亭 蓬溪 城固 文水 定兴 泰来 大方 杨凌 进贤 邵东 桑植 大悟 连江 临猗 广丰 奉新 海丰 西华 桃源 攸县 平乡 民乐 黄陂 中江 泗水 溆浦 霍山 遂川 巴东 博罗 盂县 信丰 双流 东海 夏县 汉寿 洪洞 茶陵 馆陶 平邑 汝南 沂水 肥东 永昌 舒城 商水 闻喜 平安 南部 郸城 雎宁 泾源 常山 白河 隆德 凤凰 博野 贵定 梁山 武涉 绥中 太和 织金 江口 武城 雄县 柳林 泸县 蒲江 融水 灵寿 五寨 通山 定陶 隆昌 蓬安 龙山 户县 宜丰 鄢陵 保德 泌阳 江宁 望城 昭阳 吴旗 保靖 灌阳 榕江 青冈 固始 双百 当涂 柘城 任县 高阳 龙里 莒县 塔河 宁阳 遂平 团风 蕲春 营山 沈丘 互助 蓉城 商城 剑阁 合阳 黄梅 保德";
        string inputline = "北京 上海 重庆 重庆市江津 重庆市合川 重庆市永川 重庆市南川 天津 广东省东莞 广东省广州 广东省中山 广东省深圳 广东省惠州 广东省江门 广东省珠海 广东省汕头 广东省佛山 广东省湛江 广东省河源 广东省肇庆 广东省清远 广东省潮州 广东省韶关 广东省揭阳 广东省阳江 广东省梅州 广东省云浮 广东省茂名 广东省汕尾 广东省番禺 广东省花都 广东省增城 广东省从化 广东省乐昌 广东省南雄 广东省潮阳 广东省澄海 广东省顺德 广东省南海 广东省三水 广东省高明 广东省台山 广东省新会 广东省开平 广东省鹤山 广东省恩平 广东省廉江 广东省雷州 广东省吴川 广东省高州 广东省化州 广东省信宜 广东省高要 广东省四会 广东省惠阳 广东省兴宁 广东省陆丰 广东省阳春 广东省英德 广东省连州 广东省普宁 广东省罗定 山东省济南 山东省青岛 山东省临沂 山东省济宁 山东省菏泽 山东省烟台 山东省淄博 山东省泰安 山东省潍坊 山东省日照 山东省威海 山东省滨州 山东省东营 山东省聊城 山东省德州 山东省莱芜 山东省枣庄 山东省章丘 山东省胶州 山东省即墨 山东省平度 山东省胶南 山东省莱西 山东省滕州 山东省龙口 山东省莱阳 山东省莱州 山东省蓬莱 山东省招远 山东省栖霞 山东省海阳 山东省青州 山东省诸城 山东省寿光 山东省安丘 山东省高密 山东省曲阜 山东省邹城 山东省兖州 山东省肥城 山东省文登 山东省禹城 山东省临清 山东省昌邑 山东省新泰 山东省乐陵 山东省荣成 山东省乳山 江苏省苏州 江苏省徐州 江苏省盐城 江苏省无锡 江苏省南京 江苏省南通 江苏省连云港 江苏省常州 江苏省镇江 江苏省扬州 江苏省淮安 江苏省泰州 江苏省宿迁 江苏省江阴 江苏省宜兴 江苏省锡山 江苏省新沂 江苏省邳州 江苏省溧阳 江苏省金坛 江苏省武进 江苏省常熟 江苏省张家港 江苏省昆山 江苏省吴江 江苏省太仓 江苏省海门 江苏省通州 江苏省启东 江苏省如皋 江苏省东台 江苏省吴县 江苏省大丰 江苏省仪征 江苏省高邮 江苏省江都 江苏省丹阳 江苏省扬中 江苏省句容 江苏省兴化 江苏省靖江 江苏省泰兴 江苏省姜堰 河南省郑州 河南省南阳 河南省新乡 河南省安阳 河南省洛阳 河南省信阳 河南省平顶山 河南省周口 河南省商丘 河南省开封 河南省焦作 河南省驻马店 河南省濮阳 河南省三门峡 河南省漯河 河南省许昌 河南省鹤壁 河南省济源 河南省巩义 河南省荥阳 河南省新密 河南省新郑 河南省登封 河南省偃师 河南省舞钢 河南省汝州 河南省林州 河南省卫辉 河南省辉县 河南省沁阳 河南省孟州 河南省禹州 河南省长葛 河南省义马 河南省灵宝 河南省邓州 河南省永城 河南省项城 河北省石家庄 河北省唐山 河北省保定 河北省邯郸 河北省邢台 河北省沧州 河北省秦皇岛 河北省张家口 河北省衡水 河北省廊坊 河北省承德 河北省辛集 河北省藁城 河北省晋州 河北省新乐 河北省鹿泉 河北省遵化 河北省丰南 河北省迁安 河北省武安 河北省南宫 河北省沙河 河北省涿州 河北省定州 河北省高碑店 河北省泊头 河北省任丘 河北省黄骅 河北省安国 河北省霸州 河北省三河 河北省冀州 河北省深州 河北省河间 浙江省温州 浙江省宁波 浙江省杭州 浙江省台州 浙江省嘉兴 浙江省金华 浙江省湖州 浙江省绍兴 浙江省舟山 浙江省丽水 浙江省衢州 浙江省萧山 浙江省建德 浙江省富阳 浙江省余杭 浙江省临安 浙江省余姚 浙江省慈溪 浙江省奉化 浙江省瑞安 浙江省乐清 浙江省海宁 浙江省平湖 浙江省桐乡 浙江省诸暨 浙江省上虞 浙江省嵊州 浙江省兰溪 浙江省义乌 浙江省东阳 浙江省永康 浙江省江山 浙江省温岭 浙江省临海 浙江省龙泉 陕西省西安 陕西省咸阳 陕西省宝鸡 陕西省汉中 陕西省渭南 陕西省安康 陕西省榆林 陕西省商洛 陕西省延安 陕西省铜川 陕西省兴平 陕西省韩城 陕西省华阴 陕西省商州 湖南省长沙 湖南省邵阳 湖南省常德 湖南省衡阳 湖南省株洲 湖南省湘潭 湖南省永州 湖南省岳阳 湖南省怀化 湖南省郴州 湖南省娄底 湖南省益阳 湖南省张家界 湖南省湘西 湖南省浏阳 湖南省醴陵 湖南省湘乡 湖南省韶山 湖南省耒阳 湖南省常宁 湖南省武冈 湖南省汩罗 湖南省临湘 湖南省津市 湖南省沅江 湖南省资兴 湖南省洪江 湖南省冷水江 湖南省涟源 湖南省吉首 福建省漳州 福建省厦门 福建省泉州 福建省福州 福建省莆田 福建省宁德 福建省三明 福建省南平 福建省龙岩 福建省福清 福建省长乐 福建省永安 福建省石狮 福建省晋江 福建省南安 福建省龙海 福建省邵武 福建省武夷山 福建省建瓯 福建省建阳 福建省漳平 福建省福安 福建省福鼎 云南省昆明 云南省红河 云南省大理 云南省文山 云南省德宏 云南省曲靖 云南省昭通 云南省楚雄 云南省保山 云南省玉溪 云南省丽江 云南省临沧 云南省思茅 云南省西双版纳 云南省怒江 云南省迪庆 云南省安宁 云南省宣威 云南省个旧 云南省开远 云南省景洪 云南省瑞丽 云南省潞西 四川省成都 四川省绵阳 四川省广元 四川省达州 四川省南充 四川省德阳 四川省广安 四川省阿坝 四川省巴中 四川省遂宁 四川省内江 四川省凉山 四川省攀枝花 四川省乐山 四川省自贡 四川省泸州 四川省雅安 四川省宜宾 四川省资阳 四川省眉山 四川省甘孜 四川省都江堰 四川省彭州 四川省邛崃 四川省崇州 四川省广汉 四川省什邡 四川省绵竹 四川省江油 四川省峨眉山 四川省阆中 四川省华蓥 四川省万源 四川省西昌 四川省简阳 安徽省芜湖 安徽省合肥 安徽省六安 安徽省宿州 安徽省阜阳 安徽省安庆 安徽省马鞍山 安徽省蚌埠 安徽省淮北 安徽省淮南 安徽省宣城 安徽省黄山 安徽省铜陵 安徽省亳州 安徽省池州 安徽省巢湖 安徽省滁州 海南省三亚 海南省海口 海南省文昌 海南省东方 海南省昌江 海南省陵水 海南省乐东 海南省保亭 海南省五指山 海南省澄迈 海南省万宁 海南省临高 海南省白沙 海南省定安 海南省琼中 海南省屯昌 海南省通什 海南省琼海 海南省儋州 海南省琼山 江西省南昌 江西省赣州 江西省九江 江西省新余 江西省抚州 江西省宜春 江西省景德镇 江西省萍乡 江西省鹰潭 江西省乐平 江西省瑞昌 江西省贵溪 江西省瑞金 江西省南康 江西省丰城 江西省樟树 江西省高安 江西省上饶 江西省德兴 江西省吉安 江西省井冈山 江西省临川 湖北省武汉 湖北省宜昌 湖北省襄樊 湖北省荆州 湖北省黄冈 湖北省孝感 湖北省十堰 湖北省咸宁 湖北省黄石 湖北省仙桃 湖北省天门 湖北省随州 湖北省荆门 湖北省鄂州 湖北省神农架 湖北省广水 湖北省大冶 湖北省丹江口 湖北省枝城 湖北省当阳 湖北省枝江 湖北省老河口 湖北省枣阳 湖北省宜城 湖北省钟祥 湖北省应城 湖北省安陆 湖北省汉川 湖北省石首 湖北省洪湖 湖北省松滋 湖北省麻城 湖北省武穴 湖北省赤壁 湖北省恩施 湖北省利川 湖北省潜江 山西省太原 山西省大同 山西省运城 山西省长治 山西省晋城 山西省忻州 山西省临汾 山西省吕梁 山西省晋中 山西省阳泉 山西省朔州 山西省古交 山西省潞城 山西省高平 山西省原平 山西省孝义 山西省离石 山西省汾阳 山西省榆次 山西省介休 山西省侯马 山西省霍州 山西省永济 山西省河津 辽宁省大连 吉林省吉林 辽宁省沈阳 辽宁省丹东 辽宁省辽阳 辽宁省葫芦岛 辽宁省锦州 辽宁省朝阳 辽宁省营口 辽宁省鞍山 辽宁省抚顺 辽宁省阜新 辽宁省盘锦 辽宁省本溪 辽宁省铁岭 辽宁省新民 辽宁省瓦房店 辽宁省普兰店 辽宁省庄河 辽宁省海城 辽宁省东港 辽宁省凤城 辽宁省凌海 辽宁省北宁 辽宁省大石桥 辽宁省灯塔 辽宁省铁法 辽宁省开原 辽宁省凌源 辽宁省北票 辽宁省兴城 黑龙江省齐齐哈尔 黑龙江省哈尔滨 黑龙江省大庆 黑龙江省佳木斯 黑龙江省双鸭山 黑龙江省牡丹江 黑龙江省鸡西 黑龙江省黑河 黑龙江省绥化 黑龙江省鹤岗 黑龙江省伊春 黑龙江省大兴安岭 黑龙江省七台河 黑龙江省阿城 黑龙江省双城 黑龙江省尚志 黑龙江省五常 黑龙江省讷河 黑龙江省虎林 黑龙江省密山 黑龙江省铁力 黑龙江省同江 黑龙江省富锦 黑龙江省绥芬河 黑龙江省海林 黑龙江省宁安 黑龙江省穆棱 黑龙江省北安 黑龙江省五大连池 黑龙江省安达 黑龙江省肇东 黑龙江省海伦 贵州省贵阳 贵州省黔东南 贵州省黔南 贵州省遵义 贵州省黔西南 贵州省毕节 贵州省铜仁 贵州省安顺 贵州省六盘水 贵州省桐城 贵州省天长 贵州省明光 贵州省首市 贵州省宣州 贵州省宁国 贵州省贵池 贵州省清镇 贵州省赤水 贵州省仁怀 贵州省兴义 贵州省凯里 贵州省都匀 贵州省福泉 甘肃省兰州 甘肃省天水 甘肃省庆阳 甘肃省武威 甘肃省酒泉 甘肃省张掖 甘肃省陇南 甘肃省白银 甘肃省定西 甘肃省平凉 甘肃省嘉峪关 甘肃省临夏 甘肃省金昌 甘肃省甘南 甘肃省玉门 甘肃省敦煌 甘肃省西峰 甘肃省合作 青海省西宁 青海省海西 青海省海东 青海省海北 青海省果洛 青海省玉树 青海省黄南 青海省格尔木 青海省德令哈 吉林省长春 吉林省白山 吉林省延边 吉林省白城 吉林省松原 吉林省辽源 吉林省通化 吉林省四平 吉林省九台 吉林省榆树 吉林省德惠 吉林省蛟河 吉林省桦甸 吉林省舒兰 吉林省磐石 吉林省公主岭 吉林省双辽 吉林省河口 吉林省集安 吉林省临江 吉林省洮南 吉林省梅市 吉林省图们 吉林省敦化 吉林省珲春 吉林省大安 吉林省延吉 吉林省龙井 吉林省和龙 台湾省台北 台湾省高雄 台湾省台中 台湾省新竹 台湾省基隆 台湾省台南 台湾省嘉义 宁夏回族自治区银川 宁夏回族自治区吴忠 宁夏回族自治区中卫 宁夏回族自治区石嘴山 宁夏回族自治区固原 宁夏回族自治区青铜峡 宁夏回族自治区灵武 西藏自治区拉萨 西藏自治区山南 西藏自治区林芝 西藏自治区日喀则 西藏自治区阿里 西藏自治区昌都 西藏自治区那曲 新疆维吾尔自治区乌鲁木齐 新疆维吾尔自治区伊犁 新疆维吾尔自治区昌吉 新疆维吾尔自治区石河子 新疆维吾尔自治区哈密 新疆维吾尔自治区阿克苏 新疆维吾尔自治区巴音郭楞 新疆维吾尔自治区喀什 新疆维吾尔自治区塔城 新疆维吾尔自治区克拉玛依 新疆维吾尔自治区阿勒泰 新疆维吾尔自治区吐鲁番 新疆维吾尔自治区阿拉尔 新疆维吾尔自治区博尔塔拉 新疆维吾尔自治区五家渠 新疆维吾尔自治区克孜勒苏 新疆维吾尔自治区图木舒克 新疆维吾尔自治区阜康 新疆维吾尔自治区米泉 新疆维吾尔自治区博乐 新疆维吾尔自治区库尔勒 新疆维吾尔自治区阿图什 新疆维吾尔自治区和田 新疆维吾尔自治区奎屯 新疆维吾尔自治区伊宁 新疆维吾尔自治区乌苏 内蒙古自治区赤峰 内蒙古自治区包头 内蒙古自治区通辽 内蒙古自治区呼和浩特 内蒙古自治区鄂尔多斯 内蒙古自治区乌海 内蒙古自治区呼伦贝尔 内蒙古自治区兴安 内蒙古自治区巴彦淖尔 内蒙古自治区乌兰察布 内蒙古自治区锡林郭勒 内蒙古自治区阿拉善 内蒙古自治区霍林郭勒 内蒙古自治区海拉尔 内蒙古自治区满洲里 内蒙古自治区扎兰屯 内蒙古自治区牙克石 内蒙古自治区额尔古纳 内蒙古自治区乌兰浩特 内蒙古自治区二连浩特 内蒙古自治区锡林浩特 内蒙古自治区根河 内蒙古自治区集宁 内蒙古自治区丰镇 内蒙古自治区东胜 内蒙古自治区临河 广西壮族自治区贵港 广西壮族自治区玉林 广西壮族自治区北海 广西壮族自治区南宁 广西壮族自治区柳州 广西壮族自治区桂林 广西壮族自治区梧州 广西壮族自治区钦州 广西壮族自治区来宾 广西壮族自治区崇左 广西壮族自治区防城港 广西壮族自治区岑溪 广西壮族自治区东兴 广西壮族自治区桂平 广西壮族自治区北流 广西壮族自治区凭祥 广西壮族自治区合山 广西壮族自治区贺州 广西壮族自治区百色 广西壮族自治区河池 广西壮族自治区宜州 澳门 香港 新疆奇台 四川岳池 浙江三门 河南夏邑 湖北襄阳 山西广灵 河北阜平 陕西子洲 陕西绥德 安徽临泉 陕西三原 湖北宣恩 江西玉山 湖南澧县 湖南淑浦 福建永春 湖南新化 云南施甸 湖北红安 安徽怀远 河南上蔡 湖南嘉禾 湖北宜都 湖北郧县 河南民权 安徽颖上 湖南隆回 山东长岛 河南太康 山东冠县 湖南石门 甘肃正宁 内蒙古巴盟 湖北公安 湖北嘉鱼 安徽全椒 陕西南郑 福建永泰 湖南双峰 湖北通城 河南唐河 江西永修 陕西宜川 福建漳浦 河北临漳 甘肃崇信 湖北云梦 四川渠县 贵州六枝 河北阜城 山东微山 湖北浠水 陕西富平 四川仪陇 河南正阳 湖北大治 江苏灌云 江苏泗洪 河北万全 四川盐亭 四川蓬溪 陕西城固 山西文水 河北定兴 黑龙江泰来 贵州大方 陕西杨凌 江西进贤 湖南邵东 湖南桑植 湖北大悟 福建连江 山西临猗 江西广丰 江西奉新 广东海丰 河南西华 湖南桃源 湖南攸县 河北平乡 甘肃民乐 湖北黄陂 四川中江 山东泗水 湖南溆浦 安徽霍山 江西遂川 湖北巴东 广东博罗 山西盂县 江西信丰 四川双流 江苏东海 山西夏县 湖南汉寿 山西洪洞 湖南茶陵 河北馆陶 山东平邑 河南汝南 山东沂水 安徽肥东 甘肃永昌 安徽舒城 河南商水 山西闻喜 青海平安 四川南部 河南郸城 江苏雎宁 宁夏泾源 浙江常山 陕西白河 宁夏隆德 湖南凤凰 河北博野 贵州贵定 山东梁山 湖南武涉 辽宁绥中 安徽太和 贵州织金 贵州江口 山东武城 河北省雄县 山西柳林 四川泸县 四川蒲江 广西融水 河北灵寿 山西五寨 湖北通山 山东定陶 四川隆昌 四川蓬安 湖南龙山 陕西户县 江西宜丰 河南鄢陵 广西保德 河南泌阳 江苏江宁 湖南望城 湖南昭阳 陕西吴旗 湖南保靖 广西灌阳 贵州榕江 黑龙江青冈 河南固始 云南双百 安徽当涂 河南柘城 河北任县 河北高阳 贵州龙里 山东莒县 黑龙江塔河县 山东宁阳 河南遂平 湖北团风 湖北蕲春 四川营山 河南沈丘 青海互助 河北蓉城 河南商城 四川剑阁 陕西合阳 湖北黄梅 山西保德";
        int eq;
        int[,] DistanceArray;
        MutipleThreadResetEvent countdown;
        string[] citys;
        string[] DataBase_city;
        bool passthrough;
        bool pingtestpass;
        public struct pos
        {
            public double x;
            public double y;
        };
        static pos[] posarr;
        public DistanceUpdateForm()
        {
            eq = 0;
            InitializeComponent();
        }
        public static string GetLng(string s)
        {
            if (s == null) return ("返回数据为空！");
            int n1, n2, n3;
            string l = "<location>";
            string d = "</location>";
            n1 = s.IndexOf(l, 0);
            n2 = s.IndexOf(d, 0);
            if (n1 == -1 || n2 == -1) return ("返回数据出错！");
            n1 = n1 + l.Length;
            string mm = s.Substring(n1, n2 - n1);
            n3 = mm.IndexOf(",", 0);
            return (mm.Substring(0, n3));
        }
        public static string GetDis(string s)
        {
            if (s == null) return ("返回数据为空！");
            int n1, n2;
            string l = "<status>";
            string d = "</status>";
            n1 = s.IndexOf(l, 0);
            n2 = s.IndexOf(d, 0);
            if (n1 == -1 || n2 == -1) return ("返回数据出错！");
            n1 = n1 + l.Length;
            string mm = s.Substring(n1, n2 - n1);
            if (mm == "0") return ("返回数据出错！");
            int n11, n22;
            string ll = "<distance>";
            string dd = "</distance>";
            n11 = s.IndexOf(ll, 0);
            n22 = s.IndexOf(dd, 0);
            if (n11 == -1 || n22 == -1) return ("返回数据出错！");
            n11 = n11 + ll.Length;
            string mmx = s.Substring(n11, n22 - n11);
            if (mm == "0") return ("返回数据出错！");
            return mmx;
        }
        public static string GetLat(string s)
        {
            if (s == null) return ("返回数据为空！");
            int n1, n2, n3;
            string l = "<location>";
            string d = "</location>";
            n1 = s.IndexOf(l, 0);
            n2 = s.IndexOf(d, 0);
            if (n1 == -1 || n2 == -1) return ("返回数据出错！");
            n1 = n1 + l.Length;
            string mm = s.Substring(n1, n2 - n1);
            n3 = mm.IndexOf(",", 0) + 1;
            if (n1 == -1 || n2 == -1 || n3 == -1) return ("返回数据出错！");
            return (mm.Substring(n3, mm.Length - n3));
        }
        public static string DoGetRequestSendData(string url)
        {
            try
            {
                HttpWebRequest hwRequest = null;
                HttpWebResponse hwResponse = null;
                string strResult = string.Empty;
                hwRequest = (System.Net.HttpWebRequest)WebRequest.Create(url);
                hwRequest.Timeout = 10000;
                hwRequest.Method = "GET";
                hwRequest.ContentType = "application/x-www-form-urlencoded";
                hwResponse = (HttpWebResponse)hwRequest.GetResponse();
                StreamReader srReader = new StreamReader(hwResponse.GetResponseStream(), Encoding.UTF8);
                strResult = srReader.ReadToEnd();
                srReader.Close();
                hwResponse.Close();
                return strResult;
            }
            catch (Exception err)
            {
                return err.Message;
            }
        }
        public static string DoGetPosition(string city)
        {
            return DoGetRequestSendData("https://restapi.amap.com/v3/geocode/geo?address=" + city + "&output=xml&key=7fa8d2a1b155e90cd827e02a6c67cce0");
        }
        public static string DoGetDistance(int city1, int city2)
        {
            string JD1 = Convert.ToString(posarr[city1].x);
            string WD1 = Convert.ToString(posarr[city1].y);
            string JD2 = Convert.ToString(posarr[city2].x);
            string WD2 = Convert.ToString(posarr[city2].y);
            string getstr = "http://restapi.amap.com/v3/direction/driving?key=7fa8d2a1b155e90cd827e02a6c67cce0&origin=" + JD1 + "," + WD1 + "&destination=" + JD2 + "," + WD2 + "&output=xml&originid=&destinationid=&extensions=base&strategy=1&waypoints=&avoidpolygons=&avoidroad=";
            string getjson = DoGetRequestSendData(getstr);
            return GetDis(getjson);
        }
        private Thread Temp = null;
        private Thread Th_progress = null;
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
        private void SetTextBox(string value)
        {
            lock (this.richTextBox1)
            {
                try { if (this.richTextBox1 != null && this.richTextBox1.IsDisposed != true) this.richTextBox1?.AppendText(value); } catch (Exception err) { MessageBox.Show(err.Message); }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                countdown = null;
                DistanceArray = null;
                if (Temp != null && Temp.IsAlive) Temp.Abort();
                if (Th_progress != null && Th_progress.IsAlive) Th_progress.Abort();
            }
            catch (Exception err)
            {
                if (err.Message != "正在中止线程。")
                    MessageBox.Show(err.StackTrace + "\r\n" + err.Message, "close");
            }
            this.Close();
        }
        private void Update_Position(object param)
        {
            bool notpass = true;
            object[] objArr = (object[])param;
            int i = (int)objArr[0];
            MutipleThreadResetEvent countdown = (MutipleThreadResetEvent)objArr[1];
            while (notpass)
            {
                string getjson1 = DoGetPosition(citys[i]);
                try
                {
                    posarr[i].x = Convert.ToDouble(GetLng(getjson1));
                    posarr[i].y = Convert.ToDouble(GetLat(getjson1));
                    if (i % 50 == 0) SetProgressBar(3 + i / 100);
                    if (!richTextBox1.IsDisposed)
                        SetTextBox(DataBase_city[i] + " " + posarr[i].x + " " + posarr[i].y + "\r\n");
                    notpass = false;
                }
                catch (Exception err)
                {
                    if (!richTextBox1.IsDisposed)
                        SetTextBox(DataBase_city[i] + " " + err.Message + "\r\n");
                    notpass = true;
                }
            }
            countdown.SetOne();
        }
        private void Update_Distance(object param)
        {
            try
            {
                if (citys == null) return;
                if (DistanceArray == null) return;
                if (richTextBox1.IsDisposed) return;
                object[] objArr = (object[])param;
                int i = (int)objArr[0];
                int j = (int)objArr[1];
                MutipleThreadResetEvent countdown = (MutipleThreadResetEvent)objArr[2];
                if (richTextBox1.IsDisposed) { countdown.SetOne(); return; }
                if (citys == null) { countdown.SetOne(); return; }
                if (DistanceArray == null) { countdown.SetOne(); return; }
                string sstr = DoGetDistance(i, j);
                if (richTextBox1.IsDisposed) { countdown.SetOne(); return; }
                if (DistanceArray == null) { countdown.SetOne(); return; }
                else if ((sstr == "返回数据为空！" || sstr == "返回数据出错！") && DistanceArray != null)
                { passthrough = false; DistanceArray[i, j] = DistanceArray[j, i] = 0; }
                else if (sstr != "返回数据为空！" && sstr != "返回数据出错！" && DistanceArray != null)
                    DistanceArray[i, j] = DistanceArray[j, i] = Convert.ToInt32(sstr);
                else { passthrough = false; countdown.SetOne(); return; }
                if (!richTextBox1.IsDisposed)
                    SetTextBox(i.ToString() + ":" + citys[i] + "<-------->" + j.ToString() + ":" + citys[j] + "  " + sstr + "\r\n");
                countdown.SetOne();
                return;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "界面更新出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Pingtest(object obj)
        {
            MutipleThreadResetEvent resetEvent = (MutipleThreadResetEvent)obj;
            IPAddress myIP = IPAddress.Parse("120.77.134.57");
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            string data = "Fuck You Gaode";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 3000;
            for (int a = 0; a <= 4; a++)
            {
                try
                {
                    PingReply reply = pingSender.Send(myIP, timeout, buffer, options);
                    if (reply.Status == IPStatus.Success)
                    {
                        SetTextBox("服务器连接成功，网络延迟为：" + reply.RoundtripTime + "\r\n");
                        pingtestpass = true;
                        resetEvent.SetOne();
                        return;
                    }
                    else
                    {
                        SetTextBox("第" + (a + 1).ToString() + "次连接地图更新服务器失败：" + reply.Status.ToString() + "\r\n");
                        if (a == 4)
                        {
                            SetTextBox("地图服务器无法连接。请检查网络状态。\r\n");
                            pingtestpass = false;
                            resetEvent.SetOne();
                            return;
                        }
                    }
                }
                catch (Exception err)
                {
                    SetTextBox("网络出错："+err.Message+"\r\n");
                }
            }
            pingtestpass = false;
            resetEvent.SetOne();
            return;
        }

        int sum;
        private void button2_Click(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            textBox1.ReadOnly = false;
            button6.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = button2.Visible = false;
            button3.Enabled = button3.Visible = true;
            DBControllerSet.DistanceDBController distanceclr = new DBControllerSet.DistanceDBController();
            richTextBox1.Text = "";
            citys = null;
            eq = 0;
            DistanceArray = null;
            countdown = null;
            DataBase_city = null;
            passthrough = true;
            posarr = null;
            GC.Collect();
            Control.CheckForIllegalCrossThreadCalls = false;
            Temp = new Thread(() =>
            {
                try
                {
                    Control.CheckForIllegalCrossThreadCalls = false;
                    SetTextBox("正在试图连接地图更新服务器......\r\n");
                    ThreadPool.SetMinThreads(50, 50);
                    ThreadPool.SetMaxThreads(100, 100);
                    countdown = new MutipleThreadResetEvent(1);
                    object obj = countdown;
                    ThreadPool.QueueUserWorkItem(new WaitCallback(Pingtest), obj);
                    countdown.WaitAll();
                    if (!pingtestpass)
                    {
                        button2.Enabled = button2.Visible = false;
                        button3.Enabled = button3.Visible = false;
                        button1.Enabled = true;
                        return;
                    }
                    citys = inputline.Split(' ');
                    SetTextBox("准备更新坐标......\r\n");
                    SetProgressBar(3);
                    DataBase_city = inputline_backup.Split(' ');
                    posarr = new pos[DataBase_city.Length];
                    if (textBox1.Text == "")
                        eq = citys.Length;
                    else
                        eq = Convert.ToInt32(textBox1.Text);
                    countdown = new MutipleThreadResetEvent(eq);
                    for (int i = 0; i < eq; i++)
                    {
                        object[] objectArray = new object[2];
                        objectArray[0] = i;
                        objectArray[1] = countdown;
                        object param = (object)objectArray;
                        ThreadPool.QueueUserWorkItem(new WaitCallback(Update_Position), param);
                    }
                    SetTextBox("开始更新坐标......\r\n");
                    countdown.WaitAll();
                    SetTextBox("坐标更新完毕......\r\n");
                    SetProgressBar(10);
                    int sig_eq = (eq * (eq - 1)) / 2;
                    SetTextBox("准备更新距离数据......\r\n");
                    SetTextBox("共有" + eq + "个地区需要更新......\r\n");
                    SetTextBox("共需要更新" + sig_eq + "条数据......\r\n");
                    DistanceArray = new int[eq, eq];
                    DistanceArray.Initialize();
                    SetTextBox("正在整理数据库，请稍候......\r\n");
                    for (int i = 0; i < eq; i++)
                    {
                        SetProgressBar(10 + ((i * 10) / eq));
                        for (int j = 0; j < i; j++)
                        {
                            if (distanceclr.IfExist(DataBase_city[i], DataBase_city[j]))
                            {
                                DistanceArray[j, i] = DistanceArray[i, j] = Convert.ToInt32(distanceclr.GetDis(DataBase_city[i], DataBase_city[j]));
                                sig_eq--;
                            }
                        }
                    }
                    SetProgressBar(20);
                    SetTextBox("共需要从网络更新" + sig_eq + "条数据......\r\n");
                    if (sig_eq == 0) goto LISPO;
                    countdown = new MutipleThreadResetEvent(sig_eq);
                    for (int i = 0; i < eq; i++)
                    {
                        SetProgressBar(20 + ((i * 20) / eq));
                        DistanceArray[i, i] = 1;
                        for (int j = 0; j < i; j++)
                        {
                            if (DistanceArray[j, i] == DistanceArray[i, j] && DistanceArray[i, j] != 0) continue;
                            object[] objectArray = new object[3];
                            objectArray[0] = i;
                            objectArray[1] = j;
                            objectArray[2] = countdown;
                            object param = (object)objectArray;
                            ThreadPool.QueueUserWorkItem(new WaitCallback(Update_Distance), param);
                        }
                    }
                    SetTextBox("开始更新坐标和距离数据......\r\n");
                    countdown.WaitAll();
                    SetTextBox("坐标和距离数据更新完成\r\n");
                    SetProgressBar(40);
                    while (!passthrough)
                    {
                        SetTextBox("开始重新校验距离数据.....\r\n");
                        List<int> x = new List<int>();
                        List<int> y = new List<int>();
                        int ssr = 0;
                        passthrough = true;
                        for (int i = 0; i < eq; i++)
                            for (int j = 0; j < i; j++)
                                if (DistanceArray[i, j] == 0 || DistanceArray[j, i] == 0)
                                { x.Add(i); y.Add(j); ssr++; }
                        SetTextBox("有" + ssr + "条数据获取失败或出错.....\r\n");
                        countdown = new MutipleThreadResetEvent(ssr);
                        for (int j = 0; j < ssr; j++)
                        {
                            object[] objectArray = new object[3];
                            objectArray[0] = x[j];
                            objectArray[1] = y[j];
                            objectArray[2] = countdown;
                            object param = (object)objectArray;
                            ThreadPool.QueueUserWorkItem(new WaitCallback(Update_Distance), param);
                        }
                        countdown.WaitAll();
                    }
                    citys = null;
                    SetTextBox("坐标和距离数据校验成功\r\n");
                    SetProgressBar(50);
                    SetTextBox("开始更新本地数据库.....\r\n");
                    distanceclr.Clear();
                    sum = 0;
                    for (int i = 0; i < eq; i++)
                    {
                        SetProgressBar(50 + ((i * 50) / eq));
                        distanceclr.NewDis(DataBase_city[i], DataBase_city[i], DistanceArray[i, i].ToString());
                        for (int j = 0; j < i; j++)
                        {
                            try
                            {
                                distanceclr.NewDis(DataBase_city[i], DataBase_city[j], DistanceArray[i, j].ToString());
                                distanceclr.NewDis(DataBase_city[j], DataBase_city[i], DistanceArray[j, i].ToString());
                                SetTextBox(DataBase_city[i] + "<------->" + DataBase_city[j] + " " + DistanceArray[i, j] + "已写入数据库！\r\n");
                                sum++;
                            }
                            catch (Exception err)
                            {
                                MessageBox.Show(err.Message);
                            }
                        }
                    }
                LISPO:
                    SetProgressBar(100);
                    SetTextBox(sum.ToString() + "条数据更新完成\r\n");
                    button6.Enabled = button6.Visible = true;
                    button3.Enabled = false;
                    try
                    {
                        countdown = null;
                        DistanceArray = null;
                    }
                    catch (Exception err)
                    {
                        if (err.Message != "正在中止线程。")
                            MessageBox.Show(err.StackTrace + "\r\n" + err.Message, "close");
                    }
                    button2.Enabled = button2.Visible = true;
                    button3.Enabled = button3.Visible = false;
                    button1.Enabled = true;
                    GC.Collect();
                    return;
                }
                catch (Exception err)
                {
                    if (err.Message != "正在中止线程。")
                        MessageBox.Show(err.StackTrace + "\r\n" + err.Message, "click");
                }
            });
            Temp.IsBackground = true;
            Temp.Start();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.ScrollToCaret();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SetTextBox("正在停止更新.....\r\n");
            button3.Enabled = false;
            this.CrossThreadCalls(() => { Temp.Abort(); });
            try
            {
                countdown = null;
                DistanceArray = null;
            }
            catch (Exception err)
            {
                if (err.Message != "正在中止线程。")
                    MessageBox.Show(err.StackTrace + "\r\n" + err.Message, "close");
            }
            button2.Enabled = button2.Visible = true;
            button3.Enabled = button3.Visible = false;
            button1.Enabled = true;
            GC.Collect();
        }


        private void button6_Click(object sender, EventArgs e)
        {
            bool pass = false;
            string savepath = "";
            string BaseDBPath = Application.StartupPath + "\\DataBase\\Distance.sdb";
            if (!pass)
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.ShowNewFolderButton = true;
                dialog.Description = "请选择导出的文件夹";
                dialog.SelectedPath = Application.StartupPath;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    savepath = dialog.SelectedPath.ToString();
                }
                else
                {
                    MessageBox.Show("没有选择文件夹", "未选择", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") return;
            DBControllerSet.DistanceDBController disclr = new DBControllerSet.DistanceDBController();
            int sum_db = Convert.ToInt32(Math.Sqrt(disclr.Getsum()));
            int sum_target = Convert.ToInt32(textBox1.Text);
            int toupdate = ((sum_target * (sum_target - 1)) / 2) - ((sum_db * (sum_db - 1)) / 2);
            SetTextBox("数据库中记录： " + sum_db + "\r\n");
            SetTextBox("需要更新条数： " + toupdate + "\r\n");
            GC.Collect();
            return;
        }
    }
}
