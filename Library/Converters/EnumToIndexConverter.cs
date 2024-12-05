using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Converters
{
    public class EnumToIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && value.GetType().IsEnum)
                return (int)value; 
            
            return -1; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is int index && targetType.IsEnum)
                return Enum.ToObject(targetType, index); 
            
            throw new InvalidOperationException($"Cannot convert back to {targetType}. Make sure it is an enum.");
        }
    }
}
