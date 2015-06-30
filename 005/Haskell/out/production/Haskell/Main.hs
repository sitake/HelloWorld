module Main where
    import Data.List

    ll = [25,3,46,92,11,64,12,33,33]

    main = do    getLine >>= putStrLn
                 print(sortBy(\x y ->compare x y)ll)
                 print(sortBy(\x y ->compare y x)ll)


