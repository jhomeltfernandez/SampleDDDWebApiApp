using SampleDDDWebApiApp.Models.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleDDDWebApiApp.Models.Entities
{
    public class Currency : BaseEntity
    {
        public string Name { get; set; }
        public decimal Ratio { get; set; }
    }
}
