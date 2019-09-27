/*
 * Copyright (c) 2019 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using CSharpToTypescript.VSIX;

namespace CSharpToTypescript
{
    public interface ISettingStore
    {
        bool AddIPrefixInterfaceDeclaration { get; set; }
        bool IsConvertListToArray { get; set; }
        bool IsConvertMemberToCamelCase { get; set; }
        bool IsConvertToInterface { get; set; }
        bool IsInterfaceOptionalProperties { get; set; }
        TypeNameReplacementData[] ReplacedTypeNameArray { get; set; }
    }
}