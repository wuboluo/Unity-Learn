print("---------- 11.Require ----------")

print("---------- 全局变量和本地变量 ----------")
-- 全局变量
a = 1
b = "123"

for _ =1,2 do
	c = "C"
end
print(c)

print()

-- 本地变量
for _ =1,2 do
	local d = "D"
end
print(d)

Func = function()
	local tt = "123123"
end
Func()
print(tt)

local tt2 = "555"
print(tt2)

print("---------- 多脚本执行 ----------")
-- 关键字 require("脚本名")
-- 如果是纯lua开发，这个要写完整路径
require("Test")
print(test)
print(testLocal)


print("---------- 脚本卸载 ----------")
-- 如果是require加载执行的脚本，加载一次过后不会再被执行
require("Test")

-- 检查该脚本是否被加载过，返回值为bool
-- package.loaded["脚本名"]
print(package.loaded["Test"])

-- 卸载已经加载过的脚本
package.loaded["Test"] = nil
print(package.loaded["Test"])

-- require执行一个脚本时，可以在脚本最后返回一个外部希望获取的内容
local testL = require("Test")
print(testL)


print("---------- 大G表 ----------")
-- _G：是一个总表，本质上也是一个table，它将我们声明的 【所有全局变量】 存储其中
-- 本地变量（也就是加了local的变量）是不会存到大G表中的

for k,v in pairs(_G) do
	print(k,v)
end

