print("---------- 10.Table 3 Dict Class ----------")

print("---------- 字典的声明 ----------")
-- 字典是由键值对构成
a = { ["name"]="Yang", ["age"]=20, ["money"]=100, ["1"]=10 }

-- 访问当个变量，用中括号填键来访问
print(a["name"])
print(a["age"])

-- 还可以通过类似 【.成员变量】 的形式来得到值，但是这个key不能是数字
print(a.money)

-- 修改
a.money = 200
print(a.money)

-- 添加
a.city = "shijiazhuang"
print(a.city)

-- 删除（如果访问一个不存在的key，同样会返回nil，从某种角度上，置空也可认为是删除）
a.city = nil
print(a.city)


print("---------- 字典的遍历 ----------")
-- 如果要遍历字典，一定要用pairs
for i,v in pairs(a) do
	-- 可以传多个参数，一样会通过空格分开并打印出来
	print(i,v,"zzz")
end

print("\n--- 只打印key ---")
for k in pairs(a) do
	print(k)
end

print("\n--- 只打印value ---")
for k in pairs(a) do
	print(a[k])
end

for _,v in pairs(a) do
	print(v)
end

print("---------- 类和结构体 ----------")
-- lua中是默认没有面向对象的，需要我们自己实现
-- 方法之间不要忘记使用'，'隔开
Student = 
{ 
	age = 10,
	sex = true,

	Up = function()
		-- 这样写的【age】和表中的【age】没有任何关系，这里的是一个全局变量
		-- print(age)
		--[[
			调用自身变量方法一：
			想要在表内部函数中，调用表本身的属性或方法
			一定要指明是谁的，所以要使用 【表名.属性】 或 【表名.方法】
		--]]
		print(Student.age)
		print("成长了")
	end,

	Learn = function(t)
		--[[
			调用自身变量方法二：
			把自己作为一个参数传进来，在内部访问
		--]]
		print(t.age)
		print("好好学习，天天向上")
	end,
}
Student.Up()

--[[
	lua中'.'和':'的区别
	冒号调用方法会默认把调用者作为第一个参数传入方法中
--]]
Student.Learn(Student)
Student:Learn()

print()

-- 声明了一个表之后，在表外声明对象或函数也可以
Student.name = "Yang"
Student.Speak = function()
	print("小声bb")
end

-- 冒号可以声明函数，但只能通过 【function 表名:函数名】 的形式
-- 不会受到其他参数影响，默认为第一个，只不过不显示出来
function Student:Go(where)
	-- lua中self关键字，表示默认传入的第一个参数
	-- self不是this！
	print(self.name .. " is going to " .. where)
end

-- c#中使用类的内容，需要new实例对象，或者静态直接点出来
-- lua中类的表现，更像是类中有很多静态变量和函数
print(Student.name)
print(Student.age)
Student.Speak()

-- 由于Go是由':'声明的，而本身又自带一个参数，所以在调用时，要么使用冒号自动调用本身，要么使用.且手动传入第一个参数
Student:Go("Beijing")
Student.Go(Student, "Beijing")


print("---------- 表的公共操作 ----------")
-- 表中提供的一些公共方法

t1 = 
{
	{name = "A", age = 10},
	{name = "B", age = 20}
}

t2 = 
{
	name = "Yang", age = 25
}

-- 插入
-- 把t2插入到t1的结尾处
print("----- insert -----")
table.insert(t1, t2)
print("length:" .. #t1)
print(t1[3].name)

-- 删除指定元素
-- remove方法：传一个表进去，会移除最后一个索引的内容
print("----- remove -----")
table.remove(t1)
print("length:" .. #t1)
print(t1[1].name)
print(t1[2].name)
print(t1[3])

-- 传两个参数时，第二个参数是移除对应的索引和其内容
table.remove(t1, 1)
print("length:" .. #t1)
print(t1[1].name)

print("----- sort -----")
t2 = {5,2,7,9,0}

-- 单传一个表作为参数，默认升序
print("升序")
table.sort(t2)
res = table.concat(t2, "<")
print(res)

-- 自定义排序规则，例如：降序
print("降序")
function SortFunc(a,b)
	if a>b then
		return true
	end
end
table.sort(t2, SortFunc)
res = table.concat(t2, ">")
print(res)

print("----- concat -----")
-- 拼接，只能拼接数字和字符串
-- 参数为：表，连接符号，从第几个开始，到第几个结束
tb = {"123", "456", 789, "1010"}
res = table.concat(tb, ":")
res2 = table.concat(tb, ":", 3, 4)
print(res)
print(res2)