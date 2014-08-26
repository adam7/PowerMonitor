using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;
using PowerMonitor.Web.Extensions;
using System.Drawing;

namespace PowerMonitor.Web.Controllers
{
    public class FuelType
    {
        public FuelType(string name, string code, Color color)
        {
            Name = name;
            Code = code;
            PointColor = string.Format("rgba({0}, {1}, {2},  1)", color.R, color.G, color.B);
            LineColor = string.Format("rgba({0}, {1}, {2},  0.6)", color.R, color.G, color.B);
            FillColor = string.Format("rgba({0}, {1}, {2},  0.1)", color.R, color.G, color.B);
        }

        public string Name { get; set; }
        public string Code { get; set; }
        public string PointColor { get; private set; }
        public string LineColor { get; private set; }
        public string FillColor { get; private set; }
    }

    public class ServiceController : ApiController
    {
        List<FuelType> fuelTypes = new List<FuelType>{
            new FuelType("Combined Cycle Gas Turbine",  "CCGT", Color.OliveDrab), 
            new FuelType("Open Cycle Gas Turbine", "OCGT", Color.PaleVioletRed), 
            new FuelType("Oil", "OIL", Color.PapayaWhip), 
            new FuelType("Coal", "COAL", Color.PowderBlue), 
            new FuelType("Nuclear", "NUCLEAR", Color.RosyBrown),
            new FuelType("Wind", "WIND", Color.Salmon), 
            new FuelType("Pumped Storage", "PS", Color.SeaShell),
            new FuelType("Non-Pumped Storage Hydro", "NPSHYD", Color.Silver),
            new FuelType("Other", "OTHER", Color.SkyBlue),
            new FuelType("France Interconnector", "INTFR", Color.SlateBlue),
            new FuelType("Ireland Interconnector", "INTIRL", Color.SlateGray),
            new FuelType("Netherlands Interconnector", "INTNED", Color.OrangeRed),
            new FuelType("East-West Interconnector", "INTEW", Color.SteelBlue ),
        };

        int GetFuelValue(IEnumerable<XElement> query, string typeName)
        {
            return Convert.ToInt32(query.Elements("FUEL").First(e => e.Attribute("TYPE").Value == typeName).Attribute("VAL").Value);
        }

        IEnumerable<string> GetFuelValues(IEnumerable<XElement> query, string typeName)
        {
            return query.Select(e => e.Elements("FUEL").First(x => x.Attribute("TYPE").Value == typeName).Attribute("VAL").Value);
        }

        IEnumerable<dynamic> GetGenerationByFuelTypeHistoricDataSets(IEnumerable<XElement> query)
        {
            foreach (FuelType fuelType in fuelTypes)
            {
                yield return new
                            {
                                label = fuelType.Name,
                                fillColor = fuelType.FillColor,
                                strokeColor = fuelType.LineColor,
                                pointColor = fuelType.PointColor,
                                pointStrokeColor = fuelType.LineColor,
                                pointHighlightFill = fuelType.FillColor,
                                pointHighlightStroke = fuelType.PointColor,
                                data = GetFuelValues(query, fuelType.Code)
                            };
            }
        }

        IEnumerable<dynamic> GetGenerationByFuelTypeData(IEnumerable<XElement> query)
        {
            foreach (FuelType fuelType in fuelTypes)
            {
                yield return new
                {
                    label = fuelType.Name,
                    value = GetFuelValue(query, fuelType.Code),
                    color = fuelType.PointColor,
                    highlight = fuelType.LineColor
                };
            }
        }


        [HttpGet]
        public dynamic GenerationByFuelType()
        {
            var xml = XDocument.Load(@"http://www.bmreports.com/bsp/additional/soapfunctions.php?element=generationbyfueltypetable");

            var query = xml.Root.Elements("LAST24H");

            return GetGenerationByFuelTypeData(query);
        }

        [HttpGet]
        public dynamic GenerationByFuelTypeHistoric()
        {
            var xml = XDocument.Load(@"http://www.bmreports.com/bsp/additional/soapfunctions.php?element=generationbyfueltypetablehistoric");

            var query = xml.Root.Elements("INST").Where(e => e.Attribute("AT").Value.Tail(5) == "00:00");

            return new
            {
                labels = query.Select(e => e.Attribute("AT").Value),
                datasets = GetGenerationByFuelTypeHistoricDataSets(query)
            };
        }

        [HttpGet]
        public dynamic RollingSystemFrequency()
        {
            var xml = XDocument.Load(@"http://www.bmreports.com/bsp/additional/soapfunctions.php?element=rollingfrequency&output");

            var query = xml.Root.Elements("ST").Where(e => e.Attribute("ST").Value.Tail(3) == ":00");

            return new
            {
                labels = query.Select(e => e.Attribute("ST").Value),
                datasets = new[] { 
                    new {
                        label = "Rolling System Frequency",
                        fillColor = "rgba(220,220,220,0.2)",
                        strokeColor = "rgba(220,220,220,1)",
                        pointColor = "rgba(220,220,220,1)",
                        pointStrokeColor = "#fff",
                        pointHighlightFill = "#fff",
                        pointHighlightStroke = "rgba(220,220,220,1)",
                        data = query.Select(e => e.Attribute("VAL").Value)                            
                    }
                }

            };
        }

        [HttpGet]
        public dynamic OutputByYear(int year)
        {
            var xml = XDocument.Load(@"http://www.bmreports.com/bsp/additional/soapfunctions.php?output=XML&duration=year1&element=NOUD&submit=Invoke");

            var query = xml.Root.Elements("SD")
                .Where(e => e.Attribute("WN") != null);

            return new
            {
                labels = query.Select(e => e.Attribute("WN").Value),
                datasets = new[] {
                    new {
                        label = "My First dataset",
                        fillColor = "rgba(220,220,220,0.2)",
                        strokeColor = "rgba(220,220,220,1)",
                        pointColor = "rgba(220,220,220,1)",
                        pointStrokeColor = "#fff",
                        pointHighlightFill = "#fff",
                        pointHighlightStroke = "rgba(220,220,220,1)",
                        data = query.Select(e => e.Element("SERIES").Attribute("VAL").Value)
                        }
                }
            };
        }

        [HttpGet]
        public dynamic ForecastDemand()
        {
            var xml = XDocument.Load(@"http://www.bmreports.com/bsp/additional/soapfunctions.php?element=214demand&submit=Invoke");

            var query = xml.Root.Elements("DAY_AHEAD_TSDFD_DATA");

            return new
            {
                labels = query.Select(e => e.Element("SD").Value),
                datasets = new[] {
                    new {
                        label = "My First dataset",
                        fillColor = "rgba(220,220,220,0.2)",
                        strokeColor = "rgba(220,220,220,1)",
                        pointColor = "rgba(220,220,220,1)",
                        pointStrokeColor = "#fff",
                        pointHighlightFill = "#fff",
                        pointHighlightStroke = "rgba(220,220,220,1)",
                        data = query.Select(e => e.Element("FORECAST_VALUE").Value)
                        }
                }
            };
        }

    }
}
