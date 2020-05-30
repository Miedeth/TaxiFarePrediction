using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaxiFareEstimator.Models;

namespace TaxiFareEstimator.Controllers
{
    public class EstimatesController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TaxiTrip trip)
        {
            TaxiFarePredictionEngine.Models.TaxiTrip taxitrip = new TaxiFarePredictionEngine.Models.TaxiTrip
            {
                VendorId = trip.VendorId.ToString(),
                RateCode = trip.RateCode,
                PassengerCount = trip.PassengerCount,
                TripTime = trip.TripTime,
                TripDistance = trip.TripDistance,
                PaymentType = trip.PaymentType.ToString()
            };

            TaxiFarePredictionEngine.PredictionEngine predictionEngine = new TaxiFarePredictionEngine.PredictionEngine();
            TaxiFarePredictionEngine.Models.TaxiTripFarePrediction prediction = predictionEngine.GetFarePrediction(taxitrip);

            trip.FareAmount = prediction.FareAmount;

            return View("View", trip);
        }
    }
}
