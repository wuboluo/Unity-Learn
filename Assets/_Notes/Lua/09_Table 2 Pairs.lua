print("---------- 09_Table 2 Pairs ----------")
-- 迭代器遍历，主要是用来遍历表的
-- #得到的长度，其实并不准确，一般不要用#来遍历表

a = { [0]=1,2,[-1]=3,4,5,[5]=6 }


print("---------- ipairs ----------")
-- 还是从1开始往后遍历，<=0的值得不到
-- 只能找到连续索引的键值，如果中间断序了，就无法遍历出后面的内容
for i,v in ipairs(a) do
	print(i,v)
end


print("---------- pairs ----------")
-- 它能够把所有的键都找到，通过键找到对应的值
for k,v in pairs(a) do
	print(k,v)
end


print("---------- pairs只遍历key ----------")
for i in pairs(a) do
	print(i)	
end
