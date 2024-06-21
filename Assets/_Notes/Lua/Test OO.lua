Object = {}

function Object:new()
    local obj = {}

    self.__index = self
    setmetatable(obj, self)

    return obj
end

function Object:subClass(className)
    _G[className] = {}
    local obj = _G[className]

    self.__index = self
    self.base = self
    setmetatable(obj, self)
end

Object:subClass("GameObject")

GameObject.posX = 0
function GameObject:Move()
    self.posX = self.posX + 10
    print(self.posX)
end

GameObject:subClass("Player")
function Player:Move()
    self.base.Move(self)
    print(self)

    self.posX = self.posX + 10
    print(self.posX)
end

local p1 = Player:new()
print(p1)
p1:Move()
