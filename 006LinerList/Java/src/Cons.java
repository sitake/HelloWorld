/**
 * Created by naoki on 2015/06/30.
 */
public class Cons<T> {

    private T value;
    private Cons<T> next;

    public Cons(){
    }
    public Cons(T value){
        this.setValue(value);
    }

    public T getValue(){
        return this.value;
    }

    public void setValue(T value){
        this.value = value;
    }

    public Cons<T> getNext(){
        return this.next;
    }

    public Cons<T> setNext(Cons<T> next){
        return this.next = next;
    }

    public boolean hasNext(){
        if(this.next == null){
            return false;
        }
        return true;
    }


}
