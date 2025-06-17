using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Common.DTO
{
    public class MemberDto
    {
        public string Id { get; set; }=Guid.NewGuid().ToString();

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100, ErrorMessage = "Full Name can't be longer than 100 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contact Number is required")]
        [Phone(ErrorMessage = "Invalid Contact Number")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Membership Type is required")]
        public string MembershipType { get; set; }

        [Required(ErrorMessage = "Membership Start Date is required")]
        [DataType(DataType.Date)]
        public DateTime? MembershipStartDate { get; set; }

        public List<IssueBookDto> IssueBooks { get; set; } = new();
    }
}