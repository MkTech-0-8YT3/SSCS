using Common.IcsImporter;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonTests
{
    [TestClass]
    public class CalendarMapperTests
    {
        private static readonly string EXPECTED_TEST_EVENTS_DESCRIPTION = "test123";
        [TestMethod]
        public void CAN_READ_ICS_FILE()
        {
            var objectUnderTest = new CalendarMapper();
            var result = objectUnderTest.ReadEventsFromCalendarFile(CommonMethods.GetFilePathFromTestResources("test.ics"));
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void CAN_PARSE_NAMES()
        {
            var objectUnderTest = new CalendarMapper();
            var result = objectUnderTest.ReadEventsFromCalendarFile(CommonMethods.GetFilePathFromTestResources("test.ics"));
            foreach(var x in result)
            {
                Assert.IsNotNull(x);
                Assert.AreEqual(EXPECTED_TEST_EVENTS_DESCRIPTION, x.Description);
            }
        }
    }
}
