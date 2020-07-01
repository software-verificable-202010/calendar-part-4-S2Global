using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CalendarApp
{
    /// <summary>
    /// Interaction logic for AppointmentWindow.xaml
    /// </summary>
    public partial class AppointmentWindow : Window
    {
        #region Methods
        public AppointmentWindow(Appointment appointment)
        {
            InitializeComponent();
            UpdateAppointmentView(appointment);
        }

        public void UpdateAppointmentView(Appointment appointment)
        {
            if(appointment != null)
            {
                List<TextBlock> appointmentParameters = new List<TextBlock>()
                {
                    new TextBlock(),
                    new TextBlock(),
                    new TextBlock(),
                    new TextBlock(),
                    new TextBlock(),
                    new TextBlock()
                };

                UpdateText(appointmentParameters, appointment);
                UpdateFontSize(appointmentParameters);
                UpdateRowPosition(appointmentParameters);
                UpdateColumnPosition(appointmentParameters);

                foreach (var parameter in appointmentParameters)
                {
                    AppointmentView.Children.Add(parameter);
                }

                editButton.Tag = appointment;
            }
        }

        public void UpdateText(List<TextBlock> appointmentParameters, Appointment appointment)
        {
            if (appointmentParameters != null && appointment != null)
            {
                int titlePosition = 0;
                int creatorPosition = 1;
                int startDatePosition = 2;
                int endDatePosition = 3;
                int participantsPosition = 4;
                int descriptionPosition = 5;
                StringBuilder participantText = new StringBuilder();
                appointmentParameters[titlePosition].Text = appointment.Title;
                appointmentParameters[creatorPosition].Text = appointment.Creator.Username;
                appointmentParameters[startDatePosition].Text = appointment.StartDate.ToString(CultureInfo.InvariantCulture);
                appointmentParameters[endDatePosition].Text = appointment.EndDate.ToString(CultureInfo.InvariantCulture);
                foreach (string participant in appointment.Participants)
                {
                    string separator = " ";
                    participantText.Append(separator);
                    participantText.Append(participant);
                }
                appointmentParameters[participantsPosition].Text = participantText.ToString();
                appointmentParameters[descriptionPosition].Text = appointment.Description;
            }
        }

        public static void UpdateFontSize(List<TextBlock> appointmentParameters)
        {
            if (appointmentParameters != null)
            {
                foreach(var parameter in appointmentParameters)
                {
                    parameter.FontSize = 16;
                }
            }
        }

        public static void UpdateRowPosition(List<TextBlock> appointmentParameters)
        {
            if(appointmentParameters != null)
            {
                int titleRowPosition = 0;
                int creatorRowPosition = 1;
                int startDateRowPosition = 2;
                int endDateRowPosition = 3;
                int participantsRowPosition = 4;
                int descriptionRowPosition = 5;

                appointmentParameters[0].SetValue(Grid.RowProperty, titleRowPosition);
                appointmentParameters[1].SetValue(Grid.RowProperty, creatorRowPosition);
                appointmentParameters[2].SetValue(Grid.RowProperty, startDateRowPosition);
                appointmentParameters[3].SetValue(Grid.RowProperty, endDateRowPosition);
                appointmentParameters[4].SetValue(Grid.RowProperty, participantsRowPosition);
                appointmentParameters[5].SetValue(Grid.RowProperty, descriptionRowPosition);
            }  
        }

        public static void UpdateColumnPosition(List<TextBlock> appointmentParameters)
        {
            if (appointmentParameters != null)
            {
                int columnPosition = 1;
                foreach (var parameter in appointmentParameters)
                {
                    parameter.SetValue(Grid.ColumnProperty, columnPosition);
                }
            }
        }

        public void GoToAppointmentForm(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                var button = sender as Button;
                Appointment appointment = button.Tag as Appointment;
                var AppointmentFormView = new AppointmentForm(true, appointment);
                AppointmentFormView.Show();
            }
        }
        #endregion
    }
}
