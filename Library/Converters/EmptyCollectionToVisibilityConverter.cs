using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml;
using System;
using System.Collections;


namespace Library.Converters
{
    public class EmptyCollectionToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is IEnumerable collection)
            {
                bool isEmpty = !collection.GetEnumerator().MoveNext();

                bool invert = parameter != null && parameter.ToString() == "true";
                if (invert)
                    return isEmpty ? Visibility.Collapsed : Visibility.Visible;

                return isEmpty ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (!(value is Visibility visibility))
                return false;

            bool result = visibility == Visibility.Visible;

            bool invert = parameter != null && parameter.ToString() == "true";
            return invert ? !result : result;
        }
    }
}
