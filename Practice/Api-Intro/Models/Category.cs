using System.ComponentModel.DataAnnotations;

namespace Api_Intro.Models
{
    public class Category : BaseEntity
    {
        [Required(ErrorMessage = "Bosh buraxmaq olmaz")]
        public string Name { get; set; }
    }
}
