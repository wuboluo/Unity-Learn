print("---------- 06_Loop ----------")

print("---------- while循环 ----------")
-- while 条件 do...end
num = 0
while num < 5 do
	print(num)
	num = num + 1
end


print("---------- do while循环 ----------")
-- repeat...until 结束条件（满足条件跳出）
num = 0
repeat
	print(num)
	num = num + 1
until num > 5


print("---------- for循环 ----------")
-- for 变量名=起始值,结束值,增量 do...end
-- lua中默认递增
for i = 1,5 do
	print(i)
end

print()

-- 如果想自定义增量，那就写第三个数：递增的值
for i = 1,5,2 do
	print(i)
end

print()

-- 递减，将增量改为负数即可
for i = 5,1,-1 do
	print(i)
end