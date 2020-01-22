using System;

namespace DataStructures.AVLTree
{
    /// <summary>
    /// Generic node class for AVL Tree.
    /// </summary>
    /// <typeparam name="T">Generic type. Must be IComparable to know which value is higher or lower than the current one.</typeparam>
    public class AVLTreeNode<T> where T : IComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AVLTreeNode{T}"/> class.
        /// </summary>
        /// <param name="value">Value to be stored in this node.</param>
        public AVLTreeNode(T value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets or sets the value stored on this node.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Gets or sets the reference to the Left node, the one with a lower Value.
        /// </summary>
        public AVLTreeNode<T> Left { get; set; }

        /// <summary>
        /// Gets or sets the reference to the Right node, the one with a higher Value.
        /// </summary>
        public AVLTreeNode<T> Right { get; set; }

        /// <summary>
        /// Gets the balance factor of the node.
        /// </summary>
        public int Balance => (Left?.Height ?? 0) - (Right?.Height ?? 0);

        /// <summary>
        /// Gets the height of the node.
        /// </summary>
        public int Height => 1 + Math.Max(Left?.Height ?? 0, Right?.Height ?? 0);

        /// <summary>
        /// Convenience overload. Compares a value of type T to the value in the node.
        /// </summary>
        /// <param name="value">Value to be compare.</param>
        /// <param name="node">Node to compare value to.</param>
        public static bool operator >(T value, AVLTreeNode<T> node)
        {
            if (node == null)
            {
                return false;
            }

            return value.CompareTo(node.Value) > 0;
        }

        /// <summary>
        /// Convenience overload. Compares a value of type T to the value in the node.
        /// </summary>
        /// <param name="value">Value to be compare.</param>
        /// <param name="node">Node to compare value to.</param>
        public static bool operator <(T value, AVLTreeNode<T> node)
        {
            if (node == null)
            {
                return false;
            }

            return value.CompareTo(node.Value) < 0;
        }
    }
}
