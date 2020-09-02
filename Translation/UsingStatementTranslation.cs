/*
 * Copyright (c) 2019-2020 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GPLv3.0 license (GNU General Public License v3.0),
 * located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Linq;

namespace RoslynTypeScript.Translation
{
    public class UsingStatementTranslation : StatementTranslation
    {
        public new UsingStatementSyntax Syntax
        {
            get { return (UsingStatementSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public UsingStatementTranslation() { }
        public UsingStatementTranslation(UsingStatementSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Declaration = syntax.Declaration.Get<VariableDeclarationTranslation>( this );
            Expression = syntax.Expression.Get<ExpressionTranslation>( this );
            Statement = syntax.Statement.Get<StatementTranslation>( this );

            //if(Expression != null)
            //{
            //    throw new Exception("only support Declaration");
            //}
        }

        public VariableDeclarationTranslation Declaration { get; set; }
        public ExpressionTranslation Expression { get; set; }
        public StatementTranslation Statement { get; set; }


        protected override string InnerTranslate()
        {
            // support first variable only
            //if(Declaration.Variables.SyntaxCollection.Count!=1)
            //{
            //    throw new Exception("only support one variable");
            //}

            string variable = Declaration?.Variables.GetEnumerable().First().Identifier.ToString() ?? "__temp";
            string block = Statement.Translate();
            if (!(Statement is BlockTranslation))
            {
                block = $@"{{
                    {block}
                    }}";
            }
            string callDisposable = $"if({variable}!=null) {variable}.Dispose();";
            string declaration = Declaration?.Translate() ?? $"var {variable} = {Expression.Translate()}";
            return $@"{declaration}
                try 
                {block}
                finally {{
                {callDisposable} 
                }}";


            //return Syntax.ToString();
        }
    }
}
