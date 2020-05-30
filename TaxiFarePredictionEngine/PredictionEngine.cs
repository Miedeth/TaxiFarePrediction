using System;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Data;
using TaxiFarePredictionEngine.Models;

namespace TaxiFarePredictionEngine
{
    public class PredictionEngine
    {
        private ITransformer dataTransformer;
        private MLContext mlContext = new MLContext(seed: 0);

        public PredictionEngine()
        {            
            dataTransformer = Train();
        }

        public TaxiTripFarePrediction GetFarePrediction(TaxiTrip trip)
        {
            var predictionFunction = mlContext.Model.CreatePredictionEngine<TaxiTrip, TaxiTripFarePrediction>(dataTransformer);
            TaxiTripFarePrediction prediction = predictionFunction.Predict(trip);

            return prediction;
        }

        public RegressionMetrics Evaluate()
        {
            string testDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "taxi-fare-test.csv");
            IDataView dataView = mlContext.Data.LoadFromTextFile<TaxiTrip>(testDataPath, hasHeader: true, separatorChar: ',');

            IDataView predictions = dataTransformer.Transform(dataView);
            RegressionMetrics metrics = mlContext.Regression.Evaluate(predictions, "Label", "Score");

            return metrics;
        }

        private ITransformer Train()
        {
            string trainDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "taxi-fare-train.csv");

            IDataView dataView = mlContext.Data.LoadFromTextFile<TaxiTrip>(trainDataPath, hasHeader: true, separatorChar: ',');
            var pipeline = mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: "FareAmount")
                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "VendorIdEncoded", inputColumnName: "VendorId"))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "RateCodeEncoded", inputColumnName: "RateCode"))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "PaymentTypeEncoded", inputColumnName: "PaymentType"))
                .Append(mlContext.Transforms.Concatenate("Features", "VendorIdEncoded", "RateCodeEncoded", "PassengerCount", "TripDistance", "PaymentTypeEncoded"))
                .Append(mlContext.Regression.Trainers.FastTree());

            var model = pipeline.Fit(dataView);
            return model;
        }
    }
}
