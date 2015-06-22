class FizzBuzz

  def isFizzBuzz n
    isFizz(n) && isBuzz(n)
  end

  def isFizz n
    n % 3 == 0
  end

  def isBuzz n
    n % 5 == 0
  end

  def fizzBuzz n
    if isFizzBuzz n then puts "FizzBuzz"
    elsif isFizz n then puts "Fizz"
    elsif isBuzz n then puts "Buzz"
    else puts n
    end

  end


  def doFizzBuzz n
    a = 1
    while a <= n do
      fizzBuzz a
      a += 1
    end
  end
end

fb = FizzBuzz.new
fb.doFizzBuzz 100