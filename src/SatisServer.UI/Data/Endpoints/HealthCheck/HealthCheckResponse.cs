﻿namespace SatisServer.UI.Data.Endpoints.HealthCheck;

public class HealthCheckResponse
{
    public string Health { get; set; }
    public string ServerCustomData { get; set; }
}