using Microsoft.AspNetCore.Identity;

namespace Cal.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Event>? Events { get; set; }
        public ICollection<Category>? Categories { get; set; }
    }
}
