
using Microsoft.UI.Xaml.Controls;
using System;

namespace Library.Service
{
    public class NavigationService
    {
        private readonly Frame _contentFrame;

        public static Action<string, object> NavigateToPageStatic { get; private set; }
        public static Action GoBackStatic { get; private set; }
        public static Func<bool> CanGoBackStatic { get; private set; }

        public NavigationService(Frame contentFrame)
        {
            _contentFrame = contentFrame;

            NavigateToPageStatic = NavigateToPage;
            GoBackStatic = GoBack;
            CanGoBackStatic = CanGoBack;
        }

        public void NavigateToPage(string pageName, object parameter = null)
        {
            Type pageType = Type.GetType($"Library.View.{pageName}");

            if (pageType != null && _contentFrame.SourcePageType != pageType)
                _contentFrame.Navigate(pageType, parameter);
        }

        public void GoBack()
        {
            if (_contentFrame.CanGoBack)
                _contentFrame.GoBack();
            
        }

        public bool CanGoBack()
        {
            return _contentFrame.CanGoBack;
        }
    }
}
