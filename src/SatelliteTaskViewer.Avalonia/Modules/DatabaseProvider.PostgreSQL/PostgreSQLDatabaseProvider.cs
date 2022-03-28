using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SatelliteTaskViewer.Models;
using SatelliteTaskViewer.Models.Data;
using SatelliteTaskViewer.ViewModels;
using SatelliteTaskViewer.ViewModels.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SatelliteTaskViewer.Avalonia.DatabaseProvider.PostgreSQL
{
    public class PostgreSQLDatabaseProvider : IDatabaseProvider
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfigurator _configurator;
        private readonly IConfigurationRoot _configurationRoot;

        public PostgreSQLDatabaseProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _configurator = serviceProvider.GetService<IConfigurator>();
            _configurationRoot = serviceProvider.GetService<IConfigurationRoot>();
        }

        public async Task<ScenarioData?> LoadDataAsync()
        {
            return await Task.Run(() =>
            {
                using var db = new dbGlobe3DLightContext(GetOptions());
                return GetScenarioData(db);
            });
        }

        public async Task<Scenario?> LoadScenarioAsync()
        {
            var data = await Task.Run(() => LoadDataAsync());

            if (data != null)
            {
                return _configurator.GetScenario(data);
            }

            return default;
        }

        private DbContextOptions<dbGlobe3DLightContext> GetOptions()
        {
            string connectionString = _configurationRoot.GetConnectionString("DefaultConnection");
            var major = int.Parse(_configurationRoot["PostgresVersionMajor"]);
            var minor = int.Parse(_configurationRoot["PostgresVersionMinor"]);

            var optionsBuilder = new DbContextOptionsBuilder<dbGlobe3DLightContext>();
            var options = optionsBuilder.UseNpgsql(connectionString, options => options.SetPostgresVersion(new Version(major, minor))).Options;

            return options;
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
