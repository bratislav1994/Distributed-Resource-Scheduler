using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKRes.Data
{
    public class SiteEntity
    {
        private int dbID;
        private Site sEntity;

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

        public Site SEntity
        {
            get
            {
                return sEntity;
            }

            set
            {
                sEntity = value;
            }
        }
    }
}
