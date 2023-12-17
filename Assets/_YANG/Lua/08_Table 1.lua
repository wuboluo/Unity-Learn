print("---------- 08_Table 1 ----------")
-- 所有的复杂类型都是table

print("---------- 数组 ----------")
a = {1, 2, "3", true, nil}
a2 = {1, 2, 3, nil, 5}
a3 = {1, nil, 3, nil}

-- lua中索引从1开始，而非像c#从0开始
print(a[0])
print(a[1])
print()

-- #是通用的获取长度的关键字
-- 在获取长度的时候，会收到nil的影响
-- 如果 【nil在最后一位】 且 【仅有这一个nil】，则忽略这项
-- 如果 【nil在最后一位】 且 【期间还有其他nil】 且 【其他nil不为第一位】，长度则为从第一位到第一个nil之前
-- 如果 【中间位置存在nil】，则会被算入长度内
print(#a)
print(#a2)
print(#a3)


print("---------- 数组的遍历 ----------")
-- 不建议使用#获取长度遍历，因为会收到nil影响
for i=1,#a do
	print(a[i])
end


print("---------- 二维数组 ----------")
a = { {1,2,3}, {4,5,6} }
print(a[1][2])
print(a[2][3])


print("---------- 二维数组的遍历 ----------")
for x=1,#a do
	for y=1,#a[x] do
		print(a[x][y])
	end
end


print("---------- 自定义索引 ----------")
-- 不建议用，md深坑
a = { [0]=1,2,3,[-1]=4,5 }
print(a[0])
print(a[-1])
print()

-- 获取长度时，忽略<=0的索引
for i=1,#a do
	print(a[i])
end

print()

a2 = { [1]=1,[2]=2,[4]=4,[6]=6 }
for i=1,#a2 do
	print(a2[i])
end


print("---------- 冒泡排序 ----------")
a = {2,4,6,1,3,8,7,9,5}

Sort = {}
Sort.MaoPao = function(a)
	for i=1,#a do
		for j=1,#a-i do
			if a[j]>a[j+1] then
				a[j] = a[j]+a[j+1]
				a[j+1] = a[j]-a[j+1]
				a[j] = a[j]-a[j+1]
			end
		end
	end
end

Sort.MaoPao(a)
res = table.concat(a, " ")
print(res)


print("---------- 选择排序 ----------")
Sort.XuanZe = function(a)
	for i=1,#a do
		local index = i
		for j=i+1,#a do
			if a[index]>a[j] then
				index = j
			end
		end
		if index~=i then
			a[i] = a[i]+a[index]
			a[index] = a[i]-a[index]
			a[i] = a[i]-a[index]
		end
	end
end

Sort.XuanZe(a)
res = table.concat(a, " ")
print(res)


print("---------- 插入排序 ----------")
Sort.ChaRu = function()
	local noSortNum = 1
	local sortIndex = 1
	for i=1,#a do
		noSortNum = a[i]
		sortIndex = i-1
		while sortIndex>0 and noSortNum < a[sortIndex] do
			a[sortIndex+1] = a[sortIndex]
			sortIndex = sortIndex-1
		end
		a[sortIndex+1] = noSortNum
	end
end

Sort.ChaRu(a)
res = table.concat(a, " ")
print(res)

