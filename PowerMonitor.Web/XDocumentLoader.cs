using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PowerMonitor.Web
{
    public interface IXDocumentLoader
    {
        XDocument LoadForecastDemand();
        XDocument LoadGenerationByFuelType();
        XDocument LoadGenerationByFuelTypeHistoric();
        XDocument LoadRollingSystemFrequency();
        XDocument LoadOutputByYear(int year);
    }
    
    
    public class XDocumentLoader : IXDocumentLoader
    {
        public XDocument LoadForecastDemand()
        {
            return XDocument.Load(@"http://www.bmreports.com/bsp/additional/soapfunctions.php?element=214demand&submit=Invoke");
        }

        public XDocument LoadGenerationByFuelType()
        {
            return XDocument.Load(@"http://www.bmreports.com/bsp/additional/soapfunctions.php?element=generationbyfueltypetable");
        }
        
        public XDocument LoadGenerationByFuelTypeHistoric()
        {
            return XDocument.Load(@"http://www.bmreports.com/bsp/additional/soapfunctions.php?element=generationbyfueltypetablehistoric");
        }

        public XDocument LoadRollingSystemFrequency()
        {
            return XDocument.Load(@"http://www.bmreports.com/bsp/additional/soapfunctions.php?element=rollingfrequency&output");
        }
        
        public XDocument LoadOutputByYear(int year)
        {
            var url = string.Format(@"http://www.bmreports.com/bsp/additional/soapfunctions.php?output=XML&duration=year{0}&element=NOUD&submit=Invoke", year);
            return XDocument.Load(url);        
        }
    }
}