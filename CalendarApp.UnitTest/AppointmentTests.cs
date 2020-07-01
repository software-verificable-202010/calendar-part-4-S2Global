using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace CalendarApp.UnitTest
{
    public class AppointmentTests
    {
        #region Fields
        private static string testAppointmentFileName;
        private static string title;
        private static string description;
        private static string username;
        private static User creator;
        private Appointment appointment;
        private Appointment updatedAppointment;
        List<Appointment> allAppointments;
        #endregion

        #region Methods
        [SetUp]
        public void Setup()
        {
            testAppointmentFileName = "Appointments.json";
            title = "Test Appointment try2";
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
            string updatedTitle = "Updated Test Appointment";
            updatedAppointment = new Appointment(updatedTitle, description, startDate, endDate, creator, participants);
            MainWindow.SessionUser = creator;
        }

        [Test]
        public void SaveNewAppointment_AppointmentHasNoOverlap_AddAppointmentToJson()
        {
            // Arrange
            List<Appointment> allAppointments = null;

            // Act
            bool isSaved = appointment.SaveNewAppointment();
            bool isPopulated = false;
            string jsonAppointments = null;
            try
            {
                jsonAppointments = File.ReadAllText(testAppointmentFileName);
            }
            catch (FileNotFoundException e)
            {
                Debug.Write(e);
            }

            if (jsonAppointments != null)
            {
                allAppointments = JsonConvert.DeserializeObject<List<Appointment>>(jsonAppointments);
                isPopulated = true;
            }
            var apointments = allAppointments.Find(appointment => appointment.Title == title);
            bool isFound = apointments != null;
            bool result = isSaved && isFound && isPopulated;

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Update_ChangeAppointmentFields_UpdatesAppointment()
        {
            // Arrange
            string updatedDescription = "Updated appointment used for testing.";
            int updatedDuration = 3;
            DateTime updatedStartDate = DateTime.Now.AddHours(updatedDuration);
            DateTime updatedEndDate = updatedStartDate.AddHours(updatedDuration);
            string updatedUsername = "SecondTestUser";
            List<string> updatedParticipants = new List<string>()
            {
                username,
                updatedUsername
            };

            // Act
            updatedAppointment.Update(creator, updatedDescription, updatedStartDate, updatedEndDate, updatedParticipants);

            bool descriptionResult = appointment.Description != updatedAppointment.Description;
            bool startDateResult = appointment.StartDate != updatedAppointment.StartDate;
            bool endDateResult = appointment.EndDate != updatedAppointment.EndDate;
            bool participantsResult = appointment.Participants != updatedAppointment.Participants;
            bool result = descriptionResult && startDateResult && endDateResult && participantsResult;

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void SaveUpdatedAppointment_SaveNewParameters_AddAppointmentToJson()
        {
            // Arrange
            string updatedDescription = "Updated appointment used for testing.";
            int updatedDuration = 3;
            DateTime updatedStartDate = DateTime.Now.AddHours(updatedDuration);
            DateTime updatedEndDate = updatedStartDate.AddHours(updatedDuration);
            string updatedUsername = "SecondTestUser";
            List<string> updatedParticipants = new List<string>()
            {
                username,
                updatedUsername
            };
            updatedAppointment.Update(creator, updatedDescription, updatedStartDate, updatedEndDate, updatedParticipants);
            bool isSaved = updatedAppointment.SaveUpdatedAppointment();
            bool isPopulated = false;

            // Act
            string jsonAppointments = null;
            try
            {
                jsonAppointments = File.ReadAllText(testAppointmentFileName);
            }
            catch (FileNotFoundException e)
            {
                Debug.Write(e);
            }

            if (jsonAppointments != null)
            {
                allAppointments = JsonConvert.DeserializeObject<List<Appointment>>(jsonAppointments);
                isPopulated = true;
            }
            var apointments = allAppointments.Find(appointment => appointment.Title == updatedAppointment.Title);
            bool isFound = apointments != null;
            bool result = isSaved && isFound && isPopulated;

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void HasOverlap_StartDateAfterEndDate_ReturnTrue()
        {
            // Arrange
            int dayOffset = 2;
            updatedAppointment.StartDate = DateTime.Now.AddDays(dayOffset);
            updatedAppointment.EndDate = DateTime.Now;
            allAppointments = new List<Appointment>();
            // Act
            bool result = updatedAppointment.HasOverlap(allAppointments);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void HasOverlap_OverlappingDate_ReturnTrue()
        {
            // Arrange
            int dayOffset = 2;
            updatedAppointment.StartDate = DateTime.Now;
            updatedAppointment.EndDate = DateTime.Now.AddDays(dayOffset);
            appointment.StartDate = DateTime.Now;
            appointment.EndDate = DateTime.Now.AddDays(dayOffset);
            allAppointments = new List<Appointment>()
            {
                appointment
            };
            // Act
            bool result = updatedAppointment.HasOverlap(allAppointments);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Delete_DeleteAppointment_RemoveFromJson()
        {
            // Arrange
            List<Appointment> allAppointments = null;

            // Act
            appointment.SaveNewAppointment();
            updatedAppointment.SaveNewAppointment();
            appointment.Delete();
            string jsonAppointments = null;
            try
            {
                jsonAppointments = File.ReadAllText(testAppointmentFileName);
            }
            catch (FileNotFoundException e)
            {
                Debug.Write(e);
            }

            if (jsonAppointments != null)
            {
                allAppointments = JsonConvert.DeserializeObject<List<Appointment>>(jsonAppointments);
            }
            var apointments = allAppointments.Find(appointment => appointment.Title == title);
            bool result = apointments == null;

            // Assert
            Assert.IsTrue(result);
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(testAppointmentFileName);
        }
        #endregion
    }
}