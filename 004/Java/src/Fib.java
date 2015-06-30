/**
 * Created by naoki on 2015/06/27.
 */
public class Fib {

    private static final double PHI = (1+Math.sqrt(5))/2;

    private Fib(){}

    public static int getFib(int x){
        if(x<=0)return 0;
        else return fibGT(x);
    }

    private static int fibGT(int x){
        return (int) ((Math.pow(getPhi(),x)-(Math.pow(-getPhi(),-x)))/Math.sqrt(5));
    }

    private static double getPhi(){
        return PHI;
    }

}
