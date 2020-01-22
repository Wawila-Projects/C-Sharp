using System;
using System.Collections.Generic;

namespace DataStructures.AVLTree
{
    /// <summary>
    /// AVL Tree is a self balancing binary tree. Every root node has a max of two children.
    /// The left one being a node with a value lower than the root node
    /// while the right node has a higher value than the root node.
    /// </summary>
    /// <typeparam name="T">Generic type. Must be IComparable to know which value is higher or lower than the current one.</typeparam>
    public class AVLTree<T> where T : IComparable
    {
        /// <summary>
        /// Root of the tree.
        /// </summary>
        private AVLTreeNode<T> root;

        /// <summary>
        /// Initializes a new instance of the <see cref="AVLTree{T}"/> class.
        /// </summary>
        /// <param name="value">Value of the original root node.</param>
        public AVLTree(T value)
        {
            Add(value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AVLTree{T}"/> class.
        /// </summary>
        /// <param name="values">Enumerable of data to add to the tree.</param>
        public AVLTree(IEnumerable<T> values)
        {
            foreach (var value in values)
            {
                Add(value);
            }
        }

        /// <summary>
        /// Adds a value to tree and rebalances it if necessary.
        /// Keeps the recurssion inside a local function given that it wil never be called elsewhere.
        /// </summary>
        /// <param name="value">Value to add to the tree.</param>
        public void Add(T value)
        {
            root = AddRecursion(value, root);

            AVLTreeNode<T> AddRecursion(T value, AVLTreeNode<T> node)
            {
                if (node == null)
                {
                    return new AVLTreeNode<T>(value);
                }

                if (value < node)
                {
                    node.Left = AddRecursion(value, node.Left);
                    if (node.Balance > 1 && node.Left != null)
                    {
                        if (value < node.Left)
                        {
                            return RotateRight(node);
                        }

                        return RotateLeftRight(node);
                    }
                }
                else if (value > node)
                {
                    node.Right = AddRecursion(value, node.Right);
                    if (node.Balance < -1 && node.Right != null)
                    {
                        if (value > node.Right)
                        {
                            return RotateLeft(node);
                        }

                        return RotateRightLeft(node);
                    }
                }

                return node;
            }
        }

        /// <summary>
        /// Removes a value to tree and rebalances it if necessary.
        /// Keeps the recurssion inside a local function given that it wil never be called elsewhere.
        /// </summary>
        /// <param name="value">Value to be removed from the tree.</param>
        public void Remove(T value)
        {
            root = RemoveRecursion(value, root);

            AVLTreeNode<T> RemoveRecursion(T value, AVLTreeNode<T> node)
            {
                if (node == null)
                {
                    return node;
                }

                if (value < node)
                {
                    node.Left = RemoveRecursion(value, node.Left);
                }
                else if (value > node)
                {
                    node.Right = RemoveRecursion(value, node.Right);
                }
                else if (node.Left != null && node.Right != null)
                {
                    var min = GetMinNode(node.Right);
                    node.Value = min.Value;
                    node.Right = RemoveRecursion(node.Value, node.Right);
                }
                else
                {
                    node = node.Right ?? node.Left;
                }

                if (node == null)
                {
                    return node;
                }

                var balance = node.Balance;

                if (balance < -1)
                {
                    var rightBalance = node.Right.Balance;
                    if (rightBalance > 0)
                    {
                        return RotateRightLeft(node);
                    }

                    return RotateLeft(node);
                }

                if (balance > 1)
                {
                    var leftBalance = node.Left.Balance;
                    if (leftBalance < 0)
                    {
                        return RotateLeftRight(node);
                    }

                    return RotateRight(node);
                }

                return node;
            }

            AVLTreeNode<T> GetMinNode(AVLTreeNode<T> node)
            {
                var current = node;
                while (current.Left != null)
                {
                    current = current.Left;
                }

                return current;
            }
        }

        /// <summary>
        /// Looks up a node with the value from the parameter in the tree. Takes advantages of the fact the the tree is always ordered.
        /// Keeps the recurssion inside a local function given that it wil never be called elsewhere.
        /// </summary>
        /// <param name="value">Value to look up inside the tree.</param>
        /// <returns>Returns a node if the value is in the tree or null if not found.</returns>
        public AVLTreeNode<T> FindNode(T value)
        {
            return GetNodeRecursion(value, root);

            AVLTreeNode<T> GetNodeRecursion(T value, AVLTreeNode<T> node)
            {
                if (node == null)
                {
                    return null;
                }

                if (value < node)
                {
                    return GetNodeRecursion(value, node.Left);
                }

                if (value > node)
                {
                    return GetNodeRecursion(value, node.Right);
                }

                return node;
            }
        }

        /// <summary>
        /// See if the tree contains node with the paramter as value.
        /// </summary>
        /// <param name="value">Value to look up inside the tree.</param>
        /// <returns>True if the tree contains the value or false if it does not.</returns>
        public bool Contains(T value)
        {
            return FindNode(value) != null;
        }

        /// <summary>
        /// Returns the highest value in tree taking advantage of how it is organized.
        /// </summary>
        /// <returns>Highest value in the tree.</returns>
        public T GetMax()
        {
            var current = root;
            while (current.Right != null)
            {
                current = current.Right;
            }

            return current.Value;
        }

        /// <summary>
        /// Returns the lowest value in tree taking advantage of how it is organized.
        /// </summary>
        /// <returns>Lowest value in the tree.</returns>
        public T GetMin()
        {
            var current = root;
            while (current.Left != null)
            {
                current = current.Left;
            }

            return current.Value;
        }

        /// <summary>
        /// Traverses each node of the tree in order and performs an action on it.
        /// <para>In order = Left -> Root -> Right. For example: 1 -> 2 -> 3.</para>
        /// </summary>
        /// <param name="action">Action to take on value of each node.</param>
        public void TraverseInOrder(Action<T> action)
        {
            InOrderRecursion(action, root);

            void InOrderRecursion(Action<T> action, AVLTreeNode<T> node)
            {
                if (node == null)
                {
                    return;
                }

                InOrderRecursion(action, node.Left);
                action(node.Value);
                InOrderRecursion(action, node.Right);
            }
        }

        /// <summary>
        /// Traverses each node of the tree in Pre order and performs an action on it.
        /// <para>Pre order = Root -> Left -> Right. For example: 2 -> 1 -> 3.</para>
        /// </summary>
        /// <param name="action">Action to take on value of each node.</param>
        public void TraversePreOrder(Action<T> action)
        {
            PreOrderRecursion(action, root);

            void PreOrderRecursion(Action<T> action, AVLTreeNode<T> node)
            {
                if (node == null)
                {
                    return;
                }

                action(node.Value);
                PreOrderRecursion(action, node.Left);
                PreOrderRecursion(action, node.Right);
            }
        }

        /// <summary>
        /// Traverses each node of the tree in post order and performs an action on it.
        /// <para>Post order = Left -> Right -> Root. For example: 1 -> 3 -> 2.</para>
        /// </summary>
        /// <param name="action">Action to take on value of each node.</param>
        public void TraversePostOrder(Action<T> action)
        {
            PostOrderRecursion(action, root);

            void PostOrderRecursion(Action<T> action, AVLTreeNode<T> node)
            {
                if (node == null)
                {
                    return;
                }

                PostOrderRecursion(action, node.Left);
                PostOrderRecursion(action, node.Right);
                action(node.Value);
            }
        }

        /// <summary>
        /// Returns the values of the tree in order.
        /// </summary>
        /// <returns>Values of the tree in order.</returns>
        public IList<T> GetValuesInOrder()
        {
            var arr = new List<T>();
            TraverseInOrder(t => arr.Add(t));

            return arr;
        }

        /// <summary>
        ///  Single rotation.
        /// </summary>
        private AVLTreeNode<T> RotateRight(AVLTreeNode<T> node)
        {
            var temp = node.Left;
            node.Left = temp.Right;
            temp.Right = node;
            return temp;
        }

        /// <summary>
        ///  Single rotation.
        /// </summary>
        private AVLTreeNode<T> RotateLeft(AVLTreeNode<T> node)
        {
            var temp = node.Right;
            node.Right = temp.Left;
            temp.Left = node;
            return temp;
        }

        /// <summary>
        ///  Double rotation.
        /// </summary>
        private AVLTreeNode<T> RotateLeftRight(AVLTreeNode<T> node)
        {
            node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }

        /// <summary>
        ///  Double rotation.
        /// </summary>
        private AVLTreeNode<T> RotateRightLeft(AVLTreeNode<T> node)
        {
            node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }
    }
}
