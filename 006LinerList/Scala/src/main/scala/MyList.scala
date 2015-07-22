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

  def add[B>:A](value:B):MyList[B] ={
    new Cons[B](value,this)
  }

  def size:Int =
    this match{
      case Cons(_,tail) => 1 + tail.size
      case Nil => 0
    }

  def reverse[B>:A]:MyList[B]={
    this match{
      case Cons(head,tail) => tail.reverse(Nil.add(head))
      case Nil => Nil
    }
  }
  private def reverse[B>:A](list:MyList[B]):MyList[B]=this match{
    case Cons(head,tail) => tail.reverse(list.add(head))
    case Nil => list
  }


  def append[B>:A](list:MyList[B]):MyList[B]=list match{
    case Cons(head,tail)=> apend(tail).add(head)
    case Nil => this
  }

  def map[B](f:(A=>B)):MyList[B]=this match{
    case Cons(head,tail)=> tail.map(f).add(f(head))
    case Nil => Nil
  }

  def drop(index:Int):MyList[A]=this match{
    case Cons(head,tail)=>{
      if(index<=0)tail.drop(index).add(head)
      else tail.drop(index-1)
    }
    case Nil => Nil
  }

  def take(index:Int):MyList[A]=this match{
    case Cons(head,tail)=>{
      if(index<=0)Nil
      else tail.take(index-1).add(head)
    }
    case Nil => Nil
  }


}

object MyList{
  def apply[A](values:A*):MyList[A] =
    values.foldRight(Nil:MyList[A])(Cons(_,_))
}

case class Cons[A](head:A,tail:MyList[A]) extends MyList[A]

case object Nil extends MyList[Nothing]


