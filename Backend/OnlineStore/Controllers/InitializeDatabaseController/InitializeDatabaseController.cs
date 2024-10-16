using Microsoft.AspNetCore.Mvc;
using OnlineStore.Repository.Base;

namespace OnlineStore.Controllers.InitializeDatabaseController
{
    [Route("api/v2/initialize/")]
    [ApiController]
    public class InitializeDatabaseController : ControllerBase
    {
        private readonly IPopulateDatabase _populateDatabase;
        public InitializeDatabaseController(IPopulateDatabase populateDatabase)
        {
            _populateDatabase = populateDatabase;
        }

        [HttpGet]
        [Route("start")]
        public async Task InitializeDatabase()
        {
            await _populateDatabase.Run();
        }
    }
}
