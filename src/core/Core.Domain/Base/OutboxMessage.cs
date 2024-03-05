using Core.Domain.Abstractions;

namespace Core.Domain.Base
{
    public class OutboxMessage : EntityBase, IAggregateRoot
    {
        public string Type { get; set; }
        public string Data { get; set; }
        public string TopicName { get; set; }

        private OutboxMessage()
        {

        }

        private OutboxMessage(string type, string data, string topicName)
        {
            Type = type;
            Data = data;
            TopicName = topicName;
        }

        public static OutboxMessage Create(string type, string data, string topicName)
        {
            return new OutboxMessage(type, data, topicName);
        }
    }
}
