using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vtbbook.Application.Service;
using vtbbook.Application.Service.Models;

namespace vtbbook.Controllers
{
    [ApiController]
    public class GameConroller : VtbConrollerBase
    {
        private readonly IGameService _gameService;

        public GameConroller(IGameService gameService)
        {
            _gameService = gameService;
        }
        
        [Authorize]
        [Route("game/end")]
        [HttpPost]
        public IActionResult GameEnd([FromBody] string status)
        {
            GameEndStatus? statusRigth = status switch
            {
                "win" => GameEndStatus.Win,
                "lose" => GameEndStatus.Lose,
                _ => null,
            };

            if (statusRigth is null)
            {
                return Forbid();
            }

            _gameService.End(GetUserId(User), statusRigth.Value);
            return Ok();
        }
    }
}
