using AspDataModel.Contracts;

namespace AspDataModel.Models
{
    public class User: IdBase, IUser
    {
        public string Username { get; set; }

        public Role Role { get; set; }

        public string FullName { get; set; }

        public string Password { get; set; }

        public bool Valid { get; set; }
    }
}