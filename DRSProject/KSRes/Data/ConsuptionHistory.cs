using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSRes.Data
{
    public class ConsuptionHistory
    {
        private String username;
        private double consuption;

        [Key]
        public String Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
            }
        }

        public double Consuption
        {
            get
            {
                return consuption;
            }

            set
            {
                consuption = value;
            }
        }
    }
}
