#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using CargoEnvMon.Reader.Infrastructure;
using CargoEnvMon.Reader.Models.Client;

namespace CargoEnvMon.Reader.Models
{
    public static class MeasurementRequestParser
    {
        public static SaveCargoRequest? Parse(string request)
        {
            try
            {
                var lines = request
                    .Replace("\r\n", "\n")
                    .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                var model = ParseHeader(lines[0]);
                model.Items = ParseMeasurements(lines.Skip(1));

                return model;
            }
            catch
            {
                return null;
            }
        }

        private static SaveCargoRequest ParseHeader(string header)
        {
            var parts = header.Split('|');

            return new SaveCargoRequest
            {
                CargoId = parts.TryGetFromIndex(0),
                StartTimestamp = parts.TryGetFromIndex(1),
                TimeShiftUnits = parts.TryGetIntFromIndex(2)
            };
        }

        private static IReadOnlyList<MeasurementModel> ParseMeasurements(IEnumerable<string> lines)
        {
            return lines
                .Select(e =>
                {
                    var parts = e.Split('|');
                    return new MeasurementModel
                    {
                        TimeShift = parts.TryGetIntFromIndex(0),
                        Temperature = parts.TryGetFloatFromIndex(1),
                        Humidity =  parts.TryGetFloatFromIndex(2),
                    };
                })
                .ToArray();
        }
    }
}