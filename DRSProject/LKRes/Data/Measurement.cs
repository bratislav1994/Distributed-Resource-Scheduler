using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKRes.Data
{
    public class Measurement
    {
        private int dbID;
        private string mRID;
        private double activePower;
        private DateTime timeStamp;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DbID
        {
            get
            {
                return dbID;
            }

            set
            {
                dbID = value;
            }
        }

        public string MRID
        {
            get
            {
                return mRID;
            }

            set
            {
                mRID = value;
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
