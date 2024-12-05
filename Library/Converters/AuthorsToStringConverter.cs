using Library.Entities;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Converters
{
    public class AuthorsToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is List<Author> authorsList)
                return string.Join(", ", authorsList.Select(a => a.FullName));
            
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
