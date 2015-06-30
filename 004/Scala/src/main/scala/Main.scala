/**
 * Created by naoki on 2015/06/29.
 */
object Main extends App{

  for(i <- 1 to 30){
    println(Fib.getFib(i))
  }

  val list = List(1,3,2,5,4)
  println(list.max)
  println(list.min)
}
