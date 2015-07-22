import java.util.Optional;

/**
 * Created by naoki on 2015/07/09.
 */
public class Cons<T> implements MyList<T> {

    private T head;
    private MyList<T> tail;

    public Cons(T head,MyList<T> tail){
        this.head = head;
        this.tail = tail;
    }

    @Override
    public Optional<T> get(int index) {
        if(index <= 0)return Optional.of(this.head);
        else return tail.get(index-1);
    }

    @Override
    public int size() {
        return tail.size()+1;
    }

    @Override
    public String toString(){
        return head.toString() +" "+tail.toString();
    }


}
