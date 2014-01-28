using Microsoft.AspNet.Identity.EntityFramework;

namespace CULTMACEDONIA_v2.Models
{
 
    /// <summary>
    /// Custom User κλάση. Εδώ μπορούμε να προσθέσουμε και άλλα πεδία/properties σχετικά με
    /// το Profile ενός χρήστη της εφαρμογής μας. Αυτά σετάρονται είτε γίνεται login με τον
    /// ίδιο τον Provider της υπηρεσίας μας, είτε με κάποιον third party provider πχ. Google
    /// 
    /// Δημιουργούμε τα αντίστοιχα Profile Properties στα αντιστοιχα Models και όπου χρησι-
    /// μοποιούνται σε controllers αντικείμενα CultMacedoniaUser
    /// 
    /// You can add profile data for the user by adding more properties to your ApplicationUser 
    /// class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    /// </summary>
    public class CultMacedoniaUser : IdentityUser
    {
        /// <summary>
        /// Στο Profile του χρήστη μας κρατάμε και το email
        /// </summary>
        public string UserEmail { get; set; }

        public string GetUserEmail()
        {
            return this.UserEmail;
        }
    }

    public class CultMacedoniaUserDbContext : IdentityDbContext<CultMacedoniaUser>
    {
        public CultMacedoniaUserDbContext()
            : base("CultMacedoniaConnection")
        {

        }
    }
}