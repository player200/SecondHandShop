namespace SecondHandShop.Web.Models.AccountViewModels
{
    using SecondHandShop.Data;
    using System.ComponentModel.DataAnnotations;

    public class LoginWithRecoveryCodeViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = DataConstants.DisplayRecorveyCode)]
        public string RecoveryCode { get; set; }
    }
}