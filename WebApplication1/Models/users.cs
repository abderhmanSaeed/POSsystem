using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class users
    {
        [Key]
        public int id { set; get; }
        public string name { set; get; }
        public int? dep_id { set; get; }
        //public Boolean out	 { set; get; }
        public string password { set; get; }
        public string email { set; get; }
        public Boolean? is_admin { set; get; }
        public Boolean? IsPOSUser { set; get; }
        public int? MaxDiscount { set; get; }
        public Boolean? UserCanChangeItemPrices { set; get; }
        public int? FormId { set; get; }
        public int? ProgId { set; get; }
        public int? Levels_id { set; get; }
        public int? Stock_ShopCard_Id { set; get; }
        public Boolean? JustUseExactShop { set; get; }
        public int? Bransh_id { set; get; }
        public Boolean? JustUseExactBransh { set; get; }
        public Boolean? UserCannotSoldByNegative { set; get; }
        public Boolean? UserCannotSeeDocForOthers { set; get; }
        public Boolean? ProgramLanguage { set; get; }
        public Boolean? IsPOSUser_Market { set; get; }
        public List<users_perm> users_perm { set; get; }
    }
}
