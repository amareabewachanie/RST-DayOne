namespace DayOne.API.Model
{
    public class RegisterUserDto
    {
        /// <summary>
        /// First Name of the user
        /// </summary>
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string DateOfBirth { get; set; }

    }
}
