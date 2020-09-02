/*
 * Copyright (c) 2019-2020 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GPLv3.0 license (GNU General Public License v3.0),
 * located in the root of this project, under the name "LICENSE.md".
 *
 */

using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using RoslynTypeScript.Translation;

using CSharpToTypescript.Contract;

namespace CSharpToTypescript
{
    public class CSharpToTypescriptConverter : ICSharpToTypescriptConverter
    {
        private MetadataReference mscorlib;
        private MetadataReference Mscorlib
        {
            get
            {
                if (mscorlib == null)
                {
                    mscorlib = MetadataReference.CreateFromFile( typeof( object ).Assembly.Location );
                }

                return mscorlib;
            }
        }

        public string ConvertToTypescript(string text, ISettingStore settingStore)
        {
            try
            {
                var tree = (CSharpSyntaxTree)CSharpSyntaxTree.ParseText( text );

                // detect to see if it's actually C# sourcode by checking whether it has any error
                if (tree.GetDiagnostics().Any( f => f.Severity == DiagnosticSeverity.Error ))
                {
                    return null;
                }

                var root = tree.GetRoot();

                // if it only contains comments, just return the original texts
                if (IsEmptyRoot( root )) return null;

                if (settingStore.IsConvertToInterface)
                {
                    root = ClassToInterfaceReplacement.ReplaceClass( root );
                }

                if (settingStore.IsConvertMemberToCamelCase)
                {
                    root = MakeMemberCamelCase.Make( root );
                }

                if (settingStore.IsConvertListToArray)
                {
                    root = ListToArrayReplacement.ReplaceList( root );
                }

                if (settingStore.ReplacedTypeNameArray.Length > 0)
                {
                    root = TypeNameReplacement.Replace( settingStore.ReplacedTypeNameArray, root );
                }

                if (settingStore.AddIPrefixInterfaceDeclaration)
                {
                    root = AddIPrefixInterfaceDeclaration.AddIPrefix( root );
                }

                if (settingStore.IsInterfaceOptionalProperties)
                {
                    root = OptionalInterfaceProperties.AddOptional( root );
                }

                tree = (CSharpSyntaxTree)root.SyntaxTree;

                var translationNode = TF.Get( root, null );

                var compilation = CSharpCompilation.Create( "TemporaryCompilation",
                     syntaxTrees: new[] { tree }, references: new[] { Mscorlib } );
                var model = compilation.GetSemanticModel( tree );

                translationNode.Compilation = compilation;
                translationNode.SemanticModel = model;

                translationNode.ApplyPatch();
                return translationNode.Translate();

            }
            catch (Exception ex)
            {
                // TODO
                // swallow exception .!!!!!!!!!!!!!!!!!!!!!!!
            }

            return null;
        }

        private bool IsEmptyRoot(SyntaxNode root)
        {
            return !root.DescendantNodes().Any();
        }
    }
}
