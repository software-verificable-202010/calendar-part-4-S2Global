using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace CalendarApp.UnitTests
{
    public class UserTests
    {
        #region Fields
        private string username;
        #endregion

        #region Methods
        [Test]
        public void UserValidConstructor()
        {
            username = "Test User";
            
            User user = new User(username);

            Assert.IsNotNull(user);
        }
        #endregion
    }
}