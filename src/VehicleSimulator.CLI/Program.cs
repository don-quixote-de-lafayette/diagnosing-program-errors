// Create the vehicle at point (0, 0) with a fuel consumption of 0.1 units per unit distance
// Log precision has been left to the default value of 2 decimal places.
var vehicle = new Vehicle(
    initialPosition: new Position(0, 0),
    maxFuelLevel: 100,
    initialFuelLevel: 100,
    fuelConsumptionPerUnitDistance: 0.1
);

/* ******************************
 * TASK 1: Drive
 ***************************** */

// Drive in place.
// TODO: comment this out - what happens to Totel Distance reported?
vehicle.Drive(0, 0);

// Turn on and drive in one direction for given distance
vehicle.Drive(100, 0);

// Drive in another direction for given distance (simulates a turn)
vehicle.Drive(200, 0);

// Print the trip stats
Console.WriteLine($"Total Distance: {vehicle.Log.TotalDistance}");

/* ******************************
 * TASK 2: Refuel
 ***************************** */

// Refuel
vehicle.Refuel(100);

// Check values after refuel
Console.WriteLine($"Vehicle can hold {vehicle.MaxFuelLevel} gallons of fuel.");
Console.WriteLine($"Vehicle has {vehicle.FuelLevel} gallons of fuel.");
