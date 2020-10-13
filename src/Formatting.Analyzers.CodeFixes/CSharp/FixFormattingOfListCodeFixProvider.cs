﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;
using System.Composition;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslynator.Formatting.CSharp;

namespace Roslynator.Formatting.CodeFixes.CSharp
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(FixFormattingOfListCodeFixProvider))]
    [Shared]
    internal class FixFormattingOfListCodeFixProvider : BaseCodeFixProvider
    {
        private const string Title = "Fix formatting";

        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(DiagnosticIdentifiers.FixFormattingOfList); }
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            SyntaxNode root = await context.GetSyntaxRootAsync().ConfigureAwait(false);

            if (!TryFindFirstAncestorOrSelf(
                root,
                context.Span,
                out SyntaxNode node,
                predicate: f =>
                {
                    switch (f.Kind())
                    {
                        case SyntaxKind.ParameterList:
                        case SyntaxKind.BracketedParameterList:
                        case SyntaxKind.TypeParameterList:
                        case SyntaxKind.ArgumentList:
                        case SyntaxKind.BracketedArgumentList:
                        case SyntaxKind.AttributeArgumentList:
                        case SyntaxKind.TypeArgumentList:
                        case SyntaxKind.AttributeList:
                        case SyntaxKind.BaseList:
                        case SyntaxKind.TupleType:
                        case SyntaxKind.TupleExpression:
                        case SyntaxKind.ArrayInitializerExpression:
                        case SyntaxKind.CollectionInitializerExpression:
                        case SyntaxKind.ComplexElementInitializerExpression:
                        case SyntaxKind.ObjectInitializerExpression:
                            return true;
                        default:
                            return false;
                    }
                }))
            {
                return;
            }

            Document document = context.Document;
            Diagnostic diagnostic = context.Diagnostics[0];
            CodeAction codeAction = CreateCodeAction();

            context.RegisterCodeFix(codeAction, diagnostic);

            CodeAction CreateCodeAction()
            {
                switch (node)
                {
                    case ParameterListSyntax parameterList:
                        {
                            return CodeAction.Create(
                                Title,
                                ct => CodeFixHelpers.FixListAsync(document, parameterList, ct),
                                GetEquivalenceKey(diagnostic));
                        }
                    case BracketedParameterListSyntax bracketedParameterList:
                        {
                            return CodeAction.Create(
                                Title,
                                ct => CodeFixHelpers.FixListAsync(document, bracketedParameterList, ct),
                                GetEquivalenceKey(diagnostic));
                        }
                    case TypeParameterListSyntax typeParameterList:
                        {
                            return CodeAction.Create(
                                Title,
                                ct => CodeFixHelpers.FixListAsync(document, typeParameterList, ct),
                                GetEquivalenceKey(diagnostic));
                        }
                    case ArgumentListSyntax argumentList:
                        {
                            return CodeAction.Create(
                                Title,
                                ct => CodeFixHelpers.FixListAsync(document, argumentList, ct),
                                GetEquivalenceKey(diagnostic));
                        }
                    case BracketedArgumentListSyntax bracketedArgumentList:
                        {
                            return CodeAction.Create(
                                Title,
                                ct => CodeFixHelpers.FixListAsync(document, bracketedArgumentList, ct),
                                GetEquivalenceKey(diagnostic));
                        }
                    case AttributeArgumentListSyntax attributeArgumentList:
                        {
                            return CodeAction.Create(
                                Title,
                                ct => CodeFixHelpers.FixListAsync(document, attributeArgumentList, ct),
                                GetEquivalenceKey(diagnostic));
                        }
                    case TypeArgumentListSyntax typeArgumentList:
                        {
                            return CodeAction.Create(
                                Title,
                                ct => CodeFixHelpers.FixListAsync(document, typeArgumentList, ct),
                                GetEquivalenceKey(diagnostic));
                        }
                    case AttributeListSyntax attributeList:
                        {
                            return CodeAction.Create(
                                Title,
                                ct => CodeFixHelpers.FixListAsync(document, attributeList, ct),
                                GetEquivalenceKey(diagnostic));
                        }
                    case BaseListSyntax baseList:
                        {
                            return CodeAction.Create(
                                Title,
                                ct => CodeFixHelpers.FixListAsync(document, baseList, ct),
                                GetEquivalenceKey(diagnostic));
                        }
                    case TupleTypeSyntax tupleType:
                        {
                            return CodeAction.Create(
                                Title,
                                ct => CodeFixHelpers.FixListAsync(document, tupleType, ct),
                                GetEquivalenceKey(diagnostic));
                        }
                    case TupleExpressionSyntax tupleExpression:
                        {
                            return CodeAction.Create(
                                Title,
                                ct => CodeFixHelpers.FixListAsync(document, tupleExpression, ct),
                                GetEquivalenceKey(diagnostic));
                        }
                    case InitializerExpressionSyntax initializerExpression:
                        {
                            return CodeAction.Create(
                                Title,
                                ct => CodeFixHelpers.FixListAsync(document, initializerExpression, ct),
                                GetEquivalenceKey(diagnostic));
                        }
                    default:
                        {
                            throw new InvalidOperationException();
                        }
                }
            }
        }
    }
}
