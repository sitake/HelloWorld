/**
 * Created by naoki on 2015/07/06.
 */
object Main extends App{

  val l1 = new MyListAlpha[String]()
  val l2 = l1.add("apple").add("an").add("have").add("I")
  println(l2)
  println(l2.get(3))


  val l3 = MyList.apply("I","have","an","apple")
  val l4 = l3.add("test")
  println(l3.get(2))
  println(l4)
  println(l4.reverse)
  println(l4.append(l3))
  println(l4.map(str=>str.hashCode()))
  println(l4.drop(3))
  println(l4.take(2))
  println(l4.drop(3).append(l4.take(3)))

}
