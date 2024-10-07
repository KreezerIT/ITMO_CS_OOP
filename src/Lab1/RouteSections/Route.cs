using Itmo.ObjectOrientedProgramming.Lab1.Vehicle;

namespace Itmo.ObjectOrientedProgramming.Lab1.RouteSections;

public class Route
{
    public double RouteLeftDuration { get; set; }

    public double TotalPassingTime { get; set; }

    public double MaxPermittedVehicleSpeedOnTheRoute { get; set; }

    public IVehicle RouteVehicle { get; set; }

    public double IterativeAccuracyRemainderFromPreviousSection { get; set; }

    public IRouteSections[] ListOfRouteSections { get; }

    public Route(IRouteSections[] listOfRouteSections, double routeLengthInKilometers, IVehicle routeVehicle, double maxPermittedVehicleSpeedOnTheRoute)
    {
        ArgumentNullException.ThrowIfNull(listOfRouteSections);

        ListOfRouteSections = listOfRouteSections;
        RouteLeftDuration = routeLengthInKilometers;
        RouteVehicle = routeVehicle;
        MaxPermittedVehicleSpeedOnTheRoute = maxPermittedVehicleSpeedOnTheRoute;
    }

    public void CalculationTheRemainderOfTheLastVisitedSection()
    {
        if (RouteVehicle.ActualSpeed != 0)
        {
            TotalPassingTime += IterativeAccuracyRemainderFromPreviousSection * RouteVehicle.Accuracy /
                                RouteVehicle.ActualSpeed;
        }
    }

    public Tuple<string, double> RouteSimulator()
    {
        foreach (IRouteSections section in ListOfRouteSections)
        {
            section.Duration += IterativeAccuracyRemainderFromPreviousSection;
            double entireSectionParts = section.Duration / RouteVehicle.Accuracy;
            double remainderSectionPart = section.Duration % RouteVehicle.Accuracy;
            IterativeAccuracyRemainderFromPreviousSection = remainderSectionPart;

            string resultOfVisitingTheSection = section.PassingTheSection(RouteVehicle, this, entireSectionParts);
            if (resultOfVisitingTheSection != "Успех" || RouteVehicle.Acceleration == 0 || RouteVehicle.ActualSpeed < 0)
            {
                CalculationTheRemainderOfTheLastVisitedSection();
                return Tuple.Create("Неудача", TotalPassingTime);
            }
        }

        if (RouteVehicle.ActualSpeed > MaxPermittedVehicleSpeedOnTheRoute)
        {
            CalculationTheRemainderOfTheLastVisitedSection();
            return Tuple.Create("Неудача", TotalPassingTime);
        }

        CalculationTheRemainderOfTheLastVisitedSection();
        return Tuple.Create("Удача", TotalPassingTime);
    }
}