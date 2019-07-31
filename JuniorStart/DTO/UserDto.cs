namespace JuniorStart.DTO
{
    public class UserDto
    {
        /// <summary>
        /// User id
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// first name
        /// </summary>
        /// <example>Jonathan</example>
        public string FirstName { get; set; }

        /// <summary>
        /// last name
        /// </summary>
        /// <example>Wick</example>
        public string LastName { get; set; }

        /// <summary>
        /// email
        /// </summary>
        /// <example>johnwick@example.com</example>
        public string Email { get; set; }

        /// <summary>
        /// login
        /// </summary>
        /// <example>johnWick</example>
        public string Login { get; set; }

        /// <summary>
        /// password
        /// </summary>
        /// <example>J0hnw!ck</example>
        public string Password { get; set; }
    }
}