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
    class SecondWindowTests
    {
        #region Fields
        SecondWindow secondWindow;
        string username;
        #endregion

        #region Methods
        [SetUp]
        public void Setup()
        {
            username = "Test Username";
            secondWindow = new SecondWindow(username);
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
            string mainTitle = secondWindow.UpdateTitle(new DateTime(2020, 2, 2), username);

            // Assert
            Assert.AreEqual(mainTitle, title.ToString());
        }

        [Test, Apartment(ApartmentState.STA)]
        public void UpdateWeekendRectangle_UpdatesParameters()
        {
            // Arrange
            int weekendRowProperty = 0;
            int weekendColumnProperty = 5;
            int weekendRowSpanProperty = 6;
            int weekendColumnSpanProperty = 2;
            bool equalRow;
            bool equalColumn;
            bool equalRowSpan;
            bool equalColumnSpan;

            // Act
            Rectangle rectangle = secondWindow.UpdateWeekendRectangle();
            equalRow = rectangle.GetValue(Grid.RowProperty).Equals(weekendRowProperty);
            equalColumn = rectangle.GetValue(Grid.ColumnProperty).Equals(weekendColumnProperty);
            equalRowSpan = rectangle.GetValue(Grid.RowSpanProperty).Equals(weekendRowSpanProperty);
            equalColumnSpan = rectangle.GetValue(Grid.ColumnSpanProperty).Equals(weekendColumnSpanProperty);
            bool result = equalRow && equalColumn && equalRowSpan && equalColumnSpan;

            // Assert
            Assert.IsTrue(result);
        }
        #endregion
    }
}
