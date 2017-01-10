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
        List<ConsuptionHistory> ReadConsuptions();
        bool AddProductions(ProductionHistory history);
        List<ProductionHistory> ReadProductions(DateTime condition);
        bool Registration(RegisteredService service);
        RegisteredService GetService(string username);
    }
}
