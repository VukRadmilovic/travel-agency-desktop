using System.Collections;
using AutoCompleteTextBox.Editors;
using Tourist_Agency.Services;

namespace Tourist_Agency.Utils
{
    public class AutocompleteProvider : ISuggestionProvider
    {
        public IEnumerable GetSuggestions(string input)
        {
            return MapService.GetAutocompleteResults(input);
        }
    }
}
