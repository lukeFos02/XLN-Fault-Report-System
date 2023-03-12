using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace XLN_Fault_Report_System.Models
{
    public class Fault
    {
        [Key]
        public int FaultId { get; set; }
        [ForeignKey(nameof(Asset.AssetId))]
        public int AssetId { get; set; }
        [ForeignKey(nameof(User.UserId))]
        public int UserId { get; set; }
        [Required]
        public string ContactName { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        [Required]
        public string ContactHoursFrom { get; set; }
        [Required]
        public string ContactHoursTo { get; set; }
        [Required]
        public string ServiceType { get; set; }
        [Required]
        public string IncidentType { get; set; }
        [AllowNull]
        public string? ErrorDescription { get; set; }
        [Required]
        public string IntermittentStatus { get; set; }
        [AllowNull]
        public string? IntermittentStatusDescription { get; set; }
        [Required]
        public string DiagnosticResult { get; set; }
        [Required]
        public string Time { get; set; }    
        [Required]
        public string Status { get; set; }
    }
}
