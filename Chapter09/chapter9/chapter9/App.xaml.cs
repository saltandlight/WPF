using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Markup;

namespace chapter9
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {

        public App()
        {

            //ResourceDictionary resources = null;
            //using (FileStream fs = new FileStream("Dictionary1.xaml", FileMode.Open, FileAccess.Read))
            //{
            //    resources = (ResourceDictionary)XamlReader.Load(fs);

            //}
            //Application.Current.Resources=resources;
            //this.Resources = new ResourceDictionary { Source = new Uri(@"Dictionary1.xaml", UriKind.Relative) };

            //var resources = new ResourceDictionary();
            //resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri(@"Dictionary1.xaml", UriKind.Relative) });

            //this.Resources = resources;
        }
    }
}
