﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCXAirAPI.Domain.Models.Flight
{
    public class Flight : Entity.Entity
    {
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public double? Price { get; set; }
        public Guid TransportId { get; set; }
        [ForeignKey("TransportId")]
        public Transport.Transport? Transport { get; set; }
    }
}
