namespace PaCoSe.Models
{
    public class UserProfile
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int UserId { get; set; }

        public string DisplayName
        {
            get
            {
                return $"{this.FirstName ?? string.Empty} {this.LastName ?? string.Empty}".Trim();
            }
        }
    }
}
