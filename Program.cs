using System;
using System.Collections.Generic;
using System.Linq;

namespace BlockTime
{
    class Program
    {
        static void Main(string[] args)
        {
            Calendar calendar = new Calendar();

            IntroText();

            int mode = Convert.ToInt32(Console.ReadLine());

            string bounds1, bounds2, c1, c2 = string.Empty;
            int meetingDuration = 0;

            if (mode == 1)
            {
                bounds1 = "['9:00', '20:00']";
                bounds2 = "['10:00', '18:30']";

                c1 = "[['9:00', '10:30'], ['12:00', '13:00'], ['16:00', '18:00']]";
                c2 = "[[10:00', '11:30'], ['12:30', '14:30'], ['14:30', '15:00'], ['16:00', '17:00']";

                meetingDuration = 30;
            }
            else
            {
                Console.Write("Enter first calendar times: ");
                c1 = Console.ReadLine();
                Console.Write("Enter the first calendar lower and upper bounds: ");
                bounds1 = Console.ReadLine();

                Console.Write("Enter second calendar times: ");
                c2 = Console.ReadLine();
                Console.Write("Enter the second calendar lower and upper bounds: ");
                bounds2 = Console.ReadLine();

                Console.Write("How long is the meeting for: ");
                meetingDuration = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine();
                Console.WriteLine("All free times are as listed:"); 
            }

            List<Tuple<string, string>> freeTimes1 = calendar.AllFreeTimes(c1, bounds1, meetingDuration);
            List<Tuple<string, string>> freeTimes2 = calendar.AllFreeTimes(c2, bounds2, meetingDuration);

            var commonFreeTimes = freeTimes1.Where(t => freeTimes2.Contains(t));

            List<Tuple<string, string>> result = commonFreeTimes.ToList();

            Console.WriteLine();
            if (result.Count != 0)
            {
                Console.WriteLine("All common free time slots are as listed:");
                calendar.PrintTimeSlots(result);
            }
            else
            {
                Console.WriteLine("No common free time slots available");
            }
        }

        static void IntroText()
        {
            Console.WriteLine("Choose mode by typing 1 or 2:");
            Console.WriteLine("1. Demo mode with preset inputs");
            Console.WriteLine("2. User mode where you supply the inputs");
            Console.WriteLine();
        }
    }
}


