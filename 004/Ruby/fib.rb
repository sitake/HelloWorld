class Fib


  def fib n
    phi = (1+Math.sqrt(5))/2
    (phi ** n - ((-phi)**(-n)))/Math.sqrt(5)
  end


end

fb = Fib.new
for n in 1..100
    puts fb.fib(n).to_int
end

a = [1,3,2,5,4]
puts(a.max)
puts(a.min)