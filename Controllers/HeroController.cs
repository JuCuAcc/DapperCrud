namespace DapperCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroController : ControllerBase
    {
        private readonly IConfiguration _config;

        public HeroController(IConfiguration config)
        {
            _config = config;
        }


        [HttpGet("{heroId}")]
        public async Task<ActionResult<List<Hero>>> GetAHero(int heroId)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var hero = await connection.QueryFirstAsync<Hero>("SELECT * FROM Hero WHERE Id = @Id",
                    new { Id = heroId });
                return Ok(hero);
            }

        }

        [HttpGet]
        public async Task<ActionResult<List<Hero>>> GetAllHeros()
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                IEnumerable<Hero> heroes = await SelectAllHeros(connection);
                return Ok(heroes);
            }

        }

        [HttpPost]
        public async Task<ActionResult<List<Hero>>> CreateHero(Hero hero)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.ExecuteAsync("INSERT INTO Hero (Name, FirstName, LastName, Place) VALUES (@Name, @FirstName, @LastName, @Place)", hero);
                return Ok(await SelectAllHeros(connection));
            }

        }


        [HttpPut]
        public async Task<ActionResult<List<Hero>>> UpdateHero(Hero hero)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.ExecuteAsync("UPDATE Hero SET Name = @Name, FirstName = @FirstName, LastName = @LastName, Place = @Place WHERE Id = @Id", hero);
                return Ok(await SelectAllHeros(connection));
            }

        }

        [HttpDelete("{heroId}")]
        public async Task<ActionResult<List<Hero>>> DeleteHero(int heroId)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var hero = await connection.ExecuteAsync("DELETE FROM Hero WHERE Id = @Id",
                    new { Id = heroId });
                return Ok(await SelectAllHeros(connection));
            }

        }

        private static async Task<IEnumerable<Hero>> SelectAllHeros(SqlConnection connection)
        {
            return await connection.QueryAsync<Hero>("SELECT * FROM Hero");
        }
    }
}
