using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerMonitor.Web;
using System.Xml.Linq;
using PowerMonitor.Web.Controllers;
using System.Collections.Generic;
using System.Linq;
using PowerMonitor.Web.Models.ChartData;

namespace PowerMonitor.Tests
{
    [TestClass]
    public class ServiceControllerTests
    {
        public class MockXDocumentLoader : IXDocumentLoader
        {
            public XDocument LoadForecastDemand()
            {
                return XDocument.Parse(
                    @"<DAY_AHEAD_TSDFD_DATA_SET>
                        <DAY_AHEAD_TSDFD_DATA><SD>2014-09-02</SD><FORECAST_VALUE>38100.000</FORECAST_VALUE></DAY_AHEAD_TSDFD_DATA>
                        <DAY_AHEAD_TSDFD_DATA><SD>2014-09-03</SD><FORECAST_VALUE>38100.000</FORECAST_VALUE></DAY_AHEAD_TSDFD_DATA>
                        <DAY_AHEAD_TSDFD_DATA><SD>2014-09-04</SD><FORECAST_VALUE>38200.000</FORECAST_VALUE></DAY_AHEAD_TSDFD_DATA>
                        <DAY_AHEAD_TSDFD_DATA><SD>2014-09-05</SD><FORECAST_VALUE>37100.000</FORECAST_VALUE></DAY_AHEAD_TSDFD_DATA>
                        <DAY_AHEAD_TSDFD_DATA><SD>2014-09-06</SD><FORECAST_VALUE>33500.000</FORECAST_VALUE></DAY_AHEAD_TSDFD_DATA>
                        <DAY_AHEAD_TSDFD_DATA><SD>2014-09-07</SD><FORECAST_VALUE>33600.000</FORECAST_VALUE></DAY_AHEAD_TSDFD_DATA>
                        <DAY_AHEAD_TSDFD_DATA><SD>2014-09-08</SD><FORECAST_VALUE>38400.000</FORECAST_VALUE></DAY_AHEAD_TSDFD_DATA>
                        <DAY_AHEAD_TSDFD_DATA><SD>2014-09-09</SD><FORECAST_VALUE>38500.000</FORECAST_VALUE></DAY_AHEAD_TSDFD_DATA>
                        <DAY_AHEAD_TSDFD_DATA><SD>2014-09-10</SD><FORECAST_VALUE>38600.000</FORECAST_VALUE></DAY_AHEAD_TSDFD_DATA>
                        <DAY_AHEAD_TSDFD_DATA><SD>2014-09-11</SD><FORECAST_VALUE>38800.000</FORECAST_VALUE></DAY_AHEAD_TSDFD_DATA>
                        <DAY_AHEAD_TSDFD_DATA><SD>2014-09-12</SD><FORECAST_VALUE>37100.000</FORECAST_VALUE></DAY_AHEAD_TSDFD_DATA>
                        <DAY_AHEAD_TSDFD_DATA><SD>2014-09-13</SD><FORECAST_VALUE>34600.000</FORECAST_VALUE></DAY_AHEAD_TSDFD_DATA>
                    </DAY_AHEAD_TSDFD_DATA_SET>");
            }

            public XDocument LoadGenerationByFuelType()
            {
                throw new NotImplementedException();
            }

            public XDocument LoadGenerationByFuelTypeHistoric()
            {
                throw new NotImplementedException();
            }

            public XDocument LoadRollingSystemFrequency()
            {
                throw new NotImplementedException();
            }

            public XDocument LoadOutputByYear(int year)
            {
                throw new NotImplementedException();
            }
        }


        [TestMethod]
        public void WhenCallingForecastDemand_LabelsShouldBePopulated()
        {
            var expected = new[] { "2014-09-02", "2014-09-03", "2014-09-04", "2014-09-05", "2014-09-06", "2014-09-07", "2014-09-08", "2014-09-09", "2014-09-10", "2014-09-11", "2014-09-12", "2014-09-13" };

            var serviceController = new ServiceController
            {
                XDocumentLoader = new MockXDocumentLoader()
            };

            var result = serviceController.ForecastDemand();

            Assert.IsTrue(expected.SequenceEqual(result.labels));
        }

        [TestMethod]
        public void WhenCallingForecastDemand_FirstDatasetShouldHavePopulatedData()
        {
            var expected = new[] { 38100.000M, 38100.000M, 38200.000M, 37100.000M, 33500.000M, 33600.000M, 38400.000M, 38500.000M, 38600.000M, 38800.000M, 37100.000M, 34600.000M };

            var serviceController = new ServiceController
            {
                XDocumentLoader = new MockXDocumentLoader()
            };

            var forecastDemand = serviceController.ForecastDemand();

            var data = forecastDemand.datasets.First().data;

            Assert.IsTrue(expected.SequenceEqual(data));
        }

        [TestMethod]
        public void WhenCallingForecastDemand_OnlyOneDatasetShouldBeReturned()
        {
            var serviceController = new ServiceController
            {
                XDocumentLoader = new MockXDocumentLoader()
            };

            var forecastDemand = serviceController.ForecastDemand();

            Assert.AreEqual(1, forecastDemand.datasets.Count());
        }
    }
}
