namespace NIF.PT.Client.IntegrationTests
{
    using System;
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class LaunchSettingsFixture
        : IDisposable
    {
        public LaunchSettingsFixture()
        {
            using (var file = File.OpenText("Properties\\launchSettings.json"))
            {
                using (var reader = new JsonTextReader(file))
                {
                    var launchSettingsObject = JObject.Load(reader);
                    var environmentVariables = launchSettingsObject
                        .SelectToken("$..environmentVariables")
                        .SelectMany(t => t.Children<JProperty>());

                    foreach (var variable in environmentVariables)
                    {
                        Environment.SetEnvironmentVariable(variable.Name, variable.Value.ToString());
                    }
                }
            }
        }

        public void Dispose()
        {
        }
    }
}