namespace PaCoSe.Models
{
    public class User
    {
        public User()
        {
            this.UserProfile = new UserProfile();
        }

        public int Id { get; set; }

        public string Sub { get; set; }

        public string Username { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}
