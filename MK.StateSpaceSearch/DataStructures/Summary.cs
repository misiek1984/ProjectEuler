using System.Collections.Generic;

namespace MK.StateSpaceSearch.DataStructures
{
    public class Summary<TCustomData>
    {
        public int MaxNumberOfStates { get; set; }

        public int TotalNumberOfStates { get; set; }

        public IList<Result<TCustomData>> Results { get; private set; }

        public Summary()
        {
            Results = new List<Result<TCustomData>>();
        }
    }
}
