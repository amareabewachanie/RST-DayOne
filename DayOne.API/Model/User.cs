using Microsoft.AspNetCore.Identity;

namespace DayOne.API.Model
{
    public class User:IdentityUser
    {
        public string FirstName { get;set; }    
        public string LastName { get;set; }
        public DateTime DateOfBirth { get;set; }
        public User(string firstName,string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
