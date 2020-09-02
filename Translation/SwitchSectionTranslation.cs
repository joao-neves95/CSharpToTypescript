/*
 * Copyright (c) 2019-2020 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GPLv3.0 license (GNU General Public License v3.0),
 * located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Linq;

namespace RoslynTypeScript.Translation
{
    public class SwitchSectionTranslation : SyntaxTranslation
    {
        public new SwitchSectionSyntax Syntax
        {
            get { return (SwitchSectionSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public SwitchSectionTranslation() { }
        public SwitchSectionTranslation(SwitchSectionSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Labels = syntax.Labels.Get<SwitchLabelSyntax, SwitchLabelTranslation>( this );
            Statements = syntax.Statements.Get<StatementSyntax, StatementTranslation>( this );
        }
        public SyntaxListTranslation<SwitchLabelSyntax, SwitchLabelTranslation> Labels { get; set; }
        public SyntaxListTranslation<StatementSyntax, StatementTranslation> Statements { get; set; }

        public bool IsDefaultCase
        {
            get { return Labels.GetEnumerable().Any( f => f.Syntax.IsKind( SyntaxKind.DefaultSwitchLabel ) ); }
        }

        protected override string InnerTranslate()
        {
            return $@"{Labels.Translate()}
                    {Statements.Translate()}";
        }
    }
}
