using System.Collections.Generic;

namespace Goal.Seedwork.Infra.Crosscutting.Collections;

public interface IPagedList<out T> : IPagedList, IEnumerable<T>
{
}
