﻿using Moq;
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
    class SecondWeekWindowTests
    {
        #region Fields
        private SecondWeekWindow secondWeekWindow;
        private string username;
        #endregion

        #region Methods
        [SetUp]
        public void Setup()
        {
            username = "Test Username";
            secondWeekWindow = new SecondWeekWindow(username);
        }

        [Test, Apartment(ApartmentState.STA)]
        public void UpdateTitle_TitleChanges()
        {
            // Arrange
            StringBuilder title = new StringBuilder();
            string separator = " - ";
            string userNameField = "Viewing: ";
            title.Append(new DateTime(2020, 2, 2).Month.ToString(CultureInfo.InvariantCulture));
            title.Append(separator);
            title.Append(new DateTime(2020, 2, 2).Year);
            title.Append(separator);
            title.Append(userNameField);
            title.Append(username);

            // Act
            string mainTitle = secondWeekWindow.UpdateTitle(new DateTime(2020, 2, 2));

            // Assert
            Assert.AreEqual(mainTitle, title.ToString());
        }
        #endregion
    }
}