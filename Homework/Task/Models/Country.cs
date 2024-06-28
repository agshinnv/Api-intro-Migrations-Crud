using System.ComponentModel.DataAnnotations;

namespace Task.Models
{
    public class Country : BaseEntity
    {
        [Required]
        public string CountryName { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}
