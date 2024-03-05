namespace Core.Infrastructure.DependencyOptions
{
    public class BrokerDependencyOptions
    {
        public ServiceType ServiceType { get; set; }
        public string BrokerAddress { get; set; }
        public string ConsumerGroupId { get; set; }
    }

    public enum ServiceType
    {
        Both = 0,
        Producer = 1,
        Consumer = 2
    }
}
