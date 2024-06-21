print("---------- 12.Special ----------")

print("---------- 多变量赋值 ----------")
a,b,c = 1,2,"3"
print(a)
print(b)
print(c)

print()

-- 多变量赋值时，如果后面的值不够会自动补空，多的话会被忽略掉
local a,b,c = 1,2
print(a)
print(b)
print(c)


print("---------- 多返回值 ----------")
function Test()
	return 10,20
end

-- 多返回值时，根据接受的变量数依次赋值，不够补空，多的忽略
a,b,c = Test()
print(a)
print(b)
print(c)


print("---------- and or ----------")
--[[
	and和or 不仅可以连接bool变量，任何类型都可以连接
	lua中只有 nil和false 认为是假

	and：有假则假 or：有真则真
	会根据“短路”原则判断
	所以，只需要判断第一个是否满足，就会停止计算
--]]

-- and：一真返二，一假返一
print("----- and -----")
print(1 and "123")		-- 123
print(1 and nil)		-- nil
print(true and 1)		-- 1
print(false and 1)		-- false
print(nil and false)	-- nil

-- or：一真返一，一假返二
print("----- or -----")
print(1 or "123")		-- 1
print(1 or nil)			-- 1
print(true or 1)		-- true
print(false or 1)		-- 1
print(false or nil)		-- nil

-- lua不支持三目运算符
print("----- ?: -----")
a,b,c = 1,2,3

-- 分析：由左向右依次判断
local resAB = (a>b) and a or b
--[[
	a>b 		=> false
	false and a	=> false
	false or b	=> b
--]] 
local resCB = (c>b) and b or c
--[[
	c>b 		=> true
	true and b	=> b
	b or c		=> b
--]]

print(resAB)	-- 2(b)
print(resCB)	-- 2(b)