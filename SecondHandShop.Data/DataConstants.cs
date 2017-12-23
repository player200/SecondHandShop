namespace SecondHandShop.Data
{
    public class DataConstants
    {
        public const int UsersNameMinLenth = 3;
        public const int UsersNameMaxLenth = 60;

        public const int PicturesUrlPathMinLenth = 10;
        public const int PicturesUrlPathMaxLenth = 700;

        public const int CategoriesNameMinLenth = 2;
        public const int CategoriesNameMaxLenth = 30;

        public const int AdvertisementsNameMinLenth = 3;
        public const int AdvertisementsNameMaxLenth = 100;

        public const int AdvertisementsDescriptionMinLenth = 3;
        public const int AdvertisementsDescriptionMaxLenth = 400;

        public const int MessagesContentMinLenth = 15;
        public const int MessagesContentMaxLenth = 400;

        public const int MinLenthOfStuffs = 6;

        public const int NewPasswordMaxLenth = 100;
        public const int CodeMaxLenth = 7;

        public const int UserNameMaxLenth = 50;
        public const int NameMinLenth = 3;
        public const int NameMaxLenth = 30;
        public const int PhoneNumberMinLenth = 6;
        public const int PhoneNumberMaxLenth = 11;

        public const string ImgDefoutNotFound = "http://bento.cdn.pbs.org/hostedbento-prod/filer_public/_bento_media/img/no-image-available.jpg";

        public const string DisplayUrlPathPicture = "Url Path";
        public const string DisplayFirstUrlPicture = "First picure";
        public const string DisplaySecondUrlPicture = "Second picure";
        public const string DisplayThirdUrlPicture = "Third picure";
        public const string DisplayUserName = "User name";
        public const string DisplayRememberMe = "Remember me?";
        public const string DisplayRememberThisMachine = "Remember this machine";
        public const string DisplayAuthenticationCode = "Authenticator code";
        public const string DisplayRecorveyCode = "Recovery Code";
        public const string DisplayPhoneNumber = "Phone number";
        public const string DisplayConfirmPassword = "Confirm password";
        public const string DisplayNewPassword = "New password";
        public const string DisplayConfirmNewPassword = "Confirm new password";
        public const string DisplayCode = "Verification Code";
        public const string DisplayCurrentPassword = "Current password";

        public const string ErrorMessageTwoFactorCode = "The {0} must be at least {2} and at max {1} characters long.";
        public const string ErrorMessageConfirmPassword = "The password and confirmation password do not match.";
        public const string ErrorMessageConfirmNewPassword = "The new password and confirmation password do not match.";

        public const string Password = "Password";
        public const string Email = "Email";
        public const string NewPassword = "NewPassword";
    }
}