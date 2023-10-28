using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    internal class MyDynamicArray<T> : IEnumerable<T>
        where T : IEquatable<T>
    {
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new IndexOutOfRangeException();

                return _items[index];
            }
            set
            {
                if (index < 0 || index >= Count)
                    throw new IndexOutOfRangeException();

                _items[index] = value;
            }
        }

        // 동적배열의 용량 (현재 참조하고있는 배열의 길이)
        public int Capacity => _items.Length;

        // 실제 가지고있는 자료의 갯수
        public int Count => _count;

        private const int DEFAULT_SIZE = 1;
        private T[] _items = new T[DEFAULT_SIZE]; // 동적배열은 배열 기반이기때문에 아이템을 저장할 배열이 필요함.
        private int _count;

        // 매치 조건 탐색
        public T Find(Predicate<T> match)
        {
            for (int i = 0; i < _count; i++)
            {
                if (match.Invoke(_items[i]))
                    return _items[i];
            }
            return default;
        }

        // 매치 조건 탐색
        public int FindIndex(Predicate<T> match)
        {
            for (int i = 0; i < _count; i++)
            {
                if (match.Invoke(_items[i]))
                    return i;
            }
            return -1;
        }

        // 삽입
        public void Add(T item)
        {
            // 공간 모자라면
            if (_count >= _items.Length)
            {
                T[] tmp = new T[_count * 2]; // 두배짜리 더 큰거 만듬
                Array.Copy(_items, tmp, _count);
                _items = tmp; // 배열참조 변경
            }

            _items[_count++] = item; // 마지막에 아이템 추가
        }

        // 삭제
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();

            // index 위치부터 다 한칸씩 당김
            for (int i = index; i < _count - 1; i++)
            {
                _items[i] = _items[i + 1];
            }
            _count--; // 하나 뺐으니까 차감
        }

        public bool Remove(T item)
        {
            int index = FindIndex(x => x.Equals(item));

            if (index < 0)
                return false;

            RemoveAt(index);
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }


        // 책 읽어주는 사람
        public struct Enumerator : IEnumerator<T>
        {
            // 현재 페이지 내용
            public T Current => _data[_index];

            object IEnumerator.Current => _data[_index];

            private readonly MyDynamicArray<T> _data; // 책
            private int _index; // 현재 페이지

            public Enumerator(MyDynamicArray<T> data)
            {
                _data = data;
                _index = -1;
            }

            // 다음 페이지로
            public bool MoveNext()
            {
                // 다음페이지 넘길 수 있을때
                if (_index < _data.Count - 1)
                {
                    _index++;
                    return true;
                }

                return false;
            }

            // 책 표지 덮기
            public void Reset()
            {
                _index = -1;
            }

            // 책 읽을때 필요한 리소스들을 해제하는 기능
            public void Dispose()
            {
            }
        }




    }
}
