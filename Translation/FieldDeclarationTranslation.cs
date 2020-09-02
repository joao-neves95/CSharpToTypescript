/*
 * Copyright (c) 2019-2020 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GPLv3.0 license (GNU General Public License v3.0),
 * located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RoslynTypeScript.Translation
{
    public class FieldDeclarationTranslation : MemberDeclarationTranslation
    {
        public new FieldDeclarationSyntax Syntax
        {
            get { return (FieldDeclarationSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }
        public VariableDeclarationTranslation Declaration { get; set; }
        public SyntaxTokenListTranslation Modifiers { get; set; }

        public FieldDeclarationTranslation() { }
        public FieldDeclarationTranslation(FieldDeclarationSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Declaration = syntax.Declaration.Get<VariableDeclarationTranslation>( this );
            Declaration.ExcludeVar = true;
            Modifiers = syntax.Modifiers.Get( this );
            Modifiers.ConstantToStatic = true;
        }

        protected override string InnerTranslate()
        {
            return string.Format( "{0} {1};", Modifiers.Translate(), Declaration.Translate() );
        }
    }
}
