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
        public const string MaterialPriceNotFound = "Material price not found for the given type and material.";
        public const string InvalidBalconyDimensions = "Balcony dimensions (Length and Width) must be greater than zero.";
        public const string InvalidCeilingArea = "Ceiling area must be greater than zero.";
        public const string InvalidRailingMaterial = "Invalid railing material type.";
        public const string InvalidCeilingMaterial = "Invalid ceiling material type.";
        public const string SpecificationCannotBeNull = "Specification cannot be null.";
        public const string InvalidChimneyCount = "Invalid chimney count provided.";
        public const string InvalidSuspendedCeilingArea = "Invalid suspended ceiling area provided.";
        public const string InvalidSuspendedCeilingHeight = "Invalid suspended ceiling height provided.";
        public const string InvalidDoorsAmount = "Doors amount must be greater than zero.";
        public const string InvalidDoorsHeight = "Doors height must be greater than zero.";
        public const string InvalidDoorsWidth = "Doors width must be greater than zero.";
        public const string DoorsMaterialPricesNotFound = "Material prices not found for the given door type and material.";
        public const string ClosestMaterialPriceNotFound = "Closest material price not found for the given dimensions.";
        public const string InvalidFacadeSurfaceArea = "Facade surface area must be greater than zero.";
        public const string InsulationPriceNotFound = "Insulation price not found for the given type.";
        public const string FinishPriceNotFound = "Finish price not found for the given type.";
        public const string InvalidFlooringArea = "Flooring area must be greater than zero.";
        public const string FlooringMaterialPriceNotFound = "Material price not found for the given flooring material.";
        public const string InvalidFoundationLength = "Foundation length must be greater than zero.";
        public const string InvalidFoundationWidth = "Foundation width must be greater than zero.";
        public const string InvalidFoundationDepth = "Foundation depth must be greater than zero.";
        public const string FoundationMaterialPriceNotFound = "Material price not found for the given foundation type.";
        public const string InvalidInsulationArea = "Insulation area must be greater than zero.";
        public const string InvalidInsulationThickness = "Insulation thickness must be greater than zero.";
        public const string InsulationMaterialPricesNotFound = "No material prices found for the given insulation type.";
        public const string ClosestInsulationMaterialPriceNotFound = "No suitable material price found for the given insulation type and thickness.";
        public const string InvalidPaintingSurfaceArea = "Wall surface area must be greater than zero.";
        public const string InvalidNumberOfCoats = "Number of coats must be greater than zero.";
        public const string PaintingMaterialPriceNotFound = "Material price not found for the given paint type.";
        public const string InvalidWallSurfaceArea = "Wall surface area must be greater than zero.";
        public const string PlasteringMaterialPriceNotFound = "Material price not found for the given plaster type.";
        public const string InvalidNumberOfSteps = "Number of steps must be greater than zero.";
        public const string InvalidStaircaseHeight = "Staircase height must be greater than zero.";
        public const string InvalidStaircaseWidth = "Staircase width must be greater than zero.";
        public const string StaircaseMaterialPriceNotFound = "Material price not found for the given staircase material.";
        public const string InvalidVentilationSystemCount = "Ventilation system count must be greater than zero.";
        public const string VentilationSystemMaterialPriceNotFound = "Material price not found for the given ventilation system type.";
        public const string InvalidWallHeight = "Wall height must be greater than zero.";
        public const string InvalidWallWidth = "Wall width must be greater than zero.";
        public const string InvalidWallThickness = "Wall thickness must be greater than zero.";
        public const string LoadBearingWallMaterialPriceNotFound = "Material price not found for the given load-bearing wall type.";
        public const string PartitionWallMaterialPriceNotFound = "Material price not found for the given partition wall type.";
        public const string InvalidWindowAmount = "Window amount must be greater than zero.";
        public const string InvalidWindowHeight = "Window height must be greater than zero.";
        public const string InvalidWindowWidth = "Window width must be greater than zero.";
        public const string WindowsMaterialPricesNotFound = "Material prices not found for the given window type and material.";
        public const string InvalidRoofArea = "Roof area must be greater than zero.";
        public const string InvalidRoofPitch = "Roof pitch must be between 0 and 90 degrees.";
        public const string RoofMaterialPriceNotFound = "Material price not found for the given roof type and material.";

    }
}
