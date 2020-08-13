using System;
using System.Collections.Generic;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Hei.Captcha
{
    public static class ImageSharpExtension
    {

        /// <summary>
        /// 绘制中文字符（可以绘制字母数字，但样式可能需要改）
        /// </summary>
        /// <typeparam name="TPixel"></typeparam>
        /// <param name="processingContext"></param>
        /// <param name="containerWidth"></param>
        /// <param name="containerHeight"></param>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <param name="font"></param>
        /// <returns></returns>
        public static IImageProcessingContext DrawingCnText(this IImageProcessingContext processingContext, int containerWidth, int containerHeight, string text, Rgba32 color, Font font)
        {
            if (string.IsNullOrEmpty(text)) return processingContext;

            var size = processingContext.GetCurrentSize();
            Random random = new Random();
            var textWidth = (size.Width / text.Length);
            var img2Size = Math.Min(textWidth, size.Height);
            var fontMiniSize = (int)(img2Size * 0.6);
            var fontMaxSize = (int)(img2Size * 0.95);

            for (int i = 0; i < text.Length; i++)
            {
                using (Image<Rgba32> img2 = new Image<Rgba32>(img2Size, img2Size))
                {
                    Font scaledFont = new Font(font, random.Next(fontMiniSize, fontMaxSize));
                    var point = new Point(i * textWidth, (containerHeight - img2.Height) / 2);
                    var textGraphicsOptions = new TextGraphicsOptions
                    {
                        TextOptions = new TextOptions
                        {
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top
                        }
                    };

                    img2.Mutate(ctx => ctx
                        .DrawText(textGraphicsOptions, text[i].ToString(), scaledFont, color, new Point(0, 0))
                        .Rotate(random.Next(-45, 45))
                    );
                    processingContext.DrawImage(img2, point, 1); //.Mutate(ctx => ctx.DrawImage(img2, point, 1));
                }
            }

            return processingContext;
        }

        public static IImageProcessingContext DrawingEnText(this IImageProcessingContext processingContext,
            int containerWidth,
            int containerHeight,
            string text,
            string[] colorHexArr,
            Font[] fonts,
            int grids = 6,
            int rotate = 45)
        {
            if (string.IsNullOrEmpty(text)) return processingContext;

            var size = processingContext.GetCurrentSize();
            Random random = new Random();
            var textWidth = (size.Width / text.Length);
            var img2Size = Math.Min(textWidth, size.Height);
            var fontMiniSize = (int)(img2Size * 0.9);
            var fontMaxSize = (int)(img2Size * 1.37);
            Array fontStyleArr = Enum.GetValues(typeof(FontStyle));

            for (int i = 0; i < text.Length; i++)
            {
                using (Image<Rgba32> img2 = new Image<Rgba32>(img2Size, img2Size))
                {
                    Font scaledFont = new Font(fonts[random.Next(0, fonts.Length)], random.Next(fontMiniSize, fontMaxSize), (FontStyle)fontStyleArr.GetValue(random.Next(fontStyleArr.Length)));
                    var point = new Point(i * textWidth, (containerHeight - img2.Height) / 2);
                    var colorHex = colorHexArr[random.Next(0, colorHexArr.Length)];
                    var textGraphicsOptions = new TextGraphicsOptions
                    {
                        TextOptions = new TextOptions
                        {
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Top
                        }
                    };

                    img2.Mutate(ctx => ctx
                                    .DrawText(textGraphicsOptions, text[i].ToString(), scaledFont, Rgba32.ParseHex(colorHex), new Point(0, 0))
                                    .DrawingGrid(containerWidth, containerHeight, Rgba32.ParseHex(colorHex), grids, 1)
                                    .Rotate(random.Next(-1 * rotate, rotate))
                                );
                    processingContext.DrawImage(img2, point, 1);
                }
            }

            return processingContext;
        }

        /// <summary>
        /// 画圆圈（泡泡）
        /// </summary>
        /// <param name="processingContext"></param>
        /// <param name="containerWidth"></param>
        /// <param name="containerHeight"></param>
        /// <param name="count"></param>
        /// <param name="miniR"></param>
        /// <param name="maxR"></param>
        /// <param name="color"></param>
        /// <param name="canOverlap"></param>
        /// <returns></returns>
        public static IImageProcessingContext DrawingCircles(this IImageProcessingContext processingContext, int containerWidth, int containerHeight, int count, int miniR, int maxR, Color color, bool canOverlap = false)
        {
            if (count <= 0) return processingContext;

            EllipsePolygon ep = null;
            Random random = new Random();
            PointF tempPoint = new PointF();
            List<PointF> points = new List<PointF>();

            for (int i = 0; i < count; i++)
            {
                if (canOverlap)
                {
                    tempPoint = new PointF(random.Next(0, containerWidth), random.Next(0, containerHeight));
                }
                else
                {
                    tempPoint = GetCirclePointF(containerWidth, containerHeight, (miniR + maxR), ref points);
                }

                ep = new EllipsePolygon(tempPoint, random.Next(miniR, maxR));
                
                processingContext.Draw(color, (float)(random.Next(94, 145) / 100.0), ep.Clip());
            }

            return processingContext;
        }
        /// <summary>
        /// 画杂线
        /// </summary>
        /// <typeparam name="TPixel"></typeparam>
        /// <param name="processingContext"></param>
        /// <param name="containerWidth"></param>
        /// <param name="containerHeight"></param>
        /// <param name="color"></param>
        /// <param name="count"></param>
        /// <param name="thickness"></param>
        /// <returns></returns>
        public static IImageProcessingContext DrawingGrid(this IImageProcessingContext processingContext, int containerWidth, int containerHeight, Color color, int count, float thickness)
        {
            if (count <= 0) return processingContext;

            var points = new List<PointF> { new PointF(0, 0) };
            for (int i = 0; i < count; i++)
            {
                GetCirclePointF(containerWidth, containerHeight, 9, ref points);
            }
            points.Add(new PointF(containerWidth, containerHeight));
            processingContext.DrawLines(color, thickness, points.ToArray());

            return processingContext;
        }

        /// <summary>
        /// 散 随机点
        /// </summary>
        /// <param name="containerWidth"></param>
        /// <param name="containerHeight"></param>
        /// <param name="lapR"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private static PointF GetCirclePointF(int containerWidth, int containerHeight, double lapR, ref List<PointF> list)
        {
            Random random = new Random();
            PointF newPoint = new PointF();
            int retryTimes = 10;

            do
            {
                newPoint.X = random.Next(0, containerWidth);
                newPoint.Y = random.Next(0, containerHeight);
                bool tooClose = false;
                foreach (var p in list)
                {
                    var tempDistance = Math.Sqrt((Math.Pow((p.X - newPoint.X), 2) + Math.Pow((p.Y - newPoint.Y), 2)));
                    if (tempDistance < lapR)
                    {
                        tooClose = true;
                        break;
                    }
                }
                if (tooClose == false)
                {
                    list.Add(newPoint);
                    break;
                }
            }
            while (retryTimes-- > 0);

            if (retryTimes <= 0)
            {
                list.Add(newPoint);
            }
            return newPoint;
        }
    }
}
