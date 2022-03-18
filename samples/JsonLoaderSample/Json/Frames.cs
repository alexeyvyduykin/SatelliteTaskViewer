using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SatelliteTaskViewer.Avalonia.DatabaseProvider.PostgreSQL;

namespace JsonLoaderSample
{
    //public class Class2
    //{

    //    public void Main()
    //    {
    //        // sat1
    //        {
    //            string path = @"X:\SPEC\Work\Projects\Globe3D 1.1 (NET.Core)\Globe3DViewer\Globe3D.Resources\resources\data\satellite1.globe3d.json";

    //            Satellite sat1 = Load<Satellite>(path);

    //            string pathCreate1 = @"X:\SPEC\Work\Projects\Globe3D 1.1 (NET.Core)\Globe3DViewer\Globe3D.Resources\resources\data\orbital1.globe3d.json";

    //            JsonFrame frame1 = new JsonFrame()
    //            {
    //                Target = "J2000",
    //                Name = "Orbital1",
    //                TimeBegin = sat1.LifeTimeBegin,
    //                TimeEnd = sat1.LifeTimeEnd,
    //                TimeStep = sat1.LifeTimeStep,
    //                Records = sat1.Records
    //            };

    //            SaveFrame(frame1, pathCreate1);
    //        }

    //        // sat2
    //        {
    //            string path = @"X:\SPEC\Work\Projects\Globe3D 1.1 (NET.Core)\Globe3DViewer\Globe3D.Resources\resources\data\satellite2.globe3d.json";

    //            Satellite sat2 = Load<Satellite>(path);

    //            string pathCreate2 = @"X:\SPEC\Work\Projects\Globe3D 1.1 (NET.Core)\Globe3DViewer\Globe3D.Resources\resources\data\orbital2.globe3d.json";

    //            JsonFrame frame2 = new JsonFrame()
    //            {
    //                Target = "J2000",
    //                Name = "Orbital2",
    //                TimeBegin = sat2.LifeTimeBegin,
    //                TimeEnd = sat2.LifeTimeEnd,
    //                TimeStep = sat2.LifeTimeStep,
    //                Records = sat2.Records
    //            };

    //            SaveFrame(frame2, pathCreate2);
    //        }

    //        // sat3
    //        {
    //            string path = @"X:\SPEC\Work\Projects\Globe3D 1.1 (NET.Core)\Globe3DViewer\Globe3D.Resources\resources\data\satellite3.globe3d.json";

    //            Satellite sat3 = Load<Satellite>(path);

    //            string pathCreate3 = @"X:\SPEC\Work\Projects\Globe3D 1.1 (NET.Core)\Globe3DViewer\Globe3D.Resources\resources\data\orbital3.globe3d.json";

    //            JsonFrame frame3 = new JsonFrame()
    //            {
    //                Target = "J2000",
    //                Name = "Orbital3",
    //                TimeBegin = sat3.LifeTimeBegin,
    //                TimeEnd = sat3.LifeTimeEnd,
    //                TimeStep = sat3.LifeTimeStep,
    //                Records = sat3.Records
    //            };

    //            SaveFrame(frame3, pathCreate3);
    //        }

    //        // sat4
    //        {
    //            string path = @"X:\SPEC\Work\Projects\Globe3D 1.1 (NET.Core)\Globe3DViewer\Globe3D.Resources\resources\data\satellite4.globe3d.json";

    //            Satellite sat4 = Load<Satellite>(path);

    //            string pathCreate4 = @"X:\SPEC\Work\Projects\Globe3D 1.1 (NET.Core)\Globe3DViewer\Globe3D.Resources\resources\data\orbital4.globe3d.json";

    //            JsonFrame frame4 = new JsonFrame()
    //            {
    //                Target = "J2000",
    //                Name = "Orbital4",
    //                TimeBegin = sat4.LifeTimeBegin,
    //                TimeEnd = sat4.LifeTimeEnd,
    //                TimeStep = sat4.LifeTimeStep,
    //                Records = sat4.Records
    //            };

    //            SaveFrame(frame4, pathCreate4);
    //        }


    //        SceneLoader scene = LoadScene(@"X:\SPEC\Work\Projects\Globe3D 1.1 (NET.Core)\Globe3DViewer\Globe3D.Resources\resources\data\scene.globe3d.json");

    //        List<Retranslator> retr = scene.Retranslators;

    //        // retr1
    //        {
    //        //    string path = @"X:\SPEC\Work\Projects\Globe3D 1.1 (NET.Core)\Globe3DViewer\Globe3D.Resources\resources\data\retranslator1.globe3d.json";

    //        //    Retranslator retr1 = Load<Retranslator>(path);

    //            string pathCreate5 = @"X:\SPEC\Work\Projects\Globe3D 1.1 (NET.Core)\Globe3DViewer\Globe3D.Resources\resources\data\orbital5.globe3d.json";

    //            List<double[]> list = new List<double[]>();

    //            foreach (var item in retr[0].Records)
    //            {
    //                list.Add(new double[7] { item[0], item[1], item[2], 0.0, 0.0, 0.0, item[3] });
    //            }

    //            JsonFrame frame5 = new JsonFrame()
    //            {
    //                Target = "J2000",
    //                Name = "Orbital5",
    //                TimeBegin = retr[0].LifeTimeBegin,
    //                TimeEnd = retr[0].LifeTimeEnd,
    //                TimeStep = retr[0].LifeTimeStep,
    //                Records = list
    //            };

    //            SaveFrame(frame5, pathCreate5);
    //        }


    //        // retr2
    //        {
    //       //     string path = @"X:\SPEC\Work\Projects\Globe3D 1.1 (NET.Core)\Globe3DViewer\Globe3D.Resources\resources\data\retranslator2.globe3d.json";

    //       //     Retranslator retr2 = Load<Retranslator>(path);

    //            string pathCreate6 = @"X:\SPEC\Work\Projects\Globe3D 1.1 (NET.Core)\Globe3DViewer\Globe3D.Resources\resources\data\orbital6.globe3d.json";

    //            List<double[]> list = new List<double[]>();

    //            foreach (var item in retr[1].Records)
    //            {
    //                list.Add(new double[7] { item[0], item[1], item[2], 0.0, 0.0, 0.0, item[3] });
    //            }

    //            JsonFrame frame6 = new JsonFrame()
    //            {
    //                Target = "J2000",
    //                Name = "Orbital6",
    //                TimeBegin = retr[1].LifeTimeBegin,
    //                TimeEnd = retr[1].LifeTimeEnd,
    //                TimeStep = retr[1].LifeTimeStep,
    //                Records = list
    //            };

    //            SaveFrame(frame6, pathCreate6);
    //        }

    //        // retr3
    //        {
    //        //    string path = @"X:\SPEC\Work\Projects\Globe3D 1.1 (NET.Core)\Globe3DViewer\Globe3D.Resources\resources\data\retranslator3.globe3d.json";

    //        //    Retranslator retr3 = Load<Retranslator>(path);

    //            string pathCreate7 = @"X:\SPEC\Work\Projects\Globe3D 1.1 (NET.Core)\Globe3DViewer\Globe3D.Resources\resources\data\orbital7.globe3d.json";

    //            List<double[]> list = new List<double[]>();

    //            foreach (var item in retr[2].Records)
    //            {
    //                list.Add(new double[7] { item[0], item[1], item[2], 0.0, 0.0, 0.0, item[3] });
    //            }

    //            JsonFrame frame7 = new JsonFrame()
    //            {
    //                Target = "J2000",
    //                Name = "Orbital7",
    //                TimeBegin = retr[2].LifeTimeBegin,
    //                TimeEnd = retr[2].LifeTimeEnd,
    //                TimeStep = retr[2].LifeTimeStep,
    //                Records = list
    //            };

    //            SaveFrame(frame7, pathCreate7);
    //        }

    //        // groundstations
    //        {
    //            string pathCreate8 = @"X:\SPEC\Work\Projects\Globe3D 1.1 (NET.Core)\Globe3DViewer\Globe3D.Resources\resources\data\groundstations.globe3d.json";



    //        }


    //        CreateData();
    //    }

    //    void CreateData()
    //    {

    //        List<JsonDataProvider> dps = new List<JsonDataProvider>();
    //        for (int i = 1; i <= 7; i++)
    //        {
    //            string path = string.Format(@"X:\SPEC\Work\Projects\Globe3D 1.1 (NET.Core)\Globe3DViewer\Globe3D.Resources\resources\data\orbital{0}.globe3d.json", i);

    //            JsonFrame fr = Load<JsonFrame>(path);

    //            JsonDataProvider dp = new JsonDataProvider()
    //            {
    //                Name = "JOrbital" + i,
    //                Target = "J2000",
    //                Data = new JsonData01()
    //                { 
    //                TimeBegin = fr.TimeBegin,
    //                TimeEnd = fr.TimeEnd,
    //                TimeStep = fr.TimeStep,
    //                Records = fr.Records
    //                }
    //            };

    //            dps.Add(dp);

    //        }

    //        Save<List<JsonDataProvider>>(dps, @"X:\SPEC\Work\Projects\Globe3D 1.1 (NET.Core)\Globe3DViewer\Globe3D.Resources\resources\data\true_orbitals.globe3d.json");



    //        for (int i = 1; i <= 4; i++)
    //        {
    //            string path = string.Format(@"X:\SPEC\Work\Projects\Globe3D 1.1 (NET.Core)\Globe3DViewer\Globe3D.Resources\resources\data\satellite{0}.globe3d.json", i);

    //            Satellite sat = Load<Satellite>(path);
               
    //            JsonDataProvider dp1 = new JsonDataProvider()
    //            {
    //                Name = "JSensor" + i,
    //                Target = "JOrbital" + i,
    //                Data = new JsonData02() 
    //                { 
    //                LifeTimeBegin = sat.LifeTimeBegin,
    //                LifeTimeEnd = sat.LifeTimeEnd,
    //                LifeTimeStep = sat.LifeTimeStep,
    //                Shootings = sat.Shootings
    //                }
    //            };


    //            JsonDataProvider dp2 = new JsonDataProvider() 
    //            { 
    //            Name = "JAntenna" + i,
    //            Target = "JOrbital" + i,
    //            Data = new JsonData03()
    //            {
    //                LifeTimeBegin = sat.LifeTimeBegin,
    //                LifeTimeEnd = sat.LifeTimeEnd,
    //                LifeTimeStep = sat.LifeTimeStep,
    //                Translations = sat.Translations
    //            }
    //            };

    //            Save<JsonDataProvider>(dp1, string.Format(@"X:\SPEC\Work\Projects\Globe3D 1.1 (NET.Core)\Globe3DViewer\Globe3D.Resources\resources\data\true_sensor{0}.globe3d.json", i));
    //            Save<JsonDataProvider>(dp2, string.Format(@"X:\SPEC\Work\Projects\Globe3D 1.1 (NET.Core)\Globe3DViewer\Globe3D.Resources\resources\data\true_antenna{0}.globe3d.json", i));


    //        }

    //        string path0 = @"X:\SPEC\Work\Projects\Globe3D 1.1 (NET.Core)\Globe3DViewer\Globe3D.Resources\resources\data\scene.globe3d.json";

    //        SceneLoader scene = Load<SceneLoader>(path0);

    //        List<GroundStation> grs = new List<GroundStation>();
    //        for (int i = 1; i <= scene.GroundStations.Count; i++)
    //        {
    //            grs.Add(new GroundStation() 
    //            { 
    //            Lon = scene.GroundStations[i - 1].Lon,
    //            Lat = scene.GroundStations[i - 1].Lat,
    //            Name = "GroundStation" + i
    //            });
    //        }
    //        JsonDataProvider dp0 = new JsonDataProvider()
    //        {
    //            Name = "JGS",
    //            Target = "J2000",
    //            Data = new JsonData04()
    //            {
    //                GroundStations = grs,                    
    //            }
    //        };

    //        Save<JsonDataProvider>(dp0, @"X:\SPEC\Work\Projects\Globe3D 1.1 (NET.Core)\Globe3DViewer\Globe3D.Resources\resources\data\true_groundstations.globe3d.json");

    //    }


    //    T Load<T>(string pathJson)
    //    {
    //        TextReader reader = null;
    //        try
    //        {
    //            using (reader = new StreamReader(pathJson))
    //            {
    //                JsonSerializer serializer = new JsonSerializer();

    //                return (T)serializer.Deserialize(reader, typeof(T));
    //            }
    //        }
    //        finally
    //        {
    //            if (reader != null)
    //                reader.Close();
    //        }
    //    }

    //    void Save<T>(T frame, string pathJson)
    //    {
    //        TextWriter reader = null;
    //        try
    //        {
    //            using (reader = new StreamWriter(pathJson))
    //            {
    //                JsonSerializer serializer = new JsonSerializer();

    //                serializer.Serialize(reader, frame, typeof(T));

    //                return;
    //            }
    //        }
    //        finally
    //        {
    //            if (reader != null)
    //                reader.Close();
    //        }
    //    }

    //    void SaveFrame(JsonFrame frame, string pathJson)
    //    {
    //        TextWriter reader = null;
    //        try
    //        {
    //            using (reader = new StreamWriter(pathJson))
    //            {
    //                JsonSerializer serializer = new JsonSerializer();

    //                serializer.Serialize(reader, frame, typeof(JsonFrame));

    //                return;
    //            }
    //        }
    //        finally
    //        {
    //            if (reader != null)
    //                reader.Close();
    //        }
    //    }

    //    SceneLoader LoadScene(string pathJson)
    //    {
    //        TextReader reader = null;
    //        try
    //        {
    //            using (reader = new StreamReader(pathJson))
    //            {
    //                JsonSerializer serializer = new JsonSerializer();

    //                return (SceneLoader)serializer.Deserialize(reader, typeof(SceneLoader));
    //            }
    //        }
    //        finally
    //        {
    //            if (reader != null)
    //                reader.Close();
    //        }
    //    }
    //}

    //public class Class2
    //{
    //    public void Main()
    //    {
    //        string connectionString = "Host=localhost;Port=5432;Database=dbGlobe3DLight;Username=postgres;Password=user";

    //        var optionsBuilder = new DbContextOptionsBuilder<dbGlobe3DLightContext>();
    //        var options = optionsBuilder.UseNpgsql(connectionString, options => options.SetPostgresVersion(new Version(9, 6))).Options;

    //        IProjectContainer project = null;

    //        using (var db = new dbGlobe3DLightContext(options))
    //        {
    //            project = GetProject(db);
    //        }

    //        return project;
    //    }
    //}
}
