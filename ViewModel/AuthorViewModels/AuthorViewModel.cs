using System;
using System.ComponentModel.DataAnnotations;

namespace ViewModel.AuthorViewModels
{
    public class AuthorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please Enter Name.")]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Email.")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Please Enter Password.")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Please Enter Confirm Password.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter Mobile No")]
        [DataType(DataType.PhoneNumber)]
        public string Contact { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> JoinDate { get; set; }
        public Nullable<System.DateTime> LeaveDate { get; set; }
        public string AspnetUser { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public bool? Active { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
