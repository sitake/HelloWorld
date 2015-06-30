/**
 * Created by naoki on 2015/06/29.
 */
object Fib{
  val phi = (1+Math.sqrt(5))/2

  def getFib(x:Int):Int = ((math.pow(phi,x)-math.pow(-phi,-x))/math.sqrt(5)).toInt

}
