namespace Goal.Seedwork.Infra.Crosscutting.Adapters;

public interface ITypeAdapter
{
    TTarget Adapt<TSource, TTarget>(TSource source);

    TTarget Adapt<TTarget>(object source);
}
