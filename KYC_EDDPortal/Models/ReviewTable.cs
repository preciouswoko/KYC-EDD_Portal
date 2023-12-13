using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KYC_EDDPortal.Models
{
    public class ReviewTable
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string RequestByUsername { get; set; }
        [StringLength(100)]
        public string RequestByName { get; set; }
        [StringLength(150)]
        public string RequestByemail { get; set; }
        [StringLength(50)]
        public string ToBeAuthroiziedBy { get; set; }

        public DateTime? AuthorizedDate { get; set; }

        [StringLength(50)]
        public string AuthorizedByUsername { get; set; }
        [StringLength(100)]
        public string AuthorizedByName { get; set; }
        [StringLength(150)]
        public string AuthorizedByEmail { get; set; }
        public string RequestId { get; set; }
        public string Status { get; set; }
        [StringLength(50)]
        public string ReviewerUsername { get; set; }
        [StringLength(100)]
        public string ReviewerName { get; set; }
        [StringLength(150)]
        public string ReviewerEmail { get; set; }
        public DateTime? ReviewDate { get; set; }
        public string ReviewStatus { get; set; }

    }
}
