using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace BusinessLogicLayer.Entities;

public class Product
{
    public Guid ProductID { get; set; }
    public required string ProductName { get; set; }
    public required string Category { get; set; }
    public required double UnitPrice { get; set; }
    public required int QuantityInStock { get; set; }
}
