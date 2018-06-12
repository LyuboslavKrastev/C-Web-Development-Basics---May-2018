namespace CustomWebServer.GameStoreApplication.Helpers
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class PasswordAttribute : ValidationAttribute
    {
        public PasswordAttribute()
        {
            this.ErrorMessage = "Your password should be at least six symbols long and should contain at least: one uppercase letter, one lowercase letter, one digit";
        }

        public override bool IsValid(object value)
        {
            var password = (string)value;

            if (password == null)
            {
                return true;
            }

            bool validPassword = 
                password.Any(s => char.IsUpper(s))
                && password.Any(s => char.IsLower(s))
                && password.Any(s => char.IsDigit(s));

            return validPassword;
        }
    }
}
