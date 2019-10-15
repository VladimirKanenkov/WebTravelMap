using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using WebTravelMap.Models;
using System.ComponentModel;

namespace WebTravelMap.Controllers
{
    public class HomeController : Controller
    {
        List<Route> Routes = new List<Route>();
        public IActionResult Index()
        {            
            return View(Routes);
        }

        /// <summary>
        /// Получить данные из XML
        /// </summary>
        void GetRoutesFromXml()
        {
            XmlDocument xDoc = new XmlDocument();

            string filename= new DirectoryInfo(@"..").FullName + @"\Travel.xml";
            xDoc.Load(filename);
            XmlElement xRoot = xDoc.DocumentElement;
            // выбор всех дочерних узлов
            XmlNodeList childRoutes = xRoot.SelectNodes("route");
            foreach (XmlNode n in childRoutes)
            {
                XmlNode attr = n.Attributes.GetNamedItem("part");
                Route route = new Route() {
                    Name = attr.Value,
                    Places = new List<Place>()
                };
                Routes.Add(route);

                foreach (XmlNode placeNode in n.ChildNodes)
                {
                    Place place = new Place();
                    route.Places.Add(place);
                    foreach (XmlNode childNode in placeNode.ChildNodes)
                    {
                        if (childNode.Name == "name")
                        {
                            place.Name = childNode.InnerText;
                        }
                        if (childNode.Name == "lat")
                        {
                            place.Lat = double.Parse(childNode.InnerText, CultureInfo.InvariantCulture);
                        }
                        if (childNode.Name == "long")
                        {
                            place.Long = double.Parse(childNode.InnerText, CultureInfo.InvariantCulture);
                        }
                    }
                }
            }

        }

        public IActionResult LoadXml()
        {
            GetRoutesFromXml();
            return View("Index", Routes);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Задание";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Контакты";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }





    }
}
