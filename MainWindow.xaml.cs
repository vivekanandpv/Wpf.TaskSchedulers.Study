using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Wpf.TaskSchedulers.Study
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            button.Click += Button_Click;

            this.Closed += (sender, args) =>
            {
                Application.Current.Shutdown(0);
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //  This raises an exception as WPF and other
            //  UI based frameworks have thread affinity requirements

            //  UI elements can only be accessed on the main thread
            //  This continuation runs in a pooled thread and
            //  tries to set the property of a UI element

            //  It causes: System.InvalidOperationException:
            //  'The calling thread cannot access this object
            //  because a different thread owns it.'

            ClickStartAsync()
                .ContinueWith(t =>
                {
                    label.Content = t.Result;
                });
        }

        private async Task<string> ClickStartAsync()
        {
            var number = await DoStuffAsync();
            return $"We got: {number}";
        }

        private Task<int> DoStuffAsync()
        {
            return Task.Run(() =>
            {
                Thread.Sleep(2000);
                Random random = new Random();
                return random.Next(1_000, 10_0000);
            });
        }
    }
}
