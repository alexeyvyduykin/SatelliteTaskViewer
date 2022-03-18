using System;
using System.Collections.Generic;
using System.Text;

namespace SatelliteTaskViewer.Models
{
    public interface IJsonSerializer
    {
        string Serialize<T>(T value);

        string SerializerWithSettings<T>(T value);

        T Deserialize<T>(string json);

        T DeserializeWithSettings<T>(string json);
    }
}
