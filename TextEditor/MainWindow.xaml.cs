using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DocumentManager _documentManager;

        public MainWindow()
        {
            InitializeComponent();

            _documentManager = new DocumentManager(body);

            //if (_documentManager.OpenDocument())
            //    status.Text = "Document is geladen.";
        }

        private void TextEditorToolbar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (toolbar.IsSynchronizing) return;

            ComboBox source = e.OriginalSource as ComboBox;
            if (source == null) return;

            switch (source.Name)
            {
                case "fonts":
                    _documentManager.ApplyToSelection(TextBlock.FontFamilyProperty, source.SelectedItem);
                    break;
                case "fontSize":
                    _documentManager.ApplyToSelection(TextBlock.FontSizeProperty, source.SelectedItem);
                    break;
            }

            body.Focus();
        }

        private void body_SelectionChanged(object sender, RoutedEventArgs e)
        {
            // update the toolbar
            toolbar.SynchronizeWith(body.Selection);
        }
    }
}
