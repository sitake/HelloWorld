using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyListProgram
{
    class MyList<T>
    {
        public delegate V Function<V>(T x);

        MyList<T> tail;
        T head;

        public MyList(T head, MyList<T> tail)
        {
            this.head = head;
            this.tail = tail;
        }
        public MyList() { }

        public MyList<T> Add(T value)
        {
            return new MyList<T>(value, this);
        }

        public bool IsEmpty()
        {
            return tail == null;
        }

        public int Size()
        {
            if (IsEmpty()) return 0;
            else return tail.Size() + 1;
        }

        public T Get(int index)
        {
            if (index == 0) return head;
            else return tail.Get(index - 1);
        }

        override public string ToString()
        {
            if (IsEmpty()) return "";
            else return head.ToString() + " " + tail.ToString();
        }

        public MyList<T> Reverse()
        {
            MyList<T> rList = new MyList<T>();
            for (int i = 0; i < this.Size(); i++)
            {
                rList = rList.Add(this.Get(i));
            }
            return rList;

        }

        public MyList<T> Append(MyList<T> list)
        {
            var rList = this;
            for (int i = list.Size() - 1; i >= 0; i--)
            {
                rList = rList.Add(list.Get(i));
            }
            return rList;
        }

        public  MyList<V> Map<V>(Function<V> f)
        {
            var rList = new MyList<V>();
            for(int i = this.Size()-2;i>=0;i--){
                rList = rList.Add(f(this.Get(i)));
            }
            return rList;
        }

        public MyList<T> Take(int num)
        {
            var rList = new MyList<T>();
            if (num >= this.Size()) return this;
            for (int i = num-1; i >= 0; i--)
            {
                rList = rList.Add(this.Get(i));
            }
            return rList;

        }

        public MyList<T> Drop(int num)
        {
            var rList = new MyList<T>();
            if (num >= this.Size()) return this;
            for (int i = this.Size() - 1; i >= num; i--)
            {
                rList = rList.Add(this.Get(i));
            }
            return rList;
        }


    }
}
