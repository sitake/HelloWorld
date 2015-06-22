/**
 * Created by kaneko on 2015/06/18.
 */
object FizzBuzz {

  def fizzBuzz(x:Int)={
    for(i <- 1 to x){
      println(fizzOrBuzz(i))
    }
  }

  private def fizzOrBuzz(x:Int):String={

    if(x%3==0&&x%5==0)return "FizzBuzz"
    if(x%3==0)return "Fizz"
    if(x%5==0)return "Buzz"
    return x.toString

  }

}
