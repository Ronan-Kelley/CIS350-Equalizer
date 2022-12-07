using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Equalizer
{
    public partial class EqualizerOptions : UserControl
    {
        private Button[] _nodes;
        private int _selectedNodeIndex; // index of node in _nodes
        private int _numOfNodes;

        private bool _moving;

        public EqualizerOptions() {
            InitializeComponent();
            
            // Setup
            _nodes = new Button[10];
            _selectedNodeIndex = 0;
            _numOfNodes = 0;
            _moving = false;
        }

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

            // Adding node
            Controls.Add(newNode);
            _nodes[_numOfNodes] = newNode; // could add error checking here
            _numOfNodes++;
        }

        private Button CreateButton() {
            Button button = new();
            button.Location = new Point(100, 100); // TODO could make dynamic
            button.Name = "newNode";
            button.Size = new Size(15, 15);
            button.UseVisualStyleBackColor = true;
            return button;
        }

        private void DeleteNode(int index) {
            if (index < 0 || index >= _numOfNodes) {
                return;
            }

            Controls.Remove(_nodes[index]);

            while (index < _numOfNodes - 1) {
                _nodes[index] = _nodes[index + 1];
                _nodes[index].Tag = index;
                index++;
            }

            _numOfNodes--;
            _selectedNodeIndex = 0;
        }

        private void MoveNode(object sender, MouseEventArgs e) {
            if (_moving) {
                Point newPos = PointToClient(Cursor.Position);
                newPos.X -= _nodes[_selectedNodeIndex].Width / 2;
                newPos.Y -= _nodes[_selectedNodeIndex].Height / 2;
                _nodes[_selectedNodeIndex].Location = newPos;
            }
        }

        // I don't honestly know why we need this, but it won't compile without it
        private void MoveNode(object senedr, DragEventArgs e) {
            
        }

        private void StopMovingNode(object sender, MouseEventArgs e) {
            _moving = false;
        }

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



        private void _btn_addnode_Click(object sender, EventArgs e) {
            CreateNode();
        }

        private void _btn_delnode_Click(object sender, EventArgs e) {
            if (_numOfNodes <= 0) {
                return;
            }

            DeleteNode(_selectedNodeIndex);
        }
    }
}
