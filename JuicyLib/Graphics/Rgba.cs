using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace JuicyLib.Graphics
{
    public struct Rgba
    {
        public static implicit operator Rgba(string s)
        {
            return new Rgba(s);
        }

        public static implicit operator string (Rgba rgba)
        {
            return rgba.ToString();
        }

        public Rgba(int r, int g, int b)
        {
            color = new int[] { r, g, b, 255 };
        }

        public Rgba(int r, int g, int b, int a)
        {
            color = new int[] { r, g, b, a };
        }

        public Rgba(int[] rgba)
        {
            color = rgba;
        }

        public Rgba(string rgba)
        {
            rgba.Replace(" ", "");
            string[] s = rgba.Split(',');

            color = new int[4];
            for (int i = 0; i < 4; i++)
            {
                color[i] = 255;
            }

            for (int iC = 0; iC < s.Length; iC++)
            {
                int.TryParse(s[iC], out color[iC]);
            }
        }

        int[] color;
        public int R { get => color[0]; }
        public int G { get => color[1]; }
        public int B { get => color[2]; }
        public int A { get => color[3]; }

        public Color Color
        {
            get
            {
                return Color.FromArgb(A, R, G, B);
            }
        }

        public override string ToString() => $"[R={R}, G={G}, B={B}, A={A}]";
    }
}
