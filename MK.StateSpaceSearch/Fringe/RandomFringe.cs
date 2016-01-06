using System;
using System.Collections.Generic;
using MK.StateSpaceSearch.DataStructures;

namespace MK.StateSpaceSearch.Fringe
{
    public class RandomFringe<TCustomData> : Fringe<TCustomData>
    {
        private readonly List<State<TCustomData>> _fringe = new List<State<TCustomData>>();

        private Random _rand = new Random((int)DateTime.Now.Ticks);

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
            get 
            { 
                var index = _rand.Next(0, Count - 1);
                var res = _fringe[index];
                _fringe.RemoveAt(index);
                return res;
            }
        }

        public override void Add(State<TCustomData> s)
        {
            _fringe.Add(s);
        }
    }
}
