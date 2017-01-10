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
    public class GroupEntity
    {
        private int dbID;
        private Group gEntity;

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

        public Group GEntity
        {
            get
            {
                return gEntity;
            }

            set
            {
                gEntity = value;
            }
        }
    }
}
