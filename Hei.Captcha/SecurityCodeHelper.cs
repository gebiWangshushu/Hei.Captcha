using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Hei.Captcha
{
    /// <summary>
    /// 验证码配置和绘制逻辑
    /// </summary>
    public class SecurityCodeHelper
    {
        /// <summary>
        /// 验证码文本池
        /// </summary>
        private static readonly string[] _cnTextArr = new string[] { "的", "一", "国", "在", "人", "了", "有", "中", "是", "年", "和", "大", "业", "不", "为", "发", "会", "工", "经", "上", "地", "市", "要", "个", "产", "这", "出", "行", "作", "生", "家", "以", "成", "到", "日", "民", "来", "我", "部", "对", "进", "多", "全", "建", "他", "公", "开", "们", "场", "展", "时", "理", "新", "方", "主", "企", "资", "实", "学", "报", "制", "政", "济", "用", "同", "于", "法", "高", "长", "现", "本", "月", "定", "化", "加", "动", "合", "品", "重", "关", "机", "分", "力", "自", "外", "者", "区", "能", "设", "后", "就", "等", "体", "下", "万", "元", "社", "过", "前", "面", "农", "也", "得", "与", "说", "之", "员", "而", "务", "利", "电", "文", "事", "可", "种", "总", "改", "三", "各", "好", "金", "第", "司", "其", "从", "平", "代", "当", "天", "水", "省", "提", "商", "十", "管", "内", "小", "技", "位", "目", "起", "海", "所", "立", "已", "通", "入", "量", "子", "问", "度", "北", "保", "心", "还", "科", "委", "都", "术", "使", "明", "着", "次", "将", "增", "基", "名", "向", "门", "应", "里", "美", "由", "规", "今", "题", "记", "点", "计", "去", "强", "两", "些", "表", "系", "办", "教 正", "条", "最", "达", "特", "革", "收", "二", "期", "并", "程", "厂", "如", "道", "际 及", "西", "口", "京", "华", "任", "调", "性", "导", "组", "东", "路", "活", "广", "意", "比", "投", "决", "交", "统", "党", "南", "安", "此", "领", "结", "营", "项", "情", "解", "议", "义", "山", "先", "车", "然", "价", "放", "世", "间", "因", "共", "院", "步", "物", "界", "集", "把", "持", "无", "但", "城", "相", "书", "村", "求", "治", "取", "原", "处", "府", "研", "质", "信", "四", "运", "县", "军", "件", "育", "局", "干", "队", "团", "又", "造", "形", "级", "标", "联", "专", "少", "费", "效", "据", "手", "施", "权", "江", "近", "深", "更", "认", "果", "格", "几", "看", "没", "职", "服", "台", "式", "益", "想", "数", "单", "样", "只", "被", "亿", "老", "受", "优", "常", "销", "志", "战", "流", "很", "接", "乡", "头", "给", "至", "难", "观", "指", "创", "证", "织", "论", "别", "五", "协", "变", "风", "批", "见", "究", "支", "那", "查", "张", "精", "每", "林", "转", "划", "准", "做", "需", "传", "争", "税", "构", "具", "百", "或", "才", "积", "势", "举", "必", "型", "易", "视", "快", "李", "参", "回", "引", "镇", "首", "推", "思", "完", "消", "值", "该", "走", "装", "众", "责", "备", "州", "供", "包", "副", "极", "整", "确", "知", "贸", "己", "环", "话", "反", "身", "选", "亚", "么", "带", "采", "王", "策", "真", "女", "谈", "严", "斯", "况", "色", "打", "德", "告", "仅", "它", "气", "料", "神", "率", "识", "劳", "境", "源", "青", "护", "列", "兴", "许", "户", "马", "港", "则", "节", "款", "拉", "直", "案", "股", "光", "较", "河", "花", "根", "布", "线", "土", "克", "再", "群", "医", "清", "速", "律", "她", "族", "历", "非", "感", "占", "续", "师", "何", "影", "功", "负", "验", "望", "财", "类", "货", "约", "艺", "售", "连", "纪", "按", "讯", "史", "示", "象", "养", "获", "石", "食", "抓", "富", "模", "始", "住", "赛", "客", "越", "闻", "央", "席", "坚" };
        private static readonly string[] _enTextArr = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "k", "m", "n", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        /// <summary>
        /// 验证码图片宽高
        /// </summary>
        private readonly int _imageWidth = 120;
        private readonly int _imageHeight = 50;

        /// <summary>
        /// 泡泡数量
        /// </summary>
        private int _circleCount = 14;
        /// <summary>
        /// 泡泡半径范围
        /// </summary>
        private readonly int _miniCircleR = 2;
        private readonly int _maxCircleR = 8;

        /// <summary>
        /// 颜色池,较深的颜色
        /// https://tool.oschina.net/commons?type=3
        /// </summary>
        private static readonly string[] _colorHexArr = new string[] { "#00E5EE", "#000000", "#2F4F4F", "#000000", "#43CD80", "#191970", "#006400", "#458B00", "#8B7765", "#CD5B45" };
        ///较浅的颜色
        private static readonly string[] _lightColorHexArr = new string[] { "#FFFACD", "#FDF5E6", "#F0FFFF", "#BBFFFF", "#FAFAD2", "#FFE4E1", "#DCDCDC", "#F0E68C" };

        private static readonly Random _random = new Random();

        /// <summary>
        /// 字体池
        /// </summary>
        private static Font[] _fontArr;

        public SecurityCodeHelper()
        {
            initFonts(_imageHeight);
        }

        /// <summary>
        /// 生成随机中文字符串
        /// </summary>
        /// <param name="lenght"></param>
        /// <returns></returns>
        public string GetRandomCnText(int length)
        {
            StringBuilder sb = new StringBuilder();
            if (length > 0)
            {
                do
                {
                    sb.Append(_cnTextArr[_random.Next(0, _cnTextArr.Length)]);
                }
                while (--length > 0);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 生成随机英文字母/数字组合字符串
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public string GetRandomEnDigitalText(int length)
        {
            StringBuilder sb = new StringBuilder();
            if (length > 0)
            {
                do
                {
                    if (_random.Next(0, 2) > 0)
                    {
                        sb.Append(_random.Next(2, 10));
                    }
                    else
                    {
                        sb.Append(_enTextArr[_random.Next(0, _enTextArr.Length)]);
                    }
                }
                while (--length > 0);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取泡泡样式验证码
        /// </summary>
        /// <param name="text">2-3个文字，中文效果较好</param>
        /// <param name="width">验证码宽度，默认宽度100px，可根据传入</param>
        /// <param name="format"></param>
        /// <returns>验证码图片字节数组</returns>
        public byte[] GetBubbleCodeByte(string text)
        {
            using (Image<Rgba32> img = new Image<Rgba32>(_imageWidth, _imageHeight))
            {
                Font textFont = _fontArr.FirstOrDefault(c => "STCAIYUN".Equals(c.Name, StringComparison.CurrentCultureIgnoreCase));
                if (textFont == null)
                {
                    textFont = _fontArr[_random.Next(0, _fontArr.Length)];
                }

                var colorCircleHex = _colorHexArr[_random.Next(0, _colorHexArr.Length)];
                var colorTextHex = colorCircleHex;

                if (_random.Next(0, 6) == 3)
                {
                    colorCircleHex = "#FFFFFF";//白色
                    _circleCount = (int)(_circleCount * 2.65);
                }

                img.Mutate(ctx => ctx
                               .Fill(Rgba32.White)
                               .DrawingCnText(_imageWidth, _imageHeight, text, Rgba32.FromHex(colorTextHex), textFont)
                               .DrawingCircles(_imageWidth, _imageHeight, _circleCount, _miniCircleR, _maxCircleR, Rgba32.FromHex(colorCircleHex))
                           );

                using (var ms = new MemoryStream())
                {
                    img.Save(ms, PngFormat.Instance);
                    return ms.ToArray();
                }
            }
        }

        /// <summary>
        /// 获取动态(gif)泡泡验证码
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public byte[] GetGifBubbleCodeByte(string text)
        {
            var gifCicleCount = (int)(_circleCount * 1.5);
            var color = Rgba32.FromHex(_colorHexArr[_random.Next(0, _colorHexArr.Length)]);
            //优先使用STCAIYUN 这个字体
            Font textFont = _fontArr.FirstOrDefault(c => "STCAIYUN".Equals(c.Name, StringComparison.CurrentCultureIgnoreCase));
            if (textFont == null)
            {
                textFont = _fontArr[_random.Next(0, _fontArr.Length)];
            }

            Image<Rgba32> img = new Image<Rgba32>(_imageWidth, _imageHeight);
            {
                img.Mutate(ctx => ctx
                              .Fill(Rgba32.White)
                              .DrawingCircles(_imageWidth, _imageHeight, gifCicleCount, _miniCircleR, _maxCircleR, color)
                           );

                for (int i = 0; i < 5; i++)
                {
                    using (var tempImg = new Image<Rgba32>(_imageWidth, _imageHeight))
                    {
                        tempImg.Frames[0].MetaData.GetFormatMetaData(GifFormat.Instance).FrameDelay = _random.Next(20, 50);
                        tempImg.Mutate(ctx => ctx
                                          .Fill(Rgba32.White)
                                          .DrawingCircles(_imageWidth, _imageHeight, gifCicleCount, _miniCircleR, _maxCircleR, color)
                                       );
                        img.Frames.AddFrame(tempImg.Frames[0]);
                    }
                }
                img.Frames[0].MetaData.GetFormatMetaData(GifFormat.Instance).FrameDelay = _random.Next(20, 50);
                img.Mutate(ctx => ctx
                        .DrawingCnText(_imageWidth, _imageHeight, text, color, textFont)
                     );
                return img.ToGifArray();
            }
        }

        /// <summary>
        /// 英文字母+数字组合验证码
        /// </summary>
        /// <param name="text"></param>
        /// <param name="singleWordGridCoefficient">单字的划线系数（>=0 系数越大 划线越多）</param>
        /// <param name="gridCoefficient">整体图片的划线系数（>=0 系数越大 划线越多)</param>
        /// <param name="gaussianBlurWeight">高斯模糊（>=0,为0时不模糊）</param>
        /// <param name="circleCount">泡泡个数（>=0,为0时不画泡泡）</param>
        /// <returns>验证码图片字节数组</returns>
        public byte[] GetEnDigitalCodeByte(string text, int singleWordGridCoefficient = 6, int gridCoefficient = 8, float gaussianBlurWeight = 0.4f, int circleCount = 15)
        {
            using (Image<Rgba32> img = getEnDigitalCodeImage(text, singleWordGridCoefficient, gridCoefficient, gaussianBlurWeight, circleCount))
            {
                return img.ToGifArray();
            }
        }

        /// <summary>
        /// 动态(gif)数字字母组合验证码
        /// </summary>
        /// <param name="text"></param>
        /// <param name="singleWordGridCoefficient">单字的划线系数（>=0 系数越大 划线越多）</param>
        /// <param name="gridCoefficient">整体图片的划线系数（>=0 系数越大 划线越多)</param>
        /// <param name="gaussianBlurWeight">高斯模糊（>=0,为0时不模糊）</param>
        /// <param name="circleCount">泡泡个数（>=0,为0时不画泡泡）</param>
        /// <returns></returns>
        public byte[] GetGifEnDigitalCodeByte(string text, int singleWordGridCoefficient = 6, int gridCoefficient = 8, float gaussianBlurWeight = 0.4f, int circleCount = 15)
        {
            using (Image<Rgba32> img = getEnDigitalCodeImage(text, singleWordGridCoefficient, gridCoefficient, gaussianBlurWeight, circleCount))
            {
                for (int i = 0; i < 5; i++)
                {
                    using (Image<Rgba32> tempImg = getEnDigitalCodeImage(text, singleWordGridCoefficient, gridCoefficient, gaussianBlurWeight, circleCount))
                    {
                        tempImg.Frames[0].MetaData.GetFormatMetaData(GifFormat.Instance).FrameDelay = _random.Next(80, 150);
                        img.Frames.AddFrame(tempImg.Frames[0]);
                    }
                }
                img.Frames[0].MetaData.GetFormatMetaData(GifFormat.Instance).FrameDelay = _random.Next(200, 400);
                return img.ToGifArray();
            }
        }


        /// <summary>
        /// 生成一个数组组合验证码素材（Image）
        /// </summary>
        /// <param name="text"></param>
        /// <param name="singleWordGridCoefficient">单字的划线系数（>=0 系数越大 划线越多）</param>
        /// <param name="gridCoefficient">整体图片的划线系数（>=0 系数越大 划线越多)</param>
        /// <param name="gaussianBlurWeight">高斯模糊（>=0,为0时不模糊）</param>
        /// <param name="circleCount">泡泡个数（>=0,为0时不画泡泡）</param>
        /// <returns></returns>
        private Image<Rgba32> getEnDigitalCodeImage(string text, int singleWordGridCoefficient = 6, int gridCoefficient = 8, float gaussianBlurWeight = 0.4f, int circleCount = 15)
        {
            if (gridCoefficient < 0)
            {
                gridCoefficient = 0;
            }
            if (gaussianBlurWeight < 0)
            {
                gaussianBlurWeight = 0;
            }
            if (circleCount < 0)
            {
                circleCount = 0;
            }
            Image<Rgba32> img = new Image<Rgba32>(_imageWidth, _imageHeight);
            var colorTextHex = _colorHexArr[_random.Next(0, _colorHexArr.Length)];
            var lignthColorHex = _lightColorHexArr[_random.Next(0, _lightColorHexArr.Length)];

            img.Mutate(ctx =>
            {
                ctx
                    .Fill(Rgba32.FromHex(_lightColorHexArr[_random.Next(0, _lightColorHexArr.Length)]))
                    .Glow(Rgba32.FromHex(lignthColorHex))
                    .DrawingGrid(_imageWidth, _imageHeight, Rgba32.FromHex(lignthColorHex), gridCoefficient, 1)
                    .DrawingEnText(_imageWidth, _imageHeight, text, _colorHexArr, _fontArr, singleWordGridCoefficient)
                    ;

                if (gaussianBlurWeight > 0)
                {
                    ctx.GaussianBlur(gaussianBlurWeight);
                }
                if (circleCount > 0)
                {
                    ctx.DrawingCircles(_imageWidth, _imageHeight, circleCount, _miniCircleR, _maxCircleR, Rgba32.White);
                }
            });

            return img;
        }

        /// <summary>
        /// 初始化字体池
        /// </summary>
        /// <param name="fontSize">一个初始大小</param>
        private void initFonts(int fontSize)
        {
            if (_fontArr == null)
            {
                var fontDir = "./fonts";
                var list = new List<Font>();

                if (Directory.Exists(fontDir))
                {
                    var fontFiles = Directory.GetFiles(fontDir, "*.ttf");
                    var fontCollection = new FontCollection();

                    if (fontFiles?.Length > 0)
                    {
                        foreach (var ff in fontFiles)
                        {
                            list.Add(new Font(fontCollection.Install(ff), fontSize));
                        }
                    }
                }
                else
                {
                    throw new Exception($"绘制验证码字体文件不存在，请将字体文件(.ttf)复制到目录：{fontDir}");
                }
                _fontArr = list.ToArray();
            }
        }

    }
}
