using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace JsonLoaderSample
{
//    [JsonConverter(typeof(BaseClassConverter))]
//    public abstract class SceneObject
//    {
//        [JsonProperty("Type")]
//        public abstract SceneObjectTypes Type { get; }

//        [JsonProperty("Name")]
//        public string Name { get; set; }
//    }

//    public enum SceneObjectTypes
//    {
//        Satellite,
//        Earth,
//        Sun,
//        Retranslator,
//        GroundStation,
//    }

//    public class BaseClassConverter : CustomCreationConverter<SceneObject>
//    {
//        private SceneObjectTypes _currentObjectType;

//        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
//        {
//            var jobj = JObject.ReadFrom(reader);
//            _currentObjectType = jobj["Type"].ToObject<SceneObjectTypes>();
//            return base.ReadJson(jobj.CreateReader(), objectType, existingValue, serializer);
//        }

//        public override SceneObject Create(Type objectType)
//        {
//            switch (_currentObjectType)
//            {
//                case SceneObjectTypes.Satellite:
//                    return new Satellite();
//                case SceneObjectTypes.Retranslator:
//                    return new Retranslator();
//                case SceneObjectTypes.GroundStation:
//                    return new GroundStation();
//                case SceneObjectTypes.Sun:
//                    return new Sun();

//                default:
//                    throw new NotImplementedException();
//            }
//        }
//    }

//    [Serializable]
//    public class Satellite : SceneObject
//    {
//        public Satellite() { }

//        /*
//        0    public double x;
//        1    public double y;
//        2    public double z;
//        3    public double vx;
//        4    public double vy;
//        5    public double vz;
//        6    public double u;       
//        */

//        [JsonProperty("Records")]
//        public List<double[]> Records { get; set; } = new List<double[]>();

//        [JsonProperty("Rotations")]
//        public List<Rotation> Rotations { get; set; } = new List<Rotation>();

//        [JsonProperty("Shootings")]
//        public List<Shooting> Shootings { get; set; } = new List<Shooting>();

//        [JsonProperty("Translation")]
//        public List<Translation> Translations { get; set; } = new List<Translation>();

//        [JsonProperty("LifeTimeBegin")]
//        public double LifeTimeBegin { get; set; }

//        [JsonProperty("LifeTimeEnd")]
//        public double LifeTimeEnd { get; set; }

//        [JsonProperty("LifeTimeStep")]
//        public double LifeTimeStep { get; set; }

//        [JsonProperty("Type")]
//        public override SceneObjectTypes Type => SceneObjectTypes.Satellite;
//    }


//    public struct Rotation
//    {
//        public double BeginTime { get; set; } // local(or satellite) time
//        public double EndTime { get; set; } // local(or satellite) time
//        public double Angle { get; set; } // deg
//    }

//    public struct Shooting
//    {
//        public double BeginTime { get; set; } // local(or satellite) time
//        public double EndTime { get; set; } // local(or satellite) time
//        public double Gam1 { get; set; } // deg
//        public double Gam2 { get; set; } // deg
//        public double Range1 { get; set; }
//        public double Range2 { get; set; }
//    }

//    public enum TranslationType
//    {
//        GroundStation = 0,
//        Retranslator = 1
//    }

//    public struct Translation
//    {
//        public double BeginTime { get; set; } // local(or satellite) time
//        public double EndTime { get; set; } // local(or satellite) time
//        public TranslationType Type { get; set; }  // 0 - PPI, 1 - Retranslator
//        public int IndexTarget { get; set; }
//    }

//    [Serializable]
//    public class Retranslator : SceneObject
//    {
//        public Retranslator() { }

//        /*
//0    public double x;
//1    public double y;
//2    public double z;
//3    public double u;       
//*/

//        [JsonProperty("Records")]
//        public List<double[]> Records { get; set; } = new List<double[]>();

//        [JsonProperty("LifeTimeBegin")]
//        public double LifeTimeBegin { get; set; }

//        [JsonProperty("LifeTimeEnd")]
//        public double LifeTimeEnd { get; set; }

//        [JsonProperty("LifeTimeStep")]
//        public double LifeTimeStep { get; set; }

//        [JsonProperty("Type")]
//        public override SceneObjectTypes Type => SceneObjectTypes.Retranslator;
//    }

//    [Serializable]
//    public class SceneLoader
//    {
//        public SceneLoader() { }

//        [JsonProperty("JulianDateBegin")]
//        public double JulianDateBegin { get; set; }

//        [JsonProperty("SceneObjects_retr")]
//        public List<Retranslator> Retranslators { get; set; } = new List<Retranslator>();

//        [JsonProperty("SceneObjects_sat")]
//        public List<Satellite> Satellites { get; set; } = new List<Satellite>();

//        [JsonProperty("GroundStations")]
//        public List<GroundStation> GroundStations { get; set; } = new List<GroundStation>();

//        [JsonProperty("Sun")]
//        public Sun Sun { get; set; } = new Sun();
//    }

//    [Serializable]
//    public class GroundStation : SceneObject
//    {
//        public GroundStation() { }

//        [JsonProperty("Lon")]
//        public double Lon { get; set; }

//        [JsonProperty("Lat")]
//        public double Lat { get; set; }

//        [JsonProperty("Type")]
//        public override SceneObjectTypes Type => SceneObjectTypes.GroundStation;
//    }

//    [Serializable]
//    public class Sun : SceneObject
//    {
//        public Sun() { }

//        /*
//0    public double x1;
//1    public double y1;
//2    public double z1;
//3    public double x2;
//4    public double y2;
//5    public double z2;  
//*/
//        [JsonProperty("Record")]
//        public double[] Record { get; set; } = new double[6];

//        [JsonProperty("LifeTimeBegin")]
//        public double LifeTimeBegin { get; set; }

//        [JsonProperty("LifeTimeEnd")]
//        public double LifeTimeEnd { get; set; }

//        [JsonProperty("Type")]
//        public override SceneObjectTypes Type => SceneObjectTypes.Sun;
//    }
}
