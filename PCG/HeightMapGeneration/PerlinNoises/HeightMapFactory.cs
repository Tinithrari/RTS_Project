using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PerlinNoises
{
    public class HeightMapFactory
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        private static readonly double unit = 1 / Math.Sqrt(2);
        private static readonly Tuple<double, double>[] unitVectors = {
            new Tuple<double, double>(0, 1), new Tuple<double, double>(0, -1),
            new Tuple<double, double>(-1, 0), new Tuple<double, double>(1, 0),
            new Tuple<double, double>(unit, unit), new Tuple<double, double>(unit, -unit),
            new Tuple<double, double>(-unit, unit), new Tuple<double, double>(-unit, -unit)
        };

        private Tuple<double, double>[,] gradient;

        private void ChangeNoiseValue()
        {
            Random generator = new Random();
            for (int i = 0; i < gradient.GetLength(0); ++i)
            {
                for (int j = 0; j < gradient.GetLength(1); ++j)
                {
                    gradient[i, j] = unitVectors[generator.Next(unitVectors.Length)];
                }
            }
        }

        public HeightMapFactory(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.gradient = new Tuple<double, double>[width*4,height*4];
            ChangeNoiseValue();
        }

        private double dotGradientGrid(double x, double y)
        {
            int x0 = (int)Math.Floor(x);
            int y0 = (int)Math.Floor(y);
            int x1 = x0 + 1;
            int y1 = y0 + 1;

            Tuple<double, double> v1 = new Tuple<double, double>(x0 - x, y0 - y);
            Tuple<double, double> v2 = new Tuple<double, double>(x0 - x, y1 - y);
            Tuple<double, double> v3 = new Tuple<double, double>(x1 - x, y0 - y);
            Tuple<double, double> v4 = new Tuple<double, double>(x1 - x, y1 - y);

            
            double icoefX = x0 - x;
            icoefX = (icoefX < 0) ? icoefX * -1 : icoefX;
            double icoefY = y0 - y;
            icoefY = (icoefY < 0) ? icoefY * -1 : icoefY;

            double scalar1 = v1.Item1 * gradient[x0, y0].Item1 + v1.Item2 * gradient[x0, y0].Item2;
            double scalar2 = v2.Item1 * gradient[x0, y1].Item1 + v2.Item2 * gradient[x0, y1].Item2;
            double scalar3 = v3.Item1 * gradient[x1, y0].Item1 + v3.Item2 * gradient[x1, y0].Item2;
            double scalar4 = v4.Item1 * gradient[x1, y1].Item1 + v4.Item2 * gradient[x1, y1].Item2;

            double interpoX = lerp(scalar1, scalar3, icoefX);
            double interpoX2 = lerp(scalar2, scalar4, icoefX);

            return lerp(interpoX, interpoX2, icoefY);
        }

        private static double lerp(double a0, double a1, double w)
        {
            return (1.0 - w) * a0 + w * a1;
        }

        private double perlin(int octaves, double frequency, double persistency, double x, double y)
        {
            double result = 0;
            double f = frequency;
            double amplitude = 1;

            Console.WriteLine(result);
            for (int i = 0; i < octaves; ++i)
            {
                result += dotGradientGrid(x * f, y * f) * amplitude;
                amplitude *= persistency;
                f *= 2;
            }
            Console.WriteLine(result);
            return result * (((1 - persistency) == 0 ? 1 : (1 - persistency)) / ((1 - amplitude) == 0 ? 1 : (1 - amplitude)));
        }

        public Bitmap CreateHeightMap(int octaves, double step, double persistency)
        {
            Bitmap image = new Bitmap(Width, Height);
            for (int i = 0; i < Width; ++i)
            {
                for (int j = 0; j < Height; ++j)
                {
                    double result = perlin(octaves, 1 / step, persistency, i, j) + 1;
                    Console.WriteLine(result);
                    int color = (int)((result) * 127.5);
                    Color c = Color.FromArgb(color, color, color);
                    image.SetPixel(i, j, c);
                }
            }
            return image;
        }
    }
}
