﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGR.Application.Dtos.Restaurant_and_Products.Products
{
    public record DisableProductDTO
    {
        public int IdProduct { get; set; }
        public string? DeletedBy { get; set; }
    }
}
