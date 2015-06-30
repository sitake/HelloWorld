import java.util.ArrayList;

/**
 * Created by naoki on 2015/06/27.
 */
public class Main {
    public static void main(String[] args){
        for(int i = 1;i<=30;i++){
            System.out.println(Fib.getFib(i));
        }

        ArrayList<Integer> list = new ArrayList<Integer>();
        list.add(100);
        list.add(50);
        list.add(101);
        list.add(4);
        list.add(63);

        System.out.println(ListTest.getMax(list));
        System.out.println(ListTest.getMin(list));

    }





}
