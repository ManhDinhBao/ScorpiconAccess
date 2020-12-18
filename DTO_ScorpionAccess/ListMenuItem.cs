using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_ScorpionAccess
{
    public class ListMenuItem
    {
        public string TextBinding { get; set; }
        public string ImageSource { get; set; }
        public string Key { get; set; }

        public ListMenuItem()
        {
        }

        public ListMenuItem(string textBinding, string imageSource, string key)
        {
            TextBinding = textBinding;
            ImageSource = imageSource;
            Key = key;
        }
    }
}
