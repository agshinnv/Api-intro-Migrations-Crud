namespace Task.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CratedDate { get; set; } = DateTime.Now;
    }
}
