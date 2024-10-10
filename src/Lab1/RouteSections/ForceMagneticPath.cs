using Itmo.ObjectOrientedProgramming.Lab1.Vehicle;

namespace Itmo.ObjectOrientedProgramming.Lab1.RouteSections;

public class ForceMagneticPath : IRouteSections
{
    public double Duration { get; set; }

    public double Power { get; set; }

    public ForceMagneticPath(double duration, double power)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(duration, nameof(duration));

        Duration = duration;
        Power = power;
    }

    public string PassingTheSection(IVehicle vehicle, Route route, double entireSectionParts)
    {
        double numberOfSectionParts = entireSectionParts;
        while (entireSectionParts > 0)
        {
            vehicle.Acceleration = Power / vehicle.Mass;
            route.RouteLeftDuration -= Duration / numberOfSectionParts;

            vehicle.ActualSpeed = vehicle.CalculationResultingSpeed();
            vehicle.CalculationPassedDistance();
            route.TotalPassingTime += Duration / numberOfSectionParts / vehicle.ActualSpeed;

            entireSectionParts--;

            if (Power > vehicle.MaxPermittedAppliedPower)
            {
                return "Неудача";
            }
        }

        return "Успех";
    }
}