using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Console.Helpers
{
    public static class JSONParser
    {
        /// <summary>
        /// Parse Value of type T of passed path's content 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T ParseFromFile<T>(string path)
        {
            T result = default;
            if (!string.IsNullOrEmpty(path))
            {
                string json = File.ReadAllText(path);
                result = JsonConvert.DeserializeObject<T>(json);
            }
            return result;
        }
    }
}
