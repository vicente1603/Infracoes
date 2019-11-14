using System.ComponentModel.DataAnnotations;

namespace Infracoes.Attributes
{
    public class RequiredIfAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return (bool)value;
        }
    }
}
