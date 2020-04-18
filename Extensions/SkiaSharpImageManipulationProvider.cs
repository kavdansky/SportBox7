using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Extensions
{
    public static class SkiaSharpImageManipulationProvider
    {
        public static (byte[] FileContents, int Height, int Width) Resize(byte[] fileContents,
            int maxWidth, int maxHeight,
            SKFilterQuality quality = SKFilterQuality.Medium)
        {
            using MemoryStream ms = new MemoryStream(fileContents);
            using SKBitmap sourceBitmap = SKBitmap.Decode(ms);

            int height = Math.Min(maxHeight, sourceBitmap.Height);
            int width = Math.Min(maxWidth, sourceBitmap.Width);

            using SKBitmap scaledBitmap = sourceBitmap.Resize(new SKImageInfo(width, height), quality);
            using SKImage scaledImage = SKImage.FromBitmap(scaledBitmap);
            using SKData data = scaledImage.Encode();

            return (data.ToArray(), height, width);
        }

        public static (byte[] FileContents, int Height, int Width) ResizeStaticProportions(byte[] fileContents,
            int maxWidth,
            SKFilterQuality quality = SKFilterQuality.Medium)
        {
            using MemoryStream ms = new MemoryStream(fileContents);
            using SKBitmap sourceBitmap = SKBitmap.Decode(ms);
            double heightWidthRatio = (double)sourceBitmap.Height/sourceBitmap.Width;


            int height = (int)Math.Min(maxWidth*heightWidthRatio, sourceBitmap.Height);
            int width = Math.Min(maxWidth, sourceBitmap.Width);

            using SKBitmap scaledBitmap = sourceBitmap.Resize(new SKImageInfo(width, height), quality);
            using SKImage scaledImage = SKImage.FromBitmap(scaledBitmap);
            using SKData data = scaledImage.Encode();

            return (data.ToArray(), height, width);
        }
    }
}

