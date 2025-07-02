namespace PWG5.CoffeeMek.Simulator.Models;
using Bogus;
public static class CNCPayloadFactory
{
    private static Faker<CNCPayload> _faker = new Faker<CNCPayload>()
        .RuleFor(c => c.CycleTime, f => f.Random.Double(0.5, 5.0))
        .RuleFor(c => c.CutDepth, f => f.Random.Double(0.1, 2.0))
        .RuleFor(c => c.Vibration, f => f.Random.Double(0.1, 1.0));
    public static CNCPayload Generate() => _faker.Generate();
}

public static class LathePayloadFactory
{
    private static Faker<LathePayload> _faker = new Faker<LathePayload>()
        .RuleFor(l => l.RotationSpeed, f => f.Random.Int(100, 3000))
        .RuleFor(l => l.SpindleTemperature, f => f.Random.Double(20.0, 100.0));
    public static LathePayload Generate() => _faker.Generate();
}

public static class AssemblyPayloadFactory
{
    private static Faker<AssemblyLinePayload> _faker = new Faker<AssemblyLinePayload>()
        .RuleFor(a => a.MeanStationTime, f => f.Random.Double(1.0, 10.0));
        //.RuleFor(a => a.Anomalies, f => f.Lorem.Sentence(3));
    public static AssemblyLinePayload Generate() => _faker.Generate();
}

public static class TestLinePayloadFactory
{
    private static Faker<TestLinePayload> _faker = new Faker<TestLinePayload>()
        .RuleFor(t => t.TestResult, f => f.Random.WeightedRandom(new[] { "OK", "Fail" }, new[] { (float)0.99, (float)0.01 }))
        .RuleFor(t => t.BoilerPressure, f => f.Random.Double(1.0, 5.0))
        .RuleFor(t => t.BoilerTemperature, f => f.Random.Double(50.0, 150.0))
        .RuleFor(t => t.EnergyConsumption, f => f.Random.Double(10.0, 100.0));
    public static TestLinePayload Generate() => _faker.Generate();
}