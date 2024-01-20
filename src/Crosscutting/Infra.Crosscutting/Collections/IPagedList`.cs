using System.Collections.Generic;

namespace Goal.Infra.Crosscutting.Collections;

public interface IPagedList<out T> : IPagedList, IEnumerable<T>
{
}
