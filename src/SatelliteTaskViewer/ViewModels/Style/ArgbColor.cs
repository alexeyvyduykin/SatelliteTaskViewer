using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using SatelliteTaskViewer.Models.Style;
using ReactiveUI.Fody.Helpers;

namespace SatelliteTaskViewer.ViewModels.Style
{
    public class ArgbColor : ViewModelBase, IArgbColor
    {
        [Reactive]
        public byte A { get; set; }

        [Reactive]
        public byte R { get; set; }

        [Reactive]
        public byte G { get; set; }

        [Reactive]
        public byte B { get; set; }

        public string ToXamlString() => ToXamlHex(this);

        public string ToSvgString() => ToSvgHex(this);

        public static IArgbColor FromUInt32(uint value)
        {
            return new ArgbColor
            {
                A = (byte)((value >> 24) & 0xff),
                R = (byte)((value >> 16) & 0xff),
                G = (byte)((value >> 8) & 0xff),
                B = (byte)(value & 0xff),
            };
        }

        public static IArgbColor FromArgb(byte r, byte g, byte b, byte a)
        {
            return new ArgbColor
            {
                A = a,
                R = r,
                G = g,
                B = b,
            };
        }

        public static void Parse(string s, out uint color)
        {
            if (s[0] == '#')
            {
                var or = 0u;

                if (s.Length == 7)
                {
                    or = 0xff000000;
                }
                else if (s.Length != 9)
                {
                    throw new FormatException($"Invalid color string: '{s}'.");
                }

                color = uint.Parse(s.Substring(1), NumberStyles.HexNumber, CultureInfo.InvariantCulture) | or;
            }
            else
            {
                var upper = s.ToUpperInvariant();
                var member = typeof(Colors).GetTypeInfo().DeclaredProperties.FirstOrDefault(x => x.Name.ToUpperInvariant() == upper);
                if (member is not null)
                {
                    color = (uint)member.GetValue(null);
                }
                else
                {
                    throw new FormatException($"Invalid color string: '{s}'.");
                }
            }
        }

        public static IArgbColor Parse(string s)
        {
            Parse(s, out var value);
            return FromUInt32(value);
        }

        public static string ToXamlHex(IArgbColor c)
        {
            return string.Concat('#', c.A.ToString("X2"), c.R.ToString("X2"), c.G.ToString("X2"), c.B.ToString("X2"));
        }

        public static string ToSvgHex(IArgbColor c)
        {
            return string.Concat('#', c.R.ToString("X2"), c.G.ToString("X2"), c.B.ToString("X2")); // NOTE: Not using c.A.ToString("X2")
        }
    }
}
