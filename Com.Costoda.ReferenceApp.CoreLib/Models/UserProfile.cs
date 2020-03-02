using System;
namespace Com.Costoda.ReferenceApp.CoreLib.Models
{
    public class UserProfile
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string Title { get; set; }
        public string Department { get; set; }
        public string Bio { get; set; }
		public string ImageName { get; set; }
    }
}
