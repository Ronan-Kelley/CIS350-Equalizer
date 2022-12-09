namespace Equalizer
{
    public partial class EqualizerOptions : UserControl
    {
        private Button[] _nodes;
        private int _selectedNodeIndex; // index of node in _nodes
        private int _numOfNodes;

        private bool _moving;

        public Action<NodeData>? OnChanged = null;

        public EqualizerOptions() {
            InitializeComponent();

            // Setup
            _nodes = new Button[10];
            _selectedNodeIndex = 0;
            _numOfNodes = 0;
            _moving = false;
        }

        /// <summary>
        /// standard button factory method
        /// </summary>
        /// <returns>new button</returns>
        private Button CreateButton() {
            Button button = new() {
                Location = new Point(204, 140), // Scary magic numbers
                Name = "newNode",
                Size = new Size(15, 15),
                UseVisualStyleBackColor = true
            };
            return button;
        }

        /// <summary>
        /// create and register a new node
        /// </summary>
        private void CreateNode() {
            if (_numOfNodes == 10) {
                return;
            }

            // Setup for node
            Button newNode = CreateButton();
            newNode.Tag = _numOfNodes;
            newNode.MouseDown += new MouseEventHandler(SelectNode);
            newNode.MouseMove += new MouseEventHandler(MoveNode);
            newNode.MouseUp += new MouseEventHandler(StopMovingNode);

            // Adding node and setting it as selected
            _nodes[_numOfNodes] = newNode; // could add error checking here
            _selectedNodeIndex = _numOfNodes;
            Controls.Add(newNode);

            // node book-keeping
            _numOfNodes++;
        }

        /// <summary>
        /// delete a node at the given index
        /// 
        /// if there does not exist a node at the given index,
        /// or the index given is out of bounds, the function
        /// will do nothing and exit.
        /// </summary>
        /// <param name="index"></param>
        private void DeleteNode(int index) {
            // sanity check: is the index valid?
            if (index < 0 || index >= _numOfNodes) {
                return;
            }

            // delete the requested node
            Controls.Remove(_nodes[index]);

            // re-order the array such that the nodes are
            // placed one after another
            while (index < _numOfNodes - 1) {
                _nodes[index] = _nodes[index + 1];
                _nodes[index].Tag = index;
                index++;
            }

            // update node book keeping
            _numOfNodes--;
            // reset selected node index
            _selectedNodeIndex = 0;
        }

        /// <summary>
        /// allows for movement of the node; intended to be used by UI event system
        /// </summary>
        /// <param name="sender">standard UI event sender parameter</param>
        /// <param name="e"> standard UI event MouseEventArgs parameter</param>
        private void MoveNode(object sender, MouseEventArgs e) {
            // only update position if the node is moving
            if (_moving) {
                // calculate the new position
                Point newPos = PointToClient(Cursor.Position);
                // clamp the new position to acceptable bounds
                newPos.X = Math.Clamp(newPos.X - _nodes[_selectedNodeIndex].Width / 2, 28, 373);
                newPos.Y = Math.Clamp(newPos.Y - _nodes[_selectedNodeIndex].Height / 2, 30, 250);
                // update the position
                _nodes[_selectedNodeIndex].Location = newPos;
            }
        }

        /// <summary>
        /// not implemented; included due to object oriented contract
        /// </summary>
        /// <param name="sender">standard UI event sender parameter</param>
        /// <param name="e">standard UI event DragEventsArgs parameter</param>
        private void MoveNode(object sender, DragEventArgs e) {

        }

        /// <summary>
        /// enable nodes to stop moving; intended to be used by UI event system
        /// </summary>
        /// <param name="sender">standard UI event sender parameter</param>
        /// <param name="e">standard UI event MouseEventArgs parameter</param>
        private void StopMovingNode(object sender, MouseEventArgs e) {
            _moving = false;
            if (OnChanged != null) {
                OnChanged.Invoke(GetNodeData(_selectedNodeIndex));
            }
        }

        // [index, freq, q, gain]
        /// <summary>
        /// map the position of a node to the band values it represents
        /// </summary>
        /// <param name="index">index of the node to map</param>
        /// <returns>a NodeData struct containing the mapped values</returns>
        public NodeData GetNodeData(int index) {
            NodeData nodeData = new();

            // if the index is out of bounds, return an empty NodeData struct
            if (index < 0 || index >= _numOfNodes) {
                return nodeData;
            }

            // Map X and Y position from 0 to 1 linearly
            float xRatio = (float)(_nodes[index].Location.X - 28) / (373 - 28);
            float yRatio = (float)(_nodes[index].Location.Y - 30) / (250 - 30);

            // Map X from 62.5 to 16000 exponentially
            float freq = (float)(62.5 * Math.Pow(2, 8 * xRatio));
            // Need some way to change q?
            float q = 0.5f;
            // Map from Y from -20 to 20 linearly
            float gain = (float)((yRatio * 2) - 1) * 20;
            // Need to flip since Y increases downwards in UI coordinates
            gain *= -1;

            // I have no idea why it wouldn't work togther, but it wouldn't for me
            nodeData.SetIndex((int)_nodes[index].Tag);
            nodeData.SetFreq(freq);
            nodeData.SetQ(q);
            nodeData.SetGain(gain);

            return nodeData;
        }

        /// <summary>
        /// enable the selection of individual nodes; intended to be used by UI event system
        /// </summary>
        /// <param name="sender">standard UI event sender parameter</param>
        /// <param name="e">standard UI event MouseEventArgs parameter</param>
        private void SelectNode(object sender, MouseEventArgs e) {
            Button? node = sender as Button;
            if (node == null) {
                return;
            }

            int nodeIndex = (int)node.Tag;
            if (nodeIndex < 0 || nodeIndex >= _numOfNodes) {
                return;
            }

            _selectedNodeIndex = nodeIndex;
            _moving = true;
        }

        /// <summary>
        /// register the callback function for the "add node" button so that it does
        /// as its name suggests
        /// </summary>
        /// <param name="sender">standard UI event sender parameter</param>
        /// <param name="e">standard UI event EventArgs parameter</param>
        private void _btn_addnode_Click(object sender, EventArgs e) {
            CreateNode();
            NodeData data = GetNodeData(_selectedNodeIndex);
            if (OnChanged != null) {
                OnChanged.Invoke(data);
            }
        }

        /// <summary>
        /// register a callback function for the "delete node" button so that it
        /// does as its name suggests
        /// </summary>
        /// <param name="sender">standard UI event sender parameter</param>
        /// <param name="e">standard UI event EventArgs parameter</param>
        private void _btn_delnode_Click(object sender, EventArgs e) {
            // ensure at least one node exists to delete
            if (_numOfNodes <= 0) {
                return;
            }

            // remove filter from equalizer
            if (OnChanged != null) {
                NodeData data = GetNodeData(_selectedNodeIndex);
                data.SetDeleted(true);
                OnChanged.Invoke(data);
            }

            // delete the selected node from ui
            DeleteNode(_selectedNodeIndex);
        }
    }
}
