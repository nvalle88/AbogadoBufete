#region Using

using Microsoft.AspNetCore.Identity;

#endregion

namespace SmartAdmin.Web.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}
