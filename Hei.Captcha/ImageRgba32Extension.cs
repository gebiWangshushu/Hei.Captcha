using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hei.Captcha
{
    public static class ImageRgba32Extension
    {
        public static byte[] ToPngArray<TPixel>(this Image<TPixel> img) where TPixel : struct, IPixel<TPixel>
        {
            using (var ms = new MemoryStream())
            {
                img.Save(ms,PngFormat.Instance);
                return ms.ToArray();
            }
        }

        public static byte[] ToGifArray<TPixel>(this Image<TPixel> img) where TPixel : struct, IPixel<TPixel>
        {
            using (var ms = new MemoryStream())
            {
                img.Save(ms, new GifEncoder());
                return ms.ToArray();
            }
        }
    }
}
