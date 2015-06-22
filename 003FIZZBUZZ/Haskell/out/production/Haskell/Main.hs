module Main where
    isfizzbuzz :: Int -> Bool
    isfizzbuzz x = if isfizz x && isbuzz x then True else False
    isfizz :: Int -> Bool
    isfizz x = if x `mod` 3 == 0 then True else False
    isbuzz :: Int -> Bool
    isbuzz x = if x `mod` 5 == 0 then True else False

    dofizzbuzz :: Int -> Int -> IO ()
    dofizzbuzz x y  | x > y          = return ()
                    | isfizzbuzz x   =  do  print("FizzBuzz")
                                            dofizzbuzz (x+1) y
                    | isfizz x       =  do  print("Fizz")
                                            dofizzbuzz (x+1) y
                    | isbuzz x       =  do  print("Buzz")
                                            dofizzbuzz (x+1) y
                    | otherwise      =  do  print(x)
                                            dofizzbuzz (x+1) y

    fizzbuzz :: Int -> IO()
    fizzbuzz x = dofizzbuzz 1 x


    main = fizzbuzz 100

