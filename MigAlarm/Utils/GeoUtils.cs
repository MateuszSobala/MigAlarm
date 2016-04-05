using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MigAlarm.Utils
{
    public static class GeoUtils
    {
        /// <summary>
        /// Create a GeoLocation point based on latitude and longitude
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public static DbGeography CreatePoint(double latitude, double longitude)
        {
            var lon = longitude.ToString(CultureInfo.InvariantCulture);
            var lat = latitude.ToString(CultureInfo.InvariantCulture);
            var point = string.Format("POINT({0} {1})", lon, lat);
            return DbGeography.PointFromText(point, DbGeography.DefaultCoordinateSystemId);
        }

        /// <summary>
        /// Create a GeoLocation point based on latitude and longitude
        /// </summary>
        /// <param name="latitudeLongitude">
        /// String should be two values either single comma or space delimited
        /// 45.710030,-121.516153
        /// 45.710030 -121.516153
        /// </param>
        /// <returns></returns>
        public static DbGeography CreatePoint(string latitudeLongitude)
        {
            var tokens = latitudeLongitude.Split(',', ' ');
            if (tokens.Length != 2)
                throw new ArgumentException();
            var lon = tokens[1].ToString(CultureInfo.InvariantCulture);
            var lat = tokens[0].ToString(CultureInfo.InvariantCulture);
            var text = string.Format("POINT({0} {1})", lon, lat);
            return DbGeography.PointFromText(text, 4326);
        }
    }
}