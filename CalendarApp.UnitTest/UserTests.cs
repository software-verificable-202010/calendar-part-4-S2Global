using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace CalendarApp.UnitTests
{
    class UserTests
    {
        #region Fields
        private string username;
        #endregion

        #region Methods
        [Test]
        public void User_ValidConstructor()
        {
            // Arrange
            username = "Test User";
            
            // Act
            User user = new User(username);

            // Assert
            Assert.IsNotNull(user);
        }
        #endregion
    }
}