using Itmo.ObjectOrientedProgramming.Lab1.Vehicle;

namespace Itmo.ObjectOrientedProgramming.Lab1.RouteSections;

public class DefaultMagneticPath : IRouteSections
{
    public double Duration { get; set; }

    public DefaultMagneticPath(double duration)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(duration, nameof(duration));

        Duration = duration;
    }

    public string PassingTheSection(IVehicle vehicle, Route route, double entireSectionParts)
    {
        double numberOfSectionParts = entireSectionParts;
        while (entireSectionParts > 0)
        {
            route.RouteLeftDuration -= Duration / numberOfSectionParts;

            vehicle.ActualSpeed = vehicle.CalculationResultingSpeed();
            vehicle.TotalPassedDistance += vehicle.CalculationPassedDistance();
            route.TotalPassingTime += Duration / numberOfSectionParts / vehicle.ActualSpeed;

            entireSectionParts--;
        }

        return "Успех";
    }
}