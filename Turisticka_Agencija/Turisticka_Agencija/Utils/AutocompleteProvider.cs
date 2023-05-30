using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Web;
using AutoCompleteTextBox.Editors;

namespace Turisticka_Agencija.Utils
{
    public class AutocompleteProvider : ISuggestionProvider
    {
        private readonly string _autocompletePartialUrl = "https://dev.virtualearth.net/REST/v1/Autosuggest?userLocation=45.2396,19.8827,5&maxRes=5&query=";

        private string _autocompleteKeyPartialUrl =
            "&key=sZuSgR7v5bz2vg9wVM59~Qfpz84S4i9QJtmeokKTzAQ~AjleVBuRM03IHDNl1GLAcpYYT1lY_lo8Tylvsk2iwhlP-dAar9aOaKFkdqdxjePN";
        private List<Address> GetAutocompleteResults(string input)
        {
            List<Address> suggestions = new List<Address>();
            input = HttpUtility.UrlEncode(input);
            string completeUrl = _autocompletePartialUrl + input + _autocompleteKeyPartialUrl;
            HttpClient client = new HttpClient();
            var response = client.GetStringAsync(completeUrl).Result;
            if (response == "") return suggestions;
            var jsonObject = JsonNode.Parse(response).AsObject();
            List<JsonObject> resourceSets = jsonObject["resourceSets"].Deserialize<List<JsonObject>>();
            List<JsonObject> resources = resourceSets[0]["resources"].Deserialize<List<JsonObject>>();
            List<JsonObject> values = resources[0]["value"].Deserialize<List<JsonObject>>();

            foreach (var value in values)
            {
                Address address = new Address();
                JsonObject temp = value["address"].Deserialize<JsonObject>();
                address.FormattedAddress = temp["formattedAddress"].Deserialize<string>();
                address.CountryRegion = temp["countryRegion"].Deserialize<string>();
                address.Locality = temp["locality"].Deserialize<string>();
                address.PostalCode = temp["postalCode"].Deserialize<string>();
                address.AddressLine = temp["addressLine"].Deserialize<string>();
                suggestions.Add(address);
            }
            return suggestions;
        }
        public IEnumerable GetSuggestions(string input)
        {
            return GetAutocompleteResults(input);
        }

        public class Address
        {
            public string Locality { get; set; }
            public string FormattedAddress { get; set; }
            public string CountryRegion { get; set; }
            public string PostalCode { get; set; }
            public string AddressLine { get; set; }

            public Address()
            {
                Locality = "";
                FormattedAddress = "";
                CountryRegion = "";
                PostalCode = "";
                AddressLine = "";
            }
        }
    }
}
