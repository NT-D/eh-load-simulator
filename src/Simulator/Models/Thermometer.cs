using System;

namespace Simulator
{
    public class Thermometer
    {
        public string SensorId { get; }
        public string SensorType { get { return nameof(Thermometer); } }
        public double Value { get; }
        public string GeneratedDate { get; }

        public Thermometer(string id, double value)
        {
            SensorId = id;
            Value = value;
            GeneratedDate = DateTime.UtcNow.ToString("s");
        }
    }
}