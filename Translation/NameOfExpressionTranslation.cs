/*
 * Copyright (c) 2019-2020 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GPLv3.0 license (GNU General Public License v3.0),
 * located in the root of this project, under the name "LICENSE.md".
 *
 */

//using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.CSharp.Syntax;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RoslynTypeScript.Translation
//{
//    public class NameOfExpressionTranslation : ExpressionTranslation
//    {
//        public new NameOfExpressionSyntax Syntax
//        {
//            get { return (NameOfExpressionSyntax)base.Syntax; }
//            set { base.Syntax = value; }
//        }

//        public NameOfExpressionTranslation() { }
//        public NameOfExpressionTranslation(NameOfExpressionSyntax syntax, SyntaxTranslation parent) : base(syntax, parent)
//        {
//            Argument = syntax.Argument.Get<ExpressionTranslation>(this);
//        }

//        public ExpressionTranslation Argument { get; set; }

//        protected override string InnerTranslate()
//        {

//            //return Syntax.ToString();
//            return $"'{Argument.Translate()}'";
//        }
//    }
//}
