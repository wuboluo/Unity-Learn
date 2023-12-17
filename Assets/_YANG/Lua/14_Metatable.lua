print("---------- 14.Metatable ----------")

print("---------- 元表概念 ----------")
-- 任何表变量都可以作为另一个表变量的元表
-- 任何表变量都可以有自己的元表（父亲）
-- 当我们“子表”（有元表的这个表）中进行一些特定操作时，会执行元表中的内容


print("---------- 设置元表 ----------")
meta = {}
myTable = {}

-- 设置元表（参数1:子表，参数2:元表）
setmetatable(myTable, meta)


print("---------- 特定操作 __tostring ----------")
meta2 = 
{
	-- 当子表要被当作字符串使用时，会默认调用元表中的 __tostring
	__tostring = function()
		return "Yang"
	end
}
myTable2 = {}
setmetatable(myTable2, meta2)
print(myTable2)


meta3 = 
{
	__tostring = function(t)
		return t.age
	end
}
myTable3 = 
{
	age = 25
}
setmetatable(myTable3, meta3)

-- 这里因为元表中的 __tostring方法有一个参数
-- 并且已知这个子表的元表是谁（也就是明确知道有age这个变量）
-- 此时 myTable3 会把自己当作参数传进去，也就是 t=myTable3
print(myTable3)


print("---------- 特定操作 __call ----------")
meta4 = 
{
	__tostring = function()
		return "meta4 __tostring"
	end,
	
	-- 当子表要被当作函数使用时，会默认调用元表中的 __call
	-- 当希望传参数时，默认第一个参数是调用者自己
	__call = function(a,b)
		-- 这里注意：此时 a=myTable4，在这里子表被当作字符串使用了，所以会先调用元表中的 __tostring 方法
		print(a)	-- a:myTable4
		print(b)	-- b:666
		print("听我说谢谢你")
	end
}
myTable4 = 
{   
	print("444")
}
setmetatable(myTable4, meta4)
myTable4(666)
--[[
	444					--> 被作为字符串，调用元表的 tostring
	meta4 __tostring 	--> 调用myTable本身的内容，因为myTable4被默认为第一个参数对应的a
	666					--> 打印实际传入的参数666对应b
	听我说谢谢你			--> call方法里剩余的内容
--]]


print("---------- 特定操作 运算符操作 ----------")
-- 在元表内实现特定运算规则的方法。相当于运算符重载

meta5 = 
{
	-- 子表使用对应运算符时，调用的不同方法

	-- 加法 +
	__add = function(t1,t2)
		return t1.money + t2.money
	end,

	-- 减法 -
	__sub = function(t1,t2)
		return t1.money - t2.money
	end,

	-- 乘法 *
	__mul = function(t1,t2)
		return t1.money * t2.money
	end,

	-- 除法 /
	__div = function(t1,t2)
		return t1.money / t2.money
	end,

	-- 取余 %
	__mod = function(t1,t2)
		return t1.money % t2.money
	end,

	-- 幂运算 ^
	__pow = function(t1,t2)
		return t1.money ^ t2.money
	end,

	-- 是否相等 ==
	__eq = function(t1,t2)
		return t1.money == t2.money
	end,

	-- 小于 <
	__lt = function(t1,t2)
		return t1.money < t2.money
	end,

	-- 小于等于 <=
	__le = function(t1,t2)
		return t1.money <= t2.money
	end,

	-- 拼接 ..
	__concat = function(t1,t2)
		return t1.money .. t2.money
	end

	-- 没有 >和>= 对应的函数，只能手动取反
}
myTable5 = { money = 100 }
setmetatable(myTable5, meta5)

myTable6 = { money = 50 }
setmetatable(myTable6, meta5)

print("----- 算术运算符 -----")
print(myTable5 + myTable6)
print(myTable5 - myTable6)
print(myTable5 * myTable6)
print(myTable5 / myTable6)
print(myTable5 % myTable6)
print(myTable5 ^ myTable6)

-- 当两个表进行条件运算符相关判断时，要保证他俩的元表是同一个。否则不会调用对应的函数，只是单纯比较这两个表是否如何。
print("----- 条件运算符 -----")
print(myTable5 == myTable6)
print(myTable5 < myTable6)
print(myTable5 <= myTable6)

print("----- 其他运算符 -----")
print(myTable5 .. myTable6)


print("---------- 特定操作 __index 和 __newindex ----------")
-- __index用于查询，__newindex用于更新

meta7 = {}
myTable7 = {}
setmetatable(myTable7, meta7)


print("----- __index -----")
meta88 = { number = 88 }
meta88.__index = meta88

-- 如果子表的元表指向的表中找不到对应的属性，会根据此元表是否还有元表且同样有 __index指向，会依次找上去，都没有返回nil
meta8 = 
{ 
	--number = 8 
}
meta8.__index = meta8

--[[
	__index尽量在表外初始化。如果写成：
	meta8 = 
	{ 
		number = 8,
		__index = meta8
	}
	
	则无法准确指向，会报空。
--]] 

myTable8 = {}

setmetatable(meta8, meta88)
setmetatable(myTable8, meta8)

-- 在子表中找不到调用的属性时，会去元表中__index指向的表中去找该属性
print(myTable8.number)

-- rawget：会忽略 __index，只在自己这个表中寻找这个属性，不存在返回nil
myTable8.number = 8
print(rawget(myTable8, "number"))


print("----- __newindex -----")
meta9 = {}
meta9.__newindex = meta9

myTable9 = {}
setmetatable(myTable9, meta9)

-- 当给表中的一个不存在的属性赋值时，会将这个属性和值放进元表__newindex指向的表中，不会影响此表
myTable9.number = 99

print(myTable9.number)	-- nil，由于 __newindex 元方法的存在，myTable9 中不存在 number 这个属性
print(meta9.number)		-- 99，由于 __newindex 元方法的存在，meta9 中被放进来一个 number 的属性

-- rawset：忽略 __newindex，将这个不存在的变量和其对应的值放进该表中，不会被扔进 __newindex指向的表
rawset(myTable9, "number", 9)
print(myTable9.number)


print("---------- 其他方法 ----------")
-- 得到元表的方法
print(getmetatable(myTable9))
print(getmetatable(myTable9) == meta9)
