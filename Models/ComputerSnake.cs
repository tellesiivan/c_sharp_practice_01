namespace HelloWorld.Models
{
    public class ComputerSnake
    {
        public int computer_id
        {
            get; set;
        }
        // private string _motherboard;
        public string motherboard
        {
            get; set;
        } = "";
        public string video_card
        {
            get; set;
        } = "";
        public int? cpu_cores
        {
            get; set;
        }
        public bool has_wifi
        {
            get; set;
        }
        public bool has_lte
        {
            get; set;
        }
        public DateTime? release_date
        {
            get; set;
        }
        public decimal price
        {
            get; set;
        }

        public ComputerSnake()
        {
            if (cpu_cores == null)
            {
                cpu_cores = 0;
            }
        }
    }

}