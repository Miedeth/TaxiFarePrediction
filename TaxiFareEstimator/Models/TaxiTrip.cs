using System;
using System.ComponentModel.DataAnnotations;

namespace TaxiFareEstimator.Models
{
    public class TaxiTrip
    {
        [Required]
        public string VendorId { get; set; }

        [Required]
        public string RateCode { get; set; }

        [Range(1, 3)]
        public float PassengerCount { get; set; }

        [Range(1, 3000)]
        public float TripTime { get; set; }

        [Range(1, 30)]
        public float TripDistance { get; set; }

        [Required]
        public PaymentTypes PaymentType { get; set; }

        public float FareAmount { get; set; }
    }
}
