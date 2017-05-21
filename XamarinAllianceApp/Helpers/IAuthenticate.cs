using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinAllianceApp.Helpers
{
    public interface IAuthenticate
    {
        Task<bool> Authenticate();
    }
}
