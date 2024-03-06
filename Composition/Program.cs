using System;
class Motor
{
    public void Start()
    {
        Console.WriteLine("Motor started");
    }
}

class Vehicle
{
    private Motor motor;
    public Vehicle()
    {
        motor = new Motor();
    }

    public void StartVehicle()
    {
        motor.Start();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Vehicle vehicle = new Vehicle();
        vehicle.StartVehicle();
    }
}
