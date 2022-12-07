﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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

            AllowDrop = true;
            DragDrop += new DragEventHandler(MoveNode);
            
            // Setup
            _nodes = new Button[10];
            _selectedNodeIndex = 0;
            _numOfNodes = 0;
            _moving = false;
        }

        private void CreateNode() {
            if (_numOfNodes >= 10) {
                return;
            }

            // Button component setup
            Button newNode = new();
            newNode.Location = new Point(100, 100); // TODO could make dynamic
            newNode.Name = "newNode";
            newNode.Size = new Size(15, 15);
            newNode.UseVisualStyleBackColor = true;
            newNode.AllowDrop = true;

            // Setup for tracking
            _selectedNodeIndex = _numOfNodes;
            newNode.Tag = _numOfNodes;
            _nodes[(int)(newNode.Tag)] = newNode; // could add error checking here
            newNode.MouseDown += new MouseEventHandler(SelectNode);
            newNode.MouseMove += new MouseEventHandler(MoveNode);
            newNode.MouseUp += new MouseEventHandler(StopMovingNode);

            Controls.Add(newNode);
            _numOfNodes++;
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
    }
}
