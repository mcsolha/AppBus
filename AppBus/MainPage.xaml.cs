using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AppBus
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {        
        public static MainPage Current;
        private StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        public List<Scenario> Scenarios
        {
            get { return this.scenarios; }
        }

        public StorageFolder LocalFolder
        {
            get
            {
                return localFolder;
            }
        }

        public MainPage()
        {
            this.InitializeComponent();
            Current = this;
            AppName.Text = FEATURE_NAME;
            SizeChanged += MainPage_SizeChanged;
        }

        private void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Window.Current.Bounds.Width > 640)
            {
                ScenarioFrame.Visibility = Visibility.Visible;
            }
            else if (Window.Current.Bounds.Width <= 640)
            {
                if (Splitter.IsPaneOpen)                
                    ScenarioFrame.Visibility = Visibility.Collapsed;
                else
                    ScenarioFrame.Visibility = Visibility.Visible;

            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Populate the scenario list from the SampleConfiguration.cs file
            ScenarioControl.ItemsSource = scenarios;
            ScenarioControl.SelectedIndex = 0;
        }
        /// <summary>
        /// Called whenever the user changes selection in the scenarios list.  This method will navigate to the respective
        /// sample scenario page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScenarioControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox scenarioListBox = sender as ListBox;
            Scenario s = scenarioListBox.SelectedItem as Scenario;
            if (s != null)
            {
                ScenarioFrame.Navigate(s.ClassType);
                if (Window.Current.Bounds.Width < 640)
                {
                    Splitter.IsPaneOpen = false;
                    ScenarioFrame.Visibility = Visibility.Visible;
                }
            }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            if (Window.Current.Bounds.Width <= 640)
            {
                if (!Splitter.IsPaneOpen)
                    ScenarioFrame.Visibility = Visibility.Collapsed;
                else
                    ScenarioFrame.Visibility = Visibility.Visible;

            }
            Splitter.IsPaneOpen = !Splitter.IsPaneOpen;
        }
    }
}
