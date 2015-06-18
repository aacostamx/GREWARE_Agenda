using System.ComponentModel.DataAnnotations;

namespace Agenda.Models
{
    public class PersonInfo
    {
        public int PersonInfoID { get; set; }
        public int PersonID { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        [DisplayFormat(NullDisplayText = "None")]
        public string Website { get; set; }

        public virtual Person Person { get; set; }
    }
}