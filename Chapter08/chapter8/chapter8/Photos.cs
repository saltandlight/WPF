using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace chapter8
{
    public class Photo
    {
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public int Size { get; set; }
    }

    public class Photos : ObservableCollection<Photo>
    {
    }

    
}
