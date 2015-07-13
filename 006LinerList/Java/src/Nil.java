import java.util.Optional;

/**
 * Created by naoki on 2015/07/09.
 */
public class Nil<T> implements MyList<T> {


    @Override
    public Optional<T> get(int index) {
        return Optional.empty();
    }

    @Override
    public int size() {
        return 0;
    }

    @Override
    public MyList<T> reverse() {
        return null;
    }
    @Override
    public String toString(){
        return "";
    }
}
