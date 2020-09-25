using System;
using System.Collections.Generic;
using System.Text;

namespace Th_Bot
{
    class Weather_Model
    {
        public Coord coord { get; set; }
        public List<Weather> weather { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }    //Visibility, meter
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int timezone { get; set; } //Shift in seconds from UTC
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
}

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Coord    {
        public double lon { get; set; } 
        public double lat { get; set; } 
    }

    public class Weather    {
        public int id { get; set; }     //Weather condition id
        public string main { get; set; } 
        public string description { get; set; } 
        public string icon { get; set; } //Weather icon id
    }

    public class Main    {
        public double temp { get; set; } 
        public double feels_like { get; set; } 
        public double temp_min { get; set; } 
        public double temp_max { get; set; } 
        public int pressure { get; set; } 
        public int humidity { get; set; } 
    }

    public class Wind    {
        public double speed { get; set; } 
        public double deg { get; set; } 
    }

    public class Clouds    {
        public int all { get; set; } 
    }

    public class Sys    {
        public int type { get; set; } 
        public int id { get; set; } 
        public string country { get; set; }  // Country code (GB, JP etc.)
        public int sunrise { get; set; } 
        public int sunset { get; set; } 
    }
}
