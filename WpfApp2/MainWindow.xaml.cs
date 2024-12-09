using System.Data;
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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string operations = "";  
        private bool resultCalculated = false; 

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonNumber_Click(object sender, RoutedEventArgs e)
        {
            string number = (string)((Button)sender).Tag;

            if (resultCalculated)
            {
                resultCalculated = false;
            }

            operations += number;
            UpdateInputTextBox();
        }

        private void ButtonOperator_Click(object sender, RoutedEventArgs e)
        {
            string op = (string)((Button)sender).Tag;

            if (resultCalculated)
            {
                resultCalculated = false;
            }

            if (operations.Length > 0 && !char.IsDigit(operations[^1]))
            {
                operations = operations.Remove(operations.Length - 1) + op;
            }
            else
            {
                operations += " " + op + " ";
            }

            UpdateInputTextBox();
        }

        private void ButtonEquals_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(operations))
            {
                try
                {
                    var result = new DataTable().Compute(operations, null);
                    CurrentNumber.Text = result.ToString();
                    resultCalculated = true;
                }
                catch (Exception)
                {
                    CurrentNumber.Text = "Error";
                }
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            operations = "";
            UpdateInputTextBox();
            CurrentNumber.Text = "0";
            resultCalculated = false;
        }

        private void ButtonClearEntry_Click(object sender, RoutedEventArgs e)
        {
            if (operations.Length > 0)
            {
 
                if (char.IsDigit(operations[^1]))
                {
                    int index = operations.LastIndexOf(' ');
                    if (index != -1)
                    {
                        operations = operations.Substring(0, index);
                    }
                    else
                    {
                        operations = "";
                    }
                }
                else
                {
                    operations = operations.Remove(operations.Length - 1);
                }
            }

            UpdateInputTextBox();
            resultCalculated = false;
        }

        private void ButtonBackspace_Click(object sender, RoutedEventArgs e)
        {
            if (operations.Length > 0)
            {
                operations = operations.Remove(operations.Length - 1);
                UpdateInputTextBox();
            }
        }

        private void UpdateInputTextBox()
        {
            InputTextBox1.Text = operations;
        }
    }
}