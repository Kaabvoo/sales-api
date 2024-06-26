﻿using System.ComponentModel.DataAnnotations.Schema;

namespace jorge_api.Model
{
    public class SalesDetails : BaseModel
    {
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Products? Product { get; set; }
        public int SaleId { get; set; }
        [ForeignKey("SaleId")]
        public virtual Sales? Sale { get; set; }
    }
}
