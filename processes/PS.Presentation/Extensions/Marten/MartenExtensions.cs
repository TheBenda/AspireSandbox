﻿using Marten;

using Weasel.Core;

using Wolverine.Marten;

namespace PS.Presentation.Extensions.Marten;

internal static class MartenExtensions
{
    internal static IServiceCollection ConfigureMarten(this IServiceCollection services, string connectionString)
    {
        services.AddMarten(options =>
        {
            options.Connection(connectionString);
            options.DatabaseSchemaName = "marten";
            options.AutoCreateSchemaObjects = AutoCreate.All;
        }).IntegrateWithWolverine();

        return services;
    }
}
