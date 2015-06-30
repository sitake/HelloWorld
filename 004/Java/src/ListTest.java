import java.util.List;

/**
 * Created by naoki on 2015/06/29.
 */
public class ListTest {

    static <T extends Comparable<? super T>> T getMax(List<T> list){

        if(list == null || list.size()==0)return null;
        T max = list.get(0);
        T tmp;
        for(int  i = 1;i < list.size();i++){
            tmp = list.get(i);
            if(tmp == null)continue;
            if(tmp.compareTo(max)==1)max = tmp;
        }
        return max;

    }

    static <T extends Comparable<? super T>> T getMin(List<T> list){

        if(list == null || list.size()==0)return null;
        T min = list.get(0);
        T tmp;
        for(int  i = 1;i < list.size();i++){
            tmp = list.get(i);
            if(tmp == null)continue;
            if(tmp.compareTo(min)==-1)min = tmp;
        }
        return min;

    }



}
