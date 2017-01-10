using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LKRes.Data;
using CommonLibrary;

namespace LKRes.Access
{
    public interface IDataBase
    {
        bool AddGenerator(GeneratorEntity newGenerator);
        bool AddGroup(GroupEntity newGroup);
        bool AddSite(SiteEntity newSite);
        UpdateInfo ReadData();
    }
}
