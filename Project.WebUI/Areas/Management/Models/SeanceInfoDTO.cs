namespace Project.WebUI.Areas.Management.Models
{
    public class SeanceInfoDTO
    {
        public int ID { get; set; }
        public string SaloonName { get; set; } //Salon adı
        public string MovieName { get; set; } //Filmin adı
        public string Date { get; set; } //Seans tarihi
        public List<string> Times { get; set; } //Seans saatleri


        //public string Time { get; set; } //Seans saati


        //public List<string> Dates { get; set; }
        //public List<string> Times { get; set; }
    }
}
