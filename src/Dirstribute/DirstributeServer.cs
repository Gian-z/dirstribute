using Dirstribute.Helpers;
using DirstributeContracts.Interfaces;

namespace Dirstribute;

public class DirstributeServer : IDirstributeServer
{
    private readonly ConfigurationService _configurationService;
    private readonly MainWorker _mainWorker;

    public DirstributeServer(ConfigurationService configurationService, MainWorker mainWorker)
    {
        _configurationService = configurationService;
        _mainWorker = mainWorker;
    }
    
    
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