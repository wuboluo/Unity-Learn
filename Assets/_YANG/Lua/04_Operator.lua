print("---------- 04.Operator ----------")


print("---------- 算数运算符 ----------")
--[[
	+ - * / %
	没有自增自减(++ --)
	没有复合运算符(+= -= *= /= %=)	

	string可以进行算术运算符，会自动转成 number
--]]
print("加法运算 " .. 1 + 2)
a = 1
b = 2
print(a + b)
print("10" + 5)
print("10.5" + 5)

print("减法运算 " .. 3 - 1.5)
print("3" - 1.5)

print("乘法运算 " .. 3 * 1.5)
print("3" * 1.5)

print("除法运算 " .. 3 / 1.5)
print("3" / 1.5)

print("取余运算 " .. 3 % 1)
print("3" % 1.2)

-- ^ lua中是幂运算（次方）
print("幂运算 " .. 2 ^ 2)
print("2" ^ 5)


print("---------- 条件运算符 ----------")
-- > < >= <= == ~=
print(3 > 1)
print(3 < 1)
print(3 >= 1)
print(3 <= 1)
print(3 == 1)
print(3 ~= 1)


print("---------- 逻辑运算符 ----------")
--[[ 
	c#:  &&  ||  !
	lua: and or  not

	and: 同true才true
	or:  有true则true
--]] 
print("----- and -----")
print(true and true)
print(true and false)
print(false and false)

print("----- or -----")
print(true or true)
print(true or false)
print(false or false)

print("----- not -----")
print(not true)
print(not false)

print("----- 短路 -----")
-- 当and前的判断如果为false，后面的判断就不会再继续执行
print(true and type(true))
print(false and type(true))


print("---------- 位运算符 ----------")
-- c#: & |
-- lua: 不支持

print("---------- 三目运算符 ----------")
-- c#: ?:
-- lua: 不支持
