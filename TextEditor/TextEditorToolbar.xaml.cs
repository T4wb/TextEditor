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
    /// Interaction logic for TextEditorToolbar.xaml
    /// </summary>
    public partial class TextEditorToolbar : UserControl
    {
        public TextEditorToolbar()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            for (double i = 8; i < 48; i += 2)
            {
                fontSize.Items.Add(i);
            }
        }

        public void SynchronizeWith(TextSelection selection)
        {
            Synchronize<double>(selection, TextBlock.FontSizeProperty, SetFontSize);
            Synchronize<FontWeight>(selection, TextBlock.FontWeightProperty, SetFontWeight);
            Synchronize<FontStyle>(selection, TextBlock.FontStyleProperty, SetFontStyle);
            Synchronize<TextDecorationCollection>(selection, TextBlock.TextDecorationsProperty, SetDecorations);
            Synchronize<FontFamily>(selection, TextBlock.FontFamilyProperty, SetFontFamily);
        }

        private void Synchronize<T>(TextSelection selection, DependencyProperty property, Action<T> methodToCall)
        {
            object value = selection.GetPropertyValue(property);

            if (value != DependencyProperty.UnsetValue)
            {
                methodToCall((T)value);
            }
        }

        private void SetFontSize(Double size)
        {
            fontSize.SelectedValue = size;
        }

        private void SetFontWeight(FontWeight weight)
        {
            boldButton.IsChecked = (weight == FontWeights.Bold);
        }

        private void SetFontStyle(FontStyle style)
        {
            italicButton.IsChecked = (style == FontStyles.Italic);
        }

        private void SetDecorations(TextDecorationCollection decoration)
        {
            underlineButton.IsChecked = (decoration == TextDecorations.Underline);
        }

        private void SetFontFamily(FontFamily family)
        {
            fonts.SelectedItem = family;
        }

    }
}
