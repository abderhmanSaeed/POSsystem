using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class AccountTree
    {
        [Key]
        public long Accounts_Id { get; set; }
        public string Accounts_Code { get; set; }
        public string Accounts_Name { get; set; }    
        public AccountTree Parent { get; set; }
        public ICollection<AccountTree> Children { get; set; }
    }
}
