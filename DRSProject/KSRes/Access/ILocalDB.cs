using KSRes.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSRes.Access
{
    public interface ILocalDB
    {
        bool AddConsuption(ConsuptionHistory history);
        List<ConsuptionHistory> ReadConsuptions(string username);
    }
}
