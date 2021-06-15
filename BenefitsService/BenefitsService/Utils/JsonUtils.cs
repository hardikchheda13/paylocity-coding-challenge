using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Runtime.Serialization;
namespace BenefitsService.BenefitsService.Utils
{
    class JsonUtils
    {
        public static string ToJson(object obj)
        {
            var settings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat
            };
            return JsonConvert.SerializeObject(obj, Formatting.Indented, settings);
        }
        
    }
}
