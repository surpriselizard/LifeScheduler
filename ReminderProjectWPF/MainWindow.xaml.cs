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

        // New Reminder Window
        TextBox nameTxtbox = new TextBox();
        Window window = new Window();
        StackPanel mainPanel = new StackPanel();
        WrapPanel timePanel = new WrapPanel();
        Button finishButton = new Button();
        Label nameLabel = new Label();
        Label descLabel = new Label();
        Label dateLabel = new Label();
        Label timeLabel = new Label();
        TextBox descTxtbox = new TextBox();
        ComboBox hours = new ComboBox();
        ComboBox minutes = new ComboBox();
        ComboBox seconds = new ComboBox();
        DatePicker datePicker = new DatePicker();
        ReminderDbManager dbManager = new ReminderDbManager();

        

        private void NewReminder_Clicked(object sender, RoutedEventArgs e)
        {
            var hoursSource = new int[24];
            for (int i = 0; i < 24; ++i)
                hoursSource[i] = i;

            var secondMinuteSource = new int[60];
            for (int i = 0; i < 60; ++i)
                secondMinuteSource[i] = i;

            window.Padding = new Thickness(10);
            window.Title = "New Reminder";
            window.Width = 300.0d;
            window.Height = 385.0d;
            window.Content = mainPanel;
            window.ResizeMode = ResizeMode.NoResize;

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

            timeLabel.Content = "Choose the time the reminder is due: (24-Hour)";
            timePanel.HorizontalAlignment = HorizontalAlignment.Center;
            hours.ItemsSource = hoursSource;
            hours.Margin = new Thickness(4.0d);
            minutes.ItemsSource = secondMinuteSource;
            minutes.Margin = new Thickness(4.0d);
            seconds.ItemsSource = secondMinuteSource;
            seconds.Margin = new Thickness(4.0d);

            finishButton.Width = 60;
            finishButton.Content = "Finish";
            finishButton.HorizontalAlignment = HorizontalAlignment.Center;
            finishButton.Margin = new Thickness(0, 23.0d, 0, 0);


            mainPanel.Children.Add(nameLabel);
            mainPanel.Children.Add(nameTxtbox);

            mainPanel.Children.Add(descLabel);
            mainPanel.Children.Add(descTxtbox);

            mainPanel.Children.Add(dateLabel);
            mainPanel.Children.Add(datePicker);

            mainPanel.Children.Add(timeLabel);
            mainPanel.Children.Add(timePanel);

            timePanel.Children.Add(new Label() { Content = "Hour" });
            timePanel.Children.Add(hours);
            timePanel.Children.Add(new Label() { Content = "Minute" });
            timePanel.Children.Add(minutes);
            timePanel.Children.Add(new Label() { Content = "Second" });
            timePanel.Children.Add(seconds);

            mainPanel.Children.Add(finishButton);

            window.Show();

            finishButton.Click += new RoutedEventHandler(FinishButton_Click);
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameTxtbox.Text))
            {
                MessageBox.Show("Invalid Reminder: No Name");
            }
            else if (string.IsNullOrWhiteSpace(descTxtbox.Text))
            {
                MessageBox.Show("Invalid Reminder: No Description");
            }
            else if (string.IsNullOrWhiteSpace(datePicker.Text))
            {
                MessageBox.Show("Invalid Reminder: No Date Picked");
            }
            else if (string.IsNullOrWhiteSpace(hours.Text) || string.IsNullOrWhiteSpace(minutes.Text) || string.IsNullOrWhiteSpace(seconds.Text))
            {
                MessageBox.Show("Invalid Reminder: No Complete Time Picked");
            }

            DateTime dueDate = new DateTime(datePicker.DisplayDate.Year, datePicker.DisplayDate.Month, datePicker.DisplayDate.Day, (int)hours.SelectionBoxItem, (int)minutes.SelectionBoxItem, (int)seconds.SelectionBoxItem);

            dbManager.AddReminder(new Reminder(dueDate, nameTxtbox.Text) { ReminderDesc = descTxtbox.Text });

            window.Close();
        }
    }
}
