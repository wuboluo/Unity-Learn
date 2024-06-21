namespace Yang.CSharp.Notes.Indexer
{
    public class Person
    {
        private Person[] friends;

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public string Name { get; }
        private int Age { get; }

        
        public string this[string str]
        {
            get
            {
                return str switch
                {
                    "name" => Name,
                    "age" => Age.ToString(),

                    _ => string.Empty
                };
            }
        }
        public Person this[int index]
        {
            get
            {
                if (friends == null || friends.Length < index - 1) return null;
                return friends[index];
            }
            set
            {
                if (friends == null)
                {
                    friends = new[] { value };
                }
                else if (index > friends.Length - 1)
                {
                    // 选择数组最某一项三种方式：
                    // friends[new Index(1, true)]
                    // friends[friends.Length - 1]
                    friends[^1] = value;
                }

                friends[index] = value;
            }
        }
    }
       
    // -------------------- 写法：
    // 访问修饰符 返回值 this[参数类型 参数名，参数类型 参数名]
    // {
    //      get;
    //      set;
    // }
    
    // -------------------- 作用：
    // 以 '[]' 的形式访问访问自定义类中的元素，自定义规则，访问时和数组一样
    // 适用于在类中有数组变量时使用，可以方便的访问和进行逻辑处理

    // -------------------- 注意：
    // 1，索引器可以重载
    // 2，索引器中可以添加逻辑
    // 3，结构体同样支持索引器
}