namespace Kafka.Domain
{
    public record KafkaProducerModel
    {
        public string Message { get; set; }
        public string ExtraData { get; set; }

    }
}
