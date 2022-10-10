using System.Data.SqlClient;
using Dapper;
using gisAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace gisAPI.Controllers
{
    public class DaftUnitController : BaseController
    {
        private readonly IConfiguration _config;
        private IWebHostEnvironment _webHostEnvironment;
        public DaftUnitController(IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _config = config;
        }

        [HttpGet]
        public async Task<ActionResult<List<DaftUnitRepository>>> GetAllUnits()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            IEnumerable<DaftUnitRepository> units = await SelectAllDaftUnits(connection);
            return Ok(units);
        }

        private static async Task<IEnumerable<DaftUnitRepository>> SelectAllDaftUnits(SqlConnection connection)
        {
            return await connection.QueryAsync<DaftUnitRepository>("select * from DAFTUNIT");
        }

        [HttpGet("{UNITKEY}")]
        public async Task<ActionResult<KibRepository>> GetUnitKey(string UNITKEY)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var kibDLok = await connection.QueryAsync<KibRepository>("select * from ASET_KIB where UNITKEY = @UNITKEY",
                    new { UNITKEY = UNITKEY });
            return Ok(kibDLok);
        }
    }
}