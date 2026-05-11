namespace WPFRestartApplication.Beispiele
{
    using System.Windows;
    using System.Windows.Controls;

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

            WeakEventManager<ComboBox, SelectionChangedEventArgs>.AddHandler(this.CbEnvironment, "SelectionChanged", this.OnEnvironmentSelectionChanged);
            this.CbEnvironment.SelectedIndex = 2; // Default Wert
            this.CbEnvironment.SelectedValuePath = "Key";
            this.CbEnvironment.DisplayMemberPath = "Value";
            this.CbEnvironment.ItemsSource = EnvironmentSource;
        }

        private void OnEnvironmentSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindow.SelectedEnvironment = (string)((KeyValuePair<int, string>)this.CbEnvironment.SelectedItem).Value;
        }
        #endregion WindowEventHandler
    }
}
