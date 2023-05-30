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
using Turisticka_Agencija.Services;

namespace Turisticka_Agencija.Utils
{
    public class AutocompleteProvider : ISuggestionProvider
    {
        public IEnumerable GetSuggestions(string input)
        {
            return MapService.GetAutocompleteResults(input);
        }
    }
}
