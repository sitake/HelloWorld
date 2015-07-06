import java.util.Iterator;
import java.util.NoSuchElementException;

/**
 * Created by naoki on 2015/07/02.
 */
public class MyList<T> implements Iterable<T>{

    private Cons<T> head, tail;

    public MyList() {
        head = tail = new Cons<T>();
    }

    public void add(T value) {
        if(!head.hasNext()) {
            head = tail = new Cons(value);
            tail.setNext(new Cons());
        }else{
            tail.setNext(new Cons(value));
            tail = tail.getNext();
            tail.setNext(new Cons());
        }
    }

    public boolean isEmpty(){
        return head.equals(tail);
    }

    public int size(){
        int sum = 0;
        Iterator<T> iterator = new MyListIterator(head);
        if(isEmpty())return sum;
        while(iterator.hasNext()){
            sum++;
            iterator.next();
        }

        return sum;
    }

    public T get(int index){
        if(index<0)throw new IllegalArgumentException();
        Iterator<T> iterator = new MyListIterator(head);
        T value = null;
        for(int i = 0;i <= index;i++){
            value = iterator.next();
        }

        return value;

    }



    @Override
    public String toString(){

        StringBuilder str = new StringBuilder();
        Cons<T> p = head;
        while(p.hasNext()){
            str.append(p.getValue().toString());
            p = p.getNext();
            if(p.hasNext())str.append(",");
        }

        return str.toString();
    }

    @Override
    public Iterator<T> iterator() {
        return null;
    }

    class MyListIterator implements Iterator<T> {

        private Cons<T> pointer;
        private Cons<T> last;

        public MyListIterator(Cons<T> head){
            pointer = head;
        }

        @Override
        public boolean hasNext() {
            return pointer.hasNext();
        }

        @Override
        public T next() {
            if(!hasNext())throw new NoSuchElementException();
            last = pointer;
            T x = pointer.getValue();
            pointer = pointer.getNext();
            return x;
        }

        @Override
        public void remove(){
            if(last == null)throw new IllegalStateException();
            last.setNext(pointer.getNext());
            last.setValue(pointer.getValue());
            pointer = last;
            last = null;
        }
    }
}
