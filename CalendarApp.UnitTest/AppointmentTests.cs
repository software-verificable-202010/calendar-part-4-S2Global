using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace CalendarApp.UnitTests
{
    public class AppointmentTests
    {
        #region Fields
        private static string title;
        private static string description;
        private static string creator;
        private Appointment appointment;
        private Appointment updatedAppointment;
        List<Appointment> allAppointments;
        #endregion

        #region Methods
        [SetUp]
        public void Setup()
        {
            title = "Test Appointment";
            description = "Appointment used for testing.";
            creator = "TestUser";
            int duration = 2;
            DateTime startDate = DateTime.Now;
            DateTime endDate = startDate.AddHours(duration);
            List<string> participants = new List<string>()
            {
                creator
            };
            appointment = new Appointment(title, description, startDate, endDate, creator, participants);
            string updatedTitle = "Updated Test Appointment";
            updatedAppointment = new Appointment(updatedTitle, description, startDate, endDate, creator, participants);
        }

        [Test]
        public void Appointment_ValidConstrucor()
        {
            // Arrange
            int duration = 2;
            DateTime startDate = DateTime.Now;
            DateTime endDate = startDate.AddHours(duration);
            List<string> participants = new List<string>()
            {
                creator
            };

            // Act
            appointment = new Appointment(title, description, startDate, endDate, creator, participants);

            // Assert
            Assert.IsNotNull(appointment);
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
        #endregion
    }
}