using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;


namespace Robot
{
    public class Service
    {
        private JsonSerializerSettings _settings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full,
        };

        private string GetPath(string fileName)
        {
            var directory = "Data";
            var format = ".json";
            return Path.Combine(directory, fileName + format);
        }

        /// <summary>
        /// Проверка, есть ли в списке алгоритмов алгоритм с таким же названием
        /// </summary>
        /// <param name="algorithm"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private bool CheckAlgorithmName(Algorithm algorithm, List<Algorithm> list)
        {
            return list.FirstOrDefault(x => x.Name == algorithm.Name) == null ? true : false;
        }

        #region  Блок сериализации

        /// <summary>
        /// Записать алгоритм в файл
        /// </summary>
        /// <param name="algorithm"></param>
        /// <returns></returns>
        public void WriteAlgorithm(Algorithm algorithm)
        {
            var path = GetPath(algorithm.Name);                 
            var serList = JsonConvert.SerializeObject(algorithm, _settings);
            if (!Directory.Exists(Path.Combine("Data"))) Directory.CreateDirectory(Path.Combine("Data"));
            WriteFile(serList, path);       
        }    

        /// <summary>
        /// Запись сериализованного объекта в файл
        /// </summary>
        /// <param name="serFile"></param>
        /// <param name="path"></param>
        private void WriteFile(string serFile, string path)
        {
            using (var file = new StreamWriter(path, !File.Exists(path)))
            {
                file.WriteLine(serFile);
            }
        }
        #endregion

        #region Блок десериализации

        /// <summary>
        /// Считать выбранный алгоритм
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Algorithm ReadAlgorithm(string path)
        {
            var json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<Algorithm>(json, _settings);
        }

        #endregion

    }
}
