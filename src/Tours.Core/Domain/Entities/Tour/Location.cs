using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tours.Core.Domain.Entities.Tour
{
    public class Location : ValueObject
    {
        private const double EarthRadiusMeters = 6_371_000;

        public double Latitude { get; }
        public double Longitude { get; }
        public string Country { get; }
        public string City { get; }

        [JsonConstructor]
        public Location(double latitude, double longitude, string country, string city)
        {
            Latitude = latitude;
            Longitude = longitude;
            Country = country;
            City = city;
            Validate();
        }

        public bool IsNearby(double latitude, double longitude, double thresholdMeters = 10)
        {
            return DistanceTo(latitude, longitude) <= thresholdMeters;
        }

        public double DistanceTo(double latitude, double longitude)
        {
            double ToRad(double deg) => deg * Math.PI / 180.0;

            var dLat = ToRad(latitude - Latitude);
            var dLon = ToRad(longitude - Longitude);

            var lat1 = ToRad(Latitude);
            var lat2 = ToRad(latitude);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(lat1) * Math.Cos(lat2) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return EarthRadiusMeters * c;
        }

        private void Validate()
        {
            if (Latitude < -90 || Latitude > 90) throw new ArgumentException("Invalid Latitude.");
            if (Longitude < -180 || Longitude > 180) throw new ArgumentException("Invalid Longitude.");
            if (string.IsNullOrWhiteSpace(Country)) throw new ArgumentException("Invalid Country.");
            if (string.IsNullOrWhiteSpace(City)) throw new ArgumentException("Invalid City.");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Longitude;
            yield return Latitude;
            yield return City;
            yield return Country;
        }
    }
}
