using System;
using System.Collections.Generic;

namespace CMCS
{
    public class Class1
    {
        // Static list to store the claims in memory
        private static List<Claim> claims = new List<Claim>();

        // Method to add a new claim to the repository
        public void AddClaim(Claim claim)
        {
            // Calculate the total claim
            claim.TotalClaim = claim.HoursWorked * claim.HourlyRate;

            // Add the claim to the list
            claims.Add(claim);
        }

        // Method to retrieve all claims in the system
        public List<Claim> GetClaims()
        {
            return claims;
        }

        // Method to retrieve claims by lecturer's name
        public List<Claim> GetClaimsByLecturer(string lecturerName)
        {
            return claims.FindAll(c => c.LecturerName.Equals(lecturerName, StringComparison.OrdinalIgnoreCase));
        }

        // Method to retrieve all pending claims for coordinators or managers to approve
        public List<Claim> GetPendingClaims()
        {
            return claims.FindAll(c => c.Status.Equals("Pending", StringComparison.OrdinalIgnoreCase));
        }

        // Method to approve a claim
        public void ApproveClaim(Claim claim)
        {
            Claim foundClaim = claims.Find(c => c.LecturerName == claim.LecturerName && c.SubmittedOn == claim.SubmittedOn);
            if (foundClaim != null)
            {
                foundClaim.Status = "Approved";
            }
        }

        // Method to reject a claim
        public void RejectClaim(Claim claim)
        {
            Claim foundClaim = claims.Find(c => c.LecturerName == claim.LecturerName && c.SubmittedOn == claim.SubmittedOn);
            if (foundClaim != null)
            {
                foundClaim.Status = "Rejected";
            }
        }

        // Method to get the total claim for all claims
        public double GetTotalClaim()
        {
            double total = 0;

            foreach (var claim in claims)
            {
                total += claim.TotalClaim; // Sum up the total claim for each claim
            }

            return total;
        }

        // Method to get the total claim for a specific lecturer
        public double GetTotalClaimForLecturer(string lecturerName)
        {
            double total = 0;

            foreach (var claim in claims.FindAll(c => c.LecturerName.Equals(lecturerName, StringComparison.OrdinalIgnoreCase)))
            {
                total += claim.TotalClaim; // Sum up the total claim for each claim belonging to the specific lecturer
            }

            return total;
        }
    }

    // The Claim class definition
    public class Claim
    {
        public string LecturerName { get; set; }
        public double HoursWorked { get; set; }
        public double HourlyRate { get; set; }
        public string Notes { get; set; }
        public string SupportingDocumentPath { get; set; }
        public string Status { get; set; }
        public DateTime SubmittedOn { get; set; }
        public double TotalClaim { get; set; } // Total claim amount
    }
}
