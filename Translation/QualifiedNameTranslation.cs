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
    public class QualifiedNameTranslation : NameTranslation
    {
        public new QualifiedNameSyntax Syntax
        {
            get { return (QualifiedNameSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }

        public QualifiedNameTranslation(QualifiedNameSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
            Left = syntax.Left.Get<NameTranslation>( this );
            Right = syntax.Right.Get<SimpleNameTranslation>( this );

            var genericTranslation = Left as GenericNameTranslation;
            if (genericTranslation != null)
            {
                ((GenericNameTranslation)Left).ExcludeTypeParameter = true;

            }

            var simpleName = Right as SimpleNameTranslation;
            if (simpleName != null)
            {
                simpleName.DetectApplyThis = false;
                var identifierName = simpleName as IdentifierNameTranslation;
                if (genericTranslation != null && identifierName != null)
                {
                    identifierName.TypeArgumentList = genericTranslation.TypeArgumentList;
                }

            }
        }

        public override string GetTypeIgnoreGeneric()
        {
            return $"{Left.GetTypeIgnoreGeneric()}.{Right.Translate()}";
        }

        public NameTranslation Left { get; set; }
        public SimpleNameTranslation Right { get; set; }

        protected override string InnerTranslate()
        {
            return $"{Left.Translate()}.{Right.Translate()}";
        }
    }
}
