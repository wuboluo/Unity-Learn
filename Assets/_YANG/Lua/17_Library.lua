print("---------- 17.Library 自带库 ----------")

-- string 详见03_String
-- table 详见10_Table 3


print("---------- 时间 ----------")
-- 系统时间
print(os.time())
-- 传入一个日期得到时间
print(os.time( {year=2022, month=10, day=10} ))

-- os.date("*t")
-- 返回一个表，有当前很详细的时间
local nowTime = os.date("*t")
print(nowTime["year"])
print(nowTime["day"])

for k,v in pairs(nowTime) do
	--print(k,v)
end


print("---------- 数学运算 ----------")

-- 绝对值
print(math.abs(-10))		-- 10

-- 弧度转角度
print(math.deg(math.pi))	-- 180

-- 三角函数（传弧度）
print(math.cos(math.pi))	-- -1

-- 向下向上取整
print(math.floor(2.8))		-- 2
print(math.ceil(3.2))		-- 4

-- 最大最小值
print(math.max(-1,1))		-- 1
print(math.min(-1,1))		-- -1

-- 把小数分离成整数部分和小数部分
print(math.modf(1.2))		-- 1	0.2 

-- 幂运算
print(math.pow(2,5))		-- 32

-- 随机数
-- 需要先设置随机数的种子
math.randomseed(os.time())
print(math.random(100))
print(math.random(100))

-- 开方
print(math.sqrt(4))			-- 2


print("---------- 路径 ----------")
-- lua脚本加载路径，可以被修改，但是是由分号隔开
print(package.path)
package.path = package.path .. ";----------"
print(package.path)

print()