using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserVault.Domain.Enums
{
    [Flags]
    public enum StatusEnum : short
    {
        Created = 1,
        Success = 2,
        Failed = 4,
        Authenticated = 8,
        Authorized = 16
    }
}
