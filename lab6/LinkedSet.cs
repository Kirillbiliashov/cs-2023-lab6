using System;
using System.Collections;
using lab6.Model;

namespace lab6
{
    public class LinkedSet : ISet<Vegetable>
    {

        private Node? _head;

        private Node? _tail;

        private int _size;

        public int Count => _size;

        public bool IsReadOnly => false;


        public LinkedSet()
        {

        }

        public LinkedSet(Vegetable item)
        {
            Add(item);
        }

        public LinkedSet(params Vegetable[] items)
        {
            foreach(var item in items)
            {
                Add(item);
            }
        }

        public bool Add(Vegetable item)
        {
            if (Contains(item))
                return false;
            var newNode = new Node()
            {
                Data = item
            };
            _size++;
            if (_head == null)
            {
                _head = newNode;
                _tail = newNode;
            } else
            {
                _tail.Next = newNode;
                _tail = newNode;
            }
            return true;
        }

        public void Clear()
        {
            _head = null;
            _tail = null;
            _size = 0;
        }

        public bool Contains(Vegetable item)
        {
            var enumerator = new LinkedSetEnumerator(_head);
            while (enumerator.MoveNext())
            {
                var currItem = enumerator.Current;
                if (currItem == item)
                    return true;
            }
            return false;
        }

        public void CopyTo(Vegetable[] array, int arrayIndex)
        {
            if (_size > array.Length - arrayIndex)
            {
                throw new ArgumentException("The number of elements in the set exceeds the available space in the array.");
            }
            var idx = arrayIndex;
            foreach (var vegetable in this)
            {
                array[idx] = vegetable;
                idx++;
            }
        }

        public void ExceptWith(IEnumerable<Vegetable> other)
        {
            foreach(var item in other)
            {
                Remove(item);
            }
        }

        public IEnumerator<Vegetable> GetEnumerator()
        {
            return new LinkedSetEnumerator(_head);
        }

        public void IntersectWith(IEnumerable<Vegetable> other)
        {
            var enumerator = new LinkedSetEnumerator(_head);
            while (enumerator.MoveNext())
            {
                if (!other.Contains(enumerator.Current))
                {
                    Remove(enumerator.Current);
                }
            }
        }

        public bool IsProperSubsetOf(IEnumerable<Vegetable> other) 
        {
            var enumerator = GetEnumerator();
            var supersetEnumerator = other.GetEnumerator();
            if (_size == 0)
                return true;
            if (other.Count() == 0)
                return false;
            enumerator.MoveNext();
            while (supersetEnumerator.MoveNext())
            {
                if (supersetEnumerator.Current == enumerator.Current)
                {
                    var supersetHasMore = supersetEnumerator.MoveNext();
                    var enumeratorHasMore = enumerator.MoveNext();
                    while (enumeratorHasMore && supersetHasMore)
                    {
                        if (enumerator.Current != supersetEnumerator.Current)
                            return false;
                        supersetHasMore = supersetEnumerator.MoveNext();
                        enumeratorHasMore = enumerator.MoveNext();
                    }
                    return !enumeratorHasMore;
                }
            }
            return false;
        }

        public bool IsProperSupersetOf(IEnumerable<Vegetable> other)
        {
            var subsetEnumerator = other.GetEnumerator();
            var enumerator = GetEnumerator();
            if (other.Count() == 0)
                return true;
            if (_size == 0)
                return false;

            if (SetEquals(other))
                return true;
            while (enumerator.MoveNext())
            {
                if (enumerator.Current == subsetEnumerator.Current)
                {
                    var subsetHasMore = subsetEnumerator.MoveNext();
                    var supersetHasMore = enumerator.MoveNext();
                    while (subsetHasMore && supersetHasMore)
                    {
                        if (enumerator.Current != subsetEnumerator.Current)
                            return false;
                         subsetHasMore = subsetEnumerator.MoveNext();
                         supersetHasMore = enumerator.MoveNext();
                    }
                    return !subsetHasMore;
                }
            }
            return false;
        }

        public bool IsSubsetOf(IEnumerable<Vegetable> other)
        {
            var enumerator = GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (!other.Contains(enumerator.Current))
                    return false;
            }
            return true;
        }

        public bool IsSupersetOf(IEnumerable<Vegetable> other)
        {
            foreach(var item in other)
            {
                if (!Contains(item))
                    return false;
            }
            return true;
        }

        public bool Overlaps(IEnumerable<Vegetable> other)
        {
            var enumerator = GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (other.Contains(enumerator.Current))
                    return true;
            }
            return false;
        }

        public bool Remove(Vegetable item)
        {
            if (_size == 0)
                return false;
            if (item == _head?.Data)
            {
               if (_size == 1)
                {
                    Clear();
                } else
                {
                    _head = _head.Next;
                    _size--;
                }
                return true;
            }
            var currNode = _head;
            while (currNode != null)
            {
                if (currNode.Next != null && currNode.Next.Data == item)
                {
                    if (currNode.Next == _tail)
                    {
                        _tail = currNode;
                    }
                    var next = currNode.Next;
                    currNode.Next = next.Next;
                    _size--;
                    return true;
                }
                currNode = currNode.Next;
            }
            return false;
        }

        public bool SetEquals(IEnumerable<Vegetable> other)
        {
            if (other.Count() != _size)
                return false;
            foreach(var item in other)
            {
                if (!Contains(item))
                    return false;
            }
            return true;
        }

        public void SymmetricExceptWith(IEnumerable<Vegetable> other) 
        {
            foreach (var item in other)
            {
                if (!Contains(item))
                {
                    Add(item);
                } else
                {
                    Remove(item);
                }
            }
        }

        public void UnionWith(IEnumerable<Vegetable> other)
        {
            foreach (var item in other)
            {
                if (!Contains(item))
                {
                    Add(item);
                }
            }
        }

        void ICollection<Vegetable>.Add(Vegetable item)
        {
            Add(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new LinkedSetEnumerator(_head);
        }

        private class Node
        {
            public Vegetable Data { get; set; }

            public Node? Next { get; set; }
        }

        private class LinkedSetEnumerator : IEnumerator<Vegetable>
        {

            private Node _node;

            private Node _headNode;

            private Node _prevNode;

            public Vegetable Current => _node.Data;

            public Node? PrevNode => _prevNode;

            public Node CurrNode => _node;

            object IEnumerator.Current => _node.Data;


            public LinkedSetEnumerator(Node node)
            {
     
                _headNode = node;
            }

            public void Dispose()
            {

            }

            public bool MoveNext()
            {
                if (_headNode == null)
                    return false;
                if (_node == null)
                {
                    _node = _headNode;
                    return true;
                }
                if (_node.Next == null)
                    return false;
                _prevNode = _node;
                _node = _node.Next;
                return true;
            }

            public void Reset()
            {
                _node = _headNode;
            }
        }
    }

}

