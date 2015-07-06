/**
 * Created by naoki on 2015/07/02.
 */
public class Main {
    public static void main(String [] args){

        MyList<String> list = new MyList<String>();

        list.add("h");
        list.add("e");
        list.add("l");
        list.add("l");
        list.add("o");

        System.out.println(list);
        System.out.println(list.size());
        System.out.println(list.get(5));


    }
}
