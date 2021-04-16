using Mars_Rover_Challenge;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Mars_Rover_Challange.Tests
{
    public class RoverTests
    {
        [Fact]
        public void Test_Successful_Movement()
        {
            // Arrange
            var _log = new NullLogger<RoverService>();
            var _position = new Position();
            var _roverService = new RoverService(_log, _position);
            List<string> instructions = new List<string>();
            instructions.Add("5 5"); // Limits
            instructions.Add("1 2 N"); // Starting Position
            instructions.Add("LMLMLMLMM"); // Movements

            var expectedOutput = "1 3 N";

            // Act
            _roverService.Run(instructions);



            // Assert
            Assert.Equal(expectedOutput, $"{_position.X} {_position.Y} {_position.direction}");
        }

        [Fact]
        public void Test_Boundy_Exeeded()
        {
            // Arrange
            var _log = new NullLogger<RoverService>();
            var _position = new Position();
            var _roverService = new RoverService(_log, _position);
            List<string> instructions = new List<string>();
            instructions.Add("5 5"); // Limits
            instructions.Add("1 2 N"); // Starting Position
            instructions.Add("MMMMMMLMLMMMMLMLMMMM"); // Movements

            var expectedOutput = "0 0 N"; // If boundry has been exeeded position is set to 0 0 N

            // Act
            _roverService.Run(instructions);



            // Assert
            Assert.Equal(expectedOutput, $"{_position.X} {_position.Y} {_position.direction}");
        }
    }
}
