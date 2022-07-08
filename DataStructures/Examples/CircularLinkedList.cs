using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Examples
{
    public class CircularLinkedList<T> : LinkedList<T>
    {
        public new IEnumerator GetEnumerator()
        {
            return new CircularLinkedListEnumerator<T>(this);
        }
    }


    public class CircularLinkedListEnumerator<T> : IEnumerator<T>
    {
        LinkedListNode<T> _current;
        public T Current => _current.Value;

        object IEnumerator.Current => Current;


        public CircularLinkedListEnumerator(LinkedList<T> list)
        {
            _current = list.First;
        }

        public void Dispose()
        {
           
        }

        public bool MoveNext()
        {
            if(_current == null)
                return false;

            _current = _current.Next ?? _current.List.First;

            return true;
        }

        public void Reset()
        {
            _current = _current.List.First;
        }
    }

    public static class CircularLinkedListExtension
    {
        public static LinkedListNode<T> Next<T>(this LinkedListNode<T> node)
        {
            if (node != null && node.List != null)
            {
                return node.Next ?? node.List.First;
            }

            return null;
        }

        public static LinkedListNode<T> Prvious<T>(this LinkedListNode<T> node)
        {
            if (node != null && node.List != null)
            {
                return node.Previous ?? node.List.Last;
            }

            return null;
        }
           
    }
}
