using System;
using System.Collections.Generic;
using JavaServerParser;

class Program
{
    public static void Main(string[] args)
    {
        EntityParser entityParser =
            new EntityParser("/Users/egorsergeev/IdeaProjects/demo/src/main/java/com/example/demo/Zxc.java");
        (string classname, List<Variable> fields) = entityParser.Parse();
        Console.WriteLine("class " + classname);
        fields.ForEach(f => Console.WriteLine(f.ToString()));

        Console.WriteLine();
        
        EndpointsParser endpointsParser =
            new EndpointsParser(
                "/Users/egorsergeev/IdeaProjects/demo/src/main/java/com/example/demo/ZxcController.java");
        List<Endpoint> endpoints = endpointsParser.Parse();
        endpoints.ForEach(e => Console.WriteLine(e + "\n"));
    }
}