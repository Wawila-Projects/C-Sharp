using System.Collections.Generic;
using DataStructures.AVLTree;
using NUnit.Framework;

namespace DataStructures.Tests
{
    public static class AVLTreeTests
    {
        [Test]
        public static void TestGetValues()
        {
            var tree = new AVLTree<int>(new [] { 4, 2, 5, 1, 3, 0 });
            var arr = tree.GetValuesInOrder();

            Assert.AreEqual(arr, new [] { 0, 1, 2, 3, 4, 5 });
            Assert.AreEqual(tree.GetMin(), 0);
            Assert.AreEqual(tree.GetMax(), 5);
        }

        [Test]
        public static void TestAdd()
        {
            var tree = new AVLTree<int>(new [] { 4, 2, 5, 1, 3, 0 });
            tree.Add(7);
            tree.Add(6);
            
            var arr = tree.GetValuesInOrder();
            Assert.AreEqual(arr, new [] { 0, 1, 2, 3, 4, 5, 6, 7 });
        }

        [Test]
        public static void TestRemove()
        {
            var tree = new AVLTree<int>(new [] { 4, 2, 5, 1, 3, 0 });
            tree.Remove(4);
            tree.Remove(0);
            
            var arr = tree.GetValuesInOrder();
            Assert.AreEqual(arr, new [] { 1, 2, 3, 5 });
        }

         [Test]
        public static void TestFindNode()
        {
            var tree = new AVLTree<int>(new [] { 4, 2, 5, 1, 3, 0 });
            var foundNode = tree.FindNode(2);
            var notFoundNode = tree.FindNode(8);

            Assert.AreEqual(foundNode.Value, 2);
            Assert.IsNull(notFoundNode);
        }

         [Test]
        public static void TestContains()
        {
            var tree = new AVLTree<int>(new [] { 4, 2, 5, 1, 3, 0 });
        
            Assert.IsTrue(tree.Contains(2));
            Assert.IsFalse(tree.Contains(8));
        }

        [Test]
        public static void TestTraverse()
        {
            var tree = new AVLTree<int>(new [] { 1, 2, 3 });
            
            var inorder = new List<int>();
            tree.TraverseInOrder(t => inorder.Add(t));

            var preorder = new List<int>();
            tree.TraversePreOrder(t => preorder.Add(t));
            
            var postorder = new List<int>();
            tree.TraversePostOrder(t => postorder.Add(t));
        
            Assert.AreEqual(inorder, new [] { 1, 2, 3 });
            Assert.AreEqual(preorder, new [] { 2, 1, 3 });
            Assert.AreEqual(postorder, new [] { 1, 3, 2 });
        }
    }
}
