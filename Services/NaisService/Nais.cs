using WeightingApp.Data.DatabaseModels;


namespace NaisService
{
    public class Nais
    {
        public Nais()
        {
            using(var context = new SQLContext() )
            {
                return;
            }
        }
    }
}