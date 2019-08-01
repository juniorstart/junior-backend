namespace JuniorStart.ViewModels
{
    public class LoginRequest
    {
        /// <summary>
        /// User login
        /// </summary>
        /// <example>johnWick</example>
        public string Login { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        /// <example>J0hnw!ck</example>
        public string Password { get; set; }
    }
}