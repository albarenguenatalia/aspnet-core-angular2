using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RockPaperScissors.Utilities
{
    /**
     * This static class servies for the purpose of having
     * a unique container of all the constants for the application
     * */
    public static class GlobalConstants
    {
        public static readonly string DefaultGameSettings = LoadJson();

        private static readonly string _settingsPath = @"..\gameSettings.json";

        private static string LoadJson()
        {
            string json = System.IO.File.ReadAllText(_settingsPath);
            if (ValidateType.IsValidJson(json))
            {
                return json;
            }
            else {
                return null;
            }
        }
    }
}
