/**
 * Created by naoki on 2015/07/06.
 */
object Main extends App{

  val l1 = new MyList[String]()
  val l2 = l1.add("apple").add("an").add("have").add("I")
  println(l2)
  println(l2.get(3))


}
