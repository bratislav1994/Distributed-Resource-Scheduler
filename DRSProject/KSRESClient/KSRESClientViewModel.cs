using CommonLibrary;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KSRESClient
{
    public class KSRESClientViewModel
    {
        private Client client;
        public Client Client
        {
            get
            {
                if (client == null)
                {
                    client = new Client();
                }
                return client;
            }
            set
            {
                client = value;
            }
        }

    }
}
