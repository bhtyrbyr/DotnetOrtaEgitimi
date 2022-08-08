using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Sale
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SaleId { get; set; }
        public int CustomerId { get; set; }
        public int MovieIds { get; set; }
        public int Price { get; set; }
        public DateTime BoughtDate { get; set; }
    }
}