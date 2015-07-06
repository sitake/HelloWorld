module Main where

data MyList a = MyList | a : MyList a

list = MyList 100

main = do   print list