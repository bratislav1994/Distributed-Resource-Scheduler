//-----------------------------------------------------------------------
// <copyright file="ProductionHistory.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
// <summary>Class that implements callback interface for WCF communication.</summary>
//-----------------------------------------------------------------------

namespace KSRes.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProductionHistory
    {
        private int id;
        private String username;
        private String mrid;
        private double activePower;
        private DateTime timeStamp;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

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

        public double ActivePower
        {
            get
            {
                return activePower;
            }

            set
            {
                activePower = value;
            }
        }

        public string MRID
        {
            get
            {
                return mrid;
            }

            set
            {
                mrid = value;
            }
        }

        public DateTime TimeStamp
        {
            get
            {
                return timeStamp;
            }

            set
            {
                timeStamp = value;
            }
        }
    }
}
