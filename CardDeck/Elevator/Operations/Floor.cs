using System;
using System.Collections.Generic;

namespace Elevator.Models
{
    public class Floor
    {
        public List<Elevator> Elevators { get; set; }
        public int FloorNo { get; set; }
        
        public void PressUp()
        {
            Elevator nearestElevator=null;
            int floorDiff = 0;
            foreach (var elevator in Elevators)
            {
                if (elevator.Status == Status.MovingUp && elevator.CurrentFloor < FloorNo)
                {
                    nearestElevator = elevator;
                }
                else
                {
                    var diff = Math.Abs(elevator.CurrentFloor - FloorNo);
                    if (floorDiff < diff)
                    {
                        floorDiff = diff;
                        nearestElevator = elevator;
                    }
                }
            }

            OperateElevator(nearestElevator);
        }

        public void PressDown()
        {
            Elevator nearestElevator = null;
            int floorDiff = 0;
            foreach (var elevator in Elevators)
            {
                if (elevator.Status == Status.MovingDown && elevator.CurrentFloor > FloorNo)
                {
                    nearestElevator = elevator;
                }
                else
                {
                    var diff = Math.Abs(elevator.CurrentFloor - FloorNo);
                    if (floorDiff < diff)
                    {
                        floorDiff = diff;
                        nearestElevator = elevator;
                    }
                }
            }

            OperateElevator(nearestElevator);
        }

        private void OperateElevator(Elevator nearestElevator)
        {
            if (nearestElevator != null)
            {
                Console.WriteLine("Floor #" + FloorNo + " got Elevator #" + nearestElevator.ElevatorNo);
                nearestElevator.RegisterFloor(FloorNo);
            }
        }
    }
}
