﻿    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace DataLayer
    {
        public class Product
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public int SupplierId { get; set; }
            public int CategoryId {  get; set; }
            public string? QuantityPerUnit { get; set; }
            public int UnitPrice { get; set; }
            public int UnitsInStock {  get; set; }
            public Category Category { get; set; }

            public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        }
    }
