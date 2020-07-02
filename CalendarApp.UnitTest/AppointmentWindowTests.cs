using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Controls;

namespace CalendarApp.UnitTests
{
    class AppointmentWindowTests
    {
        #region Fields
        private static string title;
        private static string description;
        private static string creator;
        private Mock<Appointment> appointment;
        private AppointmentWindow appointmentWindow;
        private List<TextBlock> appointmentParameters;
        private readonly int titlePosition = 0;
        private readonly int creatorPosition = 1;
        private readonly int startDatePosition = 2;
        private readonly int endDatePosition = 3;
        private readonly int participantsPosition = 4;
        private readonly int descriptionPosition = 5;
        #endregion

        #region Methods
        [SetUp]
        public void Setup()
        {
            
            title = "Test Appointment";
            description = "Appointment used for testing.";
            creator = "TestUser";
            int duration = 2;
            DateTime startDate = new DateTime(2020, 2, 2);
            DateTime endDate = startDate.AddHours(duration);
            List<string> participants = new List<string>()
            {
                creator
            };
            appointment = new Mock<Appointment>(title, description, startDate, endDate, creator, participants);
            appointmentWindow = new AppointmentWindow();
            appointmentParameters = new List<TextBlock>()
                {
                    new TextBlock(),
                    new TextBlock(),
                    new TextBlock(),
                    new TextBlock(),
                    new TextBlock(),
                    new TextBlock()
                };
        }

        [Test, Apartment(ApartmentState.STA)]
        public void UpdateText_ChangeText_UpdatesTextBoxes()
        {
            // Arrange
            bool equalTitle;
            bool equalCreator;
            bool equalStartDate;
            bool equalEndDate;
            bool equalParticipants;
            bool equalDescription;

            // Act
            appointmentWindow.UpdateText(appointmentParameters, appointment.Object);
            equalTitle = appointmentParameters[titlePosition].Text == appointment.Object.Title;
            equalCreator = appointmentParameters[creatorPosition].Text == appointment.Object.Creator;
            equalStartDate = appointmentParameters[startDatePosition].Text == appointment.Object.StartDate.ToString(CultureInfo.InvariantCulture);
            equalEndDate = appointmentParameters[endDatePosition].Text == appointment.Object.EndDate.ToString(CultureInfo.InvariantCulture);
            StringBuilder participantText = new StringBuilder();
            foreach (string participant in appointment.Object.Participants)
            {
                string separator = " ";
                participantText.Append(separator);
                participantText.Append(participant);
            }
            equalParticipants = appointmentParameters[participantsPosition].Text == participantText.ToString();
            equalDescription = appointmentParameters[descriptionPosition].Text == appointment.Object.Description;
            bool result = equalTitle && equalCreator && equalStartDate && equalEndDate && equalParticipants && equalDescription;

            // Assert
            Assert.IsTrue(result);
        }

        [Test, Apartment(ApartmentState.STA)]
        public void UpdateFontSize_ChangeFontSize()
        {
            // Arrange
            int fontSize = 16;
            bool titleFont;
            bool creatorFont;
            bool startDateFont;
            bool endDateFont;
            bool participantsFont;
            bool descriptionFont;

            // Act
            appointmentWindow.UpdateFontSize(appointmentParameters);
            titleFont = appointmentParameters[titlePosition].FontSize == fontSize;
            creatorFont = appointmentParameters[creatorPosition].FontSize == fontSize;
            startDateFont = appointmentParameters[startDatePosition].FontSize == fontSize;
            endDateFont = appointmentParameters[endDatePosition].FontSize == fontSize;
            participantsFont = appointmentParameters[participantsPosition].FontSize == fontSize;
            descriptionFont = appointmentParameters[descriptionPosition].FontSize == fontSize;
            bool result = titleFont && creatorFont && startDateFont && endDateFont && participantsFont && descriptionFont;

            // Assert
            Assert.IsTrue(result);
        }

        [Test, Apartment(ApartmentState.STA)]
        public void UpdateRowPosition_ChangeRows()
        {
            // Arrange
            int titleRowPosition = 0;
            int creatorRowPosition = 1;
            int startDateRowPosition = 2;
            int endDateRowPosition = 3;
            int participantsRowPosition = 4;
            int descriptionRowPosition = 5;
            bool titleRow;
            bool creatorRow;
            bool startDateRow;
            bool endDateRow;
            bool participantsRow;
            bool descriptionRow;

            // Act
            appointmentWindow.UpdateRowPosition(appointmentParameters);
            titleRow = appointmentParameters[titlePosition].GetValue(Grid.RowProperty).Equals(titleRowPosition);
            creatorRow = appointmentParameters[creatorPosition].GetValue(Grid.RowProperty).Equals(creatorRowPosition);
            startDateRow = appointmentParameters[startDatePosition].GetValue(Grid.RowProperty).Equals(startDateRowPosition);
            endDateRow = appointmentParameters[endDatePosition].GetValue(Grid.RowProperty).Equals(endDateRowPosition);
            participantsRow = appointmentParameters[participantsPosition].GetValue(Grid.RowProperty).Equals(participantsRowPosition);
            descriptionRow = appointmentParameters[descriptionPosition].GetValue(Grid.RowProperty).Equals(descriptionRowPosition);
            bool result = titleRow && creatorRow && startDateRow && endDateRow && participantsRow && descriptionRow;

            // Assert
            Assert.IsTrue(result);
        }

        [Test, Apartment(ApartmentState.STA)]
        public void UpdateColumnPosition_ChangeColumns()
        {
            // Arrange
            int columnPosition = 1;
            bool titleColumn;
            bool creatorColumn;
            bool startDateColumn;
            bool endDateColumn;
            bool participantsColumn;
            bool descriptionColumn;

            // Act
            appointmentWindow.UpdateColumnPosition(appointmentParameters);
            titleColumn = appointmentParameters[titlePosition].GetValue(Grid.ColumnProperty).Equals(columnPosition);
            creatorColumn = appointmentParameters[creatorPosition].GetValue(Grid.ColumnProperty).Equals(columnPosition);
            startDateColumn = appointmentParameters[startDatePosition].GetValue(Grid.ColumnProperty).Equals(columnPosition);
            endDateColumn = appointmentParameters[endDatePosition].GetValue(Grid.ColumnProperty).Equals(columnPosition);
            participantsColumn = appointmentParameters[participantsPosition].GetValue(Grid.ColumnProperty).Equals(columnPosition);
            descriptionColumn = appointmentParameters[descriptionPosition].GetValue(Grid.ColumnProperty).Equals(columnPosition);
            bool result = titleColumn && creatorColumn && startDateColumn && endDateColumn && participantsColumn && descriptionColumn;

            // Assert
            Assert.IsTrue(result);
        }
        #endregion
    }
}
