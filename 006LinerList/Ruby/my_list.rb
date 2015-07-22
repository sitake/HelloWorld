class MyList

  def initialize(x,y)

    @x = x
    @y = y

  end

  def isEmpty()
    @y == nil
  end

  def size()
    if isEmpty then 0
    else @y.size+1
    end
  end

  def self.empty()
    MyList.new(nil,nil)
  end

  def add(value)
    MyList.new(value,self)
  end

  def get(index)
    if index == 0 then @x
      else @y.get(index -1)
    end
  end

  def to_s()
    if isEmpty then ""
      else @x.to_s + " " + @y.to_s
    end
  end

  def reverse()
    @y.reversed(MyList.empty.add(@x))
  end



  def append(list)
    if list.isEmpty then self
    else append(list.getTail).add(list.getHead)
    end
  end


  def reversed(list)
    if self.isEmpty then list
    else @y.reversed(list.add(@x))
    end
  end

  def getHead()
    @x
  end

  def getTail()
    @y
  end



end

l1 = MyList.empty
l2 = l1.add("apple").add("an").add("have").add("I")

puts(l2)
puts(l2.get(2))
puts(l2.reverse)
puts(l2.append(l2.reverse))