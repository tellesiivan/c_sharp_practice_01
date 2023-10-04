namespace HelloWorld.Models
{
    public class Computer
    {
        public int ComputerId
        {
            get; set;
        }
        // private string _motherboard;
        public string Motherboard
        {
            get; set;
        } = "";
        public string VideoCard
        {
            get; set;
        } = "";
        public int? CPUCores
        {
            get; set;
        }
        public bool HasWifi
        {
            get; set;
        }
        public bool HasLTE
        {
            get; set;
        }
        public DateTime ReleaseDate
        {
            get; set;
        }
        public decimal Price
        {
            get; set;
        }

        public Computer()
        {
            if (CPUCores == null)
            {
                CPUCores = 0;
            }
        }
    }

}