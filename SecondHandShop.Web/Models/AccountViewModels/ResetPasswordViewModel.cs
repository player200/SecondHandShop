namespace SecondHandShop.Web.Models.AccountViewModels
{
    using SecondHandShop.Data;
    using System.ComponentModel.DataAnnotations;

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(DataConstants.NewPasswordMaxLenth, ErrorMessage = DataConstants.ErrorMessageTwoFactorCode, MinimumLength = DataConstants.MinLenthOfStuffs)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = DataConstants.DisplayConfirmPassword)]
        [Compare(DataConstants.Password, ErrorMessage = DataConstants.ErrorMessageConfirmPassword)]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}