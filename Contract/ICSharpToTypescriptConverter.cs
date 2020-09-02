/*
 * Copyright (c) 2019 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

namespace CSharpToTypescript.Contract
{
    public interface ICSharpToTypescriptConverter
    {
        string ConvertToTypescript(string text, ISettingStore settingStore);
    }
}
