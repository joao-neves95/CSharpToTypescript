/*
 * Copyright (c) 2019-2020 João Pedro Martins Neves (shivayl) - All Rights Reserved.
 *
 * CSharpToTypescript is licensed under the GPLv3.0 license (GNU General Public License v3.0),
 * located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Settings;

using System.IO;
using System.Xml.Serialization;

namespace CSharpToTypescript.VSIX
{
    public class SettingStore : ISettingStore
    {
        public static SettingStore Instance = new SettingStore();

        private WritableSettingsStore userSettingsStore;

        private const string IsConvertToInterfaceConst = "IsConvertToInterface";
        private const string IsConvertMemberToCamelCaseConst = "IsConvertMemberToCamelCase";
        private const string CollectionPath = "CSharpToTypescript";
        private const string IsConvertListToArrayConst = "IsConvertListToArray";
        private const string ReplacedTypeNameArrayConst = "ReplacedTypeNameArray";
        private const string AddIPrefixInterfaceDeclarationConst = "AddIPrefixInterfaceDeclaration";
        private const string IsInterfaceOptionalPropertiesConsts = "IsInterfaceOptionalProperties";

        protected SettingStore()
        {
            SettingsManager settingsManager = new ShellSettingsManager( ServiceProvider.GlobalProvider );
            userSettingsStore = settingsManager.GetWritableSettingsStore( SettingsScope.UserSettings );
            if (!userSettingsStore.CollectionExists( CollectionPath ))
            {
                userSettingsStore.CreateCollection( CollectionPath );
            }
        }

        public bool IsConvertMemberToCamelCase
        {
            get
            {
                if (!userSettingsStore.PropertyExists( CollectionPath, IsConvertMemberToCamelCaseConst ))
                {
                    return false;
                }

                return userSettingsStore.GetBoolean( CollectionPath, IsConvertMemberToCamelCaseConst );
            }
            set
            {

                userSettingsStore.SetBoolean( CollectionPath, IsConvertMemberToCamelCaseConst, value );
            }
        }

        public bool IsConvertToInterface
        {
            get
            {
                if (!userSettingsStore.PropertyExists( CollectionPath, IsConvertToInterfaceConst ))
                {
                    return false;
                }

                return userSettingsStore.GetBoolean( CollectionPath, IsConvertToInterfaceConst );
            }
            set
            {

                userSettingsStore.SetBoolean( CollectionPath, IsConvertToInterfaceConst, value );
            }
        }

        public bool IsConvertListToArray
        {
            get
            {
                if (!userSettingsStore.PropertyExists( CollectionPath, IsConvertListToArrayConst ))
                {
                    return false;
                }

                return userSettingsStore.GetBoolean( CollectionPath, IsConvertListToArrayConst );
            }
            set
            {

                userSettingsStore.SetBoolean( CollectionPath, IsConvertListToArrayConst, value );
            }
        }

        public bool IsInterfaceOptionalProperties
        {
            get
            {
                if (!userSettingsStore.PropertyExists( CollectionPath, IsInterfaceOptionalPropertiesConsts ))
                {
                    return false;
                }

                return userSettingsStore.GetBoolean( CollectionPath, IsInterfaceOptionalPropertiesConsts );
            }
            set
            {

                userSettingsStore.SetBoolean( CollectionPath, IsInterfaceOptionalPropertiesConsts, value );
            }
        }

        XmlSerializer serializer = new XmlSerializer( typeof( TypeNameReplacementData[] ) );
        private TypeNameReplacementData[] replacedTypeNameArray;

        public TypeNameReplacementData[] ReplacedTypeNameArray
        {
            get
            {
                if (replacedTypeNameArray != null)
                {
                    return replacedTypeNameArray;
                }

                if (!userSettingsStore.PropertyExists( CollectionPath, ReplacedTypeNameArrayConst ))
                {
                    return new TypeNameReplacementData[] {
                        new TypeNameReplacementData
                        {
                            NewTypeName = "Date",
                            OldTypeName = "DateTimeOffset"
                        },
                        new TypeNameReplacementData
                        {
                            NewTypeName = "Date",
                            OldTypeName = "DateTime"
                        }
                    };
                }

                using (StringReader textReader = new StringReader( userSettingsStore.GetString( CollectionPath, ReplacedTypeNameArrayConst ) ))
                {
                    replacedTypeNameArray = (TypeNameReplacementData[])serializer.Deserialize( textReader );
                }

                return replacedTypeNameArray;

            }
            set
            {
                using (StringWriter textWriter = new StringWriter())
                {
                    serializer.Serialize( textWriter, value );
                    userSettingsStore.SetString( CollectionPath, ReplacedTypeNameArrayConst, textWriter.ToString() );
                    replacedTypeNameArray = value;
                }
            }
        }

        public bool AddIPrefixInterfaceDeclaration
        {
            get
            {
                if (!userSettingsStore.PropertyExists( CollectionPath, AddIPrefixInterfaceDeclarationConst ))
                {
                    return false;
                }

                return userSettingsStore.GetBoolean( CollectionPath, AddIPrefixInterfaceDeclarationConst );
            }
            set
            {

                userSettingsStore.SetBoolean( CollectionPath, AddIPrefixInterfaceDeclarationConst, value );
            }
        }
    }
}
