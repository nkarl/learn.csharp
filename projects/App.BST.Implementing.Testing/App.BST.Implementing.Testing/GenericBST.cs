using System;
using System.Collections.Generic;

namespace App.BST.Implementing.Testing
{
    /// <summary>
    /// Generic Binary Search Tree class, applicable for generic data types that are comparable.
    /// </summary>
    public class GenericBST : IComparable<T>
    {
        private GenericBST()
        {
        }

        private static GenericBST<T> Instance { get; } = new GenericBST<T>();

        /// <summary>
        /// Factory method to hide the default constructor; public API.
        /// </summary>
        /// <returns>Instance of new object.</returns>
        public static GenericBST<T> CreateInstance() =>  Instance;
    }
}