using System.Data.SqlClient;
using Dapper;
using gisAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace gisAPI.Controllers
{
    public class KibController : BaseController
    {
        private readonly IConfiguration _config;
        private IWebHostEnvironment _webHostEnvironment;
        public KibController(IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _config = config;
        }

        [HttpGet]
        public async Task<ActionResult<List<KibRepository>>> GetAllKibs()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            IEnumerable<KibRepository> kibs = await SelectAllKib(connection);
            return Ok(kibs);
        }

        private static async Task<IEnumerable<KibRepository>> SelectAllKib(SqlConnection connection)
        {
            return await connection.QueryAsync<KibRepository>("select * from ASET_KIB");
        }

        [HttpGet("{asetKey}")]
        public async Task<ActionResult<KibRepository>> GetKdKib(string asetKey)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var kib = await connection.QueryAsync<KibRepository>("select * from ASET_KIB where ASETKEY = @ASETKEY",
                    new { ASETKEY = asetKey });
            return Ok(kib);
        }
    }
}