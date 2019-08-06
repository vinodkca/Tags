using System;

namespace Tags.Model {
    public class Title {
        public int ID { get; set; }
        public string Description { get; set; }           
        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}