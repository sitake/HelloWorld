import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

/**
 * Created by kaneko on 2015/06/23.
 */
public class LoopStop {

    private static boolean isrunning = true;

    public  static void main(String[] args){

        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
        Thread doNums = new Thread(new DoNums());
        doNums.start();

        try {
            reader.readLine();
        } catch (IOException e) {
            e.printStackTrace();

        }

        isrunning = false;

    }

    private static class DoNums implements Runnable {


        @Override
        public void run() {
            int i = 0;
            while(isrunning){
                System.out.println(i);
                i++;
                try {
                    Thread.sleep(10);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
            }
        }
    }
}
