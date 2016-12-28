using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary;
using CommonLibrary.Interfaces;
using System.ComponentModel;

namespace KLRESClient
{
    public class LKClientService : ILKClient
    {
        public void Update(UpdateInfo update)
        {
            switch (update.UpdateType)
            {
                case UpdateType.ADD:
                    if (update.Generators != null)
                    {
                        ClientDatabase.Instance().Generators.Add(update.Generators[0]);
                    }
                    if (update.Groups != null)
                    {
                        ClientDatabase.Instance().Groups.Add(update.Groups[0]);
                    }
                    if (update.Sites != null)
                    {
                        ClientDatabase.Instance().Sites.Add(update.Sites[0]);
                    }
                    break;
                case UpdateType.REMOVE:
                    if (update.Generators != null)
                    {
                        ClientDatabase.Instance().Generators.Remove(update.Generators[0]);
                    }
                    if (update.Groups != null)
                    {
                        ClientDatabase.Instance().Groups.Remove(update.Groups[0]);
                    }
                    if (update.Sites != null)
                    {
                        ClientDatabase.Instance().Sites.Remove(update.Sites[0]);
                    }
                    break;
                case UpdateType.UPDATE:
                    if (update.Generators != null)
                    {
                        Generator gen = ClientDatabase.Instance().Generators.SingleOrDefault(p => p.MRID == update.Generators[0].MRID);
                        if (gen != null)
                        {
                            gen = update.Generators[0];
                        }
                    }
                    if (update.Groups != null)
                    {
                        Group group = ClientDatabase.Instance().Groups.SingleOrDefault(p => p.MRID == update.Groups[0].MRID);
                        if (group != null)
                        {
                            group = update.Groups[0];
                        }
                    }
                    if (update.Sites != null)
                    {
                        Site site = ClientDatabase.Instance().Sites.SingleOrDefault(p => p.MRID == update.Sites[0].MRID);
                        if (site != null)
                        {
                            site = update.Sites[0];
                        }
                    }
                    break;
            }
        }
    }
}
