using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.RightsManagement;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace CalendarApp
{
    public class User
    {
        #region Fields
        private readonly string usersFileName = "Users.json";
        #endregion

        #region Properties
        public string Username { get; set; }
        #endregion

        #region Methods
        public User(string username)
        {
            this.Username = username;
        }

        public void Save()
        {
            string jsonUserToAdd = null;
            try
            {
                jsonUserToAdd = File.ReadAllText(usersFileName);
            }
            catch (FileNotFoundException e)
            {
                Debug.Write(e);
            }

            if (jsonUserToAdd != null)
            {
                var users = JsonConvert.DeserializeObject<List<string>>(jsonUserToAdd);
                if (!users.Contains(this.Username))
                {
                    users.Add(this.Username);
                    var convertedJson = JsonConvert.SerializeObject(users, Formatting.Indented);
                    File.WriteAllText(usersFileName, convertedJson);
                }
            }
            else
            {
                List<string> users = new List<string> { this.Username };
                var convertedJson = JsonConvert.SerializeObject(users, Formatting.Indented);
                File.WriteAllText(usersFileName, convertedJson);
            }
        }
        #endregion
    }
}