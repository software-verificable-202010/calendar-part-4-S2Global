using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;

namespace CalendarApp
{

    public class Appointment
    {
        #region Fields
        private readonly string appointmentsFileName = "Appointments.json";
        #endregion

        #region Properties
        public User Creator { get; set; }
        public List<string> Participants { get; private set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        #endregion

        #region Methods
        [JsonConstructor]
        public Appointment(string title, string description, DateTime startDate, DateTime endDate, User creator, List<string> participants)
        {
            this.Title = title;
            this.Description = description;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Creator = creator;
            if(creator != null && participants != null)
            {
                participants.Add(creator.Username);
            }
            this.Participants = participants;
        }

        public bool SaveNewAppointment()
        {
            string jsonAppointmentsToAdd = null;
            try
            {
                jsonAppointmentsToAdd = File.ReadAllText(appointmentsFileName);
            }
            catch (FileNotFoundException e)
            {
                Debug.Write(e);
            }

            if (jsonAppointmentsToAdd != null)
            {
                List<Appointment> appointments = JsonConvert.DeserializeObject<List<Appointment>>(jsonAppointmentsToAdd);
                if (appointments.Find(appointment => appointment.Title == this.Title) == null && !HasOverlap(appointments))
                {
                    appointments.Add(this);
                    var convertedJson = JsonConvert.SerializeObject(appointments, Formatting.Indented);
                    File.WriteAllText(appointmentsFileName, convertedJson);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                List<Appointment> appointments = new List<Appointment> { this };
                var convertedJson = JsonConvert.SerializeObject(appointments, Formatting.Indented);
                File.WriteAllText(appointmentsFileName, convertedJson);
            }
            return true;
        }

        public void Update(User user, string description, DateTime startDate, DateTime endDate, List<string> participants)
        {
            if (user != null && user.Username == Creator.Username)
            {
                string jsonAppointmentsToRemove = null;
                try
                {
                    jsonAppointmentsToRemove = File.ReadAllText(appointmentsFileName);
                }
                catch (FileNotFoundException e)
                {
                    Debug.Write(e);
                }
                if (jsonAppointmentsToRemove != null)
                {
                    List<Appointment> appointments = JsonConvert.DeserializeObject<List<Appointment>>(jsonAppointmentsToRemove);
                    Appointment appointmentToRemove = appointments.Find(appointment => appointment.Title == this.Title);
                    appointments.Remove(appointmentToRemove);
                    var convertedJson = JsonConvert.SerializeObject(appointments, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(appointmentsFileName, convertedJson);
                }
                this.Description = description;
                this.StartDate = startDate;
                this.EndDate = endDate;
                this.Participants = participants;
            }
            else
            {
                const string messageBoxText = "Not Creator!";
                MessageBox.Show(messageBoxText);
            }
        }

        public bool SaveUpdatedAppointment()
        {
            string jsonAppointmentsToAdd = null;
            try
            {
                jsonAppointmentsToAdd = File.ReadAllText(appointmentsFileName);
            }
            catch (FileNotFoundException e)
            {
                Debug.Write(e);
            }

            if (jsonAppointmentsToAdd != null)
            {
                List<Appointment> appointments = JsonConvert.DeserializeObject<List<Appointment>>(jsonAppointmentsToAdd);
                if (!HasOverlap(appointments))
                {
                    appointments.Add(this);
                    var convertedJson = JsonConvert.SerializeObject(appointments, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(appointmentsFileName, convertedJson);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                List<Appointment> appointments = new List<Appointment> { this };
                var convertedJson = JsonConvert.SerializeObject(appointments, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(appointmentsFileName, convertedJson);
            }
            return true;
        }

        public bool HasOverlap(List<Appointment> appointments)
        {
            if (this.StartDate > this.EndDate)
            {
                return true;
            }
            if (appointments != null && appointments.Find(appointment => appointment.Title == this.Title) != null)
            {
                return true;
            }
            foreach (string participant in this.Participants)
            {
                IEnumerable<Appointment> overlappingAppointments = appointments.Where(appointment => (appointment.Participants.Contains(participant) && (((appointment.StartDate >= this.StartDate) && (appointment.StartDate <= this.EndDate)) || ((appointment.EndDate >= this.StartDate) && (appointment.EndDate <= this.EndDate)))));
                if (overlappingAppointments.Any())
                {
                    return true;
                }
            }
            return false;
        }

        public void Delete()
        {
            if (MainWindow.SessionUser.Username == Creator.Username)
            {
                string jsonAppointmentsToRemove = null;
                try
                {
                    jsonAppointmentsToRemove = File.ReadAllText(appointmentsFileName);
                }
                catch (FileNotFoundException e)
                {
                    Debug.Write(e);
                }
                if (jsonAppointmentsToRemove != null)
                {
                    List<Appointment> appointments = JsonConvert.DeserializeObject<List<Appointment>>(jsonAppointmentsToRemove);
                    Appointment appointmentToRemove = appointments.Find(appointment => appointment.Title == this.Title);
                    appointments.Remove(appointmentToRemove);
                    var convertedJson = JsonConvert.SerializeObject(appointments, Formatting.Indented);
                    File.WriteAllText(appointmentsFileName, convertedJson);
                }
            }
            else
            {
                const string messageBoxText = "Not Creator!";
                MessageBox.Show(messageBoxText);
            }
        }
        #endregion
    }
}