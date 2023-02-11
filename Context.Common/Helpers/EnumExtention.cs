using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example.Notific.Context.Common.Helpers
{
    public static class EnumExtensions
    {

        //This is taken from following article: http://stackoverflow.com/questions/1415140/can-my-enums-have-friendly-names

        //To avoid collisions, every Enum type has its own hash table
        private static readonly Dictionary<Type, Dictionary<object, string>> enumToStringDictionary = new Dictionary<Type, Dictionary<object, string>>();
        private static readonly Dictionary<Type, Dictionary<string, object>> stringToEnumDictionary = new Dictionary<Type, Dictionary<string, object>>();

        static EnumExtensions()
        {
            //let's collect the enums we care about
            List<Type> enumTypeList = new List<Type>();

            //probe this assembly for all enums
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            Type[] exportedTypes = assembly.GetExportedTypes();

            foreach (Type type in exportedTypes)
            {
                if (type.IsEnum)
                    enumTypeList.Add(type);
            }

            //for each enum in our list, populate the appropriate dictionaries
            foreach (Type type in enumTypeList)
            {
                //add dictionaries for this type
                EnumExtensions.enumToStringDictionary.Add(type, new Dictionary<object, string>());
                EnumExtensions.stringToEnumDictionary.Add(type, new Dictionary<string, object>());

                Array values = Enum.GetValues(type);

                //its ok to manipulate 'value' as object, since when we convert we're given the type to cast to
                foreach (object value in values)
                {
                    System.Reflection.FieldInfo fieldInfo = type.GetField(value.ToString());

                    //check for an attribute 
                    System.ComponentModel.DescriptionAttribute attribute =
                           Attribute.GetCustomAttribute(fieldInfo,
                             typeof(System.ComponentModel.DescriptionAttribute)) as System.ComponentModel.DescriptionAttribute;

                    //populate our dictionaries
                    if (attribute != null)
                    {
                        EnumExtensions.enumToStringDictionary[type].Add(value, attribute.Description);
                        EnumExtensions.stringToEnumDictionary[type].Add(attribute.Description, value);
                    }
                    else
                    {
                        EnumExtensions.enumToStringDictionary[type].Add(value, value.ToString());
                        EnumExtensions.stringToEnumDictionary[type].Add(value.ToString(), value);
                    }
                }
            }
        }

        public static string GetString(this Enum value)
        {
            Type type = value.GetType();
            string aString = EnumExtensions.enumToStringDictionary[type][value];
            return aString;
        }

        public static T ToEnum<T>(this string value)
        {
            Type type = typeof(T);
            T theEnum = (T)EnumExtensions.stringToEnumDictionary[type][value];
            return theEnum;
        }
    }
}
