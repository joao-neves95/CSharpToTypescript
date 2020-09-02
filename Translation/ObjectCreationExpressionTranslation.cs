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
    public class ObjectCreationExpressionTranslation : ExpressionTranslation
    {
        public new ObjectCreationExpressionSyntax Syntax
        {
            get { return (ObjectCreationExpressionSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }
        public ObjectCreationExpressionTranslation() { }
        public ObjectCreationExpressionTranslation(ObjectCreationExpressionSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            ArgumentList = syntax.ArgumentList.Get<ArgumentListTranslation>( this );
            Initializer = syntax.Initializer.Get<InitializerExpressionTranslation>( this );
            Type = syntax.Type.Get<TypeTranslation>( this );
        }

        public ArgumentListTranslation ArgumentList { get; set; }
        public InitializerExpressionTranslation Initializer { get; set; }
        public TypeTranslation Type { get; set; }

        protected override string InnerTranslate()
        {
            var name = Type.Translate();

            // the case object creation only by Initializer
            if (ArgumentList == null)
            {
                ArgumentList = new ArgumentListTranslation()
                {
                    Parent = this,
                    SyntaxString = "()"
                };
            }


            if (Initializer == null)
            {

                return $"new {Type.Translate()} {ArgumentList.Translate()}";
            }

            return $" __init(new {Type.Translate()} {ArgumentList.Translate()},{Initializer.Translate()})";
        }
    }
}
