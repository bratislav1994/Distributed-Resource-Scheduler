using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary;
using CommonLibrary.Interfaces;

namespace KLRESClient
{
    public class LKClientService : ILKClient
    {
        public void Update(UpdateInfo update)
        {
            ClientDatabase.generators = update.Generators;
            ClientDatabase.groups = update.Groups;
            ClientDatabase.sites = update.Sites;
        }
    }
}
