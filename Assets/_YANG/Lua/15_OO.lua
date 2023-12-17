print("---------- 15.OO 面向对象 ----------")

print("---------- 封装 ----------")
-- 面向对象，类这些概念都是基于 table 来实现的
-- 用到元表相关的知识点
Object = {}
Object.id = 1

-- 建议都用冒号声明方法
function Object:Test()
	print(self.id)
end

function Object:new()
	-- 1，声明一个变量，把这个变量返回出去代表 “Object的实例”
	local obj = {}

	-- 3，通过 __index元方法将obj和self的元素关联起来
	self.__index = self

	-- 2，将 obj和Object关联到一起，由于是冒号生命的new方法，所以这里只需要传入self
	setmetatable(obj, self)

	return obj
end

-- 注意要用冒号调用，因为new是由冒号声明的
myObj = Object:new()

print(myObj)
print(myObj.id)
myObj:Test()

print()

-- myObj是声明的一个新表，只是在声明的时候给它赋值为Object
-- 这里的赋值相当于在myObj表中声明了一个：值为2，名叫id的变量
myObj.id = 2
-- 所以Object本身中的id不会受到影响
print(Object.id)	-- 1
print(myObj.id)		-- 2
-- 但是再次调用Test方法时，因为myObj已有id这个属性，所以就不会执行 __index 元方法去Object里面找了
myObj:Test()		-- 2


print("---------- 继承 ----------")
function Object:subClass(className)
	-- 1，首先在总表中创建一个指定名称的表变量
	_G[className] = {}
	
	-- 2，创建一个表变量，将刚才指定名称的表变量赋值给他
	local obj = _G[className]

	-- 4，让 obj2和Object 元素也关联起来。但是这里可以不写，因为在上面new方法中，已经通过 __index指向了Object
	self.__index = self
	
	-- 【多态添加】：创建一个base的概念，指向父类，以便拿到父类的东西
	obj.base = self

	-- 3，将我们创造的 想要继承于Object 的表和Object关联起来
	setmetatable(obj, self)
end

-- 创建一个元表为Object的 名为Person的表
Object:subClass("Person")

-- 创建一个Person表的对象
-- 由于__index指向Object，所以可以调用new方法。方法本身也是可以通过 __index 依次向上查找并调用
local p1 = Person:new()

-- 因为上一行是通过 【Person:】 调用的，此时__index指向Person。
-- 但是没有在Person中找到，所以会去Person的元表Object __index指向的表（还是Object）中去找
print(p1.id)


Object:subClass("Monster")
local m1 = Monster:new()
print(m1.id)
m1.id = 100
print(m1.id)


print("---------- 多态 ----------")
-- 相同行为，不同表现
-- 相同方法，不同执行逻辑

Object:subClass("GameObject")
GameObject.posX = 0
GameObject.posY = 0
function GameObject:Move()
	self.posX = self.posX + 1
	self.posY = self.posY + 1

	print("x:" .. self.posX .. " y:" .. self.posY)
end

GameObject:subClass("Player")
function Player:Move()
	-- 这里会覆盖掉GameObject里的Move方法，如果想要拿到父类（GameObject）里的Move方法
	-- 在c#中可以通过base获取父类的内容，但是lua中需要手动创建base这个概念
	-- 办法：创建一个对象指向父类（GameObject）在【继承】内容的subClass方法中添加一个base对象，详见上面 Object:subClass
	print("这里是Player父类的Move内容：")
	-- self.base:Move()		-- 不能这么写，原因见下面详细说明
	self.base.Move(self)

	print("这里是Player的Move内容：")
	self.posX = self.posX + 1
	self.posY = self.posY + 1

	print("x:" .. self.posX .. " y:" .. self.posY)
end

local p1 = Player:new()
p1:Move()

-- 这种写法有坑。此时声明了一个新的p2对象，所以执行Move方法时，应该也是从0开始，但是这样会根据上面p1的结果继续
-- 前提：在 【Player:Move】 中书写 self.base:Move()
-- 原因：本身Player表中不存在 posX,posY 这两个变量，所以p1,p2改的值都是改的GameObject中的 posX,posY
-- 解决办法：把【前提】中的代码更改为 self.base.Move(self)
-- 			这样，哪怕Player表中没有posX,posY变量，也会在Move方法声明，也就是各用各的了
local p2 = Player:new()
p2:Move()
