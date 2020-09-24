using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSoft 
{
    public interface IEngineStatus
    {
        int GetPendingCount();
        bool IsRequestPendig();
    }
}
