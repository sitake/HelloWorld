
/**
 * Created by naoki on 2015/07/06.
 */


class MyListAlpha[A] (head:A = null,tail:MyListAlpha[A] = null){


  def isEmpty():Boolean = tail == null



  def size():Int = isEmpty match{
    case true => 0
    case false => tail.size + 1
  }

  def add(value:A):MyListAlpha[A] = new MyListAlpha[A](value,this)

  def get(index:Int):A = {
    if(index < 0||index >= size())throw new IllegalArgumentException()
    gett(index)
  }

  private def gett(index:Int):A = index match{
    case 0 => head
    case _ => tail.gett(index - 1)
  }

  override def toString():String = isEmpty match{
    case true => ""
    case false => head.toString +" " +tail.toString()
  }

}
