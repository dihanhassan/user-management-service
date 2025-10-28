using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserVault.Application.Interfaces
{
    public interface ICacheService : IEnumerable<KeyValuePair<string, object>>
    {
        bool TryGetValue<T>(string key, out T value);
        bool Set<T>(string key, T value, DateTimeOffset options);
        void Clear(string pattern);
    }
}
