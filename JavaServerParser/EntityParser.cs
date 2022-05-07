using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JavaServerParser
{
    public class EntityParser
    {
        private string _path;

        public EntityParser(string path)
        {
            _path = path;
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"There is not file with path {path}");
            }
        }

        public (string, List<Variable>) Parse()
        {
            string className = "";
            List<Variable> variables = new List<Variable>();

            foreach (string line in System.IO.File.ReadLines(_path))
            {
                string[] words = line.Split(' ');
                words = words.Where(w => w != "\n" && w != "").ToArray();

                if (words.Contains("class"))
                {
                    className = words[Array.IndexOf(words, "class") + 1];
                }

                if (words.Length != 0 && words[0] == "public" && words[^1].Contains(';'))
                {
                    string type = words[^2];

                    if (type == null)
                        throw new NullReferenceException("Type cannot be null. Something went wrong.");

                    type = type switch
                    {
                        "String" => "string",
                        "ArrayList<String>" => "List<string>",
                        _ => type
                    };

                    variables.Add(new Variable(type, words[^1][..(words[^1].Length - 1)]));
                }
            }

            return (className, variables);
        }
    }
}