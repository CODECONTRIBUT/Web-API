﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class Genre
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Slug { get; set; }

    public int? GamesCount { get; set; }

    public string ImageBackground { get; set; }

    public string Description { get; set; }
}