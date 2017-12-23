namespace SecondHandShop.Web.Models.AccountViewModels
{
    using SecondHandShop.Data;
    using System.ComponentModel.DataAnnotations;

    public class RegisterViewModel
    {
        [Required]
        [StringLength(DataConstants.UserNameMaxLenth)]
        [Display(Name = DataConstants.DisplayUserName)]
        public string Username { get; set; }

        [Required]
        [MinLength(DataConstants.NameMinLenth)]
        [MaxLength(DataConstants.NameMaxLenth)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = DataConstants.Email)]
        public string Email { get; set; }

        [Required]
        [StringLength(DataConstants.NewPasswordMaxLenth, ErrorMessage = DataConstants.ErrorMessageTwoFactorCode, MinimumLength = DataConstants.MinLenthOfStuffs)]
        [DataType(DataType.Password)]
        [Display(Name = DataConstants.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = DataConstants.DisplayConfirmPassword)]
        [Compare(DataConstants.Password, ErrorMessage = DataConstants.ErrorMessageConfirmPassword)]
        public string ConfirmPassword { get; set; }

        [Required]
        [MinLength(DataConstants.PhoneNumberMinLenth)]
        [MaxLength(DataConstants.PhoneNumberMaxLenth)]
        [Display(Name = DataConstants.DisplayPhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}