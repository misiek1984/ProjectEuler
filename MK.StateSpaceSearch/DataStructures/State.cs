using System;
using System.Collections.Generic;

namespace MK.StateSpaceSearch.DataStructures
{
    [Serializable]
    public class State<TCustomData>
    {
        #region Properties

        public TCustomData Data { get; set; }

        public double Cost { get; set; }

        public State<TCustomData> Parent { get; set; }

        public IList<State<TCustomData>> Children { get; set; }

        #endregion
    }
}
