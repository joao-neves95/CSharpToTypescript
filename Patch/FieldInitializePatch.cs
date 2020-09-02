/*
 * Copyright (c) 2019-2020 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GPLv3.0 license (GNU General Public License v3.0),
 * located in the root of this project, under the name "LICENSE.md".
 *
 */

using RoslynTypeScript.Translation;

using System.Linq;

namespace RoslynTypeScript.Patch
{
    /// <summary>
    /// Not using at this moment!!!!
    /// try to find constructor and set default value for field which is number, boolean. Because
    /// in javascript all fields are undefined at the first place.
    /// </summary>
    public class FieldInitializePatch : Patch
    {
        public void Apply(ClassDeclarationTranslation typeTranslation)
        {
            var fields = typeTranslation.Members.GetEnumerable<FieldDeclarationTranslation>();
            if (!fields.Any())
            {
                return;
            }

            ConstructorDeclarationTranslation constructor = FindConstructor( typeTranslation );
            if (constructor == null)
            {
                return;
            }

            if (!typeTranslation.HasExplicitBase())
            {
                return;
            }

            foreach (FieldDeclarationTranslation field in fields)
            {
                if (field.Modifiers.IsStatic)
                {
                    continue;
                }

                AssignmentExpressionTranslation assignment = BuildAssignment( field );
                constructor.Body.Statements.Insert( 0, assignment );
            }
        }

        private ConstructorDeclarationTranslation FindConstructor(TypeDeclarationTranslation typeTranslation)
        {
            var constructor = typeTranslation.Members.GetEnumerable<ConstructorDeclarationTranslation>().FirstOrDefault( f => !f.IsDeclarationOverload );
            return constructor;
        }

        private AssignmentExpressionTranslation BuildAssignment(FieldDeclarationTranslation field)
        {
            var declarator = field.Declaration.Variables.GetEnumerable().First();
            var initializeStr = declarator.GetInitializerStr();
            string statement = $"this.{declarator.Identifier.Translate()}{initializeStr};";
            return new AssignmentExpressionTranslation() { SyntaxString = statement };
        }
    }
}
