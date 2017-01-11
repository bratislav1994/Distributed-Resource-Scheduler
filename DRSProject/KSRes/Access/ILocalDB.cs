//-----------------------------------------------------------------------
// <copyright file="ILocalDB.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
// <summary>Class that implements callback interface for WCF communication.</summary>
//-----------------------------------------------------------------------

namespace KSRes.Access
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using KSRes.Data;

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
