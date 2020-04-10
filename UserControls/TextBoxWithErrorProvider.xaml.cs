using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Footballers
{
    /// <summary>
    /// Interaction logic for TextBoxWithErrorProvider.xaml
    /// </summary>
    public partial class TextBoxWithErrorProvider : UserControl
    {
        public TextBoxWithErrorProvider()
        {
            InitializeComponent();
            SetError("");
        }
        #region Prop

        public static Brush BrushForAll { get; set; } = Brushes.Red;

        public Brush TextBorderBrush
        {
            get;
            set;
        }

        #endregion Prop
        #region Zdarzenie własne
        //rejestracja zdarzenia tak, żeby możliwe było jego bindowanie
        public static readonly RoutedEvent NumberChangedEvent =
        EventManager.RegisterRoutedEvent("TabItemSelected",
                     RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                     typeof(TextBoxWithErrorProvider));

        //definicja zdarzenia NumberChanged
        public event RoutedEventHandler NumberChanged
        {
            add { AddHandler(NumberChangedEvent, value); }
            remove { RemoveHandler(NumberChangedEvent, value); }
        }

        //Metoda pomocnicza wywołująca zdarzenie
        //przy okazji metoda ta tworzy obiekt argument przekazywany przez to zdarzenie
        void RaiseTextChanged()
        {
            //argument zdarzenia
            RoutedEventArgs newEventArgs =
                    new RoutedEventArgs(TextBoxWithErrorProvider.NumberChangedEvent);
            //wywołanie zdarzenia
            RaiseEvent(newEventArgs);
        }
        #endregion

        #region Własność zależna
        //zarejestrowanie własności zależenej - taki mechanizm konieczny jest
        // aby możliwe było Bindowanie tej właśności z innnymi obiektami
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(TextBoxWithErrorProvider),
                new FrameworkPropertyMetadata(null)
            );
        //"czysta" właściwość powiązania z właściwości zależną
        //do niej będziemy się odnosić w XAMLU
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        #endregion

        #region metody wewnetrzne
       

        public void SetError(string errorText)
        {
            if (errorText == "")
            {
                border.BorderThickness = new System.Windows.Thickness(0);
                tooltipW.Visibility = System.Windows.Visibility.Hidden;
                textBlockErrorText.Text = "";
                return;
            }
            textBlockErrorText.Text = errorText;
            border.BorderThickness = new System.Windows.Thickness(1);
            tooltipW.Visibility = System.Windows.Visibility.Visible;
        }

        public void SetFocus()
        {
            textBlockErrorText.Focus();
        }

        private void textBoxContent_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (decimal.TryParse(e.Text, out _)) e.Handled = true;
        }
        #endregion
    }
}