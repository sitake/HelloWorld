import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayList;

/**
 * Created by naoki on 2015/06/29.
 */
public class ListSort {

    public static void main(String[] args){

        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));

        try {
            System.out.println(reader.readLine());
        } catch (IOException e) {
            e.printStackTrace();
        }

        try {
            reader.close();
        } catch (IOException e) {
            e.printStackTrace();
        }

        ArrayList<Integer> list = new ArrayList<Integer>();
        list.add(3);
        list.add(5);
        list.add(1);
        list.add(8);
        list.add(2);
        list.add(3);
        list.sort((x,y)->x.compareTo(y));
        System.out.println(list);
        list.sort((x,y)->y.compareTo(x));
    }




}
