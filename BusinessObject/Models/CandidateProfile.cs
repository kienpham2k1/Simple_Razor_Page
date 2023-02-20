using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

#nullable disable

namespace BusinessObject.Models
{
    public partial class CandidateProfile
    {
        public string CandidateId { get; set; }
        [Required(ErrorMessage = "Fullname is required")]
        [StringLength(maximumLength: 200, MinimumLength = 12, ErrorMessage = "Value for candidate’s FullName is greater than 12 characters.")]
        public string Fullname { get; set; }
        [Required(ErrorMessage = "DateTime is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? Birthday { get; set; }
        [Required(ErrorMessage = "Profile Short Description is required")]
        [StringLength(maximumLength: int.MaxValue, MinimumLength =12, ErrorMessage = "Value for ProfileDescription from 12 – 200 characters")]
        public string ProfileShortDescription { get; set; }
        [Required(ErrorMessage = "Profile Url is required")]
        public string ProfileUrl { get; set; }
        [Required(ErrorMessage = "Posting is required")]
        public string PostingId { get; set; }
        public virtual JobPosting Posting { get; set; }
    }
}
