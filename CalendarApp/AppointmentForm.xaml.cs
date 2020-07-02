using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CalendarApp
{
    /// <summary>
    /// Interaction logic for AppointmentForm.xaml
    /// </summary>
    public partial class AppointmentForm : Window
    {
        #region Methods
        public AppointmentForm(bool isUpdate, Appointment appointment)
        {
            InitializeComponent();
            if (isUpdate && appointment != null)
            {
                titleBox.Text = appointment.Title;
                titleBox.IsReadOnly = true;
                titleBox.IsEnabled = false;
                string tooltip = "Cannot change title.";
                titleBox.ToolTip = tooltip;
                descriptionBox.Text = appointment.Description;
                startDateBox.Value = appointment.StartDate;
                endDateBox.Value = appointment.EndDate;
                participantBox.Text = string.Join(" ", appointment.Participants);
                submitButton.Tag = appointment;
                submitButton.Click += new RoutedEventHandler(UpdateAppointment);
                deleteButton.Tag = appointment;
                deleteButton.Click += new RoutedEventHandler(DeleteAppointment);
            }
            else
            {
                submitButton.Tag = null;
                submitButton.Click += new RoutedEventHandler(CreateAppointment);
                deleteButton.IsEnabled = false;
            }
        }

        private void CreateAppointment(object sender, RoutedEventArgs e)
        {
            bool titleExists = titleBox.Text.Length > 0;
            bool descriptionExists = descriptionBox.Text.Length > 0;
            bool startDateExists = startDateBox.Value.HasValue;
            bool endDateExists = endDateBox.Value.HasValue;

            if (titleExists && descriptionExists && startDateExists && endDateExists)
            {
                List<string> participants = participantBox.Text.Split().ToList();
                Appointment appointment = new Appointment(titleBox.Text, descriptionBox.Text, (DateTime)startDateBox.Value, (DateTime)endDateBox.Value, MainWindow.SessionUser, participants);
                if (appointment.SaveNewAppointment())
                {
                    this.Close();
                }
                else
                {
                    const string messageBoxText = "Appointment overlap!";
                    MessageBox.Show(messageBoxText);
                }
            }
            else
            {
                const string messageBoxText = "Data Missing!";
                MessageBox.Show(messageBoxText);
            }
        }

        private void UpdateAppointment(object sender, RoutedEventArgs e)
        {
            bool titleExists = titleBox.Text.Length > 0;
            bool descriptionExists = descriptionBox.Text.Length > 0;
            bool startDateExists = startDateBox.Value.HasValue;
            bool endDateExists = endDateBox.Value.HasValue;
            bool participantExists = participantBox.Text.Length > 0;

            if (titleExists && descriptionExists && startDateExists && endDateExists && participantExists)
            {
                List<string> participants = participantBox.Text.Split().ToList();
                Appointment appointment = MainWindow.GetSessionUserAppointments().Find(tokenAppointment => tokenAppointment.Title == titleBox.Text);
                appointment.Delete();
                appointment.Update(MainWindow.SessionUser, descriptionBox.Text, (DateTime)startDateBox.Value, (DateTime)endDateBox.Value, participants);
                appointment.SaveUpdatedAppointment();
                this.Close();
            }
            else
            {
                const string messageBoxText = "Data Missing!";
                MessageBox.Show(messageBoxText);
            }
        }

        private void DeleteAppointment(object sender, RoutedEventArgs e)
        {
            const string messageBoxText = "Are You Sure?";
            MessageBoxResult messageBoxResult = MessageBox.Show(messageBoxText, Properties.Resources.AppointmentCaption, MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes && sender != null)
            {
                var button = sender as Button;
                Appointment appointment = button.Tag as Appointment;
                appointment.Delete();
                this.Close();
            }
        }
        #endregion
    }
}
