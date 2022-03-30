using System;
using System.Collections.Generic;
using CargoEnvMon.Reader.Models;
using CargoEnvMon.Reader.Models.Client;
using FluentAssertions;
using NUnit.Framework;

namespace CargoEnvMon.Reader.Tests
{
    public class MeasurementRequestParserTest
    {
        private const string UTC_TIMESTAMP = "1648410051380";
        private const string CARGO_ID = "slo-3874";

        [TestCaseSource(nameof(GetCases))]
        public void Test(string request, SaveCargoRequest expected)
        {
            MeasurementRequestParser
                .Parse(request)
                .Should()
                .BeEquivalentTo(expected);
        }

        public static IEnumerable<TestCaseData> GetCases()
        {
            yield return new TestCaseData(null, null).SetName("Null");

            yield return new TestCaseData(
                $"{CARGO_ID}|{UTC_TIMESTAMP}|3",
                new SaveCargoRequest
                {
                    CargoId = CARGO_ID,
                    StartTimestamp = UTC_TIMESTAMP,
                    TimeShiftUnits = 3,
                    Items = ArraySegment<MeasurementModel>.Empty
                }
            ).SetName("Header line only");
            
            yield return new TestCaseData(
                $@"{CARGO_ID}|{UTC_TIMESTAMP}|3
12|33.1|78
13|43.1|93
",
                new SaveCargoRequest
                {
                    CargoId = CARGO_ID,
                    StartTimestamp = UTC_TIMESTAMP,
                    TimeShiftUnits = 3,
                    Items = new []
                    {
                        new MeasurementModel
                        {
                            Humidity = 78,
                            Temperature = 33.1F,
                            TimeShift = 12
                        },
                        new MeasurementModel
                        {
                            Humidity = 93,
                            Temperature = 43.1F,
                            TimeShift = 13
                        }
                    }
                }
            ).SetName("Multiple lines");
            
            yield return new TestCaseData(
                $@"{CARGO_ID}|{UTC_TIMESTAMP}|
12|33.1|
13||93
",
                new SaveCargoRequest
                {
                    CargoId = CARGO_ID,
                    StartTimestamp = UTC_TIMESTAMP,
                    TimeShiftUnits = null,
                    Items = new []
                    {
                        new MeasurementModel
                        {
                            Humidity = null,
                            Temperature = 33.1F,
                            TimeShift = 12
                        },
                        new MeasurementModel
                        {
                            Humidity = 93,
                            Temperature = null,
                            TimeShift = 13
                        }
                    }
                }
            ).SetName("Multiple lines. Missing data");
            
        }
    }
}