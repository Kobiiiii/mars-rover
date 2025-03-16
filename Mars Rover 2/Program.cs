using Mars_Rover;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Numerics;
using System.Threading;

// Objects.
Rover rover1 = new Rover("Rover 1",Rover.directions.N, 1, 2);
Rover rover2 = new Rover("Rover 2", Rover.directions.E, 3, 3);
Planet mars = new Planet("Mars", new int[,] { { 0, 1, 2, 3, 4, 5 }, { 0, 1, 2, 3, 4, 5 } });

// Input.
string input = "";
string[] messages = { "Enter a command: ", "Select a Rover: ", "Begin Test?:" };
int messagesI = 0;



// A shorthand way of switching between two rovers.
Rover selectedR = new Rover();

// Test Mode.
bool testMode = false;
string expectedInput = "LMLMLMLMM";
string expectedInput2 = "MMRMMRMRRM";

string expectedOutput = "1 3 N";
string expectedOutput2 = "5 1 E";


string roverOutput = $"{rover1.x} {rover1.y} {(Rover.directions)rover1.directionsI}";
string roverOutput2 = $"{rover2.x} {rover2.y} {(Rover.directions)rover2.directionsI}";

// The commands function is only for free mode.
void AskInput()
{
    Console.Write(messages[messagesI]);
    input = Console.ReadLine();
    if (testMode == false) { commands(); }
    Console.Clear();
}


// Test mode is set to true here to present the correct error message.
void Decide()
{
    testMode = true;
    Console.WriteLine("There are two modes to this program.\n" +
    "Test mode is a mode that is meant for the mars rover unit test. \n" +
    "Free mode is a mode where you can freely move the rover around mars.\n\n" +
    "Type in `Test` to begin test mode.\n" +
    "Type in `Free` to begin free mode.\n");

    AskInput();
    switch (input.ToLower())
    {
        case "test":
            testMode = true;
            IntroductionT();
            break;
        case "free":
            testMode = false;
            Introduction();
            break;
        default:
            Console.WriteLine("Please type in `Test` or `Free`.");
            Decide();
            break;
    }

}

// ** TEST MODE **
void IntroductionT()
{
    Console.WriteLine("This is test mode for the mars rover \n" +
        "This test will move both of the rovers and will go trough the epected position through the inputs given. \n" +
        "Type in `Y` to proceed with the test or `N` not to proceed with the test");
    AskInput();
    switch (input.ToLower())
    {
        case "y":
            TestR();
            break;
        case "n":
            Decide();
            break;
        default:
            Console.WriteLine("Please type in `Y` or `N`.");
            IntroductionT();
            break;
    }

}

// TestR2 is called when this test is done and successful.
void TestR()
{
    int commandsN = 0;
    foreach (char c in expectedInput)
    {
        commandsN++;
        switch (c.ToString())
        {

            case "L":
                rover1.RotateLeft();
                break;

            case "R":
                rover1.RotateRight();
                break;

            case "M":
                rover1.Move();
                rover1.x = Math.Clamp(rover1.x, 0, mars.Coords.Length / 2 - 1);
                rover1.y = Math.Clamp(rover1.y, 0, mars.Coords.Length / 2 - 1);
                break;

            default:
                input.Trim(c);
                Console.WriteLine(
                    "There were some characters that you have inputed that were not `L`,`R` or `M` \n" +
                    "Those characters got removed within your commands.");
                break;
        }
        if (commandsN == expectedInput.Length) { Console.WriteLine($"{rover1.x} {rover1.y} {(Rover.directions)rover1.directionsI}"); roverOutput = $"{rover1.x} {rover1.y} {(Rover.directions)rover1.directionsI}"; Assert.AreEqual(expectedOutput, roverOutput, $"{rover1.name} did not get to the expected position."); TestR2(); }
    }
}

void TestR2()
{
    int commandsN = 0;
    foreach (char c in expectedInput2)
    {
        commandsN++;
        switch (c.ToString())
        {

            case "L":
                rover2.RotateLeft();
                break;

            case "R":
                rover2.RotateRight();
                break;

            case "M":
                rover2.Move();
                rover2.x = Math.Clamp(rover2.x, 0, mars.Coords.Length / 2 - 1);
                rover2.y = Math.Clamp(rover2.y, 0, mars.Coords.Length / 2 - 1);
                break;

            default:
                input.Trim(c);
                Console.WriteLine(
                    "There were some characters that you have inputed that were not `L`,`R` or `M` \n" +
                    "Those characters got removed within your commands.");
                break;
        }
        if (commandsN == expectedInput2.Length) { Console.WriteLine($"{rover2.x} {rover2.y} {(Rover.directions)rover2.directionsI}"); roverOutput2 = $"{rover2.x} {rover2.y} {(Rover.directions)rover2.directionsI}"; Assert.AreEqual(expectedOutput2, roverOutput2, $"{rover2.name} did not get to the expected position."); }
    }
}

// ** FREE MODE **
void Introduction()
{
    Console.WriteLine("This is the controller for the mars rovers.\n" +
        "To select a rover, type in 1 for Rover 1 or 2 for Rover 2 \n\n" +
        "Rover Commands:\n" +
        "Select L to rotate the selected rover 90 degrees to the left.\n" +
        "Select R to rotate the selected rover 90 degrees to the right.\n" +
        "Type in `Swap` to change the rover you are controlling.\n" +
        "Type in `Help` to remind yourself of these commands.\n" +
        "Type in `Stats` to understand the current position of both rovers \n." +
        "Type in `Move` to move the rover again. \n");

    SelectRover();
}

// As the console clears itself after every input,
// Knowing where the current rover is important.
void stats()
{
    Console.Clear();
    Console.WriteLine($"Here is the current position for {selectedR.name}\n" +
        $"X: {selectedR.x}\n" +
        $"Y: {selectedR.y}\n" +
        $"Direction: {(Rover.directions)selectedR.directionsI}");
    AskInput();
}

void SelectRover() 
{
    messagesI = 1;
    AskInput();
    selectedR = (input == "1") ? rover1 : rover2; 
    
    if(input == "1" || input == "2") 
    {
        Console.WriteLine($"You have selected {selectedR.name}");
        moveR();

    } else {
        Console.WriteLine("Please type in either 1 or 2");
        SelectRover();
    }
}

// As the console clears after every input, it's important to remind the user what the commands are.
void Help() 
{
    messagesI = 0;
    Console.WriteLine(
        "Rover Commands:\n" +
        "Select L to rotate the selected rover 90 degrees to the left.\n" +
        "Select R to rotate the selected rover 90 degrees to the right.\n" +
        "Type in `Swap` to change the rover you are controlling.\n" +
        "Type in `Help` to remind yourself of these commands.\n" +
        "Type in `Stats` to understand the current position of both rovers. \n" +
        "Type in `Move` to move the rover again. \n");

    AskInput();
}


// This method controlls all movement of the rover including the rotation.
// The commented out print statements were meant for a potential step by step movement of the rovers.
// This is when the rover would move it would print every move the rover did.
void moveR()
{
    messagesI = 0;
    AskInput();
    int commandsN = 0;
    foreach (char c in input ) 
    { 
        commandsN++;
        switch (c.ToString().ToLower())
        {

            case "l":
                selectedR.RotateLeft();
                //Console.WriteLine($"{selectedR.directions[selectedR.directionsI]}");

                break;
            case "r":
                selectedR.RotateRight();
                //Console.WriteLine($"{selectedR.directions[selectedR.directionsI]}");
                break;
            case "m":
                selectedR.Move();
                selectedR.x = Math.Clamp(selectedR.x, 0, mars.Coords.Length/2-1);
                selectedR.y = Math.Clamp(selectedR.y, 0, mars.Coords.Length/ 2-1);


                if (selectedR.x == 0 && selectedR.directionsI == 3 || selectedR.x == mars.Coords.Length / 2 - 1 && selectedR.directionsI == 1) { Console.WriteLine("One of your movements is out of bounds"); }
                if (selectedR.x == 0 && selectedR.directionsI == 1 || selectedR.x == mars.Coords.Length / 2 - 1 && selectedR.directionsI == 3) { Console.WriteLine("One of your movements is out of bounds"); }
                if (selectedR.y == 0 && selectedR.directionsI == 0 || selectedR.y == mars.Coords.Length / 2 - 1 && selectedR.directionsI == 2) { Console.WriteLine("One of your movements is out of bounds"); }
                if (selectedR.y == 0 && selectedR.directionsI == 2 || selectedR.y == mars.Coords.Length / 2 - 1 && selectedR.directionsI == 0) { Console.WriteLine("One of your movements is out of bounds"); }

                //Console.WriteLine($"{selectedR.x} {selectedR.y}"); 
                break;

            default:
                input.Trim(c);
                Console.WriteLine(
                    "There were some characters that you have inputed that were not `L`,`R` or `M` \n" +
                    "Those characters got removed within your commands.");
                break;
        }


        if (commandsN == input.Length) { Console.WriteLine($"{selectedR.x} {selectedR.y} {(Rover.directions)selectedR.directionsI}");  }


    }
    AskInput();

}

// To prevent case sensitivity, all input will turn into lowercase.
// This method checks for what input has been sent.
void commands() 
{
        switch (input.ToLower())
        {
            case "help":
                Help();
                break;

            case "swap":
                SelectRover();
                break;

            case "stats":
                stats();
                break;

            case "move":
                moveR();
                break;
        }
}

Decide(); // Used to begin the program.