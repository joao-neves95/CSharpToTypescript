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
    public class PredefinedTypeTranslation : TypeTranslation
    {
        public new PredefinedTypeSyntax Syntax
        {
            get { return (PredefinedTypeSyntax)base.Syntax; }
            set { base.Syntax = value; }
        }
        public PredefinedTypeTranslation() { }
        public PredefinedTypeTranslation(PredefinedTypeSyntax syntax, SyntaxTranslation parent) : base( syntax, parent )
        {
        }

        public override bool IsPrimitive
        {
            get
            {
                return Syntax.ToString() != "object";
            }
        }

        public string GetDefaultValue()
        {
            string type = Syntax.ToString();
            switch (type)
            {
                case "int":
                case "double":
                case "float":
                case "decimal":
                case "long":
                case "byte":
                case "sbyte":
                case "uint":
                case "ulong":
                case "short":
                case "ushort":
                    return "0";

                case "bool":
                    return "false";
                case "object":
                    return "null";
                case "string":
                case "char":
                    return "null";
            }

            return "null";
        }

        protected override string InnerTranslate()
        {
            string type = Syntax.ToString();
            switch (type)
            {
                case "int":
                case "double":
                case "float":
                case "decimal":
                case "long":
                case "byte":
                case "sbyte":
                case "uint":
                case "short":
                case "ushort":
                case "ulong":
                    return "number";

                case "bool":
                    return "boolean";
                case "object":
                    return "Object";
                // there is not char in javascript, thinking on whether char in C# should be a number or string
                // looks like string is better (not so sure)
                case "char":
                    return "string";
            }

            return type;
        }
    }
}
