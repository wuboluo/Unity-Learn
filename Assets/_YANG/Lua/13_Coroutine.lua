print("---------- 13.Coroutine ----------")

print("---------- 协程的创建 ----------")
fun = function()
	print(123)
end

-- 常用create
cor = coroutine.create(fun)
print(cor)
print(type(cor))

cor2 = coroutine.wrap(fun)
print(cor2)
print(type(cor2))


print("---------- 协程的运行 ----------")
-- resume调用，对应由create出来的协程
coroutine.resume(cor)
-- 作为函数直接调用，对应wrap出来的协程
cor2()


print("---------- 协程的挂起 ----------")
fun2 = function()
	local i = 1
	while true do
		print(i)
		i = i + 1

		-- 查看协程当前状态
		print(coroutine.status(cor3))
		print(coroutine.running())

		-- 协程的挂起函数，此处的参数i为返回值，不写也可以执行。返回值可以有多个，可以自己根据需求来
		coroutine.yield(i, "abc")
	end
end

print("----- .create -----")
-- 每次resume或作为函数调用一次，执行到yield时，就会暂停，等待下一次调用重新进while循环
cor3 = coroutine.create(fun2)
-- resume的返回值：
-- 第一项：写成是否启动成功
-- 第二项：yield里面的返回值
isOk, temp, temp2 = coroutine.resume(cor3)
print(isOk, temp, temp2)
isOk, temp, temp2 = coroutine.resume(cor3)
print(isOk, temp, temp2)
isOk, temp, temp2 = coroutine.resume(cor3)
print(isOk, temp, temp2)

print("----- .wrap -----")
cor4 = coroutine.wrap(fun2)
-- 这种方式的协程调用也可以有返回值，只是没有默认的第一个返回值了
print("返回值：" .. cor4())
print("返回值：" .. cor4())
print("返回值：" .. cor4())


print("---------- 协程的状态 ----------")
-- coroutine.status(协程对象)
-- dead 结束
-- suspended 暂停
-- running 进行中

-- 只能查看由create创造出来的协程，也就是说需要传入一个thread对象
print(coroutine.status(cor3))
print(coroutine.status(cor))

-- 这个函数可以获取当前正在运行的协程的线程号
-- 需要在协程内部查看
print(coroutine.running())