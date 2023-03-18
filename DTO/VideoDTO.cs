using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Team12EUP.DTO
{
    public class AddVideo
    {
        public VideoDTO video { get; set; }
        public TestDTO test { get; set; }
        public List<QuestionDTO> questions { get; set; }
    }
    public class VideoDTO
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LinkVideo { get; set; }
        public string Description { get; set; }
        public Guid AdvertisementId { get; set; }
        [JsonIgnore]
        public Guid CourseId { get; set; }
    }
    public class TestDTO
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]

        public int TotalQuestion { get; set; }
        [JsonIgnore]

        public Guid VideoId { get; set; }
    }
    public class QuestionDTO
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Content { get; set; }
        public int Answer { get; set; }
        [JsonIgnore]

        public Guid TestId { get; set; }
        public string? Answer1 { get; set; }
        public string? Answer2 { get; set; }
        public string? Answer3 { get; set; }
        public string? Answer4 { get; set; }
    }
}
