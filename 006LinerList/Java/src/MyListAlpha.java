import java.util.Iterator;
import java.util.NoSuchElementException;

/**
 * Created by naoki on 2015/07/02.
 */
public class MyListAlpha<T> implements Iterable<T>{

    private Node<T> head, tail;

    public MyListAlpha() {
        head = tail = new Node<T>();
    }

    public void add(T value) {
        if(!head.hasNext()) {
            head = tail = new Node(value);
            tail.setNext(new Node());
        }else{
            tail.setNext(new Node(value));
            tail = tail.getNext();
            tail.setNext(new Node());
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
        Node<T> p = head;
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

        private Node<T> pointer;
        private Node<T> last;

        public MyListIterator(Node<T> head){
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
