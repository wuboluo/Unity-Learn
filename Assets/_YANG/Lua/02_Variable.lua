print("---------- 02.变量 ----------")
--[[
	简单变量类型：

	nil 
	number 
	string 
	boolean
--]] 

--[[
	复杂数据类型：

	function 函数 
	table 表 
	userdata 数据结构 
	thread 协同程序（线程）
--]]


--[[
	lua中所有的变量声明，都不需要声明变量类型，它会自动判断类型，类似于c#中的 var
	lua中的变量可以随意赋值

	通过type函数，可以得到变量类型，type返回值类型是 string
--]]

print("---------- nil ----------")
-- 类似于c#中的 null
a = nil
print(a)
print(type(a))


print("---------- number ----------")
-- 所有的数值都是 number
a = 1
print(a)
a = 1.2
print(a)
print(type(a))


print("---------- string ----------")
-- 字符串的声明是用 '' 或 "" 都可以
a = "123"
print(a)
a = '456'
print(a)
print(type(a))


print("---------- boolean ----------")
a = true
print(a)
a = false
print(a)
print(type(a))


print("---------- type类型返回值 ----------")
print(type(type(a)))


print("---------- 未声明变量 ----------")
-- lua中使用没有声明的变量，不会报错，默认值是 nil
print(b)