using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JavaServerParser;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Roslyn
{
    public class GenerateEntity
    {
        public static async void CreateClass(string name, List<Variable> variables)
        {
            MemberDeclarationSyntax[] members = new MemberDeclarationSyntax[variables.Count + 1];

            UsingDirectiveSyntax[] usings = new UsingDirectiveSyntax[0];
            int usingsCount = 0;

            for (int i = 0; i < variables.Count; ++i)
            {
                SyntaxKind syntaxKind = 0;
                bool primitive = true;
                switch (variables[i].Type)
                {
                    case "int":
                        syntaxKind = SyntaxKind.IntKeyword;
                        primitive = true;
                        break;
                    case "string":
                        syntaxKind = SyntaxKind.StringKeyword;
                        primitive = true;
                        break;
                    case "List<string>":
                        primitive = false;

                        if (usingsCount == usings.Length)
                        {
                            UsingDirectiveSyntax[] newUsings = new UsingDirectiveSyntax[usings.Length + 1];
                            for (int j = 0; j < usings.Length; ++j)
                            {
                                newUsings[j] = usings[j];
                            }

                            usings = newUsings;
                        }

                        usings[usingsCount] = UsingDirective(QualifiedName(
                            QualifiedName(IdentifierName("System"), IdentifierName("Collections")),
                            IdentifierName("Generic")));
                        break;
                }

                members[variables.Count] = ConstructorDeclaration(
                        Identifier("Zxc"))
                    .WithModifiers(
                        TokenList(
                            Token(SyntaxKind.PublicKeyword)))
                    .WithParameterList(
                        ParameterList(
                            SeparatedList<ParameterSyntax>(
                                new SyntaxNodeOrToken[]
                                {
                                    Parameter(
                                            Identifier("playerId"))
                                        .WithType(
                                            PredefinedType(
                                                Token(SyntaxKind.IntKeyword))),
                                    Token(SyntaxKind.CommaToken),
                                    Parameter(
                                            Identifier("playerName"))
                                        .WithType(
                                            PredefinedType(
                                                Token(SyntaxKind.StringKeyword))),
                                    Token(SyntaxKind.CommaToken),
                                    Parameter(
                                            Identifier("hoursInDota2"))
                                        .WithType(
                                            PredefinedType(
                                                Token(SyntaxKind.IntKeyword))),
                                    Token(SyntaxKind.CommaToken),
                                    Parameter(
                                            Identifier("achievements"))
                                        .WithType(
                                            GenericName(
                                                    Identifier("List"))
                                                .WithTypeArgumentList(
                                                    TypeArgumentList(
                                                        SingletonSeparatedList<TypeSyntax>(
                                                            PredefinedType(
                                                                Token(SyntaxKind.StringKeyword))))))
                                })))
                    .WithBody(
                        Block(
                            ExpressionStatement(
                                AssignmentExpression(
                                    SyntaxKind.SimpleAssignmentExpression,
                                    IdentifierName("PlayerID"),
                                    IdentifierName("playerId"))),
                            ExpressionStatement(
                                AssignmentExpression(
                                    SyntaxKind.SimpleAssignmentExpression,
                                    IdentifierName("PlayerName"),
                                    IdentifierName("playerName"))),
                            ExpressionStatement(
                                AssignmentExpression(
                                    SyntaxKind.SimpleAssignmentExpression,
                                    IdentifierName("HoursInDota2"),
                                    IdentifierName("hoursInDota2"))),
                            ExpressionStatement(
                                AssignmentExpression(
                                    SyntaxKind.SimpleAssignmentExpression,
                                    IdentifierName("Achievements"),
                                    ObjectCreationExpression(
                                            GenericName(
                                                    Identifier("List"))
                                                .WithTypeArgumentList(
                                                    TypeArgumentList(
                                                        SingletonSeparatedList<TypeSyntax>(
                                                            PredefinedType(
                                                                Token(SyntaxKind.StringKeyword))))))
                                        .WithArgumentList(
                                            ArgumentList(
                                                SingletonSeparatedList<ArgumentSyntax>(
                                                    Argument(
                                                        IdentifierName("achievements")))))))));

                if (primitive)
                {
                    members[i] = PropertyDeclaration(
                            PredefinedType(Token(syntaxKind)),
                            Identifier(variables[i].Name))
                        .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                        .WithAccessorList(AccessorList(
                            List<AccessorDeclarationSyntax>(new[]
                            {
                                AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                                AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
                            })));
                }
                else
                {
                    members[i] = PropertyDeclaration(
                            GenericName(Identifier("List")).WithTypeArgumentList(
                                TypeArgumentList(SeparatedList<TypeSyntax>(new TypeSyntax[]
                                    {PredefinedType(Token(SyntaxKind.StringKeyword))}))),
                            Identifier(variables[i].Name))
                        .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                        .WithAccessorList(AccessorList(
                            List<AccessorDeclarationSyntax>(new[]
                            {
                                AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                                AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
                            })));
                }
            }

            var ZxcClass = ClassDeclaration(Identifier(name))
                .AddModifiers(Token(SyntaxKind.PublicKeyword))
                .WithMembers(List<MemberDeclarationSyntax>(members));

            Directory.CreateDirectory("/Users/egorsergeev/RiderProjects/JavaServerParser/Roslyn/codegen");
            await using var streamWriter =
                new StreamWriter("/Users/egorsergeev/RiderProjects/JavaServerParser/Roslyn/codegen/Zxc.cs",
                    false);

            var ZxcFile = NamespaceDeclaration(ParseName("Client"))
                .WithUsings(new SyntaxList<UsingDirectiveSyntax>(usings))
                .WithMembers(new SyntaxList<MemberDeclarationSyntax>(new[] {ZxcClass}));

            ZxcFile.NormalizeWhitespace().WriteTo(streamWriter);
        }
    }
}