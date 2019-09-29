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
    public class ParameterListTranslation : CSharpSyntaxTranslation
    {
        public new ParameterListSyntax Syntax
        {
            get { return (ParameterListSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public SeparatedSyntaxListTranslation<ParameterSyntax, ParameterTranslation> Parameters { get; set; }

        public ParameterListTranslation()
        {
        }

        public ParameterListTranslation(ParameterListSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Parameters = syntax.Parameters.Get<ParameterSyntax, ParameterTranslation>( this );
        }

        private bool excludeDefaultValue;

        public bool ExcludeDefaultValue
        {
            get { return excludeDefaultValue; }
            set
            {
                excludeDefaultValue = value;
                foreach (var item in Parameters.GetEnumerable())
                {
                    item.ExcludeDefaultValue = value;

                }
            }
        }

        protected override string InnerTranslate()
        {
            return $"({Parameters.Translate()})";
        }

    }
}
