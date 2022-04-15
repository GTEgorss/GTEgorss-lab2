using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using JavaServerParser;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Roslyn
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            // GenerateEntity.CreateClass("Zxc", new List<Variable>() {new Variable("int", "PlayerID"), new Variable("string", "Name"), new Variable("int", "HoursInDota2"), new Variable("List<string>", "Achievements")});
            var (classname, variables) = new EntityParser("/Users/egorsergeev/IdeaProjects/demo/src/main/java/com/example/demo/Zxc.java").Parse();
            GenerateEntity.CreateClass(classname, variables);
            
            GenerateClient.CreateClient(new EndpointsParser("/Users/egorsergeev/IdeaProjects/demo/src/main/java/com/example/demo/ZxcController.java").Parse());
        }
    }
}