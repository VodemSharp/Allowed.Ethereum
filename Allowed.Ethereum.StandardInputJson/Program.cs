using Allowed.Ethereum.StandardInputJson.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Allowed.Ethereum.StandardInputJson
{
    public static class Program
    {
        public static void Main()
        {
            string json = File.ReadAllText(@"");
            JObject document = (JObject)JsonConvert.DeserializeObject(json);
            string metadata = document["metadata"].ToString();

            StandardInput input = JsonConvert.DeserializeObject<StandardInput>(metadata);

            Dictionary<string, Source> newSources = new() { };
            foreach (KeyValuePair<string, Source> source in input.Sources)
            {
                newSources.Add(source.Key.Replace(@"", string.Empty),
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
                    newLibraries.Add(library.Key.Replace(@"", string.Empty),
                        library.Value);
                }

                input.Libraries = newLibraries;
            }

            File.WriteAllText("result.json", JsonConvert.SerializeObject(input, Formatting.Indented));
        }
    }
}
