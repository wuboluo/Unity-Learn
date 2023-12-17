print("---------- 03.String ----------")

s = '单引号string'
s2 = "双引号string"


print("---------- 字符串长度 ----------")
-- 一个汉字占3个长度
s = "aBcdEfG"
s2 = "aBcdEfG字符串"
print(#s)
print(#s2)


print("---------- 字符串多行打印 ----------")
-- lua中也是支持转义字符的
print("123\n456")

s = [[
你好
	我也好
]]
print(s)


print("---------- 字符串拼接 ----------")
-- 字符串拼接通过 ..
print("123" .. "456")
s = "abc"
s2 = 123
print(s .. s2)

print(string.format("我是yang，我今年%d岁", 26))
--[[
	%d：数字拼接
	%a：与任何字符拼接
	%s：与字符拼接
--]] 


print("---------- 别的类型转字符串 ----------")
a = true
a2 = tostring(a)
print(a)
print(a2)


print("---------- 字符串提供的公共方法 ----------")
s = "aBcdEfGcd"

-- 小写转大写
print(string.upper(s))
-- 大写转小写
print(string.lower(s))
-- 反转字符串
print(string.reverse(s))
-- 字符串索引查找（开始的位置和结束的位置，从1开始）
print(string.find(s, "cdE"))
-- 截取字符串（可以重载，设置截取长度）
print(string.sub(s, 3))
print(string.sub(s, 3, 4))
-- 字符串重复（连续拼接多次）
print(string.rep(s, 2))
-- 字符串修改（第二个返回值为修改了几处）
print(string.gsub(s, "cd" ,"**"))
-- 字符转ASCII码
a = string.byte("Lua", 1)
print(a)
-- ASCII码转字符
print(string.char(a))

-- 但是不会改变原字符串
print(s)