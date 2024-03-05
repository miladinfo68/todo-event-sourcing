namespace Core.Domain.Base
{
    public abstract class EntityBase
    {
        public long Id { get; set; }
        public DateTime CreatedOn { get; set; }

        protected EntityBase()
        {
            CreatedOn = DateTime.Now;
        }
    }
}
