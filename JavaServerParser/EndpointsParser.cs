using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JavaServerParser
{
    public class EndpointsParser
    {
        private string _path;

        public EndpointsParser(string path)
        {
            _path = path;
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"There is not file with path {path}");
            }
        }

        public List<Endpoint> Parse()
        {
            List<Endpoint> endpoints = new List<Endpoint>();

            List<string> lines = System.IO.File.ReadLines(_path).ToList();

            for (int i = 0; i < lines.Count; ++i)
            {
                string[] words = lines[i].Split(' ');
                words = words.Where(w => w != "\n" && w != "").ToArray();

                if (words.Length == 0) continue;

                if (words[0].Length > 3 && (words[0][..2] == "@G" || words[0][..2] == "@P"))
                {
                    EndpointType type;
                    if (words[0][..2] == "@G")
                        type = EndpointType.Get;
                    else
                        type = EndpointType.Post;

                    int openingBracket = words[0].IndexOf('(');
                    int closingBracket = words[0].IndexOf(')');
                    string URL = words[0].Substring(openingBracket + 1, closingBracket - openingBracket - 1);

                    words = lines[i + 1].Split(' ');
                    int indexOfName = Array.IndexOf(words, words.Where(w => w.Contains('(')).ToArray()[0]);

                    string name = words[indexOfName][..words[indexOfName].IndexOf('(')];

                    string returnType = words[indexOfName - 1];

                    openingBracket = lines[i + 1].IndexOf('(');
                    closingBracket = lines[i + 1].IndexOf(')');
                    string[] arguments = lines[i + 1].Substring(openingBracket + 1, closingBracket - openingBracket - 1)
                        .Split(',');

                    if (arguments.Length == 1 && arguments[0].Split(' ')[0] == "@RequestBody")
                    {
                        string[] body = arguments[0].Split(' ');
                        Endpoint endpoint = new Endpoint(URL, type, name, returnType, new List<Variable>(),
                            new Variable(body[1], body[2]));
                        endpoints.Add(endpoint);
                    }
                    else
                    {
                        List<Variable> requestedParameters = (from argument in arguments
                            select argument.Split(' ')
                            into body
                            where body[0] == "@RequestParam"
                            select new Variable(body[1], body[2])).ToList();

                        Endpoint endpoint = new Endpoint(URL, type, name, returnType, requestedParameters,
                            null);
                        endpoints.Add(endpoint);
                    }
                }
            }

            return endpoints;
        }
    }
}