using System;
using System.Collections.Generic;

namespace Goal.Seedwork.Infra.Http.Swagger;

public sealed class PolicySelectorWithLabel<T> where T : Attribute
{
    public required Func<IEnumerable<T>, IEnumerable<string?>> Selector { get; set; }
    public required string Label { get; set; }
}
