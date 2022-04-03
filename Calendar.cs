using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BlockTime
{
    public class Calendar
    {
        /// <summary>
        /// Calculate all free time slots on a calendar given a calendar, bounds and meeting duration
        /// </summary>
        /// <param name="inputCalendar"></param>
        /// <param name="bounds"></param>
        /// <param name="meetingDuration"></param>
        /// <returns>List of available time slots</returns>
        public List<Tuple<string, string>> AllFreeTimes(string inputCalendar, string bounds, int meetingDuration)
        {
            string[] freeTimePeriods = ReFormatForFreeTimeSlots(inputCalendar, bounds);  
            List<Tuple<string, string>> freeTimeSlots = new List<Tuple<string, string>>();
            TimeSpan interval = new TimeSpan(0, meetingDuration, 00);

            foreach (string period in freeTimePeriods)
            {
                Tuple<string, string> tPeriod = FormatTimeSlotToTuple(period);
                if (!string.Equals(tPeriod.Item1, tPeriod.Item2))
                {
                    string lowerBound = tPeriod.Item1;
                    string upperBound = tPeriod.Item2;

                    DateTime timeSlotLowerBound = Format24HrTime(lowerBound, 2022, 2, 2);
                    DateTime timeSlotUpperBound = Format24HrTime(upperBound, 2022, 2, 2);

                    DateTime timeSlotNextBound = timeSlotLowerBound.Add(interval);

                    while (timeSlotNextBound <= timeSlotUpperBound)
                    {
                        freeTimeSlots.Add(Tuple.Create(timeSlotLowerBound.ToString("HH:mm"), timeSlotNextBound.ToString(("HH:mm"))));
                        timeSlotLowerBound = timeSlotNextBound;
                        timeSlotNextBound = timeSlotLowerBound.Add(interval);
                    }                   
                }
            }
            return freeTimeSlots;
        }

        /// <summary>
        /// Print all time slots in the specified format
        /// </summary>
        /// <param name="timeslots"></param>
        public void PrintTimeSlots(List<Tuple<string, string>> timeslots)
        {
            string toPrint = "[";

            foreach (Tuple<string, string> slot in timeslots)
            {
                toPrint += string.Format("['{0}', '{1}'], ", slot.Item1, slot.Item2);
            }

            toPrint = toPrint.Remove(toPrint.Length - 2, 2);  // remove the last ", " character
            toPrint += "]";

            Console.WriteLine(toPrint);
        }

        /// <summary>
        /// Format the input calendar string by extracting the free slots, and also adding the lower and upper bounds
        /// while creating the free time slots
        /// </summary>
        /// <param name="inputCalendar"></param>
        /// <param name="bounds"></param>
        /// <returns>A string array of all free time slots in the calendar</returns>
        public string[] ReFormatForFreeTimeSlots(string inputCalendar, string bounds)
        {
            if (string.IsNullOrEmpty(inputCalendar) || string.IsNullOrEmpty(bounds))
            {
                throw new ArgumentException("Either Calendar slots or bounds were not specified in the input");
            }

            string dropLeadingTrailingBracket = inputCalendar.Trim().Substring(1, inputCalendar.Length - 2);
            string[] freeTimeSlots = dropLeadingTrailingBracket.Split("',", StringSplitOptions.None);

            // Include the lower and upper time bounds
            // Its possible we end up with ['9:00', '9:00'] but that will be dropped later
            freeTimeSlots[0] = bounds.Split(",", StringSplitOptions.None)[0] + "'," + freeTimeSlots[0];
            freeTimeSlots[^1] = freeTimeSlots[^1] + "'," + bounds.Split(",", StringSplitOptions.None)[1];

            return freeTimeSlots;
        }

        /// <summary>
        /// Helper function to convert to datetime
        /// This is used to calculate time slots based on meeting duration
        /// Note that the year, day, month are arbitrary here and not really used for the calculations
        /// We only care about the hour and minute for our calculations
        /// </summary>
        /// <param name="time"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns>Datetime representing a bound on the time slot</returns>
        private static DateTime Format24HrTime(string time, int year, int month, int day)
        {
            // hour 
            string[] hourMin = time.Split(":");
            if (hourMin.Length != 2)
            {
                throw new ArgumentException("The format of the input calendar is not correct");
            }
            int hourInt;
            if (!int.TryParse(hourMin[0], out hourInt)) hourInt = 0;
            if (hourInt >= 24)
            {
                throw new ArgumentOutOfRangeException("Invalid hour");
            }

            // minute
            int minuteInt;
            if (!int.TryParse(hourMin[1], out minuteInt)) minuteInt = 0;
            if (minuteInt >= 60)
            {
                throw new ArgumentOutOfRangeException("Invalid minute");
            }

            // year, day, month is arbitrary
            return new DateTime(year, month, day, hourInt, minuteInt, 0);
        }

        /// <summary>
        /// Helper function to format timeslot string to Tuple
        /// </summary>
        /// <param name="inputTimeSlot"></param>
        /// <returns></returns>
        private Tuple<string, string> FormatTimeSlotToTuple(string inputTimeSlot)
        {
            // remove [, ', ] characters
            inputTimeSlot = Regex.Replace(inputTimeSlot, @"(\[|'|\])", "");

            string[] times = inputTimeSlot.Split(",", StringSplitOptions.None);
            Tuple<string, string> timeslot = Tuple.Create(times[0], times[1]);

            return timeslot;
        }
    }
}
