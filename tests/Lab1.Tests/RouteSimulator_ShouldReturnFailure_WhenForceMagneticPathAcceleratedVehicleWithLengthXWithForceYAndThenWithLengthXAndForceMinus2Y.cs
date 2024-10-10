﻿using Itmo.ObjectOrientedProgramming.Lab1.RouteSections;
using Itmo.ObjectOrientedProgramming.Lab1.Vehicle;
using Xunit;

namespace Lab1.Tests;

public class RouteSimulator_ShouldReturnFailure_WhenForceMagneticPathAcceleratedVehicleWithLengthXWithForceYAndThenWithLengthXAndForceMinus2Y
{
    public static IEnumerable<object[]> TestParameters()
    {
        const double vehicle_mass = 100;
        const double vehicle_accuracy = 5;
        const double vehicle_maxPermittedAppliedPower = 100;
        var train = new Train(vehicle_mass, vehicle_accuracy, vehicle_maxPermittedAppliedPower);

        const double ForceMagneticPath_1_Duration = 50;
        const double ForceMagneticPath_1_Power = 20;
        var forceMagneticPath_1 = new ForceMagneticPath(ForceMagneticPath_1_Duration, ForceMagneticPath_1_Power);

        const double ForceMagneticPath_2_Duration = 50;
        const double ForceMagneticPath_2_Power = -40;
        var forceMagneticPath_2 = new ForceMagneticPath(ForceMagneticPath_2_Duration, ForceMagneticPath_2_Power);

        var listOfRouteSections = new IRouteSections[] { forceMagneticPath_1, forceMagneticPath_2 };
        double routeLengthInKilometers = 0;
        double maxPermittedVehicleSpeedOnTheRoute = 200;
        foreach (IRouteSections section in listOfRouteSections)
        {
            routeLengthInKilometers += section.Duration;
        }

        yield return new object[] { listOfRouteSections, routeLengthInKilometers, train, maxPermittedVehicleSpeedOnTheRoute };
    }

    [Theory]
    [MemberData(nameof(TestParameters))]
    public void TestRouteForTrain(IRouteSections[] listOfRouteSections, double routeLengthInKilometers, Train train, double maxPermittedVehicleSpeedOnTheRoute)
    {
        var route = new Route(listOfRouteSections, routeLengthInKilometers, train, maxPermittedVehicleSpeedOnTheRoute);

        Assert.Equal("Неудача", route.RouteSimulator().Item1);
    }
}