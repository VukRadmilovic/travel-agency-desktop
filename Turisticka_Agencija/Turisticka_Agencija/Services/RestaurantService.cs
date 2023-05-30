using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Web;
using Turisticka_Agencija.Models;
using Turisticka_Agencija.Utils;

namespace Turisticka_Agencija.Services
{
    class RestaurantService
     {

         public static AutocompleteProvider.Address Address = new();
         public static List<Restaurant> GetAll()
        {
            using (var dbContext = new Context())
            {
                return dbContext.Restaurants.ToList();
            }
        }

        public static bool Save(Restaurant restaurant)
        {
            using (var dbContext = new Context())
            {
                dbContext.Restaurants.Add(restaurant);
                dbContext.SaveChanges();
                return true;
            }
        }

        public static bool Delete(Restaurant restaurant)
        {
            using (var dbContext = new Context())
            {
                dbContext.Entry(restaurant).State = EntityState.Deleted;
                dbContext.SaveChanges();
                return true;
            }
        }

        public static bool Update(Restaurant restaurant)
        {
            using (var dbContext = new Context())
            {
                dbContext.Entry(restaurant).State = EntityState.Modified;
                dbContext.SaveChanges();
                return true;
            }
        }

        public static List<double> GeocodeAddress()
        {
            if (Address == null || Address.FormattedAddress == "") return null;
            string addressEncoded = HttpUtility.UrlEncode(Address.FormattedAddress.Trim());
            string _geocodePartialUrl = "https://dev.virtualearth.net/REST/v1/Locations?q=";
            string _geocodeKeyPartialUrl =
                "&key=sZuSgR7v5bz2vg9wVM59~Qfpz84S4i9QJtmeokKTzAQ~AjleVBuRM03IHDNl1GLAcpYYT1lY_lo8Tylvsk2iwhlP-dAar9aOaKFkdqdxjePN";

            string completeUrl = _geocodePartialUrl + addressEncoded + "&countryRegion=" + HttpUtility.UrlEncode(Address.CountryRegion) + "&locality=" +
                                 HttpUtility.UrlEncode(Address.Locality) + "&postalCode=" + HttpUtility.UrlEncode(Address.PostalCode) + "&addressLine=" + 
                                 HttpUtility.UrlEncode(Address.AddressLine) + _geocodeKeyPartialUrl;
            HttpClient client = new HttpClient();
            var response = client.GetStringAsync(completeUrl).Result;
            if (response == "") return null;
            var jsonObject = JsonNode.Parse(response).AsObject();
            List<JsonObject> resourceSets = jsonObject["resourceSets"].Deserialize<List<JsonObject>>();
            List<JsonObject> resources = resourceSets[0]["resources"].Deserialize<List<JsonObject>>();
            if (resources.Count == 0) return null;
            JsonObject point = resources[0]["point"].Deserialize<JsonObject>();
            JsonArray coordinates = point["coordinates"].Deserialize<JsonArray>();
            return new List<double>()
                { Double.Parse(coordinates[0].ToString()), Double.Parse(coordinates[1].ToString()) };
        }
    }
}
