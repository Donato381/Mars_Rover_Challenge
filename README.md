# Mars Rover Challenge 
Based on the google challenge https://code.google.com/archive/p/marsrovertechchallenge/

# Mars_Rover_Challange
This solution contains 2 projects, the main project and a XUnit test project
To run the project simply open the solution file in Visual Studio 2019 and restore your NuGet packages.

You can debug through Visual Studio to see the result based on the inputs defined within the RoverInstructions.txt file
The current instructions are as follows:
| Command  | Description |
| ------------- | ------------- |
| 5 5 | The grid limits |
| 1 2 N | Rover 1 start location |
| LMLMLMLMM | Rover 1 movement commands |
| 3 3 E | Rover 2 start location |
| MMRMMRMRRM | Rover 2 movement commands |


The output would be as follows:
| Command  | Description |
| ------------- | ------------- |
| 1 3 N | Rover 1 end location |
| 5 1 E | Rover 2 end location |

# XUnit Tests
The solution includes a XUnit test project which has 2 tests:
- Test 1 will test the expected outcome from a correct input
- Test 2 will test the expected outcome of 0 0 N if the grid limits are exeeded by an incorrect movement instruction
