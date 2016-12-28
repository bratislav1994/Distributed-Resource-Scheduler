using CommonLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLRESClient
{
    public class ClientDatabase
    {
        private static ClientDatabase instance;
        private BindingList<Generator> generators;
        private BindingList<Site> sites;
        private BindingList<Group> groups;

        private ClientDatabase()
        {
            generators = new BindingList<Generator>();
            sites = new BindingList<Site>();
            groups = new BindingList<Group>();
        }

        public BindingList<Generator> Generators
        {
            get
            {
                return generators;
            }
            set
            {
                generators = value;
            }
        }

        public BindingList<Site> Sites
        {
            get
            {
                return sites;
            }
            set
            {
                sites = value;
            }
        }

        public BindingList<Group> Groups
        {
            get
            {
                return groups;
            }
            set
            {
                groups = value;
            }
        }

        public static ClientDatabase Instance()
        {
            if (instance == null)
            {
                instance = new ClientDatabase();
            }
            
            return instance;
        }
    }
}
