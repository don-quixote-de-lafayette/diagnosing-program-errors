public class Vehicle
{
    public double FuelConsumptionPerUnitDistance { get; private set; }
    public double FuelLevel { get; private set; }
    public double MaxFuelLevel { get; private set; }
    public int OutputPrecision { get; private set; }
    public Position Position { get; private set; }
    public TripLog Log { get; private set; }

    public Vehicle(Position initialPosition, double maxFuelLevel, double initialFuelLevel, double fuelConsumptionPerUnitDistance, int outputPrecision = 2)
    {
        // State values are set in the constructor and exposed as properties for easy access.
        // The user cannot change these directly after construction.
        FuelConsumptionPerUnitDistance = fuelConsumptionPerUnitDistance;
        FuelLevel = initialFuelLevel;
        MaxFuelLevel = maxFuelLevel;
        OutputPrecision = outputPrecision;
        Position = initialPosition;
        Log = new TripLog(outputPrecision);
    }

    public void Drive(double distanceX, double distanceY)
    {
        UpdatePosition(distanceX, distanceY);
        Log.Record(Position);
    }

    public string GetPrintedStatus()
    {
        // Convenience method to print the vehicle's status in a readable format
        string x = Position.X.ToString($"F{OutputPrecision}");
        string y = Position.Y.ToString($"F{OutputPrecision}");
        string fuelLevel = FuelLevel.ToString($"F{OutputPrecision}");

        string output = "";
        output += $"Fuel Level: {fuelLevel}\n";
        output += $"Position: ({x}, {y})";
        return output;
    }

    public void Refuel(double amount)
    {
        if (amount < 0)
            throw new ArgumentException("Refuel amount cannot be negative.");

        FuelLevel += amount;
    }

    private void UpdatePosition(double intendedDistanceX, double intendedDistanceY)
    {
        // Short circuit if there is no fuel
        if (FuelLevel <= 0)
            return;

        // How far vehicle wants to go
        double intendedDistance = Math.Sqrt(intendedDistanceX * intendedDistanceX + intendedDistanceY * intendedDistanceY);

        // Short circuit if the vehicle wants to go nowhere
        if (intendedDistance == 0)
            return;

        // How far vehicle can travel with remaining fuel
        double maxDistanceCanTravel = FuelLevel / FuelConsumptionPerUnitDistance;

        if (maxDistanceCanTravel >= intendedDistance)
        {
            // Update the vehicle's position
            Position = new(Position.X + intendedDistanceX, Position.Y + intendedDistanceY);

            // Subtract fuel that was used
            double fuelCost = intendedDistance * FuelConsumptionPerUnitDistance;
            FuelLevel -= fuelCost;
        }
        else
        {
            // Scale the intended component distances to the actual distance
            // vehicle can go before running dry, then update the vehicle's position
            double scale = maxDistanceCanTravel / intendedDistance;
            Position = new(Position.X + intendedDistanceX * scale, Position.Y + intendedDistanceY * scale);

            // Vehicle ran out of fuel
            FuelLevel = 0;
        }
    }
}
