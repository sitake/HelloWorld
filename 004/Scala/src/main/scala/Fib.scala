/**
 * Created by naoki on 2015/06/29.
 */
object Fib{
  val phi = (1+Math.sqrt(5))/2

  def getFib(x:Int):Int = ((Math.pow(phi,x)-Math.pow(-phi,-x))/Math.sqrt(5)).toInt

}
