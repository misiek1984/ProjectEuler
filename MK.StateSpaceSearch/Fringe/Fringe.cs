using System.Collections.Generic;
using MK.StateSpaceSearch.DataStructures;

namespace MK.StateSpaceSearch.Fringe
{
    public abstract class Fringe<TCustomData>
    {
        public abstract int Count { get; }

        public abstract State<TCustomData> Next { get; }

        public abstract bool IsEmpty { get; }

        public abstract void Add(State<TCustomData> s);

        public void AddRange(IEnumerable<State<TCustomData>> data)
        {
            foreach (var s in data)
                Add(s);
        }
    }
}
