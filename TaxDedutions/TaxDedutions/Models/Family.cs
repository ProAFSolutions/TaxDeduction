﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxDedutions.Models
{
    public class Family
    {
        public string Id { get; set; }
        public string Date { get; set; }
        public string Email { get; set; }
        public Family()
        {
        }
    }
}
