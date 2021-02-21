// <copyright file="BST.cs" company="WSU">
// Copyright (c) WSU. All rights reserved.
// </copyright>

namespace HW1
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A binary search tree that supports iteration in sorted order.
    /// </summary>
    /// <typeparam name="T">
    /// The type of item being stored, must be comparable.
    /// </typeparam>
    public class BST<T> : ICollection<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Stores a single node in the tree, with pointers to its children.
        /// </summary>
        private class BSTNode : IEquatable<BSTNode>
        {
            /// <summary>
            /// Gets or sets the data stored in this node.
            /// </summary>
            public T Data { get; set; }

            private BSTNode left;

            public ref BSTNode LeftRef => ref this.left;

            private BSTNode right;

            public ref BSTNode RightRef => ref this.right;

            /// <summary>
            /// Gets the amount of children that aren't <c>null</c>.<br/>
            /// Either 0, 1, or 2.
            /// </summary>
            public int Children => (this.left is null ? 0 : 1) + (this.right is null ? 0 : 1);

            /// <summary>
            /// Adds an item to the subtree of this node and its children.
            /// </summary>
            /// <param name="item">The item to insert.</param>
            /// <returns>The inserted node, or <c>null</c> if no value was inserted.</returns>
            public BSTNode Add(T item)
            {
                // Test if the new item is less than or greater than the current node
                switch (item.CompareTo(this.Data))
                {
                    // If equal, no node is inserted as a duplicate has been found.
                    case 0:
                        return null;

                    // New item is less than this node and there isn't a left child, so create one
                    case < 0 when this.left is null:
                        this.left = new BSTNode { Data = item };
                        return this.left;

                    // New item is less than this node, but there is a left child
                    // so pass the item to that child to add into its subtree
                    case < 0:
                        return this.left.Add(item);

                    // Same for when the new item is greater, but for the right subtree
                    case > 0 when this.right is null:
                        this.right = new BSTNode { Data = item };
                        return this.right;
                    case > 0:
                        return this.right.Add(item);
                }
            }

            /// <summary>
            /// Finds a <see cref="BST{T}.BSTNode"/> by item.
            /// </summary>
            /// <param name="item">The item to find.</param>
            /// <returns>The <see cref="BST{T}.BSTNode"/> containing <paramref name="item"/>, or <c>null</c> if not found.</returns>
            public BSTNode Find(T item)
            {
                // simple recursive binary search
                return item.CompareTo(this.Data) switch
                {
                    0 => this,
                    < 0 when this.left is null => null,
                    > 0 when this.right is null => null,
                    < 0 => this.left.Find(item),
                    > 0 => this.right.Find(item),
                };
            }

            /// <summary>
            /// Pre-order traversal of the tree.
            /// </summary>
            /// <returns>An iterator of the node's subtree, sorted least to greatest.</returns>
            public IEnumerable<T> Traverse()
            {
                // First visit the whole left subtree...
                if (this.left is not null)
                {
                    foreach (var i in this.left.Traverse())
                    {
                        yield return i;
                    }
                }

                // Then visit this node...
                yield return this.Data;

                // Finally visit the whole right subtree.
                if (this.right is not null)
                {
                    foreach (var i in this.right.Traverse())
                    {
                        yield return i;
                    }
                }
            }

            public int Levels()
            {
                // 1 for the level of the current node, plus the max level of either subtree
                // Left?.Levels() ?? 0 handles the case that Left is null
                return 1 + Math.Max(
                    this.left?.Levels() ?? 0,
                    this.right?.Levels() ?? 0);
            }

            public bool Equals(BSTNode other)
            {
                if (other is null)
                {
                    return false;
                }

                if (ReferenceEquals(this, other))
                {
                    return true;
                }

                return this.Data.CompareTo(other.Data) == 0;
            }

            public override bool Equals(object obj)
            {
                if (obj is null)
                {
                    return false;
                }

                if (ReferenceEquals(this, obj))
                {
                    return true;
                }

                if (obj.GetType() != this.GetType())
                {
                    return false;
                }

                return this.Equals((BSTNode)obj);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(this.Data);
            }

            public static bool operator ==(BSTNode lhs, BSTNode rhs)
            {
                if (lhs is null)
                {
                    return rhs is null;
                }

                return lhs.Equals(rhs);
            }

            public static bool operator !=(BSTNode lhs, BSTNode rhs)
            {
                return !(lhs == rhs);
            }
        }

        private BSTNode root;

        /// <summary>
        /// Initializes a new instance of the <see cref="BST{T}"/> class.
        /// </summary>
        public BST()
        {
            this.root = null;
            this.Count = 0;
        }

        /// <inheritdoc cref="IEnumerable.GetEnumerator"/>
        public IEnumerator<T> GetEnumerator()
        {
            // This method can't entirely be delegated to _root because we have to catch the case that
            // _root is null
            return this.root is null
                ? ((IEnumerable<T>)Array.Empty<T>()).GetEnumerator()
                : this.root.Traverse().GetEnumerator();
        }

        /// <inheritdoc cref="IEnumerable.GetEnumerator"/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <inheritdoc cref="ICollection{T}.Add"/>
        public void Add(T item)
        {
            // do some null checks, then just call Add on _root
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (this.root is null)
            {
                this.root = new BSTNode { Data = item };
                this.Count = 1;
            }
            else
            {
                var node = this.root.Add(item);

                // BSTNode.Add returns null if nothing was added because it was a duplicate
                if (node is not null)
                {
                    this.Count++;
                }
            }
        }

        /// <inheritdoc cref="ICollection{T}.Clear"/>
        public void Clear()
        {
            // Garbage collector will handle it
            this.root = null;
            this.Count = 0;
        }

        /// <inheritdoc cref="ICollection{T}.Contains"/>
        public bool Contains(T item)
        {
            // Delegate to _root's find. If it finds a BSTNode, the tree contains the item.
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return this.root?.Find(item) != null;
        }

        /// <inheritdoc cref="ICollection{T}.CopyTo"/>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(arrayIndex), arrayIndex, "Array index cannot be below 0");
            }

            if (array.Rank != 1)
            {
                throw new ArgumentException("Array is multidimensional", nameof(array));
            }

            if (array.Length + arrayIndex < this.Count)
            {
                throw new ArgumentException("Array is too short", nameof(array));
            }

            if (this.root is null)
            {
                return;
            }

            // Iterating over an iterable with its index.
            foreach (var (element, index) in this.root.Traverse().Select((e, i) => (e, i)))
            {
                array[index + arrayIndex] = element;
            }
        }

        /// <inheritdoc cref="ICollection{T}.Remove"/>
        public bool Remove(T item)
        {
            if (item is null)
            {
                return false;
            }

            if (this.root is null)
            {
                return false;
            }

            // Try as I might I couldn't figure out a way to write this algorithm without ref
            // without using extra memory in BSTNode or making it horribly inefficient.
            // I also couldn't write it as a recursive method in BSTNode as there would be no way
            // to take a reference to _root.

            // Find the node with the value being removed.
            // We cannot use BSTNode.Find, as this node will have to be replaced,
            // and BSTNode.Find cannot be made to return a reference, as the root node cannot
            // possibly return a reference to itself.
            ref var toRemove = ref this.root;

            // Until toRemove is the node we want...
            while (item.CompareTo(toRemove.Data) != 0)
            {
                // set toRemove to be a reference to either the left or right child
                if (item.CompareTo(toRemove.Data) < 0)
                {
                    toRemove = ref toRemove.LeftRef;
                }
                else
                {
                    toRemove = ref toRemove.RightRef;
                }

                // if that child doesn't exist, the element is not in the tree and cannot be removed
                if (toRemove is null)
                {
                    return false;
                }
            }

            // Removal needs to be handled differently depending on how many children there are
            switch (toRemove.Children)
            {
                case 0:
                    // simplest case, toRemove is now null
                    // because toRemove is a *reference*, this now means that toRemove's parent is now
                    // properly pointing to null, or if there was no parent, this updates _root to be null
                    toRemove = null;
                    break;
                case 1 when toRemove.LeftRef is null:
                    // If the only child is the right one, replace the node to remove with its right subtree
                    toRemove = toRemove.RightRef;
                    break;
                case 1 when toRemove.RightRef is null:
                    // If the only child is the left one, replace the node to remove with its left subtree
                    toRemove = toRemove.LeftRef;
                    break;
                case 2:
                    // When there are two children, replace the Data of the node to remove with
                    // the smallest member of its right subtree, then remove that value from the right subtree
                    ref var min = ref toRemove.RightRef;
                    while (min.LeftRef != null)
                    {
                        min = ref min.LeftRef;
                    }

                    toRemove.Data = min.Data;

                    // Because min is the smallest value of a subtree, it cannot have a Left,
                    // so removing it is trivial
                    min = min.RightRef;
                    break;
            }

            this.Count--;
            return true;
        }

        /// <summary>
        /// Gets the minimum amount of levels that could be used to store <see cref="BST{T}.Count"/> items.
        /// </summary>
        public int MinLevels =>
            this.root is null ? 0 : (int)Math.Floor(Math.Log2(this.Count)) + 1; // Standard formula

        /// <summary>
        /// Gets the amount of levels being used by the BST.
        /// </summary>
        public int Levels => this.root?.Levels() ?? 0;

        /// <inheritdoc cref="ICollection{T}.Count"/>
        public int Count { get; private set; }

        /// <inheritdoc cref="ICollection{T}.IsReadOnly"/>
        public bool IsReadOnly => false;
    }
}
