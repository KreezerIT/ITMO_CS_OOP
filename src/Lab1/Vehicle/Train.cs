namespace Itmo.ObjectOrientedProgramming.Lab1.Vehicle;

public class Train : IVehicle
{
    public double Mass { get; set; }

    public double ActualSpeed { get; set; }

    public double Acceleration { get; set; }

    public double MaxPermittedAppliedPower { get; set; }

    public double Accuracy { get; set; }

    public double TotalPassedDistance { get; set; }

    public double CalculationResultingSpeed()
    {
        return ActualSpeed + (Acceleration * Accuracy);
    }

    public double CalculationPassedDistance()
    {
        double resultingSpeed = CalculationResultingSpeed();
        return resultingSpeed * Accuracy;
    }

    public Train(double mass, double accuracy, double maxPermittedAppliedPower)
    {
        Mass = mass;
        Accuracy = accuracy;
        MaxPermittedAppliedPower = maxPermittedAppliedPower;
    }
}