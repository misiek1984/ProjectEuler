using System;
using System.Collections.Generic;

namespace MK.StateSpaceSearch.DataStructures
{
    [Serializable]
    public class Result<TCustomData>
    {
        private readonly List<TCustomData> _data = new List<TCustomData>();
        public IList<TCustomData> Data
        {
            get { return _data; }
        }

        private readonly List<State<TCustomData>> _states = new List<State<TCustomData>>();
        public IList<State<TCustomData>> States
        {
            get { return _states; }
        }
    }
}
