using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CategoryName { get; set; }
        public string? ProductName
        {
            get { return Name; }
            set { Name = value; }

        }
        public Category category { get; set; }
       
    }
}
