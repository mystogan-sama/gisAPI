using System.Data.SqlClient;
using Dapper;
using gisAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace gisAPI.Controllers
{
    public class DaftAsetController : BaseController
    {
        private readonly IConfiguration _config;
        private IWebHostEnvironment _webHostEnvironment;
        public DaftAsetController(IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _config = config;
        }

        [HttpGet]
        public async Task<ActionResult<List<DaftAsetRepository>>> GetAllAset()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            IEnumerable<DaftAsetRepository> asets = await SelectAllDaftAsets(connection);
            return Ok(asets);
        }

        [HttpGet("{KDASET}/{search}")]
        public async Task<ActionResult<DaftAsetRepository>> GetUnitKey(string search, string KDASET)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            if (search != null)
            {
                var kibDLok = await connection.QueryAsync<DaftAsetRepository>("select TOP 10 b.KDASET, b.NMASET, b.KDASET +' - '+ b.NMASET as KDASET from  DAFTASET b where b.KDASET LIKE '%'+@search+'%'",
                    new { search = search });
                return Ok(kibDLok);
            }else{
                var kibDLok = await connection.QueryAsync<DaftAsetRepository>("select * from ASET_KIB where KDASET = @KDASET",
                    new { KDASET = KDASET  });
            return Ok(kibDLok);
            }

        }

        //       [HttpGet("{ASETKEY}")]
        // public async Task<ActionResult<DaftAsetRepository>> GetAsetKib(string ASETKEY)
        // {
        //     using var connection = new SqlConnection(_config.GetConnectionString("Default"));
        //     var kibDLok = await connection.QueryAsync<DaftAsetRepository>("select * from ASET_KIB where ASETKEY = @ASETKEY",
        //             new { ASETKEY = ASETKEY  });
        //     return Ok(kibDLok);
        // }

        private static async Task<IEnumerable<DaftAsetRepository>> SelectAllDaftAsets(SqlConnection connection)
        {
            return await connection.QueryAsync<DaftAsetRepository>("select * from DAFTASET");
        }
    }
}