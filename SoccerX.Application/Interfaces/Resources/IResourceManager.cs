using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerX.Application.Interfaces.Resources
{
    public interface IResourceManager
    {
        string GetString(string key);
        string GetString(string key, string defaultValue);
        string GetString(string key, params object?[] args);
        string GetCultureKey();
    }
}
