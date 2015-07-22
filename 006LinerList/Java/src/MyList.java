import java.util.Optional;
import java.util.function.Function;

/**
 * Created by naoki on 2015/07/09.
 */
public interface MyList<T>{

    public Optional<T> get(int index);

    public int size();

    public default MyList<T> add(T value){
        return new Cons<T>(value,this);
    }

    public default MyList<T> reverse(){
       MyList<T> list = new Nil<T>();
        int size = size();
        for(int i = 0;i <size;i++){

            list = get(i).equals(Optional.<T>empty())?list:list.add(get(i).get());

        }
        return list;
    }

    public default MyList<T> apend(MyList<T> list){
        int index = list.size()-1;
        MyList<T> rList = this;
        while(index >= 0){
            rList = rList.add(list.get(index).get());
            index--;
        }
        return rList;
    }

    public default <R> MyList<R> map(Function<T,R> function){

        MyList<R> rList = new Nil<R>();
        for(int i = this.size()-1;i>=0;i--){
            rList = rList.add(function.apply(this.get(i).get()));
        }
        return rList;
    }

    public default MyList<T> take(int index){
        MyList<T> rList = new Nil<T>();

        if(index > size()-1||index < 0)throw new IllegalArgumentException();

        for(int i = index;i>0;i--){

            rList = rList.add(this.get(i-1).get());
        }
        return rList;
    }

    public default MyList<T> drop(int index){
        MyList<T> rList = new Nil<T>();
        int size = size();

        if(index > size-1||index<0)throw new IllegalArgumentException();

        for(int i = size-1;i>index-1;i--){
            rList = rList.add(this.get(i).get());
        }
        return rList;
    }
}


