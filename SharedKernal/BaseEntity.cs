namespace SharedKernal
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedOn { get; set; } = DateTime.Now;
       // public DateTime UpdatedOn { get; set; } not implemented
    }

  
}