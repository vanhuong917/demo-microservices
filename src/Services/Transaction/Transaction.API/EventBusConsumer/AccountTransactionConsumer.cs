using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using Transaction.API.Repositories.Interfaces;

namespace Transaction.API.EventBusConsumer
{
    public class AccountTransactionConsumer : IConsumer<AccountTransactionEvent>
    {
        private readonly ILogger<AccountTransactionConsumer> _logger;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public AccountTransactionConsumer(ILogger<AccountTransactionConsumer> logger, ITransactionRepository transactionRepository, IMapper mapper)
        {
            _logger = logger;
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<AccountTransactionEvent> context)
        {
            try
            {
                var transaction = _mapper.Map<Entities.Transaction>(context.Message);
                if (transaction is not null)
                {
                    await _transactionRepository.Add(transaction);
                    await context.ConsumeCompleted; // Gửi xác nhận về RabbitMQ
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message");
                // Không gửi xác nhận, RabbitMQ sẽ gửi lại thông điệp
            }
        }
    }
}
