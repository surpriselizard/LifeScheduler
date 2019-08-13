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

namespace ReminderProjectWPF
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

        private void NewReminder_Clicked(object sender, RoutedEventArgs e)
        {
            Window window = new Window();
            StackPanel mainPanel = new StackPanel();
            Label nameLabel = new Label();
            Label descLabel = new Label();
            Label dateLabel = new Label();
            TextBox nameTxtbox = new TextBox();
            TextBox descTxtbox = new TextBox();
            DatePicker datePicker = new DatePicker();
            ReminderDbManager dbManager = new ReminderDbManager();

            window.Padding = new Thickness(10);
            window.Title = "New Reminder";
            window.Width = 300.0d;
            window.Height = 500.0d;
            window.Content = mainPanel;

            nameLabel.Content = "Name: ";
            nameTxtbox.Margin = new Thickness(7.0d, 0.3d, 7.0d, 0.3d);
            nameTxtbox.MaxLength = 25;

            descLabel.Content = "Description: ";
            descTxtbox.Margin = new Thickness(7.0d, 0.3d, 7.0d, 0.3d);
            descTxtbox.Height = 100.0d;
            descTxtbox.TextWrapping = TextWrapping.Wrap;
            descTxtbox.MaxLength = 140;

            dateLabel.Content = "Choose the due date for the reminder: ";
            datePicker.Margin = new Thickness(7.0d, 0.3d, 7.0d, 0.3d);

            mainPanel.Children.Add(nameLabel);
            mainPanel.Children.Add(nameTxtbox);

            mainPanel.Children.Add(descLabel);
            mainPanel.Children.Add(descTxtbox);

            mainPanel.Children.Add(dateLabel);
            mainPanel.Children.Add(datePicker);

            window.Show();
        }
    }
}
