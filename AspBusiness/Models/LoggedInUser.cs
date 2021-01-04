using AspDataModel.Contracts;

namespace AspBusiness.Models
{
    public class LoggedInUser
    {
        public string Username { get; set; }

        public Role Role { get; set; }
    }
}