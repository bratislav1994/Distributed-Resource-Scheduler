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
    public class GeneratorEntity
    {
        private int dbID;
        private Generator gen;

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

        public Generator Gen
        {
            get
            {
                return gen;
            }

            set
            {
                gen = value;
            }
        }
    }
}
