using System.ComponentModel.DataAnnotations.Schema;

namespace Team12EUP.Entity
{
    public class HistoryTest
    {
        public Guid id { get; set; }
        public int Mark { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
    }
}
