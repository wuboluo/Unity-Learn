using System;
using System.Collections.Generic;

namespace Yang.CSharp.Notes
{
    internal class Item : IComparable<Item>
    {
        public int money;

        public Item(int money)
        {
            this.money = money;
        }

        public int CompareTo(Item other)
        {
            // 返回值的含义：
            // <0：放在传入对象的前面
            // =0：当前位置保持不变
            // >0：放在传入对象的后面

            // 简单理解，传入对象的位置就是0

            if (money > other.money)
                return 1;

            return -1;
        }
    }

    internal class ShopItem
    {
        public EE ee = EE.cc;
        public int id;

        public ShopItem(int id)
        {
            this.id = id;
        }

        public ShopItem(int id, EE MyEnum)
        {
            this.id = id;
            Console.WriteLine(id + MyEnum.ToString());
        }

        public void CwTest()
        {
            Console.WriteLine("----- From: _28_List排序.ShopItem.CwTest -----");
        }
    }

    public enum EE
    {
        aa,
        bb,
        cc
    }

    internal class Notes_ListSort
    {
        private static void Main(string[] args)
        {
            List<int> list = new List<int> { 3, 5, 1, 9, 7 };
            list.Sort();

            // 通过继承接口
            List<Item> itemList = new List<Item> { new(10), new(45), new(72), new(3) };
            itemList.Sort();

            foreach (Item t in itemList)
                Console.WriteLine(t.money);


            // 通过委托函数
            List<ShopItem> shopItem = new List<ShopItem> { new(3), new(9), new(21) };
            //shopItem.Sort(SortShopItem);
            shopItem.Sort((a, b) => a.id > b.id ? 1 : -1);

            foreach (ShopItem t in shopItem)
                Console.WriteLine(t.id);
        }

        private static int SortShopItem(ShopItem a, ShopItem b)
        {
            // 传入的两个对象，为列表中的两个对象
            // 进行两两比较，用左边和右边的条件比较
            // 返回值规则同接口，0为标准，正后负前
            if (a.id > b.id) return 1;
            return -1;
        }
    }

// 总结：
// 系统自带的变量(int,float,double...)，一般都可以直接 sort
// 自定义类 Sort方式：
// 1，继承接口 IComparable
// 2，在 Sort参数中传入委托函数
}