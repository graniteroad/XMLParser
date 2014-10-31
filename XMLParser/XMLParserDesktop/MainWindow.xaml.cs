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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using XMLReader;
using System.IO;
using System.Data;
using System.Threading;
using System.ComponentModel;

namespace XMLParserDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.SelectedPath = "C:\\";

            DialogResult result = folderDialog.ShowDialog();
            if (result.ToString() == "OK")
                txtBoxFolder.Text = folderDialog.SelectedPath;
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            Search s = new Search();
            List<Files> files = new List<Files>();

            string stringPath = txtBoxFolder.Text;
            dgFiles.ItemsSource = null;
            btnFind.IsEnabled = false;
            btnBrowse.IsEnabled = false;

            // Do Process in seperate Thread
            BackgroundWorker bw = new BackgroundWorker();

            // this allows our worker to report progress during work
            bw.WorkerReportsProgress = true;

            // what to do in the background thread
            bw.DoWork += new DoWorkEventHandler(
            delegate(object o, DoWorkEventArgs args)
            {
                BackgroundWorker b = o as BackgroundWorker;
                int i = 0;
                // do some simple processing 
                files = s.XMLfiles(stringPath, b, ref i);
            });

            // what to do when progress changed (update the progress bar for example)
            bw.ProgressChanged += new ProgressChangedEventHandler(
            delegate(object o, ProgressChangedEventArgs args)
            {
                // This function fires on the UI thread so it's safe to edit

                // the UI control directly, no funny business with Control.Invoke :)

                // Update the progressBar with the integer supplied to us from the

                // ReportProgress() function.  

                progressBar.Value = args.ProgressPercentage;
                lblStatus.Content = "Processing......" + progressBar.Value.ToString() + "%";
            });

            // what to do when worker completes its task (notify the user)
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            delegate(object o, RunWorkerCompletedEventArgs args)
            {
                // The background process is complete. We need to inspect
                // our response to see if an error occurred, a cancel was
                // requested or if we completed successfully.  
                if (args.Cancelled)
                {
                    lblStatus.Content = "Task Cancelled.";
                }

                // Check to see if an error occurred in the background process.

                else if (args.Error != null)
                {
                    lblStatus.Content = "Error while performing background operation.";
                }
                else
                {
                    // Everything completed normally.
                    lblStatus.Content = "Task Completed...";
                }

                //Change the status of the buttons on the UI accordingly
                btnBrowse.IsEnabled = true;
                btnFind.IsEnabled = true;
                dgFiles.ItemsSource = files;
            });

            bw.RunWorkerAsync();
        }

        private void dgFiles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DisplayXMLTree xmlTree = new DisplayXMLTree();
            xmlTree.Owner = this;
            xmlTree.Path = ((Files)dgFiles.Items[dgFiles.SelectedIndex]).FilePath;
            xmlTree.ShowDialog();
        }
    }
}
