/*
 * Copyright (c) 2019 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis.CSharp.Syntax;
using RoslynTypeScript.Translation;

namespace RoslynTypeScript.Patch
{
    /// <summary>
    /// put the nested class into the correct structure of Typescript, which we must create 
    /// another module
    /// </summary>
    public class InnerTypeDeclarationPatch : Patch
    {
        public void Apply(BaseTypeDeclarationTranslation typeDeclarationTranslation)
        {
            // TODO: only support one level, why do you need nested > 1 level ?
            TypeDeclarationTranslation outerMemberDeclaration =
                (TypeDeclarationTranslation)typeDeclarationTranslation.TravelUpNotMe( f => f is TypeDeclarationTranslation );
            if (outerMemberDeclaration == null)
            {
                return;
            }

            SyntaxListBaseTranslation syntaxListBaseTranslation = (SyntaxListBaseTranslation)typeDeclarationTranslation.Parent;

            syntaxListBaseTranslation.Remove( typeDeclarationTranslation );

            SyntaxListBaseTranslation outerSyntaxListBaseTranslation = (SyntaxListBaseTranslation)outerMemberDeclaration.Parent;
            var newNamespace = CreateNewNamespace( outerMemberDeclaration.Syntax.Identifier.ToString(), typeDeclarationTranslation );
            outerSyntaxListBaseTranslation.Add( newNamespace );

        }

        private NamespaceDeclarationTranslation CreateNewNamespace(string identifier, BaseTypeDeclarationTranslation typeDeclarationTranslation)
        {
            NamespaceDeclarationTranslation newNamespaceTranslation = new NamespaceDeclarationTranslation();
            newNamespaceTranslation.Name = new IdentifierNameTranslation() { SyntaxString = identifier, Parent = newNamespaceTranslation };
            newNamespaceTranslation.Members = new SyntaxListTranslation<MemberDeclarationSyntax, MemberDeclarationTranslation>() { Parent = newNamespaceTranslation };
            newNamespaceTranslation.Members.Add( typeDeclarationTranslation );
            newNamespaceTranslation.IsExport = true;

            return newNamespaceTranslation;
        }
    }
}
