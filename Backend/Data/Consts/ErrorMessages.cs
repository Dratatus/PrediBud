namespace Backend.Data.Consts
{
    public static class ErrorMessages
    {
        public const string InvalidEmailOrPassword = "Invalid email or password.";
        public const string EmailRequired = "Email is required.";
        public const string PasswordRequired = "Password is required.";
        public const string InvalidEmailFormat = "Invalid email format.";
        public const string NameRequired = "Name is required.";
        public const string PhoneRequired = "Phone is required.";
        public const string AddressRequired = "Address is required.";
        public const string CityRequired = "City is required.";
        public const string PostCodeRequired = "Postcode is required.";
        public const string StreetNameRequired = "Street name is required.";
        public const string PositionRequired = "Position is required for workers.";
        public const string UserAlreadyExists = "User with this email already exists.";
        public const string UserNotFound = "User not found.";
        public const string UserHasMaterialOrders = "Cannot delete user with material orders.";
        public const string UserHasConstructionOrders = "Cannot delete user with construction orders.";
        public const string FailedToDeleteUser = "Failed to delete user.";
        public const string InvalidUserId = "Invalid user ID.";
    }
}
