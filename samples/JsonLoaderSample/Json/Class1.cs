using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using GlmSharp;

namespace JsonLoaderSample
{
    [Serializable]
    public class SceneLoader
    {
        public SceneLoader() { }

        [JsonProperty("JulianDateBegin")]
        public double JulianDateBegin { get; set; }

        [JsonProperty("Sun")]
        public Sun Sun { get; set; } = new Sun();
    }

    [Serializable]
    public class Sun : SceneObject
    {
        public Sun()
        {
            base.Type = SceneObjectTypes.Sun;
        }

        /*
0    public double x1;
1    public double y1;
2    public double z1;
3    public double x2;
4    public double y2;
5    public double z2;  
*/
        [JsonProperty("Record")]
        public double[] Record { get; set; } = new double[6];

        [JsonProperty("LifeTimeBegin")]
        public double LifeTimeBegin { get; set; }

        [JsonProperty("LifeTimeEnd")]
        public double LifeTimeEnd { get; set; }
    }

    public class SceneObject
    {
        public SceneObjectTypes Type { get; set; }

        public string Name { get; set; } = string.Empty;
    }

    public enum SceneObjectTypes
    {
        Satellite,
        Earth,
        Sun,
        Retranslator,
        GroundStation,
    }

    class fddffd
    {
        // ECI
        public static dvec3 GetSunDirection(DateTime time)
        {
            time = time.ToUniversalTime();
            double JD = 367 * time.Year - Math.Floor(7.0 * (time.Year + Math.Floor((time.Month + 9.0) / 12.0)) / 4.0) + Math.Floor(275.0 * time.Month / 9.0) + time.Day + 1721013.5 + time.Hour / 24.0 + time.Minute / 1440.0 + time.Second / 86400.0;
            double pi = 3.14159265359;
            double UT1 = (JD - 2451545) / 36525;
            double longMSUN = 280.4606184 + 36000.77005361 * UT1;
            double mSUN = 357.5277233 + 35999.05034 * UT1;
            double ecliptic = longMSUN + 1.914666471 * Math.Sin(mSUN * pi / 180) + 0.918994643 * Math.Sin(2 * mSUN * pi / 180);
            double eccen = 23.439291 - 0.0130042 * UT1;

            double x = Math.Cos(ecliptic * pi / 180);
            double y = Math.Cos(eccen * pi / 180) * Math.Sin(ecliptic * pi / 180);
            double z = Math.Sin(eccen * pi / 180) * Math.Sin(ecliptic * pi / 180);

            return new dvec3(x, y, z);
        }

        public static dvec3 GetSunPosition(DateTime time)
        {
            double sunDistance = 0.989 * 1.496E+8;
            var sunPosition = GetSunDirection(time.ToUniversalTime());

            sunPosition = sunPosition * sunDistance;

            return sunPosition;
        }
    }
}
