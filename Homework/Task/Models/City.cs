using System.ComponentModel.DataAnnotations;

namespace Task.Models
{
    public class City : BaseEntity
    {
        [Required]
        public string CityName { get; set; }
        public Country Country { get; set; }
        public int CountryId { get; set; }
    }
}
