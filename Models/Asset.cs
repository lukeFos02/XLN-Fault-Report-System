﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XLN_Fault_Report_System.Models
{
    public class Asset
    {
        [Key]
        public int AssetId { get; set; } 
        public int Number { get; set; } 
        public string Name { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
    }
}
