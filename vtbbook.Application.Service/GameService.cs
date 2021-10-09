using System;
using vtbbook.Application.Service.Models;

namespace vtbbook.Application.Service
{
    public class GameService : IGameService
    {
        private readonly IUserService _userService;

        public GameService(IUserService userService)
        {
            _userService = userService;
        }

        public void End(Guid userId, GameEndStatus status)
        {
            int currencyVolume = status switch
            {
                GameEndStatus.Win => 150,
                GameEndStatus.Lose => 50,
                _ => throw new NotImplementedException(),
            };

            _userService.AddCurrency(userId, currencyVolume);
        }
    }
}
