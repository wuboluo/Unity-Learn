#nullable enable
using System;
using UnityEngine;

namespace Yang.CSharp.Notes
{
    public class Notes_Operator : MonoBehaviour
    {
        private void Start()
        {
            Point p1 = new Point { x = 1, y = 1 };
            Point p2 = new Point { x = 2, y = 2 };

            Point p3 = p1 + p2;
            Console.WriteLine(p3.x + "  " + p3.y);

            Point p4 = p3 + 2;
            Console.WriteLine(p4.x + "  " + p4.y);
        }
    }

    internal class Point
    {
        public int x, y;

        private bool Equals(Point other)
        {
            return x == other.x && y == other.y;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Point)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }

        // 算数运算符 + - * / % ++ --
        public static Point operator +(Point p1, Point p2)
        {
            Point p = new Point { x = p1.x + p2.x, y = p1.y + p2.y };
            return p;
        }

        public static Point operator +(Point p1, int value)
        {
            Point p = new Point { x = p1.x + value, y = p1.y + value };
            return p;
        }

        public static Point? operator ++(Point p1)
        {
            return null;
        }

        // 逻辑运算符 只有逻辑非可以重载
        public static bool operator !(Point p1)
        {
            return false;
        }

        // 位运算符
        public static Point? operator |(Point p1, Point p2)
        {
            return null;
        }

        public static Point? operator &(Point p1, Point p2)
        {
            return null;
        }

        public static Point? operator ^(Point p1, Point p2)
        {
            return null;
        }

        public static Point? operator ~(Point p1)
        {
            return null;
        }

        public static Point? operator <<(Point p1, int num)
        {
            return null;
        }

        public static Point? operator >> (Point p1, int num)
        {
            return null;
        }

        // 条件运算符  需要成对实现（有 > 就要 有 <）
        public static bool operator >(Point p1, Point p2)
        {
            return false;
        }

        public static bool operator <(Point p1, Point p2)
        {
            return false;
        }

        public static bool operator >=(Point p1, Point p2)
        {
            return false;
        }

        public static bool operator <=(Point p1, Point p2)
        {
            return false;
        }

        public static bool operator ==(Point p1, Point p2)
        {
            return false;
        }

        public static bool operator !=(Point p1, Point p2)
        {
            return false;
        }


        // 不能重载的运算符
        // && ||
        // []
        // ()
        // . ?: =
    }

    // 让自定义类和结构体能够使用运算符
    // operator

    // 特点：
    // 1，一定是一个公共的静态方法
    // 2，返回值写在 operator 前面
    // 3，逻辑处理自定义

    // 作用：
    // 让自定义类和结构体对象可以进行运算

    // 注意：
    // 1，条件运算符需要成对实现
    // 2，一个符号可以多个重载
    // 3，不能使用 ref 和 out

    // public static 返回类型 operator 运算符（参数列表）
}