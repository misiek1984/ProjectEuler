using System.Collections.Generic;

using MK.StateSpaceSearch.DataStructures;

namespace MK.StateSpaceSearch.Fringe
{
    public class BFSFringe<TCustomData> : Fringe<TCustomData>
    {
        private readonly Queue<State<TCustomData>> _fringe = new Queue<State<TCustomData>>();

        public override int Count 
        {
            get { return _fringe.Count; }
        }

        public override bool IsEmpty
        {
            get { return _fringe.Count == 0; }
        }

        public override State<TCustomData> Next
        {
            get { return _fringe.Dequeue(); }
        }

        public override void Add(State<TCustomData> s)
        {
            _fringe.Enqueue(s);
        }
    }
}
