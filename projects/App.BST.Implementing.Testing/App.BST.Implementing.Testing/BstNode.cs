using System;

namespace App.BST.Implementing.Testing
{
    /// <summary>
    /// The BstNode class, for used internally by the Binary Search Tree (BST) class.
    /// </summary>
    /// <typeparam name="T">Generic data types.</typeparam>
    public class BstNode<T>
    {
        public T Data { get; set; }
        public BstNode<T> Left { get; set; }
        public BstNode<T> Right { get; set; }

        public BstNode(T newData)
        {
            this.Data = newData;
            this.Left = null;
            this.Right = null;
        }
    }
}