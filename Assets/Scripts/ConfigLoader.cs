using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Threading.Tasks;

public class ConfigLoader<T>
{
    private string _path;
    private string _name;

    public ConfigLoader(string path, string configName)
    {
        _path = path;
        _name = configName;
    }

    public async Task<T> Load()
    {
        using (var stream = new FileStream(_path + _name, FileMode.Open, FileAccess.Read, FileShare.None, 1024, FileOptions.Asynchronous))
        {
            var buffer = new byte[1024];
            await stream.ReadAsync(buffer, 0, (int)stream.Length);
            return JsonConvert.DeserializeObject<T>(System.Text.Encoding.Default.GetString(buffer));
        }
    }
}
