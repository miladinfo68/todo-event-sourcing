using Core.Application.Services;
using Core.Domain.Base;
using Dapper;

namespace OutboxWorker
{
    internal class WriteOutboxWorker : BackgroundService
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IEventProducer _producer;

        public WriteOutboxWorker(IDbConnectionFactory dbConnectionFactory,
            IEventProducer producer)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _producer = producer;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(30));

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                string sql = $@"         SELECT
                                          ""Id"",
                                          ""Type"",
                                          ""Data"",
                                          ""TopicName"",
                                          ""CreatedOn""
                                     FROM public.""OutboxMessages""
                                     ORDER BY ""Id""
                                     LIMIT 100 FOR UPDATE SKIP LOCKED; 
                                ";

                var connection = _dbConnectionFactory.GetOpenConnection();

                var messages = await connection.QueryAsync<OutboxMessage>(sql);

                var listOfIds = new List<long>();
                foreach (var outboxMessage in messages)
                {
                    try
                    {
                        await this._producer.ProduceEvent(outboxMessage.TopicName, outboxMessage.Data);

                        listOfIds.Add(outboxMessage.Id);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex?.Message);
                    }
                }

                if (listOfIds.Count > 0)
                {
                    var transaction = connection.BeginTransaction();
                    await connection.ExecuteAsync($@"DELETE FROM public.""OutboxMessages"" WHERE ""Id"" IN ('{string.Join("','", listOfIds)}')");
                    transaction.Commit();
                }
            }

        }
    }
}
