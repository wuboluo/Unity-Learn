print("---------- 07_Function ----------")
--[[
	function 函数名(...)
	end

	a = function()
	end
--]]


print("---------- 无参数无返回值 ----------")
function F1()
	print("F1函数")
end
F1()

-- 有点类似 c#中的委托和事件
F2 = function()
	print("F2函数")
end
F2()


print("---------- 有参数 ----------")
function F3(a)
	print(a)
end
F3(1)
F3("123")
F3(true)
-- 如果你传入的参数和函数参数个数不匹配，不会报错，只会补空nil，或者丢弃
F3()
F3(1,2,3)


print("---------- 有返回值 ----------")
function F4(a)
	return a, 1, true
end
temp = F4("123")
print(temp)

-- 多返回值时，在前面声明多个变量接受即可
-- 如果变量不够，不影响，只接取对应位置的返回值
-- 如果变量超出，不影响，直接赋nil
temp2, temp3, temp4, temp5 = F4("456")
print(temp2)
print(temp3)
print(temp4)
print(temp5)


print("---------- 函数的类型 ----------")
F5 = function()
end
print(type(F5))


print("---------- 函数的重载 ----------")
-- 函数名相同，参数类型不同或参数个数不同
function F6()
	print("6")
end

F6 = function(str)
	print(str)
	print("7")
end

-- lua中不支持重载，默认调用最后一个声明的函数，lua认为只是重新对这个函数变量重新赋值而已，被覆盖了
F6()


print("---------- 变长参数 ----------")
function F7( ... )
	-- 变长参数使用，先用一个table存起来，再使用
	arg = {...}
	for i=1,#arg do
		print(arg[i])
	end
end

F7(1,"123",true)


print("---------- 函数嵌套 ----------")
function F8()
	-- 这里面不能给函数命名
	return function()
		print("8")
	end
end

f8 = F8()
f8()


-- 面试题：lua中如何体现闭包
-- 闭包：改变传入参数的生命周期。通过调用含有一个 内部函数( function(y) ) 加上 该外部函数持有的外部局部变量( x ) 的外部函数( F9 )产生的一个实例函数( f9 )
-- 组成：外部函数 + 外部函数创建的外部局部变量 + 内部函数（闭包函数）

function F9(x)
	return function(y)
		return x + y
	end
end

F1 = function(x)
	return function(y)
		return x+y
	end
end

f9 = F9(10)
result = f9(5)	-- x=10, y=5, result=10+5
print(result)
