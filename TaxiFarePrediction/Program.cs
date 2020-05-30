using System;
using Microsoft.ML.Data;
using TaxiFarePredictionEngine.Models;

namespace TaxiFarePrediction
{
    class Program
    {

        static void Main(string[] args)
        {
            TaxiFarePredictionEngine.PredictionEngine predictionEngine = new TaxiFarePredictionEngine.PredictionEngine();

            Evaluate(predictionEngine);
            TestSinglePrediction(predictionEngine);
            Console.ReadLine();
        }

        private static void Evaluate(TaxiFarePredictionEngine.PredictionEngine predictionEngine)
        {
            RegressionMetrics metrics = predictionEngine.Evaluate();

            Console.WriteLine();
            Console.WriteLine($"*************************************************");
            Console.WriteLine($"*       Model quality metrics evaluation         ");
            Console.WriteLine($"*------------------------------------------------");
            Console.WriteLine($"*       RSquared Score:      {metrics.RSquared:0.##}");
            Console.WriteLine($"*       Root Mean Squared Error:      {metrics.RootMeanSquaredError:#.##}");
        }

        private static void TestSinglePrediction(TaxiFarePredictionEngine.PredictionEngine predictionEngine)
        {
            TaxiTrip taxiTripSample = new TaxiTrip()
            {
                VendorId = "VTS",
                RateCode = "1",
                PassengerCount = 1,
                TripTime = 1140,
                TripDistance = 3.75f,
                PaymentType = "CRD",
                FareAmount = 0
            };

            TaxiTripFarePrediction prediction = predictionEngine.GetFarePrediction(taxiTripSample);

            Console.WriteLine($"**********************************************************************");
            Console.WriteLine($"Predicted fare: {prediction.FareAmount:0.####}, actual fare: 15.5");
            Console.WriteLine($"**********************************************************************");
        }
    }
}
