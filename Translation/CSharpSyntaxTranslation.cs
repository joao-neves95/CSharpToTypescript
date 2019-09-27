/*
 * Copyright (c) 2019 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

namespace RoslynTypeScript.Translation
{
    public abstract class CSharpSyntaxTranslation :SyntaxTranslation
    {
        public CSharpSyntaxTranslation() { }
        public CSharpSyntaxTranslation(SyntaxNode syntax, SyntaxTranslation parent) : base(syntax, parent)
        {
        }
    }
}
