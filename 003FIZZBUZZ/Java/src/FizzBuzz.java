/**
 * Created by kaneko on 2015/06/18.
 */
public class FizzBuzz {

        public void fizzBuzz(int x){

            for(int i=1;i<=x;i++){

                if(fizz(i)){
                    System.out.println();
                }else num(i);

            }

        }

        private boolean fizz(int x){if(x%3==0){System.out.print("Fizz");buzz(x);return true;}return buzz(x);}

        private boolean buzz(int x){if(x%5==0){System.out.print("Buzz");return true;}return false;}

        private void num(int x){System.out.println(x);}



}
