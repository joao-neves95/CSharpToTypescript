/*
 * Copyright (c) 2019 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynTypeScript.Translation
{
    public class ConstructorDeclarationTranslation : BaseMethodDeclarationTranslation
    {
        public new ConstructorDeclarationSyntax Syntax
        {
            get { return (ConstructorDeclarationSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public ConstructorInitializerTranslation Initializer { get; set; }

        public ConstructorDeclarationTranslation() { }

        public bool IsDeclarationOverload { get; set; }
        public ConstructorDeclarationTranslation(ConstructorDeclarationSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Identifier = syntax.Identifier.Get( this );

            if (syntax.Initializer != null)
            {
                Initializer = syntax.Initializer.Get<ConstructorInitializerTranslation>( this );
            }
        }

        public override void ApplyPatch()
        {
            base.ApplyPatch();
            var identifier = Identifier.SyntaxString;
            if (identifier == ".ctor")
            {
                Identifier.SyntaxString = "constructor";
            }
        }

        protected override string InnerTranslate()
        {
            // var identifier = Identifier.SyntaxString;

            var identifier = "constructor";
            // TypeScript constructor does not have modifiers
            if (SemicolonToken == null || SemicolonToken.IsEmpty)
            {
                string baseCall = string.Empty;
                if (Initializer != null)
                {
                    return $@" {identifier} {ParameterList.Translate()}
                        {{
                        {Initializer.Translate()}
                        {Body.Statements.Translate()}
                        }}  ";
                }
                return $@" {identifier} {ParameterList.Translate()}
                        {Body.Translate()}";
            }

            return $" {identifier} {ParameterList.Translate()}; ";
        }
    }
}
