import java.util.Comparator;

/**
 * Created by naoki on 2015/06/29.
 */
public class CAT<T extends Comparable<? super T>> implements Comparator<T> {


    @Override
    public int compare(T o1, T o2) {
        return o1.compareTo(o2);
    }
}
