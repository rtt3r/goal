using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Goal.Infra.Http.Swagger;

public sealed class AppendAuthorizeToSummaryOperationFilter : IOperationFilter
{
    private readonly AppendAuthorizeToSummaryOperationFilter<AuthorizeAttribute> filter;

    public AppendAuthorizeToSummaryOperationFilter()
    {
        var policySelector = new PolicySelectorWithLabel<AuthorizeAttribute>
        {
            Label = "policies",
            Selector = authAttributes =>
                authAttributes
                    .Where(a => !string.IsNullOrEmpty(a.Policy))
                    .Select(a => a.Policy)
        };

        var rolesSelector = new PolicySelectorWithLabel<AuthorizeAttribute>
        {
            Label = "roles",
            Selector = authAttributes =>
                authAttributes
                    .Where(a => !string.IsNullOrEmpty(a.Roles))
                    .Select(a => a.Roles)
        };

        filter = new AppendAuthorizeToSummaryOperationFilter<AuthorizeAttribute>(
            new[] { policySelector, rolesSelector }.AsEnumerable());
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
        => filter.Apply(operation, context);
}
