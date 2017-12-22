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

        public const string ErrorMessageUserDontExist = "There is no such user.";
        public const string ErrorMessageAdvertisementDontExist = "There is no such avertisement.";
        public const string ErrorMessageCategoryDontExist = "There is no such category.";
        public const string ErrorMessagePictureDontExist = "There is no such picture.";
        public const string ErrorMessageNotAllowedToPage = "You are not allowed to that page.";
        public const string ErrorMessageThreePictureAllowed = "Cannot put more than 3 pictures in a single advertisement.";
        public const string ErrorMessageMessageToYourself = "You cannot send messages to yourself.";
        public const string ErrorMessageMessageDontExists = "There is no such message.";

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

        public const string ModelStateCustomError = "Invalid identity details.";
    }
}