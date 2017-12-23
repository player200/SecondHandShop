namespace SecondHandShop.Web.Models.AccountViewModels
{
    using SecondHandShop.Data;
    using System.ComponentModel.DataAnnotations;

    public class LoginWith2faViewModel
    {
        [Required]
        [StringLength(DataConstants.CodeMaxLenth, ErrorMessage = DataConstants.ErrorMessageTwoFactorCode, MinimumLength = DataConstants.MinLenthOfStuffs)]
        [DataType(DataType.Text)]
        [Display(Name = DataConstants.DisplayAuthenticationCode)]
        public string TwoFactorCode { get; set; }

        [Display(Name = DataConstants.DisplayRememberThisMachine)]
        public bool RememberMachine { get; set; }

        public bool RememberMe { get; set; }
    }
}