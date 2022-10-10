using System.ComponentModel.DataAnnotations;

namespace gisAPI.Persistence.Repositories
{
    public class KibLokRepository
    {
        [Key]
        public long ? ID { get; set; }
        public string IDBRG { get; set; }
        public string UNITKEY { get; set; }
        public string ASETKEY { get; set; }
        public string KET { get; set; }
        public string METODE { get; set; }
        public string LOKASI { get; set; }
        public DateTime ? DATECREATE { get; set; }
        public DateTime ? DATEUPDATE { get; set; }
        public string KDKIB { get; set; }
    }
}