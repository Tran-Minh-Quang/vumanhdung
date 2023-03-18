using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team12EUP.Entity
{
    public class Video
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LinkVideo { get; set; }
        public string Description { get; set; }
        [ForeignKey("AdvertisementId")]
        public Guid AdvertisementId { get; set; }
        [ForeignKey("CourseId")]
        public Guid CourseId { get; set; }
    }
}
