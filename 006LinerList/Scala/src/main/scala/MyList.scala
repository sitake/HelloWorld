/**
 * Created by naoki on 2015/07/08.
 */
sealed trait MyList[+A] {

  def get(index:Int):Option[A] =
    this match{
      case Nil => None
      case Cons(head,tail) =>
        if(index <= 0)
          Some(head)
        else
          tail.get(index -1 )
  }

  def add(value:A):MyList[A] ={
    new Cons[A](value,this)
  }

  def size:Int =
    this match{
      case Cons(_,tail) => 1 + tail.size
      case Nil => 0
    }


}

object MyList{
  def apply[A](values:A*):MyList[A] =
    values.foldRight(Nil:MyList[A])(Cons(_,_))
}

case class Cons[A](head:A,tail:MyList[A]) extends MyList[A]

case object Nil extends MyList[Nothing]


