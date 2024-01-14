using System;
using System.ComponentModel;

namespace Service
{
    public enum SaveNames
    {
        [Description("sound_save")] Sound,
        [Description("result_table_save")] Total,
    }

    public static class EnumDescriptionAttribute
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}