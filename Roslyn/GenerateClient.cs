using System;
using System.Collections.Generic;
using System.IO;
using JavaServerParser;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Roslyn
{
    public class GenerateClient
    {
        public static async void CreateClient(List<Endpoint> endpoints)
        {
            MemberDeclarationSyntax[] members = new MemberDeclarationSyntax[endpoints.Count];

            for (int i = 0; i < endpoints.Count; ++i)
            {
                SyntaxNodeOrToken[] parameterSyntaxes;

                if (endpoints[i].RequestParameters.Count != 0)
                {
                    parameterSyntaxes = new SyntaxNodeOrToken[endpoints[i].RequestParameters.Count * 2 - 1];

                    for (int j = 0; j < endpoints[i].RequestParameters.Count; j += 2)
                    {
                        SyntaxKind syntaxKind = 0;
                        switch (endpoints[i].RequestParameters[j].Type)
                        {
                            case "int":
                                syntaxKind = SyntaxKind.IntKeyword;
                                break;
                        }

                        parameterSyntaxes[j] = Parameter(Identifier(endpoints[i].RequestParameters[j].Name))
                            .WithType(PredefinedType(Token(syntaxKind)));

                        if (j != endpoints[i].RequestParameters.Count - 1)
                        {
                            parameterSyntaxes[j + 1] = Token(SyntaxKind.CommaToken);
                        }
                    }
                }
                else
                {
                    parameterSyntaxes = Array.Empty<SyntaxNodeOrToken>();
                }

                if (endpoints[i].EndpointType == EndpointType.Get)
                {
                    members[i] = MethodDeclaration(PredefinedType(Token(SyntaxKind.VoidKeyword)),
                            Identifier(endpoints[i].Name))
                        .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.AsyncKeyword)))
                        .WithParameterList(ParameterList(SeparatedList<ParameterSyntax>(parameterSyntaxes)))
                        .WithBody(Block(ExpressionStatement(
                            InvocationExpression(MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("Console"), IdentifierName("WriteLine")))
                                .WithArgumentList(ArgumentList(SeparatedList<ArgumentSyntax>(new ArgumentSyntax[]
                                {
                                    Argument(AwaitExpression(InvocationExpression(MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            ObjectCreationExpression(IdentifierName("HttpClient"))
                                                .WithArgumentList(ArgumentList()),
                                            IdentifierName("GetStringAsync")))
                                        .WithArgumentList(ArgumentList(SeparatedList<ArgumentSyntax>(
                                            new ArgumentSyntax[]
                                            {
                                                Argument(InterpolatedStringExpression(
                                                    Token(SyntaxKind.InterpolatedStringStartToken)).WithContents(
                                                    List<InterpolatedStringContentSyntax>(
                                                        new InterpolatedStringContentSyntax[]
                                                        {
                                                            InterpolatedStringText()
                                                                .WithTextToken(Token(TriviaList(),
                                                                    SyntaxKind.InterpolatedStringTextToken,
                                                                    "http://localhost:8080" +
                                                                    endpoints[i].URL.Substring(1,
                                                                        endpoints[i].URL.Length - 2) +
                                                                    (endpoints[i].RequestParameters.Count > 0
                                                                        ? $"?{endpoints[i].RequestParameters[0].Name}="
                                                                        : ""),
                                                                    "http://localhost:8080" +
                                                                    endpoints[i].URL.Substring(1,
                                                                        endpoints[i].URL.Length - 2) +
                                                                    (endpoints[i].RequestParameters.Count > 0
                                                                        ? $"?{endpoints[i].RequestParameters[0].Name}="
                                                                        : ""), TriviaList())),
                                                            Interpolation(
                                                                IdentifierName(endpoints[i].RequestParameters[0].Name))
                                                        })))
                                            })))))
                                }))))));
                }

                if (endpoints[i].EndpointType == EndpointType.Post)
                {
                    members[i] = MethodDeclaration(
                            PredefinedType(
                                Token(SyntaxKind.VoidKeyword)),
                            Identifier(endpoints[i].Name))
                        .WithModifiers(
                            TokenList(
                                new[]
                                {
                                    Token(SyntaxKind.PublicKeyword),
                                    Token(SyntaxKind.AsyncKeyword)
                                }))
                        .WithParameterList(
                            ParameterList(
                                SeparatedList<ParameterSyntax>(
                                    new SyntaxNodeOrToken[]
                                    {
                                        Parameter(
                                                Identifier("Id"))
                                            .WithType(
                                                PredefinedType(
                                                    Token(SyntaxKind.IntKeyword))),
                                        Token(SyntaxKind.CommaToken),
                                        Parameter(
                                                Identifier("Name"))
                                            .WithType(
                                                PredefinedType(
                                                    Token(SyntaxKind.StringKeyword))),
                                        Token(SyntaxKind.CommaToken),
                                        Parameter(
                                                Identifier("HoursInDota2"))
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
                                LocalDeclarationStatement(
                                    VariableDeclaration(
                                            IdentifierName(
                                                Identifier(
                                                    TriviaList(),
                                                    SyntaxKind.VarKeyword,
                                                    "var",
                                                    "var",
                                                    TriviaList())))
                                        .WithVariables(
                                            SingletonSeparatedList<VariableDeclaratorSyntax>(
                                                VariableDeclarator(
                                                        Identifier("jsonZxc"))
                                                    .WithInitializer(
                                                        EqualsValueClause(
                                                            InvocationExpression(
                                                                    MemberAccessExpression(
                                                                        SyntaxKind.SimpleMemberAccessExpression,
                                                                        IdentifierName("JsonSerializer"),
                                                                        GenericName(
                                                                                Identifier("Serialize"))
                                                                            .WithTypeArgumentList(
                                                                                TypeArgumentList(
                                                                                    SingletonSeparatedList<TypeSyntax>(
                                                                                        IdentifierName("Zxc"))))))
                                                                .WithArgumentList(
                                                                    ArgumentList(
                                                                        SingletonSeparatedList<ArgumentSyntax>(
                                                                            Argument(
                                                                                ObjectCreationExpression(
                                                                                        IdentifierName("Zxc"))
                                                                                    .WithArgumentList(
                                                                                        ArgumentList(
                                                                                            SeparatedList<
                                                                                                ArgumentSyntax>(
                                                                                                new SyntaxNodeOrToken[]
                                                                                                {
                                                                                                    Argument(
                                                                                                        IdentifierName(
                                                                                                            "Id")),
                                                                                                    Token(SyntaxKind
                                                                                                        .CommaToken),
                                                                                                    Argument(
                                                                                                        IdentifierName(
                                                                                                            "Name")),
                                                                                                    Token(SyntaxKind
                                                                                                        .CommaToken),
                                                                                                    Argument(
                                                                                                        IdentifierName(
                                                                                                            "HoursInDota2")),
                                                                                                    Token(SyntaxKind
                                                                                                        .CommaToken),
                                                                                                    Argument(
                                                                                                        IdentifierName(
                                                                                                            "achievements"))
                                                                                                })))))))))))),
                                LocalDeclarationStatement(
                                    VariableDeclaration(
                                            IdentifierName(
                                                Identifier(
                                                    TriviaList(),
                                                    SyntaxKind.VarKeyword,
                                                    "var",
                                                    "var",
                                                    TriviaList())))
                                        .WithVariables(
                                            SingletonSeparatedList<VariableDeclaratorSyntax>(
                                                VariableDeclarator(
                                                        Identifier("data"))
                                                    .WithInitializer(
                                                        EqualsValueClause(
                                                            ObjectCreationExpression(
                                                                    IdentifierName("StringContent"))
                                                                .WithArgumentList(
                                                                    ArgumentList(
                                                                        SeparatedList<ArgumentSyntax>(
                                                                            new SyntaxNodeOrToken[]
                                                                            {
                                                                                Argument(
                                                                                    IdentifierName("jsonZxc")),
                                                                                Token(SyntaxKind.CommaToken),
                                                                                Argument(
                                                                                    MemberAccessExpression(
                                                                                        SyntaxKind
                                                                                            .SimpleMemberAccessExpression,
                                                                                        IdentifierName("Encoding"),
                                                                                        IdentifierName("UTF8"))),
                                                                                Token(SyntaxKind.CommaToken),
                                                                                Argument(
                                                                                    LiteralExpression(
                                                                                        SyntaxKind
                                                                                            .StringLiteralExpression,
                                                                                        Literal("application/json")))
                                                                            })))))))),
                                LocalDeclarationStatement(
                                    VariableDeclaration(
                                            IdentifierName(
                                                Identifier(
                                                    TriviaList(),
                                                    SyntaxKind.VarKeyword,
                                                    "var",
                                                    "var",
                                                    TriviaList())))
                                        .WithVariables(
                                            SingletonSeparatedList<VariableDeclaratorSyntax>(
                                                VariableDeclarator(
                                                        Identifier("response"))
                                                    .WithInitializer(
                                                        EqualsValueClause(
                                                            AwaitExpression(
                                                                InvocationExpression(
                                                                        MemberAccessExpression(
                                                                            SyntaxKind.SimpleMemberAccessExpression,
                                                                            ObjectCreationExpression(
                                                                                    IdentifierName("HttpClient"))
                                                                                .WithArgumentList(
                                                                                    ArgumentList()),
                                                                            IdentifierName("PostAsync")))
                                                                    .WithArgumentList(
                                                                        ArgumentList(
                                                                            SeparatedList<ArgumentSyntax>(
                                                                                new SyntaxNodeOrToken[]
                                                                                {
                                                                                    Argument(
                                                                                        LiteralExpression(
                                                                                            SyntaxKind
                                                                                                .StringLiteralExpression,
                                                                                            Literal(
                                                                                                "http://localhost:8080/zxc"))),
                                                                                    Token(SyntaxKind.CommaToken),
                                                                                    Argument(
                                                                                        IdentifierName("data"))
                                                                                }))))))))),
                                ExpressionStatement(
                                    InvocationExpression(
                                            MemberAccessExpression(
                                                SyntaxKind.SimpleMemberAccessExpression,
                                                IdentifierName("Console"),
                                                IdentifierName("WriteLine")))
                                        .WithArgumentList(
                                            ArgumentList(
                                                SingletonSeparatedList<ArgumentSyntax>(
                                                    Argument(
                                                        AwaitExpression(
                                                            InvocationExpression(
                                                                MemberAccessExpression(
                                                                    SyntaxKind.SimpleMemberAccessExpression,
                                                                    MemberAccessExpression(
                                                                        SyntaxKind.SimpleMemberAccessExpression,
                                                                        IdentifierName("response"),
                                                                        IdentifierName("Content")),
                                                                    IdentifierName("ReadAsStringAsync")))))))))));
                }
            }


            var ClientClass = ClassDeclaration(Identifier("Client"))
                .AddModifiers(Token(SyntaxKind.PublicKeyword))
                .WithMembers(List<MemberDeclarationSyntax>(members));

            Directory.CreateDirectory("/Users/egorsergeev/RiderProjects/JavaServerParser/Roslyn/codegen");
            await using var streamWriter =
                new StreamWriter("/Users/egorsergeev/RiderProjects/JavaServerParser/Roslyn/codegen/Client.cs",
                    false);

            var ClientFile = NamespaceDeclaration(ParseName("Client"))
                .WithUsings(new SyntaxList<UsingDirectiveSyntax>(new UsingDirectiveSyntax[]
                {
                    UsingDirective(IdentifierName("System")),
                    UsingDirective(QualifiedName(QualifiedName(IdentifierName("System"), IdentifierName("Net")),
                        IdentifierName("Http"))),
                    UsingDirective(QualifiedName(
                        QualifiedName(IdentifierName("System"), IdentifierName("Collections")),
                        IdentifierName("Generic"))),
                    UsingDirective(QualifiedName(IdentifierName("System"), IdentifierName("Text"))),
                    UsingDirective(QualifiedName(
                        QualifiedName(IdentifierName("System"), IdentifierName("Text")), IdentifierName("Json")))
                }))
                .WithMembers(List<MemberDeclarationSyntax>(new[] {ClientClass}));

            ClientFile.NormalizeWhitespace().WriteTo(streamWriter);
        }
    }
}