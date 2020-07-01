using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Controls;

namespace CalendarApp.UnitTests
{
    class AppointmentWindowTest
    {
        #region Fields
        private static string title;
        private static string description;
        private static string username;
        private static User creator;
        private Appointment appointment;
        AppointmentWindow appointmentWindow;
        #endregion

        #region Methods
        [SetUp]
        public void Setup()
        {
            
            title = "Test Appointment";
            description = "Appointment used for testing.";
            username = "TestUser";
            creator = new User(username);
            int duration = 2;
            DateTime startDate = DateTime.Now;
            DateTime endDate = startDate.AddHours(duration);
            List<string> participants = new List<string>()
            {
                username
            };
            appointment = new Appointment(title, description, startDate, endDate, creator, participants);
        }

        [Test, Apartment(ApartmentState.STA)]
        public void UpdateText_ChangeText_UpdatesTextBoxes()
        {
            // Arrange
            bool equalTitle;
            bool equalCreator;
            bool equalStartDate;
            bool equalEndGDate;
            bool equalParticipants;
            bool equalDescription;
            bool result;
            int titlePosition = 0;
            int creatorPosition = 1;
            int startDatePosition = 2;
            int endDatePosition = 3;
            int participantsPosition = 4;
            int descriptionPosition = 5;
            List<TextBlock> appointmentParameters = new List<TextBlock>()
                {
                    new TextBlock(),
                    new TextBlock(),
                    new TextBlock(),
                    new TextBlock(),
                    new TextBlock(),
                    new TextBlock()
                };

            // Act
            appointmentWindow = new AppointmentWindow(appointment);
            appointmentWindow.UpdateText(appointmentParameters, appointment);
            StringBuilder participantText = new StringBuilder();
            equalTitle = appointmentParameters[titlePosition].Text == appointment.Title;
            equalCreator = appointmentParameters[creatorPosition].Text == appointment.Creator.Username;
            equalStartDate = appointmentParameters[startDatePosition].Text == appointment.StartDate.ToString(CultureInfo.InvariantCulture);
            equalEndGDate = appointmentParameters[endDatePosition].Text == appointment.EndDate.ToString(CultureInfo.InvariantCulture);
            foreach (string participant in appointment.Participants)
            {
                string separator = " ";
                participantText.Append(separator);
                participantText.Append(participant);
            }
            equalParticipants = appointmentParameters[participantsPosition].Text == participantText.ToString();
            equalDescription = appointmentParameters[descriptionPosition].Text == appointment.Description;
            result = equalTitle && equalCreator && equalStartDate && equalEndGDate && equalParticipants && equalDescription;

            // Assert
            Assert.IsTrue(result);

        }
        #endregion
    }
}
