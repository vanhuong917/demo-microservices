﻿using MediatR;

namespace Account.Application.Features.Accounts.Commands.Withdrawing
{
    public class WithdrawingCommand : IRequest<Unit>
    {
        public Guid CustomerId { get; set; }
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
