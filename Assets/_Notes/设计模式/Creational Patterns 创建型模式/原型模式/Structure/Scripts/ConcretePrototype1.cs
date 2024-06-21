namespace Yang.DesignPattern.Prototype.Structure
{
    public class ConcretePrototype1 : Prototype
    {
        public ConcretePrototype1(string id) : base(id)
        {
        }

        public override Prototype Clone()
        {
            // MemberwiseClone() 位浅复制
            return MemberwiseClone() as Prototype;
        }
    }
}

// 关于深浅复制：
// 值类型：深浅复制一样，对字段逐位复制
// 引用类型：浅复制 原始对象和其副本引用同一个对象
//          深复制 其副本指向一个新的对象