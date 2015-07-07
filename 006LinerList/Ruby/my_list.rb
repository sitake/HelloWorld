class MyList

  def initialize(x,y)

    @x = x
    @y = y

  end

  def isEmpty()
    return @y == nil
  end

  def self.empty()
    return MyList.new(nil,nil)
  end

  def add(value)
    return MyList.new(value,self)
  end

  def get(index)
    if index == 0 then return @x
      else return @y.get(index -1)
    end
  end

  def to_s()
    if isEmpty then return ""
      else return @x.to_s + " " + @y.to_s
    end
  end

end

l1 = MyList.empty
l2 = l1.add("apple").add("an").add("have").add("I")

puts(l2)
puts(l2.get(2))