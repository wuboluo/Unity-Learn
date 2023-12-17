-- 16.OO Summary
-- 实现一个万物之父 Object

Object = {}

-------------------------------------------------- 封装
-- new 实例化方法
function Object:new()
	local obj = {}

	self.__index = self
	setmetatable(obj, self)

	return obj
end

-------------------------------------------------- 继承
-- subClass 根据名字生成一个新表（一个新类）
function Object:subClass(className)
	_G[className] = {}
	local obj = _G[className]

	obj.base = self
	self.__index = self
	setmetatable(obj, self)
end

Object:subClass("GameObject")
GameObject.posX = 0
GameObject.posY = 0
function GameObject:Move()
	self.posX = self.posX + 1
	self.posY = self.posY + 1
	print(self.posX .. " " .. self.posY)
end

local obj1 = GameObject:new()
obj1:Move()

local obj2 = GameObject:new()
obj2:Move()

-------------------------------------------------- 多态
-- Player 重写同时执行父类(GameObject)的 Move()方法
GameObject:subClass("Player")
function Player:Move()
	self.base.Move(self)

	self.posX = self.posX + 1
	self.posY = self.posY + 1
	print(self.posX .. ":" .. self.posY)
end

print("----------")
local p1 = Player:new()
p1:Move()
p1:Move()

print("----------")
local p2 = Player:new()
p2:Move()
p2:Move()
