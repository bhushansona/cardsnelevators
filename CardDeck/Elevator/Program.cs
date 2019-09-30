using System;
using System.Collections.Generic;
using System.Threading;
using Elevator.Models;

namespace Elevator
{
    class Program
    {
        private static List<Models.Elevator> elevators = new List<Models.Elevator>();
        private static List<Floor> floors = new List<Floor>();
        private static int noOfFloors = 5;
        static void Main(string[] args)
        {
            // Prepare elevators
            for (int i = 0; i < 2; i++)
            {
                elevators.Add(new Models.Elevator(noOfFloors)
                {
                    ElevatorNo = i+1
                });
            }
            // Create floors
            for (int i = 0; i < noOfFloors; i++)
            {
                floors.Add(new Floor
                {
                    FloorNo = i,
                    Elevators = elevators
                });
            }
            //Floor Operations (Outside elevator)
            floors[1].PressUp();
            Thread.Sleep(1000); // delay to distinguish up and down elevator, simulating practical situation.
            floors[4].PressDown();
            floors[2].PressDown();

            //Elevator Operations (Inside elevator)
            elevators[0].RegisterFloor(4);
            elevators[1].RegisterFloor(2);

            Thread.Sleep(2000);
            floors[4].PressUp();
            Thread.Sleep(10000); // Elevators idle for long time

            floors[2].PressUp();
            elevators[0].RegisterFloor(4);
            Console.ReadLine();
        }
    }
}
