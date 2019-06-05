using System.ComponentModel.DataAnnotations;
namespace Sports.DomainModel.Models
{
    public class Test
    {
        [Key]
        public int Id { get; set; }
        public string Date { get; set; }
        public int Number { get; set; }
        public string Type { get; set; }
        public string CoachId { get; set; }
    }
}