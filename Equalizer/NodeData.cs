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
        private byte Tag;
        private float Freq;
        private float Q;
        private float Gain;
        
        /// <summary>
        /// instantiate an empty NodeData struct; can be updated later with setters
        /// </summary>
        public NodeData()
        {
            _IsEmpty = true;
            Tag = 0;
            Freq = 0;
            Q = 0;
            Gain = 0;
        }

        /// <summary>
        /// instantiate a full NodeData struct
        /// </summary>
        /// <param name="Tag">Tag value</param>
        /// <param name="Freq">Frequency value</param>
        /// <param name="Q">Q value</param>
        /// <param name="Gain">Gain value</param>
        public NodeData(byte Tag, float Freq, float Q, float Gain)
        {
            _IsEmpty = false;
            this.Tag = Tag;
            this.Freq = Freq;
            this.Q = Q;
            this.Gain = Gain;
        }

        /// <summary>
        /// set the tag value of the node
        /// </summary>
        /// <param name="Tag">value to set</param>
        /// <returns>reference to self for chain calls</returns>
        public NodeData SetTag(byte Tag)
        {
            _IsEmpty = false;
            this.Tag = Tag;
            return this;
        }

        /// <summary>
        /// set the frequency value of the node
        /// </summary>
        /// <param name="Tag">value to set</param>
        /// <returns>reference to self for chain calls</returns>
        public NodeData SetFreq(float Freq)
        {
            _IsEmpty = false;
            this.Freq = Freq;
            return this;
        }

        /// <summary>
        /// set the Q value of the node
        /// </summary>
        /// <param name="Tag">value to set</param>
        /// <returns>reference to self for chain calls</returns>
        public NodeData SetQ(float Q)
        {
            _IsEmpty = false;
            this.Q = Q;
            return this;
        }

        /// <summary>
        /// set the gain value of the node
        /// </summary>
        /// <param name="Tag">value to set</param>
        /// <returns>reference to self for chain calls</returns>
        public NodeData SetGain(float Gain)
        {
            _IsEmpty = false;
            this.Gain = Gain;
            return this;
        }

        /// <summary>
        /// get the value of the node's tag
        /// </summary>
        /// <returns>the tag</returns>
        public byte GetTag()
        {
            return Tag;
        }

        /// <summary>
        /// get the value of the node's frequency
        /// </summary>
        /// <returns>the frequency</returns>
        public float GetFreq()
        {
            return Freq;
        }

        /// <summary>
        /// get the value of the node's Q
        /// </summary>
        /// <returns>the Q value</returns>
        public float GetQ()
        {
            return Q;
        }

        /// <summary>
        /// get the value of the node's gain
        /// </summary>
        /// <returns>the gain</returns>
        public float GetGain()
        {
            return Gain;
        }

        /// <summary>
        /// determine whether or not the struct is empty
        /// </summary>
        /// <returns>true if the struct is empty, false otherwise</returns>
        public bool IsEmpty()
        {
            return _IsEmpty;
        }

    }
}