namespace Goal.Infra.Crosscutting.Adapters;

public interface ITypeAdapterFactory
{
    ITypeAdapter Create();
}