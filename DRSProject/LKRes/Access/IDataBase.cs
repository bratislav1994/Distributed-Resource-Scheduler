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

        void RemoveGenerator(Generator existingGenerator);
        void RemoveGroup(Group existingGroup);
        void RemoveSite(Site existingSite);

        void UpdateGenerator(Generator updateGenerator);
        void UpdateGroup(Group updateGroup);
        void UpdateSite(Site updateSite);

        UpdateInfo ReadData();
        bool AddMeasurement(Measurement newMeasurement);
        SortedDictionary<DateTime, double> GetMeasurements(string mRID);
    }
}
