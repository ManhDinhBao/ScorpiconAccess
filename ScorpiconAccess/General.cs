using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace ScorpiconAccess
{
    public class General
    {
        public static SolidColorBrush readOnlyBackground = new SolidColorBrush(Color.FromRgb(238, 238, 238));
        public static SolidColorBrush normalBackground = new SolidColorBrush(Colors.White);


        public static int ADD_MODE = 1;
        public static int CHANGE_MODE = 2;



        public static string StringFromRichTextBox(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(
                // TextPointer to the start of content in the RichTextBox.
                rtb.Document.ContentStart,
                // TextPointer to the end of content in the RichTextBox.
                rtb.Document.ContentEnd
            );

            // The Text property on a TextRange object returns a string
            // representing the plain text content of the TextRange.
            return textRange.Text;
        }
    }
}
