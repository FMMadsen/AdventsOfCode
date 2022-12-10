using System.ComponentModel;
using System.Reflection;

namespace AdventsOfCode2022.Day2RockPaperScissors
{
    public static class Extentions
    {
        public static string Print(this Enum value)
        {
            Type type = value.GetType();
            string? name = Enum.GetName(type, value);
            if (name  != null)
            {
                FieldInfo? field = type.GetField(name);
                if (field != null)
                {
                    Attribute? attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
                    if(attribute != null)
                    {
                        DescriptionAttribute? attr = attribute as DescriptionAttribute;
                        if (attr != null)
                        {
                            return attr.Description;
                        }
                    }

                    
                }
            }
            return string.Empty;
        }
    }
}
