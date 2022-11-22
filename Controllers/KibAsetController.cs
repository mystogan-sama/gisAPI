using System.Data.SqlClient;
using Dapper;
using gisAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace gisAPI.Controllers
{
    public class KibAsetController: BaseController
    {
        private readonly IConfiguration _config;
        private IWebHostEnvironment _webHostEnvironment;
        public KibAsetController(IConfiguration config, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _config = config;
        }

        [HttpGet]
        public async Task<ActionResult<List<KibAsetRepository>>> GetAllKibAsets()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            IEnumerable<KibAsetRepository> kibBLoks = await SelectAllKibAset(connection);
            return Ok(kibBLoks);
        }

        private static async Task<IEnumerable<KibAsetRepository>> SelectAllKibAset(SqlConnection connection)
        {
            return await connection.QueryAsync<KibAsetRepository>("select top 10 a.IDBRG, b.NMASET,b.KDASET, c.NMUNIT from ASET_KIB a join DAFTASET b on a.ASETKEY=b.ASETKEY join DAFTUNIT c on a.UNITKEY=c.UNITKEY join JNSKIB d on a.KDKIB=d.KDKIB");
        }

        [HttpGet("kdkib/{KDKIB}")]
        public async Task<ActionResult<KibAsetRepository>> GetKibAsetbyKdKib(string KDKIB)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var kibAset = await connection.QueryAsync<KibAsetRepository>("select a.IDBRG,a.TAHUN, a.NOREG, a.DOKPEROLEHAN, a.TGLPEROLEHAN, a.URUTBRG, a.STATUSPENGGUNA ,b.ASETKEY, b.NMASET,b.KDASET,c.UNITKEY, c.NMUNIT, d.KDKIB from ASET_KIB a join DAFTASET b on a.ASETKEY=b.ASETKEY join DAFTUNIT c on a.UNITKEY=c.UNITKEY join JNSKIB d on a.KDKIB=d.KDKIB where  d.KDKIB LIKE '"+KDKIB+"%'",
                    new { ID = KDKIB });
            return Ok(kibAset);
        }

        [HttpGet("{KDKIB}/{UNITKEY}")]
        public async Task<ActionResult<KibAsetRepository>> GetKibAsetbyUnitKey(string KDKIB, string UNITKEY)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var kibAset = await connection.QueryAsync<KibAsetRepository>("select a.IDBRG,a.TAHUN, a.NOREG, a.DOKPEROLEHAN, a.TGLPEROLEHAN, a.URUTBRG, a.STATUSPENGGUNA ,b.ASETKEY, b.NMASET,b.KDASET,c.UNITKEY, c.NMUNIT, d.KDKIB from ASET_KIB a join DAFTASET b on a.ASETKEY=b.ASETKEY join DAFTUNIT c on a.UNITKEY=c.UNITKEY join JNSKIB d on a.KDKIB=d.KDKIB where d.KDKIB LIKE '"+KDKIB+"%' and c.UNITKEY LIKE '"+UNITKEY+"%'",
                    new { ID = UNITKEY });
            return Ok(kibAset);
        }

        [HttpGet("{KDKIB}/{UNITKEY}/{ASETKEY}")]
        public async Task<ActionResult<KibAsetRepository>> GetKibAsetbyAsetKey(string KDKIB, string UNITKEY, string ASETKEY)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("Default"));
            var kibAset = await connection.QueryAsync<KibAsetRepository>("select a.IDBRG,a.TAHUN, a.NOREG, a.DOKPEROLEHAN, a.TGLPEROLEHAN, a.URUTBRG, a.STATUSPENGGUNA ,b.ASETKEY, b.NMASET,b.KDASET,c.UNITKEY, c.NMUNIT, d.KDKIB from ASET_KIB a join DAFTASET b on a.ASETKEY=b.ASETKEY join DAFTUNIT c on a.UNITKEY=c.UNITKEY join JNSKIB d on a.KDKIB=d.KDKIB where d.KDKIB LIKE '"+KDKIB+"%' and c.UNITKEY LIKE '"+UNITKEY+"%' and b.ASETKEY LIKE '"+ASETKEY+"%'",
                    new { ID = ASETKEY });
            return Ok(kibAset);
        }

    }
}