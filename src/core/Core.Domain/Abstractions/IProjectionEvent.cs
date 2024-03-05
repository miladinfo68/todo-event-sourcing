namespace Core.Domain.Abstractions
{
    public interface IProjectionEvent
    {
        public string Type { get; set; }
    }
}
