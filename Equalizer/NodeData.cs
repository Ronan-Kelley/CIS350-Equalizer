namespace Equalizer
{
    /// <summary>
    /// struct to store properties of a node from EqualizerOptions
    /// </summary>
    public struct NodeData
    {
        // empty flag
        private bool _IsEmpty;

        // vars to hold respective datapoints
        private int Index;
        private float Freq;
        private float Q;
        private float Gain;

        private bool Deleted;

        /// <summary>
        /// instantiate an empty NodeData struct; can be updated later with setters
        /// </summary>
        public NodeData() {
            _IsEmpty = true;
            Index = 0;
            Freq = 0;
            Q = 0;
            Gain = 0;

            Deleted = false;
        }

        /// <summary>
        /// instantiate a full NodeData struct
        /// </summary>
        /// <param name="Index">Index value</param>
        /// <param name="Freq">Frequency value</param>
        /// <param name="Q">Q value</param>
        /// <param name="Gain">Gain value</param>
        public NodeData(int Index, float Freq, float Q, float Gain) {
            _IsEmpty = false;
            this.Index = Index;
            this.Freq = Freq;
            this.Q = Q;
            this.Gain = Gain;

            Deleted = false;
        }

        /// <summary>
        /// set the Index value of the node
        /// </summary>
        /// <param name="Index">value to set</param>
        /// <returns>reference to self for chain calls</returns>
        public NodeData SetIndex(int Index) {
            _IsEmpty = false;
            this.Index = Index;
            return this;
        }

        /// <summary>
        /// set the frequency value of the node
        /// </summary>
        /// <param name="Index">value to set</param>
        /// <returns>reference to self for chain calls</returns>
        public NodeData SetFreq(float Freq) {
            _IsEmpty = false;
            this.Freq = Freq;
            return this;
        }

        /// <summary>
        /// set the Q value of the node
        /// </summary>
        /// <param name="Index">value to set</param>
        /// <returns>reference to self for chain calls</returns>
        public NodeData SetQ(float Q) {
            _IsEmpty = false;
            this.Q = Q;
            return this;
        }

        /// <summary>
        /// set the gain value of the node
        /// </summary>
        /// <param name="Index">value to set</param>
        /// <returns>reference to self for chain calls</returns>
        public NodeData SetGain(float Gain) {
            _IsEmpty = false;
            this.Gain = Gain;
            return this;
        }

        public NodeData SetDeleted(bool deleted) {
            this.Deleted = deleted;
            return this;
        }

        /// <summary>
        /// get the value of the node's index
        /// </summary>
        /// <returns>the index</returns>
        public int GetIndex() {
            return Index;
        }

        /// <summary>
        /// get the value of the node's frequency
        /// </summary>
        /// <returns>the frequency</returns>
        public float GetFreq() {
            return Freq;
        }

        /// <summary>
        /// get the value of the node's Q
        /// </summary>
        /// <returns>the Q value</returns>
        public float GetQ() {
            return Q;
        }

        /// <summary>
        /// get the value of the node's gain
        /// </summary>
        /// <returns>the gain</returns>
        public float GetGain() {
            return Gain;
        }

        /// <summary>
        /// determine whether or not the struct is empty
        /// </summary>
        /// <returns>true if the struct is empty, false otherwise</returns>
        public bool IsEmpty() {
            return _IsEmpty;
        }

        public bool GetDeleted() {
            return Deleted;
        }
    }
}