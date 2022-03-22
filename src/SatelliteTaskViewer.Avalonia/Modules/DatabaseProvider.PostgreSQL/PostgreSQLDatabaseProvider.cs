using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SatelliteTaskViewer.Models.Data;
using SatelliteTaskViewer.ViewModels;
using SatelliteTaskViewer.ViewModels.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SatelliteTaskViewer.Models;

namespace SatelliteTaskViewer.Avalonia.DatabaseProvider.PostgreSQL
{
    public class PostgreSQLDatabaseProvider : IDatabaseProvider
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfigurator _configurator;

        public PostgreSQLDatabaseProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _configurator = serviceProvider.GetService<IConfigurator>();
        }

        public async Task<ScenarioData?> LoadDataAsync() => await Task.Run(() => LoadScenarioDataFromDatabase());

        public async Task<Scenario?> LoadScenarioAsync() => await Task.Run(() => LoadScenarioFromDatabase());

        private Scenario? LoadScenarioFromDatabase()
        {
            using var db = new dbGlobe3DLightContext(GetOptions());
            var scenarioData = GetScenarioData(db);
            return _configurator.GetScenario(scenarioData);
        }

        private ScenarioData LoadScenarioDataFromDatabase()
        {
            using var db = new dbGlobe3DLightContext(GetOptions());
            return GetScenarioData(db);
        }

        private DbContextOptions<dbGlobe3DLightContext> GetOptions()
        {
            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builder.Build();
            // получаем строку подключения
            string connectionString = config.GetConnectionString("DefaultConnection");
            var major = int.Parse(config["PostgresVersionMajor"]);
            var minor = int.Parse(config["PostgresVersionMinor"]);

            var optionsBuilder = new DbContextOptionsBuilder<dbGlobe3DLightContext>();
            var options = optionsBuilder.UseNpgsql(connectionString, options => options.SetPostgresVersion(new Version(major, minor))).Options;

            return options;
            //Scaffold-DbContext "Host=localhost;Port=5432;Database=dbGlobe3DLight;Username=postgres;Password=user"
            //Npgsql.EntityFrameworkCore.PostgreSQL
        }

        private ScenarioData GetScenarioData(dbGlobe3DLightContext db)
        {
            var initialConditions = db.InitialConditions.First();
            var groundStations = db.GroundStations.ToList();
            var retranslators = db.Retranslators.Include(s => s.RetranslatorPositions).ToList();
            var groundObjects = db.GroundObjects.ToList();
            db.SatelliteOrbitPositions.Load();
            db.SatellitePositions.Load();
            db.SatelliteRotations.Load();
            db.SatelliteShootings.Load();
            db.SatelliteToGroundStationTransfers.Load();
            db.SatelliteToRetranslatorTransfers.Load();
            var satellites = db.Satellites.ToList();

            var epoch = initialConditions.JulianDateOnTheDay;
            var begin = initialConditions.ModelingTimeBegin;
            var duration = initialConditions.ModelingTimeDuration;

            return new ScenarioData
            (
                Name: "Scenario1",
                JulianDateOnTheDay: epoch,
                ModelingTimeBegin: begin,
                ModelingTimeDuration: duration,
                Sun: initialConditions.ToSunData(),
                Earth: initialConditions.ToJ2000Data(),
                GroundObjects: groundObjects.OrderBy(s => s.Id).Select(s => s.ToData()).ToList(),
                GroundStations: groundStations.OrderBy(s => s.Id).Select(s => s.ToData()).ToList(),
                RetranslatorPositions: retranslators.OrderBy(s => s.Id).Select(s => s.ToData()).ToList(),
                SatellitePositions: satellites.OrderBy(s => s.Id).Select(s => s.ToSatelliteData()).ToList(),
                SatelliteOrbits: satellites.OrderBy(s => s.Id).Select(s => s.ToOrbitData()).ToList(),
                SatelliteRotations: satellites.OrderBy(s => s.Id).Select(s => s.ToRotationData()).ToList(),
                SatelliteShootings: satellites.OrderBy(s => s.Id).Select(s => s.ToSensorData()).ToList(),
                SatelliteTransfers: satellites.OrderBy(s => s.Id).Select(s => s.ToAntennaData()).ToList()
            );
        }
    }
}
