using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mars_Rover_Challenge
{
    public class RoverService : IRoverService
    {
        private readonly ILogger<RoverService> _log;
        private readonly IPosition _position;

        public RoverService(ILogger<RoverService> log, IPosition position)
        {
            _log = log;
            _position = position;
        }

        public void Run(List<string> Instructions)
        {
            List<int> Limit = Instructions[0].Trim().Split(' ').Select(int.Parse).ToList();
            for (int i = 1; i < Instructions.Count; i += 2)
            {
                var start = Instructions[i].Trim().Split(' ');
                var movement = Instructions[i+1].Trim();

                _position.X = int.Parse(start[0]);
                _position.Y = int.Parse(start[1]);
                _position.direction = (Directions)Enum.Parse(typeof(Directions), start[2]);

                Movement(Limit, movement);
                _log.LogInformation($"{_position.X} {_position.Y} {_position.direction}");

            }
            
        }

        public void Movement(List<int> Limit, string moves)
        {
            foreach (var move in moves)
            {
                switch (move)
                {
                    case 'M':
                        Move();
                        break;
                    case 'L':
                        Left();
                        break;
                    case 'R':
                        Right();
                        break;
                    default:
                        _log.LogError($"Invalid Input: {move}");
                        break;
                }

                if (_position.X < 0 || _position.X > Limit[0] || _position.Y < 0 || _position.Y > Limit[1])
                {
                    // If boundry has been exeeded reset position to 0 0 N and end the loop
                    _log.LogError($"Invalid position: {_position.X}:{_position.Y } the limit is {Limit[0]}:{Limit[1]}");
                    _position.X = 0;
                    _position.Y = 0;
                    _position.direction = Directions.N;
                    break;
                }
            }
        }

        private void Left()
        {
            switch (_position.direction)
            {
                case Directions.N:
                    _position.direction = Directions.W;
                    break;
                case Directions.S:
                    _position.direction = Directions.E;
                    break;
                case Directions.E:
                    _position.direction = Directions.N;
                    break;
                case Directions.W:
                    _position.direction = Directions.S;
                    break;
                default:
                    break;
            }
        }

        private void Right()
        {
            switch (_position.direction)
            {
                case Directions.N:
                    _position.direction = Directions.E;
                    break;
                case Directions.S:
                    _position.direction = Directions.W;
                    break;
                case Directions.E:
                    _position.direction = Directions.S;
                    break;
                case Directions.W:
                    _position.direction = Directions.N;
                    break;
                default:
                    break;
            }
        }

        private void Move()
        {
            switch (_position.direction)
            {
                case Directions.N:
                    _position.Y += 1;
                    break;
                case Directions.S:
                    _position.Y -= 1;
                    break;
                case Directions.E:
                    _position.X += 1;
                    break;
                case Directions.W:
                    _position.X -= 1;
                    break;
                default:
                    break;
            }
        }
    }
}
