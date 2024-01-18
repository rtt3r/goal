using System;

namespace Goal.Infra.Data.Auditing;

public class Audit : Audit<string>
{
    public Audit()
    {
        Id = Guid.NewGuid().ToString();
    }
}
