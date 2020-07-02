using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CalendarApp.UnitTests
{
    public class WeekWindowTests
    {
        #region Fields
        private WeekWindow weekWindow;
        private string username;
        #endregion

        #region Methods
        [SetUp]
        public void Setup()
        {
            username = "Test Username";
            weekWindow = new WeekWindow(username);
        }

        [Test, Apartment(ApartmentState.STA)]
        public void UpdateTitleCausesTitleChanges()
        {
            StringBuilder title = new StringBuilder();
            string separator = " - ";
            string userNameField = "User: ";
            title.Append(new DateTime(2020, 2, 2).Month.ToString(CultureInfo.InvariantCulture));
            title.Append(separator);
            title.Append(new DateTime(2020, 2, 2).Year);
            title.Append(separator);
            title.Append(userNameField);
            title.Append(username);

            string mainTitle = weekWindow.UpdateTitle(new DateTime(2020, 2, 2));

            Assert.AreEqual(mainTitle, title.ToString());
        }
        #endregion
    }
}