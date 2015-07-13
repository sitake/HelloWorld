module Main where

data List a = Nil | Cons a (List a) deriving Eq

foldLeft :: b -> (b -> a -> b) -> List a -> b
foldLeft b _ Nil = b
foldLeft b f (Cons h t) = foldLeft(f b h) f t

size :: List a -> Int
size = foldLeft 0 (\n -> const(n+1))

nth :: Int -> List a -> Maybe a
nth n = fst . foldLeft(Nothing,n) f
    where
        f (Nothing,n) value | n <= 0 = (Just value,n - 1)
        f (result,n) _ = (result,n-1)

list :: [a] -> List a
list = foldr Cons Nil
