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
    public class MainWindowTests
    {
        #region Fields
        MainWindow mainWindow;
        string username;
        #endregion

        #region Methods
        [SetUp]
        public void Setup()
        {
            bool noSecondWindow = false;
            username = "Test Username";
            mainWindow = new MainWindow(username, noSecondWindow);
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

            string mainTitle = mainWindow.UpdateTitle(new DateTime(2020,2,2), username);

            Assert.AreEqual(mainTitle, title.ToString());
        }

        [Test, Apartment(ApartmentState.STA)]
        public void UpdateWeekendRectangleCausesUpdatesParameters()
        {
            int weekendRowProperty = 0;
            int weekendColumnProperty = 5;
            int weekendRowSpanProperty = 6;
            int weekendColumnSpanProperty = 2;
            bool equalRow;
            bool equalColumn;
            bool equalRowSpan;
            bool equalColumnSpan;

            Rectangle rectangle = mainWindow.UpdateWeekendRectangle();
            equalRow = rectangle.GetValue(Grid.RowProperty).Equals(weekendRowProperty);
            equalColumn = rectangle.GetValue(Grid.ColumnProperty).Equals(weekendColumnProperty);
            equalRowSpan = rectangle.GetValue(Grid.RowSpanProperty).Equals(weekendRowSpanProperty);
            equalColumnSpan = rectangle.GetValue(Grid.ColumnSpanProperty).Equals(weekendColumnSpanProperty);
            bool result = equalRow && equalColumn && equalRowSpan && equalColumnSpan;

            Assert.IsTrue(result);
        }
        #endregion
    }
}