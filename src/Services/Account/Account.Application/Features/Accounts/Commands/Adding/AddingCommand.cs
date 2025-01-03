﻿using MediatR;

namespace Account.Application.Features.Accounts.Commands.Adding
{
    public class AddingCommand : IRequest<Unit>
    {
        public Guid CustomerId { get; set; }
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
