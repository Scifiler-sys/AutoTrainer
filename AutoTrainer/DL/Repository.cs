using AutoTrainer.Models;
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
        protected string filepath;

        public Repository()
        {
            filepath = Directory.GetCurrentDirectory() + "\\" + this.GetType().Name + ".json";
        }

        /// <summary>
        /// Will overwrite a file if file already exist. If not, it will generate a new file
        /// </summary>
        /// <param name="entity">The object getting saved into JSON</param>
        public void Save(T entity)
        {
            using (StreamWriter writer = new StreamWriter(filepath))
            {
                writer.WriteLine(JsonSerializer.Serialize(entity));
            }
        }

        /// <summary>
        /// Will load a file if the file exists
        /// </summary>
        /// <returns>
        /// Will return an object version of JSON. If file does not exist, will create a default file instead.
        /// </returns>
        public virtual T Load()
        {
            T currentFile = (T)Activator.CreateInstance(typeof(T));

            try
            {
                using (StreamReader reader = new StreamReader(filepath))
                {
                    string json = reader.ReadToEnd();

                    currentFile = JsonSerializer.Deserialize<T>(json);
                }
            }
            catch (FileNotFoundException)
            {
                using (StreamWriter writer = new StreamWriter(filepath))
                {
                    writer.WriteLine(JsonSerializer.Serialize(currentFile));
                }
            }

            return currentFile;
        }
    }
}
