﻿using AutoTrainer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AutoTrainer.DL
{
    public class Repository<T>
    {
        public void Save(T entity)
        {
            string filepath = Directory.GetCurrentDirectory() +"\\"+ this.GetType().Name + ".json";

            using (StreamWriter writer = new StreamWriter(filepath))
            {
                writer.WriteLine(JsonSerializer.Serialize(entity));
            }
        }

        /// <summary>
        /// Will load a file if the file exists
        /// </summary>
        /// <returns>
        /// Will return an object version of JSON. If file does not exist, return null instead.
        /// </returns>
        public Batch Load()
        {
            string filepath = Directory.GetCurrentDirectory() +"\\"+ this.GetType().Name + ".json";

            try
            {
                using (StreamReader reader = new StreamReader(filepath))
                {
                    string json = reader.ReadToEnd();

                    return JsonSerializer.Deserialize<Batch>(json);
                }
            }
            catch (FileNotFoundException exc)
            {
                Console.Error.WriteLine(exc.Message);
                return null;
            }
        }
    }
}