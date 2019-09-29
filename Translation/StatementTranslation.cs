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
    public abstract class StatementTranslation : CSharpSyntaxTranslation
    {
        public StatementTranslation()
        {
        }

        public StatementTranslation(StatementSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
        }
    }
}
