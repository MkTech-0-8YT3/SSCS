using Common.DataModels.DatabaseModels;
using Common.DataModels.Descriptors;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common.IcsImporter
{
    public class CalendarMapper
    {
        public List<ClassEvent> ReadEventsFromCalendarFile(string icsFilePath)
        {
            var calendar = Calendar.Load(File.ReadAllText(@icsFilePath));
            List<ClassEvent> classEvents = new List<ClassEvent>();
            foreach(var ev in calendar.Children)
            {
                var calEvent = (CalendarEvent)ev;
                classEvents.Add(new ClassEvent() { 
                    Description = calEvent.Summary,
                    StartDate = calEvent.Start.AsUtc.AddHours(1),
                    EndDate = calEvent.End.AsUtc.AddHours(1),
                });
            }
            return classEvents;
        }

        /*
         * Example implementation of mapper - shitty code, needs to be refactored if someone would use mapping from calendar files
         * 
            public Schedule MapCalendarByFileName(string filename)
            {
                //Example fileName: Inf_S_Ist_6sem_2gr.ics
                var filenameParts = filename.Split("_");
                //first elem = major
                var major = MatchMajor(filenameParts[0]);
                //second elem = type of studies
                var typeOfStudies = MatchTypeOfStudies(filenameParts[1]);
                //third might be st or sem
                var degreeLevel = 0;
                var semester = 0;
                var group = "";
                if (Enum.TryParse(filenameParts[2], out Level level))
                {
                    degreeLevel = level switch
                    {
                        Level.Ist => 1,
                        Level.IIst => 2,
                        _ => 0
                    };
                    if (int.TryParse(filenameParts[3].Split()[0], out int semesterNumber))
                        semester = semesterNumber;
                    group = filenameParts[4];
                }
                else
                {
                    degreeLevel = 1;
                    if (int.TryParse(filenameParts[2].Split()[0], out int semesterNumber))
                        semester = semesterNumber;
                    group = filenameParts[3];
                }

                return new Schedule()
                {
                    DegreeLevel = degreeLevel,
                    Department = Department.WBMiI,
                    Major = major,
                    Semester = semester,
                    Group = group,
                    TypeOfStudies = typeOfStudies,
                };
            }
            private Major MatchMajor(string majorString)
            {
                if (Enum.TryParse(majorString, out Major major))
                    return major;
                else
                    throw new Exception("Unknown major!");
            }
            private TypeOfStudies MatchTypeOfStudies(string typeOfStudies)
            {
                if (Enum.TryParse(typeOfStudies, out ShortTypeOfStudies shortTypeOfStudies))
                {
                    return shortTypeOfStudies switch
                    {
                        ShortTypeOfStudies.S => TypeOfStudies.Fulltime,
                        ShortTypeOfStudies.NZ => TypeOfStudies.Extramural,
                        ShortTypeOfStudies.NW => TypeOfStudies.Evening,
                        _ => throw new Exception("Unknown Type Of Studies!")
                    };
                }
                else
                    throw new Exception("Unknown Type Of Studies!");
            }

            private enum ShortTypeOfStudies
            {
                S,
                NZ,
                NW
            }
            private enum Level
            {
                Ist,
                IIst
            }
        */
    }
}
