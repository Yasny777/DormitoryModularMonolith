using Carter;

namespace Shared.Features.Endpoints;

public abstract class PrefixedCarterModule(string? basePath = null)
    : CarterModule(basePath == null ? "/api" : $"/api/{basePath}");