using System;
using System.Collections.Generic;

namespace App.BST.Implementing.Testing
{
    // I should first create a working prototype before improving it with IComparable.
    
    /// <summary>
    /// The Binary Search Tree (BST) class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BST<T>
    {
        internal BstNode<T> Root;

        public BST(BstNode<T> root)
        {
            Root = root;
        }

        public BST(T newData)
        {
            Root = new BstNode<T>(newData);
        }
    }
}