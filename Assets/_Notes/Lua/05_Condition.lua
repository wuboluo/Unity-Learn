print("---------- 05_Condition ----------")

a = 10

-- 单分支：if 条件 then...end
if a > 5 then 
	print(a)
end

-- 双分支：if 条件 then...else...end
if a < 5 then
	print(123)
else
	print(321)
end

-- 多分支：if 条件 then...elseif...then...end
-- lua中，elseif是连着写，否则报错
if a < 5 then
	print(123)
elseif a == 6 then
	print(6)
elseif a == 10 then
	print(10)
else
	print("other")
end

if a >= 0 and a <= 15 then
	print("0~15之间")
end


-- lua中，没有switch语法，需要自己实现