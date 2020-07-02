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
            DateTime startDate = new DateTime(2020, 2, 2);
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
        public void AppointmentCausesValidConstrucor()
        {
            int duration = 2;
            DateTime startDate = new DateTime(2020, 2, 2);
            DateTime endDate = startDate.AddHours(duration);
            List<string> participants = new List<string>()
            {
                creator
            };

            appointment = new Appointment(title, description, startDate, endDate, creator, participants);

            Assert.IsNotNull(appointment);
        }

        [Test]
        public void UpdateCausesChangeAppointmentFields()
        {
            string updatedDescription = "Updated appointment used for testing.";
            int updatedDuration = 3;
            DateTime updatedStartDate = new DateTime(2020, 2, 2).AddHours(updatedDuration);
            DateTime updatedEndDate = updatedStartDate.AddHours(updatedDuration);
            string updatedUsername = "SecondTestUser";
            List<string> updatedParticipants = new List<string>()
            {
                updatedUsername
            };

            updatedAppointment.Update(creator, updatedDescription, updatedStartDate, updatedEndDate, updatedParticipants);

            bool descriptionResult = appointment.Description != updatedAppointment.Description;
            bool startDateResult = appointment.StartDate != updatedAppointment.StartDate;
            bool endDateResult = appointment.EndDate != updatedAppointment.EndDate;
            bool participantsResult = appointment.Participants != updatedAppointment.Participants;
            bool result = descriptionResult && startDateResult && endDateResult && participantsResult;

            Assert.IsTrue(result);
        }

        [Test]
        public void HasOverlapForStartDateAfterEndDate()
        {
            // Arrange
            int dayOffset = 2;
            updatedAppointment.StartDate = new DateTime(2020, 2, 2).AddDays(dayOffset);
            updatedAppointment.EndDate = new DateTime(2020, 2, 2);
            allAppointments = new List<Appointment>();

            bool result = updatedAppointment.HasOverlap(allAppointments);

            Assert.IsTrue(result);
        }

        [Test]
        public void HasOverlapForOverlappingDate()
        {
            int dayOffset = 2;
            updatedAppointment.StartDate = new DateTime(2020, 2, 2);
            updatedAppointment.EndDate = new DateTime(2020, 2, 2).AddDays(dayOffset);
            appointment.StartDate = new DateTime(2020, 2, 2);
            appointment.EndDate = new DateTime(2020, 2, 2).AddDays(dayOffset);
            allAppointments = new List<Appointment>()
            {
                appointment
            };

            bool result = updatedAppointment.HasOverlap(allAppointments);

            Assert.IsTrue(result);
        }
        #endregion
    }
}