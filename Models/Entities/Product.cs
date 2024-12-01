﻿using Microsoft.EntityFrameworkCore;

namespace EcomPortal.Models.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Category { get; set; }
        public required decimal Price { get; set; }
    }
}
