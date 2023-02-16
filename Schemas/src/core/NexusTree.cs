//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace NexusFlow.src.core
//{
//    public class NexusTree
//    {
//        /// <summary>
//        /// The root node of the tree.
//        /// </summary>
//        private NexusNode _root;

//        /// <summary>
//        /// Creates a new NexusTree instance with the specified root node.
//        /// </summary>
//        /// <param name="root">The root node of the tree.</param>
//        public NexusTree(NexusNode root)
//        {
//            _root = root;
//        }

//        /// <summary>
//        /// Gets the node at the specified path.
//        /// </summary>
//        /// <param name="path">The path to the node, in the format "root/child/subchild".</param>
//        /// <returns>The node at the specified path, or null if the node does not exist.</returns>
//        public NexusNode GetNode(string path)
//        {
//            // Check if the provided path is equal to the root node's name
//            if (path == _root.Name)
//            {
//                // If it is, return the root node
//                return _root;
//            }

//            // Split the path into individual node names
//            string[] nodes = path.Split('/');

//            // Start at the root node
//            NexusNode currentNode = _root;

//            // Traverse the tree to the target node
//            for (int i = 1; i < nodes.Length; i++)
//            {
//                // Search for a child node with the correct name
//                NexusNode child = currentNode.Children.Find(n => n.Name == nodes[i]);
//                if (child == null)
//                {
//                    // If a child node with the correct name is not found, return null
//                    return null;
//                }
//                else
//                {
//                    // If a child node with the correct name is found, move to that node and continue traversing
//                    currentNode = child;
//                }
//            }

//            // Return the target node
//            return currentNode;
//        }

//        /// <summary>
//        /// Traverses the tree in infix order, executing a function for each node.
//        /// </summary>
//        /// <param name="node">The current node being processed.</param>
//        /// <param name="function">The function to execute for each node.</param>
//        public void TraverseInfix(NexusNode node, Action<NexusNode> function)
//        {
//            // Process the children of the current node
//            foreach (NexusNode child in node.Children)
//            {
//                TraverseInfix(child, function);
//            }

//            // Execute the function for the current node
//            function(node);
//        }

//        /// <summary>
//        /// Traverses the tree in postfix order, executing a function for each node.
//        /// </summary>
//        /// <param name="node">The current node being processed.</param>
//        /// <param name="function">The function to execute for each node.</param>
//        public void TraversePostfix(NexusNode node, Action<NexusNode> function)
//        {
//            // Process the children of the current node
//            foreach (NexusNode child in node.Children)
//            {
//                TraversePostfix(child, function);
//            }

//            // Execute the function for the current node
//            function(node);
//        }

//        /// <summary>
//        /// Traverses the tree in prefix order, executing a function for each node.
//        /// </summary>
//        /// <param name="node">The current node being processed.</param>
//        /// <param name="function">The function to execute for each node.</param>
//        public void TraversePrefix(NexusNode node, Action<NexusNode> function)
//        {
//            // Execute the function for the current node
//            function(node);

//            // Process the children of the current node
//            foreach (NexusNode child in node.Children)
//            {
//                TraversePrefix(child, function);
//            }
//        }

//        /// <summary>
//        /// Returns the root node of the tree.
//        /// </summary>
//        public NexusNode GetRoot()
//        {
//            return _root;
//        }

//    }

//}
