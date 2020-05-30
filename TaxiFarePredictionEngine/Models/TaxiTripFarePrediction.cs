using Microsoft.ML.Data;

namespace TaxiFarePredictionEngine.Models
{
    public class TaxiTripFarePrediction
    {
        [ColumnName("Score")]
        public float FareAmount;
    }
}
