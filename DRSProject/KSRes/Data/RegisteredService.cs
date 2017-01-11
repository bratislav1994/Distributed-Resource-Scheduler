//-----------------------------------------------------------------------
// <copyright file="RegisteredService.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
// <summary>Class that implements callback interface for WCF communication.</summary>
//-----------------------------------------------------------------------

namespace KSRes.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RegisteredService
    {
        private string username;
        private byte[] password;

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

        public byte[] Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }
    }
}
