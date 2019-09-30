using System;
using System.Linq;
using System.Threading;

namespace Elevator.Models
{
    public class Elevator
    {
        public int CurrentFloor = 0;
        private readonly int _topFloor;
        public Status Status = Status.Stopped;
        public Direction direction = Direction.Up;
        public int[] _selectedFloors;
        protected internal int OpenWaitTime = 6000;
        public int ElevatorNo { get; set; }
        public Elevator(int numberOfFloors = 5)
        {
            _topFloor = numberOfFloors;
            _selectedFloors = new int[0];
        }

        public void Start()
        {
            Console.WriteLine("Elevator #" + ElevatorNo + " started.");
            while (Status != Status.Stopped)
            {
                if (Status == Status.MovingUp && CurrentFloor != _topFloor)
                {
                    foreach (var selectedFloor in _selectedFloors)
                    {
                        Ascend(selectedFloor);
                    }
                    Status = Status.Stopped;
                    _selectedFloors = new int[0];
                }
                if (Status == Status.MovingDown && CurrentFloor != 0)
                {
                    foreach (var selectedFloor in _selectedFloors)
                    {
                        Descend(selectedFloor);
                    }
                    Status = Status.Stopped;
                    _selectedFloors = new int[0];
                }
            }
            Console.WriteLine("Elevator #" + ElevatorNo + " stopped completely");
        }

        private void StopAndOpen(int floor)
        {
            Status = Status.DoorOpened;
            CurrentFloor = floor;
            Console.WriteLine("Elevator #" + ElevatorNo + " opened at floor {0}", floor);
            Thread.Sleep(OpenWaitTime);
            Console.WriteLine("Elevator #" + ElevatorNo + " closed at floor {0}", floor);
            Status = direction == Direction.Up ? Status.MovingUp : Status.MovingDown;
        }

        private void Descend(int floor)
        {
            for (int i = CurrentFloor; i >= 0; i--)
            {
                if (i == floor)
                {
                    StopAndOpen(floor);
                    Console.WriteLine("Elevator #" + ElevatorNo + " moved to " + CurrentFloor);
                }
                else
                    continue;
            }
            if (CurrentFloor == 0)
            {
                Status = Status.Stopped;
                Console.WriteLine("Elevator #" + ElevatorNo + " Waiting at " + CurrentFloor);
            }
        }

        private void Ascend(int floor)
        {
            for (int i = CurrentFloor; i <= _topFloor; i++)
            {
                if (i == floor)
                {
                    StopAndOpen(floor);
                    Console.WriteLine("Elevator #" + ElevatorNo + " moved to " + CurrentFloor);
                }
                else
                    continue;
            }

            if (CurrentFloor == _topFloor)
            {
                Status = Status.Stopped;
                Console.WriteLine("Elevator #"+ElevatorNo + " Waiting at "+CurrentFloor);
            }
        }

        void StayPut()
        {
            Console.WriteLine("That's our current floor");
        }

        public void RegisterFloor(int floorNo)
        {
            if (floorNo > _topFloor)
            {
                Console.WriteLine("We only have {0} floors", _topFloor);
                return;
            }

            if (Status == Status.MovingUp && CurrentFloor > floorNo)
            {
                Console.WriteLine("Elevator moving up. Can not register lower floor {0}", floorNo);
                return;
            }

            if (Status == Status.MovingDown && CurrentFloor < floorNo)
            {
                Console.WriteLine("Elevator moving down. Can not register upper floor {0}", floorNo);
                return;
            }

            if (_selectedFloors.Contains(floorNo))
            {
                return;
            }
            var temp = _selectedFloors;
            _selectedFloors = new int[_selectedFloors.Length+1];
            Array.Copy(temp, _selectedFloors, temp.Length);
            _selectedFloors[_selectedFloors.Length - 1] = floorNo;
            Console.WriteLine("Elevator #" + ElevatorNo + " registered for floors " + string.Join(",",_selectedFloors));
            if (Status == Status.Stopped)
            {
                Status = CurrentFloor < floorNo ? Status.MovingUp : Status.MovingDown;
                var sender = new Thread(Start)
                {
                    IsBackground = true
                };
                sender.Start();
            }
        }
    }
}

