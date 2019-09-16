using System.Collections.Generic;

namespace MVC.Models
{
    public class ValuesModel
    {
        public List<string> Values { get; set; }

        public ValuesModel()
        {
            Values = new List<string>();
        }
    }
}
