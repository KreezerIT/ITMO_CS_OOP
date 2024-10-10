using Itmo.ObjectOrientedProgramming.Lab1.Vehicle;

namespace Itmo.ObjectOrientedProgramming.Lab1.RouteSections;

public class Station : IRouteSections
{
    public double Duration { get; set; }

    public double MaxPermittedAppliedSpeed { get; set; }

    public double CongestionLevel { get; set; }

    public double TimeSpentOnTheStation { get; set; }

    public Station(double congestionLevel, double maxPermittedAppliedSpeed)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(congestionLevel, nameof(congestionLevel));
        ArgumentOutOfRangeException.ThrowIfNegative(maxPermittedAppliedSpeed, nameof(maxPermittedAppliedSpeed));

        CongestionLevel = congestionLevel;
        Duration = CongestionLevel * 10;
        MaxPermittedAppliedSpeed = maxPermittedAppliedSpeed;
    }

    public string PassingTheSection(IVehicle vehicle, Route route, double entireSectionParts)
    {
        if (vehicle.ActualSpeed > MaxPermittedAppliedSpeed)
        {
            return "Неудача";
        }

        double actualVehicleSpeed = vehicle.ActualSpeed;
        vehicle.ActualSpeed = 0;

        TimeSpentOnTheStation = Duration / actualVehicleSpeed;
        route.TotalPassingTime += TimeSpentOnTheStation;

        vehicle.ActualSpeed = actualVehicleSpeed;
        return "Успех";
    }
}