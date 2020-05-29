using Microsoft.ML.Data;

namespace TaxiFarePrediction
{
    public class TaxiTripFarePrediction
    {
        [ColumnName("Score")]
        public float FareAmount;
    }
}
