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
        bool AddProductions(ProductionHistroy history);
        List<ProductionHistroy> ReadProductions(string username);
        bool Registration(RegisteredService service);
        RegisteredService GetService(string username);
    }
}
