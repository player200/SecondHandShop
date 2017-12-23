namespace SecondHandShop.Web
{
    public class WebConstants
    {
        public const string AdministratorRole = "Administrator";
        public const string ModeratorRole = "Moderator";
        public const string UserRole = "Registered User";

        public const string TempDataSuccessMessageKey = "SuccessMessage";
        public const string TempDataErrorMessageKey = "ErrorMessage";

        public const string AdminArea = "Admin";
        public const string ModeratorArea = "Moderator";
        public const string EmptyArea = "";

        public const string HomeControllerName = "Home";
        public const string AdvertisementsControllerName = "Advertisements";
        public const string MessagesControllerName = "Messages";
        public const string AccountControllerName = "Account";

        public const string ErrorMessageUserDontExist = "There is no such user.";
        public const string ErrorMessageAdvertisementDontExist = "There is no such avertisement.";
        public const string ErrorMessageCategoryDontExist = "There is no such category.";
        public const string ErrorMessagePictureDontExist = "There is no such picture.";
        public const string ErrorMessageNotAllowedToPage = "You are not allowed to that page.";
        public const string ErrorMessageThreePictureAllowed = "Cannot put more than 3 pictures in a single advertisement.";
        public const string ErrorMessageMessageToYourself = "You cannot send messages to yourself.";
        public const string ErrorMessageMessageDontExists = "There is no such message.";
        public const string ErrorMessagePasswordResset = "A code must be supplied for password reset.";
        public const string ErrorMessageError = "Error";
        public const string ErrorMessageUnableToLoadUser = "Unable to load user with ID '{0}'.";
        public const string ErrorMessageLoadingExternalLogin = "Error loading external login information during confirmation.";
        public const string ErrorMessageEternalProvider = "Error from external provider: {0}";
        public const string ErrorMessageLoadTwoFaceAuthen = "Unable to load two-factor authentication user.";
        public const string ErrorMessageRecoveryCodeGenerate = "Cannot generate recovery codes for user with ID '{0}' as they do not have 2FA enabled.";
        public const string ErrorMessageDisable2F = "Unexpected error occured disabling 2FA for user with ID '{0}'.";
        public const string ErrorMessageExternamLogin = "Unexpected error occurred removing external login for user with ID '{0}'.";
        public const string ErrorMessageExternalAdding = "Unexpected error occurred adding external login for user with ID '{0}'.";
        public const string ErrorMessageLoadingExternal = "Unexpected error occurred loading external login info for user with ID '{0}'.";
        public const string ErrorMessageEmailSetting = "Unexpected error occurred setting email for user with ID '{0}'.";
        public const string ErrorMessagePhoneSetting = "Unexpected error occurred setting phone number for user with ID '{0}'.";
        public const string ErrorMessagePicutureWithNoPngOrJpg = "Your picure didn't upload correctly. The picture url should ends with '.jpg' or '.png'";

        public const string SuccessMessageAdvertisementEdit = "Successfully edited advertisement.";
        public const string SuccessMessageAdvertisementDelete = "Successfully deleted advertisement.";
        public const string SuccessMessagePictureAdd = "Three pictures were added successfully!";
        public const string SuccessMessagePicutreEdit = "Successfully edited picture.";
        public const string SuccessMessageAdvertisementCreate = "Advertisement {0} created successfully!";
        public const string SuccessMessageAdvertisementEditPlaceholder = "Successfully edited - {0} - advertisement.";
        public const string SuccessMessageAdvertisementDeletePlaceholder = "Successfully deleted - {0} - advertisement.";
        public const string SuccessMessageCategoryCreatePlaceholder = "Category {0} created successfully!";
        public const string SuccessMessageCategoryEditPlaceholder = "Category {0} successfully have been edited.";
        public const string SuccessMessageCatogoryDeletePlaceholder = "Category {0} successfully have been deleted.";
        public const string SuccessMessageRoleChangePlaceholder = "User {0} successfully is changed to {1} role.";
        public const string SuccessMessageMessageSendPlaceholder = "Message to {0} was successfully sended.";
        public const string SuccessMessageMessageDelet = "Message was successfully deleted.";
        public const string SuccessMessageConfirm = "ConfirmEmail";
        public const string SuccessMessageUserCreated = "User created an account using {Name} provider.";
        public const string SuccessMessageUserLogged = "User logged in with {Name} provider.";
        public const string SuccessMessageUserLogOut = "User logged out.";
        public const string SuccessMessageUserLoggedIn = "User logged in.";
        public const string SuccessMessageUserCreatedPassword = "User created a new account with password.";

        public const string WarningMessageUserLockedOut = "User account locked out.";
        public const string WarningMessageAuthenticationCode = "Invalid authenticator code entered for user with ID {UserId}.";
        public const string WarningMessageUserWithId = "User with ID {UserId} account locked out.";
        public const string WarningMessageUserWirh2f = "User with ID {UserId} logged in with 2fa.";
        public const string WarningMessageRecoveryCode = "Invalid recovery code entered for user with ID {UserId}";

        public const string InfformationMessageRecoveryCode = "User with ID {UserId} logged in with a recovery code.";
        public const string InfformationMessageRecoveryCode2F = "User with ID {UserId} has generated new 2FA recovery codes.";
        public const string InfformationMessageUserResset2F = "User with id '{UserId}' has reset their authentication app key.";
        public const string InfformationMessageUserEnable2F = "User with ID {UserId} has enabled 2FA with an authenticator app.";
        public const string InfformationMessageUserDisavle2F = "User with ID {UserId} has disabled 2fa.";
        public const string InfformationMessageChangedPassword = "User changed their password successfully.";

        public const string ModelStateCustomError = "Invalid identity details.";
        public const string ModelStateEmailError = "The email is already in use.";
        public const string ModelStateLogInError = "Invalid login attempt.";
        public const string ModelStateAuthenticationCode = "Invalid authenticator code.";
        public const string ModelStateRecoveryCode = "Invalid recovery code entered.";
        public const string ModelStateInvalidVerificationCode = "Verification code is invalid.";

        public const string StatusMessageLoginRemove = "The external login was removed.";
        public const string StatusMessageLoginAdded = "The external login was added.";
        public const string StatusMessagePasswordSet = "Your password has been set.";
        public const string StatusMessagePasswordChanged = "Your password has been changed.";
        public const string StatusMessageVarificationSend = "Verification email sent. Please check your email.";
        public const string StatusMessageProfileUpdated = "Your profile has been updated";

        public const string ReturnUrl = "ReturnUrl";
        public const string ExternalLogin = "ExternalLogin";
        public const string LoginProvider = "LoginProvider";

        public const string ControllerRouth = "[controller]/[action]";
        public const string AuthenticatorUrl = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
        public const string UseExceptionHandlerPath = "/Home/Error";
        public const string DefaultConnectionPath = "DefaultConnection";
        public const string Areas = "areas";
        public const string Default = "default";

        public const string AreasTemplate = "{area:exists}/{controller=Home}/{action=Index}/{id?}";
        public const string DefaultTemplate = "{controller=Home}/{action=Index}/{id?}";

        public const string Space = " ";
        public const string Dash = "-";

        public const string SecondHandShopToString = "SecondHandShop.Web";
        public const string ModelCode = "model.Code";

        public const string EndsWithJpg = ".jpg";
        public const string EndsWithPng = ".png";
    }
}