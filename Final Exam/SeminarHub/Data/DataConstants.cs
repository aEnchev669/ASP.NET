namespace SeminarHub.Data
{
    public static class DataConstants
    {
        //Seminar
        public const int SeminarTopicMinLength = 1;
        public const int SeminarTopicMaxLength = 100;

        public const int SeminarLecturerMinLength = 5;
        public const int SeminarLecturerMaxLength = 60;

        public const int SeminarDetailsMinLength = 10;
        public const int SeminarDetailsMaxLength = 500;

        public const string DateFormat = "dd/MM/yyyy HH:mm";

        public const int SeminarDurationMinRange = 30;
        public const int SeminarDurationMaxRange = 180;


        //Category
        public const int CategoryNameMinLength = 3;
        public const int CategoryNameMaxLength = 50;

        //Error Messages

        public const string RequiredErrorMessage = "The field {0} is required";
        public const string StringLengthErrorMessage = "The field {0} must be between {2} and {1}";
    }
}
