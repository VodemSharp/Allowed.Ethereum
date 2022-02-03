using Allowed.Ethereum.Common.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Allowed.Ethereum.Common.Helpers
{
    public static class StandardInputHelper
    {
        public static StandardInput GetStandardInput(string contractSource, string contractJson)
        {
            JObject document = (JObject)JsonConvert.DeserializeObject(contractJson);
            string metadata = document["metadata"].ToString();

            StandardInput input = JsonConvert.DeserializeObject<StandardInput>(metadata);

            Dictionary<string, Source> newSources = new() { };
            foreach (KeyValuePair<string, Source> source in input.Sources)
            {
                newSources.Add(source.Key.Replace(contractSource, string.Empty),
                    new Source()
                    {
                        Keccak256 = source.Value.Keccak256,
                        Content = File.ReadAllText(source.Key)
                    });
            }

            input.Sources = newSources;

            if (input.Libraries != null)
            {
                Dictionary<string, Dictionary<string, string>> newLibraries = new() { };
                foreach (KeyValuePair<string, Dictionary<string, string>> library in input.Libraries)
                {
                    newLibraries.Add(library.Key.Replace(contractSource, string.Empty),
                        library.Value);
                }

                input.Libraries = newLibraries;
            }

            return input;
        }
    }
}
