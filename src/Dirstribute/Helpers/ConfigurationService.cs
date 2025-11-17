using System.Text.Json;
using DirstributeDomain;

namespace Dirstribute.Helpers;

public class ConfigurationService
{
    public readonly Configuration Configuration;
    public readonly string ConfigPath; //todo: add command that displays this to the user
    private readonly string _configFileName = "config.json";
    private readonly string _serviceName = "dirstribute";

    public ConfigurationService(IConfiguration config)
    {
        ConfigPath = config.GetSection("MappingDirectory").Value ?? GetDefaultConfigPath();
        Directory.CreateDirectory(Path.GetDirectoryName(ConfigPath)!);
        
        try
        {
            string json = File.ReadAllText(ConfigPath);
            Configuration = JsonSerializer.Deserialize<Configuration>(json) ?? new Configuration();
        }
        catch (Exception e)
        {
            if (e is not (FileNotFoundException or JsonException))
                throw;
            
            Configuration = new Configuration();
            SaveConfiguration();
        }
    }

    private string GetDefaultConfigPath()
    {
        if (OperatingSystem.IsWindows())
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appData, _serviceName, _configFileName);
        }
        
        if (OperatingSystem.IsLinux())
        {
            string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Combine(home, ".config", _serviceName, _configFileName); 
        }

        throw new PlatformNotSupportedException();
    }
    
    public void SaveConfiguration()
    {
        string json = JsonSerializer.Serialize(Configuration);
        File.WriteAllText(ConfigPath, json);
    }
}