using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Web;

namespace Turisticka_Agencija.Services;

public class Address
{
    public Address()
    {
        Locality = "";
        FormattedAddress = "";
        CountryRegion = "";
        PostalCode = "";
        AddressLine = "";
    }

    public string Locality { get; set; }
    public string FormattedAddress { get; set; }
    public string CountryRegion { get; set; }
    public string PostalCode { get; set; }
    public string AddressLine { get; set; }
}

internal class MapService
{
    public static Address Address = new();

    private static readonly string AutocompletePartialUrl =
        "https://dev.virtualearth.net/REST/v1/Autosuggest?userLocation=45.2396,19.8827,5&maxRes=5&query=";

    private static readonly string KeyPartialUrl =
        "&key=sZuSgR7v5bz2vg9wVM59~Qfpz84S4i9QJtmeokKTzAQ~AjleVBuRM03IHDNl1GLAcpYYT1lY_lo8Tylvsk2iwhlP-dAar9aOaKFkdqdxjePN";

    private static readonly string GeocodePartialUrl = "https://dev.virtualearth.net/REST/v1/Locations?q=";

    public static List<double> GeocodeAddress()
    {
        if (Address == null || Address.FormattedAddress == "") return null;
        var addressEncoded = HttpUtility.UrlEncode(Address.FormattedAddress.Trim());

        var completeUrl = GeocodePartialUrl + addressEncoded + "&countryRegion=" +
                          HttpUtility.UrlEncode(Address.CountryRegion) + "&locality=" +
                          HttpUtility.UrlEncode(Address.Locality) + "&postalCode=" +
                          HttpUtility.UrlEncode(Address.PostalCode) + "&addressLine=" +
                          HttpUtility.UrlEncode(Address.AddressLine) + KeyPartialUrl;
        var client = new HttpClient();
        var response = client.GetStringAsync(completeUrl).Result;
        if (response == "") return null;
        var jsonObject = JsonNode.Parse(response).AsObject();
        var resourceSets = jsonObject["resourceSets"].Deserialize<List<JsonObject>>();
        var resources = resourceSets[0]["resources"].Deserialize<List<JsonObject>>();
        if (resources.Count == 0) return null;
        var point = resources[0]["point"].Deserialize<JsonObject>();
        var coordinates = point["coordinates"].Deserialize<JsonArray>();
        return new List<double> { double.Parse(coordinates[0].ToString()), double.Parse(coordinates[1].ToString()) };
    }


    public static List<Address> GetAutocompleteResults(string input)
    {
        var suggestions = new List<Address>();
        input = HttpUtility.UrlEncode(input);
        var completeUrl = AutocompletePartialUrl + input + KeyPartialUrl;
        var client = new HttpClient();
        var response = client.GetStringAsync(completeUrl).Result;
        if (response == "") return suggestions;
        var jsonObject = JsonNode.Parse(response).AsObject();
        var resourceSets = jsonObject["resourceSets"].Deserialize<List<JsonObject>>();
        var resources = resourceSets[0]["resources"].Deserialize<List<JsonObject>>();
        var values = resources[0]["value"].Deserialize<List<JsonObject>>();

        foreach (var value in values)
        {
            var address = new Address();
            var temp = value["address"].Deserialize<JsonObject>();
            address.FormattedAddress = temp["formattedAddress"].Deserialize<string>();
            address.CountryRegion = temp["countryRegion"].Deserialize<string>();
            address.Locality = temp["locality"].Deserialize<string>();
            address.PostalCode = temp["postalCode"].Deserialize<string>();
            address.AddressLine = temp["addressLine"].Deserialize<string>();
            suggestions.Add(address);
        }

        return suggestions;
    }
}