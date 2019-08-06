using System;
using System.Collections.Generic;

namespace Tags.Model {
    public class TagHierarchy {
       public int ID { get; set; }
       public int TitleID { get; set; }
       public int TitleTagID { get; set; }
       public int TitleTagTitleID { get; set; }
       public int TitleTagTitleTagID { get; set; }
       public int TitleTagTitleTagTitleID { get; set; }
       public int TitleTagTitleTagTitleTagID { get; set; } 
       public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}