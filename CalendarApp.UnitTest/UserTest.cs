using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace CalendarApp.UnitTests
{
    class UserTest
    {
        #region Fields
        private string username;
        private  string testUsersFileName;
        User user;
        #endregion

        #region Methods
        [SetUp]
        public void Setup()
        {
            username = "Test User";
            testUsersFileName = "Users.json";
            user = new User(username);
        }

        [Test]
        public void Save_SaveUser_AddsUsernameToJson()
        {
            // Arrange
            bool result = true;

            // Act
            user.Save();
            string jsonUsers = null;
            try
            {
                jsonUsers = File.ReadAllText(testUsersFileName);
            }
            catch (FileNotFoundException e)
            {
                Debug.Write(e);
            }

            if (jsonUsers != null)
            {
                var users = JsonConvert.DeserializeObject<List<string>>(jsonUsers);
                List<string> usersWithUsername = users.FindAll(user => user == username);
                int singleUser = 1;
                if (usersWithUsername.Count != singleUser)
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }

            // Assert
            Assert.IsTrue(result);
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(testUsersFileName);
        }
        #endregion
    }
}
