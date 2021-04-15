using System.Collections.Generic;
using MyPass.Core.Models;
using Newtonsoft.Json;

namespace MyPass.Core.Services
{
    public static class StorageService
    {
        public static List<PasswordItem> Get(string json)
        {
            var result = new List<PasswordItem>();
            if (string.IsNullOrEmpty(json))
                return result;

            try
            {
                result = JsonConvert.DeserializeObject<List<PasswordItem>>(json);
                return result;
            }
            catch
            {
                return result;
            }
        }

        public static string Add(PasswordItem item, string json)
        {
            var items = Get(json);
            items.Add(item);
            return JsonConvert.SerializeObject(items);
        }

        public static string Update(List<PasswordItem> items)
        {
            var json = JsonConvert.SerializeObject(items);
            return json;
        }
    }
}
