<h1 align="center">BlockTime</h1>

# Description
This program will help find free time slots, given two calendars, working hours and meeting duration. Once you have installed it and run it, this code will launch a console where you can type the input for the program.

# Installation
Clone this repo and make a local copy. Then open the csproj file using Visual Studio, and hit the Run button within Visual Studio to run the code.

# Usage
Follow the installation process and run in Visual Studio. When the console window appears, follow the instructions there. There are two modes you can run this code in
- demo mode, where the program will run with predefined parameters
- user mode, where the user can type in the input

# Sample Input
    Calendar1 = [['9:00', '10:30'], ['12:00', '13:00', ['16:00', '18:00']]
    dailyBounds1 = ['9:00', '20:00']

    Calendar2 = [[ '10:00', '11:30'],  ['12:30', '14:30'], ['14:30', '15:00'], ['16:00', '17:00']]
    dailyBounds2 = ['10:00', '18:30']

    meetingDuration = 30

# Sample Output
    [['11:30', '12:00'], ['15:00', '15:30'], ['15:30', ;'16:00'], ['18:00', '18:30']]
    
# Design
Each input calendar is first organized into a List of Tuples<string, string> where each Tuple represents a free block of time. For eg, Calendar1 input along with dailyBounds1 will result in this intermediate list. Tuples which have the same Item1 and Item2 will be discarded during the processing, so (9:00, 9:00) in this list below will eventually be discarded

        {(9:00, 9:00), (10:30, 12:00), (13:00, 16:00), (18:00, 20:00)}
        
The intermediate list is further processed using meeting duration 30m into a List that looks like this. This is basically all 30 min free timeslots for calendar1.

        {(10:30, 11:00), (11:00, 11:30), (11:30, 12:00), (13:00, 13:30), (13:30, 14:00), (14:30, 15:00), (15:00, 15:30), (15:30, 16:00), (18:00, 18:30), (18:30, 19:00), (19:00, 19:30), (19:30, 20:00)}
        
A similar process is done for calendar2 and the common timeslots are the result that is printed out to the user.
        
# Gotcha and assumptions
This design uses the string.split method to split the calendar string on the separator "'," that separates a time slot in the input string. So an input like the following where "'," is missing after 12:00 will cause the program to fail.  

        Calendar1 = [['9:00', '10:30'], ['12:00 '13:00', ['16:00', '18:00']]
        
The assumption made in this program is that the input string will always be of the right format expected.



