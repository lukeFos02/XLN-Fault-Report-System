using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XLN_Fault_Report_System.Models
{
    public class Asset
    {
        [Key]
        public int AssetId { get; set; } 
        public string Number { get; set; } 
        public string Name { get; set; }

        [ForeignKey(nameof(User.UserId))]
        public int UserId { get; set; }
    }
}
