module Main where
    import Data.IORef

    fib :: Int -> Int
    fib x   |   x<=0        =   0
            |   x==1        =   1
            |   otherwise   =   fib(x-1)+fib(x-2)

    fibb :: Int -> Float
    fibb x  |   x<=0        =   0
            |   x==1        =   1
            |   otherwise   =   (gr^x-(1/(-gr)^x))/sqrt 5

    gr      =   (1+sqrt 5)/2





    main    =   do  print(map fib [1..10])
                    print(map fibb [1..100])
                    print(maximum [1,3,7,1])
                    print(minimum [3,8,0,5,9])