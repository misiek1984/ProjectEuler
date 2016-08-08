namespace MK.StateSpaceSearch.DataStructures
{
    public class Config
    {
        public uint MaxNumberOfResults { get; set; }

        public bool ReturnStates { get; set; }

        public object DataForInitialState { get; set; }

        public bool PreserveHierarchy { get; set; }

        public Config()
        {
            PreserveHierarchy = true;
        }
    }
}
