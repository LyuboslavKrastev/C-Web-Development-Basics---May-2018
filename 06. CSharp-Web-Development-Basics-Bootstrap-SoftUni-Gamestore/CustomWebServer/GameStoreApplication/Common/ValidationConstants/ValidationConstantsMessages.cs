namespace CustomWebServer.GameStoreApplication.Common.ValidationConstants
{
    public abstract class ValidationConstantsMessages
    {
        public const string InvalidMinLengthErrorMessage = "{0} must be at least {1} symbols long.";
        public const string InvalidMaxLengthErrorMessage = "{0} must not be more than {1} symbols long.";
        public const string ExactLengthErrorMessage = "{0} must not be exactly {1} symbols long.";

    }
}
