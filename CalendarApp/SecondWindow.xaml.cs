﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CalendarApp
{
    /// <summary>
    /// Interaction logic for SecondWindow.xaml
    /// </summary>
    public partial class SecondWindow : Window
    {
        #region Fields
        private readonly string appointmentFileName = "Appointments.json";
        private static List<Appointment> secondUserAppointments;
        private static SecondWeekWindow secondWeekView;
        #endregion

        #region Properties
        public static DateTime CalendarDate { get; set; }
        public static string SecondUser { get; set; }
        public static List<Appointment> GetSecondUserAppointments()
        {
            if (secondUserAppointments != null)
            {
                return secondUserAppointments;
            }
            else
            {
                return new List<Appointment>();
            }
        }
        public static void SetSessionUserAppointments(List<Appointment> value)
        {
            secondUserAppointments = value;
        }
        #endregion

        #region Methods

        public SecondWindow(string user)
        {
            SecondUser = user;
        }

        public void UpdateSecondWindow()
        {
            InitializeComponent();
            CalendarDate = DateTime.Now;
            UpdateCalendar();
            GoToWeek();
        }

        private void UpdateCalendar()
        {
            MonthView.Children.Clear();
            MainTitle.Text = UpdateTitle(CalendarDate, SecondUser);
            MonthView.Children.Add(UpdateWeekendRectangle());
            GetAppointments();
            UpdateDayNumbers();
            UpdateDayButtons();
        }

        private void GetAppointments()
        {

            string jsonAppointments = null;
            try
            {
                jsonAppointments = File.ReadAllText(appointmentFileName);
            }
            catch (FileNotFoundException e)
            {
                Debug.Write(e);
            }

            if (jsonAppointments != null)
            {
                List<Appointment> allAppointments = JsonConvert.DeserializeObject<List<Appointment>>(jsonAppointments);
                SetSessionUserAppointments(allAppointments.Where(x => x.Participants.Contains(SecondUser)).ToList());
            }
        }

        private void NextClick(object sender, RoutedEventArgs e)
        {
            int nextMonth = 1;
            CalendarDate = CalendarDate.AddMonths(nextMonth);
            UpdateCalendar();
        }

        private void PreviousClick(object sender, RoutedEventArgs e)
        {
            int previousMonth = -1;
            CalendarDate = CalendarDate.AddMonths(previousMonth);
            UpdateCalendar();
        }

        private static void GoToWeek()
        {
            secondWeekView = new SecondWeekWindow(SecondUser);
            secondWeekView.UpdateSecondWeekView();
            secondWeekView.Show();
        }

        private void GoToAppointmentsView(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                var button = sender as Button;
                List<Appointment> appointments = button.Tag as List<Appointment>;
                foreach (Appointment appointment in appointments)
                {
                    var AppointmentView = new AppointmentWindow();
                    AppointmentView.UpdateAppointmentView(appointment);
                    AppointmentView.Show();
                }
            }
        }

#pragma warning disable CA1822 // Mark members as static.
        public Rectangle UpdateWeekendRectangle()
#pragma warning restore CA1822 // Mark members as static.
        {
            int weekendRowProperty = 0;
            int weekendColumnProperty = 5;
            int weekendRowSpanProperty = 6;
            int weekendColumnSpanProperty = 2;
            Rectangle weekendHighlight = new Rectangle();
            weekendHighlight.SetValue(Grid.RowProperty, weekendRowProperty);
            weekendHighlight.SetValue(Grid.ColumnProperty, weekendColumnProperty);
            weekendHighlight.SetValue(Grid.RowSpanProperty, weekendRowSpanProperty);
            weekendHighlight.SetValue(Grid.ColumnSpanProperty, weekendColumnSpanProperty);
            SolidColorBrush rectangleColourFill = new SolidColorBrush
            {
                Color = Color.FromArgb(67, 200, 200, 10)
            };
            weekendHighlight.SetValue(Shape.FillProperty, rectangleColourFill);
            return weekendHighlight;
        }

        private void UpdateDayNumbers()
        {
            int sunday = 7;
            int year = CalendarDate.Year;
            int month = CalendarDate.Month;
            int firstDay = 1;
            DateTime firstDayOfMonth = new DateTime(year, month, firstDay);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int firstDayOfWeek = (int)firstDayOfMonth.DayOfWeek;
            if (firstDayOfWeek == 0)
            {
                firstDayOfWeek = sunday;
            }
            sunday--;
            int day = 1;
            int week = 0;
            int weekDay = firstDayOfWeek - day;
            for (int i = firstDayOfWeek; i < daysInMonth + firstDayOfWeek; i++)
            {
                TextBlock dayNumber = new TextBlock
                {
                    Text = day.ToString(CultureInfo.InvariantCulture)
                };
                dayNumber.SetValue(Grid.RowProperty, week);
                dayNumber.SetValue(Grid.ColumnProperty, weekDay);
                MonthView.Children.Add(dayNumber);
                if (weekDay == sunday)
                {
                    week++;
                    weekDay = 0;
                }
                else
                {
                    weekDay++;
                }
                day++;
            }
        }

        private void UpdateDayButtons()
        {
            int sunday = 7;
            int year = CalendarDate.Year;
            int month = CalendarDate.Month;
            int firstDay = 1;
            DateTime firstDayOfMonth = new DateTime(year, month, firstDay);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int firstDayOfWeek = (int)firstDayOfMonth.DayOfWeek;
            if (firstDayOfWeek == 0)
            {
                firstDayOfWeek = sunday;
            }
            sunday--;
            int day = 1;
            int week = 0;
            int weekDay = firstDayOfWeek - day;
            for (int i = firstDayOfWeek; i < daysInMonth + firstDayOfWeek; i++)
            {
                DateTime currentDate = firstDayOfMonth.AddDays(day - firstDay);
                Button dayButton = new Button();
                int buttonSize = 30;
                List<Appointment> dayAppointments = GetSecondUserAppointments().FindAll(appointment => (appointment.StartDate.Date <= currentDate.Date && appointment.EndDate.Date >= currentDate.Date));
                if (dayAppointments.Count > 0)
                {
                    string tooltip = "Click to open all appointments for the day.";
                    dayButton.ToolTip = tooltip;
                    dayButton.Background = Brushes.Aquamarine;
                    dayButton.BorderThickness = new Thickness(0, 0, 0, 0);
                    dayButton.Content = dayAppointments.Count;
                    dayButton.Tag = dayAppointments;
                    dayButton.SetValue(Grid.RowProperty, week);
                    dayButton.SetValue(Grid.ColumnProperty, weekDay);
                    dayButton.Height = buttonSize;
                    dayButton.Width = buttonSize;
                    dayButton.Click += new RoutedEventHandler(GoToAppointmentsView);
                    MonthView.Children.Add(dayButton);
                }
                if (weekDay == sunday)
                {
                    week++;
                    weekDay = 0;
                }
                else
                {
                    weekDay++;
                }
                day++;
            }
        }

#pragma warning disable CA1822 // Mark members as static.
        public string UpdateTitle(DateTime date, string username)
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

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            MainWindow.SecondWindowIsOpen = false;
            secondWeekView.Close();
        }
        #endregion
    }
}
