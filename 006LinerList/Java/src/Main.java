/**
 * Created by naoki on 2015/07/02.
 */
public class Main {
    public static void main(String [] args){

        MyListAlpha<String> list = new MyListAlpha<String>();

        list.add("h");
        list.add("e");
        list.add("l");
        list.add("l");
        list.add("o");

        System.out.println(list);
        System.out.println(list.size());
        System.out.println(list.get(3));

        MyList<String> l2 = new Cons<>("hello",new Cons<>("World",new Nil<>()));
        System.out.println(l2.get(1).get());
        MyList<String> l3 = l2.add("Test");
        System.out.println(l3.get(3));
        System.out.println(l3);
        MyList<String> l4 = l3.reverse();
        System.out.println(l3.apend(l4));
        MyList<Integer> l5 = l3.map(str -> Integer.valueOf(str.hashCode()));
        System.out.println(l5);

    }
}
