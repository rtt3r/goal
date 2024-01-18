using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Goal.Infra.Http.Swagger;

internal static class OperationFilterContextExtensions
{
    public static IEnumerable<T> GetControllerAndActionAttributes<T>(this OperationFilterContext context) where T : Attribute
    {
        IEnumerable<T>? controllerAttributes = context.MethodInfo.DeclaringType?.GetTypeInfo().GetCustomAttributes<T>();
        IEnumerable<T> actionAttributes = context.MethodInfo.GetCustomAttributes<T>();

        return controllerAttributes
            ?.Concat(actionAttributes)
            ?? Enumerable.Empty<T>();
    }
}
