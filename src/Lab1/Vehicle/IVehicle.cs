namespace Itmo.ObjectOrientedProgramming.Lab1.Vehicle;

public interface IVehicle
{
    public double Mass { get; set; }

    public double ActualSpeed { get; set; }

    public double Acceleration { get; set; }

    public double MaxPermittedAppliedPower { get; set; }

    public double Accuracy { get; set; }

    public double TotalPassedDistance { get; set; }

    public double CalculationResultingSpeed();

    public double CalculationPassedDistance();
}