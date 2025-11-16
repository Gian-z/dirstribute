using DirstributeContracts.Interfaces;

namespace Dirstribute;

public class DirstributeServer : IDirstributeServer
{
    public Task Map(string name, string source, string target)
    {
        throw new NotImplementedException();
    }

    public Task Unmap(string name)
    {
        throw new NotImplementedException();
    }

    public Task Update(string name)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAll()
    {
        throw new NotImplementedException();
    }

    public Task SetUpdateMode(string name)
    {
        throw new NotImplementedException();
    }
}