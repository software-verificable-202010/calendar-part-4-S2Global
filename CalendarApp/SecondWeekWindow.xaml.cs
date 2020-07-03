using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CalendarApp
{
    /// <summary>
    /// Interaction logic for SecondWeekWindow.xaml
    /// </summary>
    public partial class SecondWeekWindow : Window
    {
        #region Fields
        private readonly int weekLength = 7;
        private readonly string username;
        #endregion

        #region Methods
        public SecondWeekWindow(string user)
        {
            username = user;
        }

        public void UpdateSecondWeekView()
        {
            InitializeComponent();
            WeekView.Children.Clear();
            DayNumbers.Children.Clear();
            WeekTitle.Text = UpdateTitle(MainWindow.CalendarDate);
            List<TextBlock> numbers = UpdateDayNumbers(MainWindow.CalendarDate);
            foreach(var number in numbers)
            {
                DayNumbers.Children.Add(number);
            }
            List<TextBlock> times = UpdateTimes();
            foreach (var time in times)
            {
                WeekView.Children.Add(time);
            }
            UpdateDayAppointments();
        }

        private void NextWeekClick(object sender, RoutedEventArgs e)
        {
            MainWindow.CalendarDate = MainWindow.CalendarDate.AddDays(weekLength);
            UpdateSecondWeekView();
        }

        private void PreviousWeekClick(object sender, RoutedEventArgs e)
        {
            MainWindow.CalendarDate = MainWindow.CalendarDate.AddDays(-weekLength);
            UpdateSecondWeekView();
        }

        private void GoToAppointmentView(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                var button = sender as Button;
                Appointment appointment = button.Tag as Appointment;
                var AppointmentView = new AppointmentWindow();
                AppointmentView.UpdateAppointmentView(appointment);
                AppointmentView.Show();
            }
        }

        public List<TextBlock> UpdateDayNumbers(DateTime date)
        {
            List<TextBlock> numbers = new List<TextBlock>();
            int dayOffset = 1;
            DateTime dayTracker = date;
            int dayOfWeek = (int)dayTracker.DayOfWeek;
            dayTracker = dayTracker.AddDays(-dayOfWeek + dayOffset);
            for (int i = 1; i < weekLength + dayOffset; i++)
            {
                int dayOfPosition = dayTracker.Day;
                int rowPosition = 0;
                TextBlock dayNumber = new TextBlock
                {
                    Text = dayOfPosition.ToString(CultureInfo.InvariantCulture),
                    FontSize = 16
                };
                dayNumber.SetValue(Grid.RowProperty, rowPosition);
                dayNumber.SetValue(Grid.ColumnProperty, i);
                dayNumber.SetValue(Grid.VerticalAlignmentProperty, VerticalAlignment.Center);
                dayNumber.SetValue(Grid.HorizontalAlignmentProperty, HorizontalAlignment.Center);
                numbers.Add(dayNumber);
                dayTracker = dayTracker.AddDays(dayOffset);
            }
            return numbers;
        }

        private void UpdateDayAppointments()
        {
            int sunday = 7;
            int dayOffset = 1;
            DateTime dayTracker = MainWindow.CalendarDate;
            int dayOfWeek = (int)dayTracker.DayOfWeek;
            dayTracker = dayTracker.AddDays(-dayOfWeek + dayOffset);
            if (dayOfWeek == 0)
            {
                dayOfWeek = sunday;
            }
            for (int i = 0; i < weekLength; i++)
            {
                List<Appointment> dayAppointments = SecondWindow.GetSecondUserAppointments().FindAll(appointment => (appointment.StartDate.Date <= dayTracker.Date && appointment.EndDate.Date >= dayTracker.Date));
                if (dayAppointments.Count > 0)
                {
                    foreach (Appointment appointment in dayAppointments)
                    {
                        CreateButton(appointment, i, dayTracker.Date);
                    }
                }
                dayTracker = dayTracker.AddDays(dayOffset);
            }
        }

        private void CreateButton(Appointment appointment, int i, DateTime positionDate)
        {
            int dayOffset = 1;
            Button appointmentButtton = new Button
            {
                Content = appointment.Title,
                ToolTip = "Click to open.",
                Background = Brushes.Aquamarine,
                BorderThickness = new Thickness(0, 0, 0, 0),
                FontSize = 16
            };
            appointmentButtton.SetValue(Grid.ColumnProperty, i + dayOffset);
            bool firstDay = positionDate.Date == appointment.StartDate.Date;
            bool lastDay = positionDate.Date == appointment.EndDate.Date;
            if (firstDay && lastDay)
            {
                appointmentButtton.SetValue(Grid.RowProperty, appointment.StartDate.Hour);
                appointmentButtton.SetValue(Grid.RowSpanProperty, appointment.EndDate.Hour - appointment.StartDate.Hour + dayOffset);
            }
            else if (firstDay)
            {
                int fullDay = 24;
                appointmentButtton.SetValue(Grid.RowProperty, appointment.StartDate.Hour);
                appointmentButtton.SetValue(Grid.RowSpanProperty, fullDay);
            }
            else if (lastDay)
            {
                int begginingHour = 0;
                appointmentButtton.SetValue(Grid.RowProperty, begginingHour);
                appointmentButtton.SetValue(Grid.RowSpanProperty, appointment.EndDate.Hour + dayOffset);
            }
            else
            {
                int begginingHour = 0;
                int fullDay = 24;
                appointmentButtton.SetValue(Grid.RowProperty, begginingHour);
                appointmentButtton.SetValue(Grid.RowSpanProperty, fullDay);
            }
            appointmentButtton.Click += new RoutedEventHandler(GoToAppointmentView);
            appointmentButtton.Tag = appointment;
            appointmentButtton.SetValue(Grid.VerticalAlignmentProperty, VerticalAlignment.Stretch);
            appointmentButtton.SetValue(Grid.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);
            WeekView.Children.Add(appointmentButtton);
        }

#pragma warning disable CA1822 // Mark members as static
        public List<TextBlock> UpdateTimes()
#pragma warning restore CA1822 // Mark members as static
        {
            List<TextBlock> times = new List<TextBlock>();
            int loopStart = 0;
            int loopEnd = 23;
            for (int i = loopStart; i < loopEnd + 1; i++)
            {
                int columnPosition = 0;
                StringBuilder text = new StringBuilder();
                string seconds = ":00";
                text.Append(i);
                text.Append(seconds);
                TextBlock time = new TextBlock
                {

                    Text = text.ToString(),
                    FontSize = 12
                };
                time.SetValue(Grid.RowProperty, i);
                time.SetValue(Grid.ColumnProperty, columnPosition);
                time.SetValue(Grid.VerticalAlignmentProperty, VerticalAlignment.Center);
                time.SetValue(Grid.HorizontalAlignmentProperty, HorizontalAlignment.Center);
                times.Add(time);
            }
            return times;
        }

#pragma warning disable CA1822 // Mark members as static.
        public string UpdateTitle(DateTime date)
#pragma warning restore CA1822 // Mark members as static.
        {
            StringBuilder title = new StringBuilder();
            string separator = " - ";
            string userNameField = "Viewing: ";
            title.Append(date.Month.ToString(CultureInfo.InvariantCulture));
            title.Append(separator);
            title.Append(date.Year);
            title.Append(separator);
            title.Append(userNameField);
            title.Append(username);
            return title.ToString();
        }
        #endregion
    }
}