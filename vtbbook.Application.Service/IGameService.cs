using System;
using vtbbook.Application.Service.Models;

namespace vtbbook.Application.Service
{
    public interface IGameService
    {
        void End(Guid userId, GameEndStatus status);
    }
}