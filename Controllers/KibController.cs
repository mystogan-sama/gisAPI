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
        // public async Task<ActionResult<List<KibRepository>>> GetAllKibs()
        // {
        //     using var connection = new SqlConnection(_config.GetConnectionString("Default"));
        //     IEnumerable<KibRepository> kibs = await SelectAllKib(connection);
        //     return Ok(kibs);
        // }

        // private static async Task<IEnumerable<KibRepository>> SelectAllKib(SqlConnection connection)
        // {
        //     return await connection.QueryAsync<KibRepository>("select * from ASET_KIB");
        // }

        // [HttpGet("{KDKIB}{UNITKEY}{ASETKEY}")]
        public async Task<ActionResult<KibRepository>> GetKibs(string KDKIB,string UNITKEY, string ASETKEY)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));

                var kibDLok = await connection.QueryAsync<KibRepository>("select b.NMASET,  b.NMASET +' - '+ a.IDBRG as IDBRG from ASET_KIB a join DAFTASET b on a.ASETKEY=b.ASETKEY join DAFTUNIT c on a.UNITKEY=c.UNITKEY join JNSKIB d on a.KDKIB=d.KDKIB where b.ASETKEY LIKE '"+ASETKEY+"%'and c.UNITKEY LIKE '"+UNITKEY+"%' and d.KDKIB LIKE '"+KDKIB+"%'");
                    return Ok(kibDLok);

        }

        [HttpGet("{IDBRG}/{search}")]
        public async Task<ActionResult<KibRepository>> GetUnitKey(string search, string IDBRG)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            if (search != null)
            {
                var kibDLok = await connection.QueryAsync<KibRepository>("select TOP 10 b.IDBRG,b.ASETKEY,b.UNITKEY,b.KDKIB, a.NMASET,a.KDASET, a.NMASET as KDASET from ASET_KIB b join DAFTASET a on b.ASETKEY=a.ASETKEY where a.KDASET LIKE '%'+@search+'%'",
                    new { search = search });
                return Ok(kibDLok);
            }else{
                var kibDLok = await connection.QueryAsync<KibRepository>("select * from ASET_KIB where IDBRG = @IDBRG",
                    new { IDBRG = IDBRG  });
            return Ok(kibDLok);
            }

        }
    }
}