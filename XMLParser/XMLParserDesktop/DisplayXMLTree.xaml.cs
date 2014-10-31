using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Forms;

namespace XMLParserDesktop
{
    /// <summary>
    /// Interaction logic for DisplayXMLTree.xaml
    /// </summary>
    public partial class DisplayXMLTree : Window
    {
        public DisplayXMLTree()
        {
            InitializeComponent();
        }

        private void treeViewXML_Loaded(object sender, RoutedEventArgs e)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(this.Path);
            XmlDataProvider dataProvider = this.FindResource("xmlDataProvider") as XmlDataProvider;
            dataProvider.Document = xmlDocument;
        }

        public string Path
        {
            get;
            set;
        }
    }
}
