using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTravelMap.Models
{
    /// <summary>
    /// Маршрут
    /// </summary>
    public class Route
    {
        public string Name { get; set; }
        public List <Place> Places { get; set; }

    }
}
