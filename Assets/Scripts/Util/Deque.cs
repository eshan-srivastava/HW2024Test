using System.Collections;
using System.Collections.Generic;

namespace Util
{
    public class Deque<T>
    {
        //can go for a List based approach too but eh
        private readonly LinkedList<T> _deque = new LinkedList<T>();
        
        public int Count => _deque.Count;

        public void Enqueue(T objToEnq)
        {
            _deque.AddFirst(objToEnq);
        }

        public void EnqueueBack(T objToEnq)
        {
            _deque.AddLast(objToEnq);
        }
        
        public T DequeueBack()
        {
            T res = _deque.Last.Value;
            _deque.RemoveLast();
            return res;
        }

        public T PeekFront()
        {
            return _deque.First.Value;
        }

        public T PeekBack()
        {
            return _deque.Last.Value;
        }

        public T Dequeue()
        {
            T res = _deque.First.Value;
            _deque.RemoveFirst();
            return res;
        }

        public void Clear()
        {
            _deque.Clear();
        }
    }
}
