using System.ComponentModel.DataAnnotations;

namespace Agenda.Models
{
    public class Address
    {
        public int AddressID { get; set; }
        public int PersonID { get; set; }
        [Display(Name = "Address")]
        public string Direction { get; set; }
        [Display(Name = "ZIP Code")]
        public string ZIP { get; set; }
        public string Country { get; set; }
        public string State { get; set; }

        public virtual Person Person { get; set; }
    }
}