using Microsoft.UI.Input;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using System;
using System.Xml.Linq;
using Windows.ApplicationModel;
using Windows.Devices.Enumeration;
using Rect = Windows.Foundation.Rect;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Library
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private AppWindow m_AppWindow;


        public MainWindow()
        {
            this.InitializeComponent();

            m_AppWindow = this.AppWindow;
            ExtendsContentIntoTitleBar = true;
            if (ExtendsContentIntoTitleBar == true)
                m_AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Standard;

            TitleBarTextBlock.Text = AppInfo.Current.DisplayInfo.DisplayName;
        }

    }
}
