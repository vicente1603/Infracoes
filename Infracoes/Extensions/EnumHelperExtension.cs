using System;
using System.ComponentModel.DataAnnotations;

namespace Infracoes.Extensions
{
    public static class EnumHelperExtension
    {
        public static string GetDisplayName(this Enum value)
        {
            var type = value.GetType();
            if (!type.IsEnum) return "";

            var members = type.GetMember(value.ToString());
            if (members.Length == 0) return "";

            var member = members[0];
            var attributes = member.GetCustomAttributes(typeof(DisplayAttribute), false);
            if (attributes.Length == 0) return "";

            var attribute = (DisplayAttribute)attributes[0];
            return attribute.GetName();
        }
    }
}