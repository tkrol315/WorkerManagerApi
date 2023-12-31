﻿using MediatR;
using WorkerManager.Application.Dto;

namespace WorkerManager.Application.Commands
{
    public record RegisterUser(RegisterUserDto Dto) : IRequest<Unit>;
}
