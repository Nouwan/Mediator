namespace Mediator.Benchmarks.Messaging;

internal static class Fixture
{
    public static void Setup()
    {
        ConsoleLogger.Default.WriteLineError("--------------------------------------");
        ConsoleLogger.Default.WriteLineError("Mediator config:");
        ConsoleLogger.Default.WriteLineError($"  - Lifetime       = {Mediator.ServiceLifetime}");
        ConsoleLogger.Default.WriteLineError($"  - Publisher      = {Mediator.NotificationPublisherName}");
        ConsoleLogger.Default.WriteLineError($"  - Total messages = {Mediator.TotalMessages}");
        ConsoleLogger.Default.WriteLineError("--------------------------------------");

        var envIsLargeProject = Environment.GetEnvironmentVariable("IsLargeProject");
        if (envIsLargeProject is not "True" and not "False")
            throw new InvalidOperationException(
                $"Invalid IsLargeProject: {envIsLargeProject}. Expected: True or False"
            );

        if (envIsLargeProject == "True")
        {
#pragma warning disable CS0162 // This code can be reached based on the current generated Mediator.
            if (Mediator.TotalMessages <= 100)
                throw new InvalidOperationException(
                    $"Unexpected messages count: {Mediator.TotalMessages}. Expected: more than 100"
                );
#pragma warning restore CS0162
        }
        else
        {
            if (Mediator.TotalMessages >= 100)
#pragma warning disable CS0162 // This code can be reached based on the current generated Mediator.
                throw new InvalidOperationException(
                    $"Unexpected messages count: {Mediator.TotalMessages}. Expected: less than 100"
                );
#pragma warning restore CS0162
        }

        var envLifetime = Environment.GetEnvironmentVariable("ServiceLifetime");
        if (envLifetime != Mediator.ServiceLifetime.ToString())
            throw new InvalidOperationException(
                $"Invalid lifetime: {Mediator.ServiceLifetime}. Expected: {envLifetime}"
            );

        var envPublisher = Environment.GetEnvironmentVariable("NotificationPublisherName");
        if (!string.IsNullOrWhiteSpace(envPublisher) && envPublisher != Mediator.NotificationPublisherName)
            throw new InvalidOperationException(
                $"Invalid publisher: {Mediator.NotificationPublisherName}. Expected: {envPublisher}"
            );
    }
}
