class Sort

end

str = STDIN.gets
puts(str)
a = [1,3,4,5,2]

a.sort!
puts(a)
a.sort!{|x,y|y<=>x}
puts(a)