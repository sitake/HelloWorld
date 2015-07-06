using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyListProgram
{
    class MyList<T>
    {

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
    }
}
