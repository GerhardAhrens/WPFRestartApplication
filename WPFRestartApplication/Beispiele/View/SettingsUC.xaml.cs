namespace WPFRestartApplication.Beispiele
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Threading;

    /// <summary>
    /// Interaktionslogik für SettingsUC.xaml
    /// </summary>
    public partial class SettingsUC : UserControl
    {
        public SettingsUC()
        {
            this.InitializeComponent();
            WeakEventManager<UserControl, RoutedEventArgs>.AddHandler(this, "Loaded", this.OnLoaded);
            this.DataContext = this;
        }

        #region WindowEventHandler
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Dictionary<int,string> EnvironmentSource = new Dictionary<int, string>();
            EnvironmentSource.Add(1, "Production");
            EnvironmentSource.Add(2, "Test");
            EnvironmentSource.Add(3, "Entwicklung");

            int index;
            if (App.Settings.Umgebung.Contains("Neustart mit Argument:") == true)
            {
                string compareValue = App.Settings.Umgebung.Split(':').LastOrDefault().Trim();
                index = EnvironmentSource.First(x => x.Value == compareValue).Key;
            }
            else
            {
                index = EnvironmentSource.First(x => x.Value == App.Settings.Umgebung).Key;
            }

            WeakEventManager<ComboBox, SelectionChangedEventArgs>.AddHandler(this.CbEnvironment, "SelectionChanged", this.OnEnvironmentSelectionChanged);

            this.Dispatcher.BeginInvoke(
                DispatcherPriority.Input,
                new Action(() =>
                {
                    this.CbEnvironment.SelectedValuePath = "Key";
                    this.CbEnvironment.DisplayMemberPath = "Value";
                    this.CbEnvironment.ItemsSource = EnvironmentSource;
                    this.CbEnvironment.SelectedValue = index;
                }));
        }

        private void OnEnvironmentSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.Settings.Umgebung = (string)((KeyValuePair<int, string>)this.CbEnvironment.SelectedItem).Value;
        }
        #endregion WindowEventHandler
    }
}
