using System.ComponentModel.DataAnnotations;

namespace gisAPI.Persistence.Repositories
{
    public class PenyewaRepository
    {
        [Key]
        public long ID { get; set; }
        public string IDBRG { get; set; }
        public string NOSERTIFIKAT { get; set; }
        public string NAMA { get; set; }
        public string ALAMAT { get; set; }
        public string LUAS { get; set; }
        public string PERUNTUKAN { get; set; }
        public string PERIODE { get; set; }
        public string BESARANSEWA { get; set; }
        public string URLIMG { get; set; }
        public string METODE { get; set; }
        public string LOKASI { get; set; }
        public string DESA { get; set; }
        public string NOHAKPAKAI { get; set; }
        public string TAHUNHAKPAKAI { get; set; }
        public string NOSKBUP { get; set; }
        public string NOMOU { get; set; }
        public bool STATUS { get; set; }
    }
}