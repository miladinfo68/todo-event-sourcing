using Core.Domain.Abstractions;

namespace Core.Domain.Base
{
    public abstract class ProjectionEventBase : IProjectionEvent
    {
        public string Type { get; set; }

        protected ProjectionEventBase(string type)
        {
            this.Type = type;
        }
    }
}
