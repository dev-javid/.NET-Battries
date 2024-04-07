using Application.Users.Commands.AddUser;
using Application.Users.Queries.GetUsers;

namespace $safeprojectname$.Controllers
{
    public class UsersController : ApiBaseController
    {
        [HttpPost]
        public async Task<IActionResult> Post(AddUserCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetUsersQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
