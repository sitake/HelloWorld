/**
 * Created by naoki on 2015/06/30.
 */
public class Node<T> {

    private T value;
    private Node<T> next;

    public Node(){
    }
    public Node(T value){
        this.setValue(value);
    }

    public T getValue(){
        return this.value;
    }

    public void setValue(T value){
        this.value = value;
    }

    public Node<T> getNext(){
        return this.next;
    }

    public Node<T> setNext(Node<T> next){
        return this.next = next;
    }

    public boolean hasNext(){
        if(this.next == null){
            return false;
        }
        return true;
    }


}
