﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class Platform
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Slug { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Product> ProductsNavigation { get; set; } = new List<Product>();
}