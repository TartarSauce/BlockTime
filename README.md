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

