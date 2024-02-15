namespace Library.Data
{
    public static class DataConstants
    {
        //Book
        public const int BookTitltMinLength = 10;
        public const int BookTitltMaxLength = 50;

        public const int BookAuthorMinLength = 5;
        public const int BookAuthorMaxLength = 50;

        public const int BookDescriptionMinLength = 5;
        public const int BookDescriptionMaxLength = 5000;

        public const int BookRangeMinValue = 0;
        public const int BookRangeMaxValue = 10;

        //Category
        public const int CategoryNameMinLength = 5;
        public const int CategoryNameMaxLength = 50;

        //Error Messages
        public const string RequiredErrorMessage = "The field {0} is required";
        public const string StringLengthErrorMessage = "The field {0} must be between {2} and {1}";


    }
}
