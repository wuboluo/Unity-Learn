print("---------- 18_Collectgarbage ----------")

-- 垃圾回收关键字：collectgarbage

test = { id=1, name="LM" }

print("---------- 当前内存占用 ----------")
-- 获取当前lua占用内存数（K字节）用返回值*1024就可以得到具体的内存占用字节数
print(collectgarbage("count"))


print("---------- 垃圾回收，类似C#的GC ----------")
-- 解除羁绊就是变垃圾，相当于C#中解除引用
test = nil

collectgarbage("collect")
print(collectgarbage("count"))



-- lua中有自动定时进行GC的方法
-- Unity热更新开发时，尽量不要去用自动回收垃圾，比较消耗性能
-- 但是在切换场景或内存达到瓶颈时，可以手动执行一次
