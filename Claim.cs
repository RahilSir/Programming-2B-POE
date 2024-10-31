using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMCS.Models
{
    public class Claim
    {
        [Key]
        [Column("claim_id")]
        public int ClaimId { get; set; }  // Match to claim_id in DB

        [Column("lecturer_name")]
        public string LecturerName { get; set; }

        [Column("hours_worked")]
        public double HoursWorked { get; set; }

        [Column("hourly_rate")]
        public double HourlyRate { get; set; }

        [Column("total_claim")]
        public double TotalClaim { get; set; }

        [Column("notes")]
        public string Notes { get; set; }

        [Column("supporting_document")]
        public string SupportingDocument { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
