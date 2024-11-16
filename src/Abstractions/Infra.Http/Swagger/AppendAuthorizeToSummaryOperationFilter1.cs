using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Goal.Infra.Http.Swagger;

public sealed class AppendAuthorizeToSummaryOperationFilter<T>(IEnumerable<PolicySelectorWithLabel<T>> policySelectors) : IOperationFilter where T : Attribute
{
    private readonly IEnumerable<PolicySelectorWithLabel<T>> policySelectors = policySelectors;

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.GetControllerAndActionAttributes<AllowAnonymousAttribute>().Any())
        {
            return;
        }

        IEnumerable<T> authorizeAttributes = context.GetControllerAndActionAttributes<T>();

        if (authorizeAttributes.Any())
        {
            operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
            operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

            var securityScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
            };

            operation.Security =
            [
                new OpenApiSecurityRequirement
                {
                    [ securityScheme ] = ["scheme"]
                }
            ];

            var authorizationDescription = new StringBuilder(" (Auth");

            foreach (PolicySelectorWithLabel<T> policySelector in policySelectors)
            {
                AppendPolicies(authorizeAttributes, authorizationDescription, policySelector);
            }

            operation.Summary += authorizationDescription.ToString().TrimEnd(';') + ")";
        }
    }

    private static void AppendPolicies(IEnumerable<T> authorizeAttributes, StringBuilder authorizationDescription, PolicySelectorWithLabel<T> policySelector)
    {
        IOrderedEnumerable<string?> policies = policySelector
            .Selector(authorizeAttributes)
            .OrderBy(policy => policy);

        if (policies.Any())
        {
            authorizationDescription.Append($" {policySelector.Label}: {string.Join(", ", policies)};");
        }
    }
}
