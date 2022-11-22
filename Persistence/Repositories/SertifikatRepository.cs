using System.ComponentModel.DataAnnotations;

namespace gisAPI.Persistence.Repositories
{
    public class SertifikatRepository
    {
        public long ID { get; set; }
        [Key]
 
        public string IDBRG { get; set; }
        public string UNITKEY { get; set; }
        public string ASETKEY { get; set; }
        public string KDKIB { get; set; }
        public string NOSERTIFIKAT { get; set; }
        public DateTime TANGGAL { get; set; }
        public string ALAMAT { get; set; }
        public string LUAS { get; set; }
        public string URLIMG { get; set; }
        public string KET { get; set; }
    }
}