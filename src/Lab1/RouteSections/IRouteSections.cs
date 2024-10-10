using Itmo.ObjectOrientedProgramming.Lab1.Vehicle;

namespace Itmo.ObjectOrientedProgramming.Lab1.RouteSections;

public interface IRouteSections
{
    public double Duration { get; set; }

    public string PassingTheSection(IVehicle vehicle, Route route, double entireSectionParts);
}