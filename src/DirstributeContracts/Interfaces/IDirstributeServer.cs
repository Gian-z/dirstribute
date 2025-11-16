namespace DirstributeContracts.Interfaces;

public interface IDirstributeServer
{
    Task Map(string name, string source, string target);
    Task Unmap(string name);

    Task Update(string name);
    Task UpdateAll();

    Task SetUpdateMode(string name);
}

public enum UpdateMode
{
    OnDemand,
    OnUpdate
}