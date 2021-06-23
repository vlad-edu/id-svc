namespace IdService.Core.Constants
{
    public static class RegexConstants
    {
        public const string Username = @"^[A-Za-z\d-]{5,20}$";
        public const string Phone = @"^\+?\d{10,13}$";
        public const string Password = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[_\W])[A-Za-z\d\W_]{8,64}$";
    }
}
