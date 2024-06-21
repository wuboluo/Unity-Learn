namespace Yang.CSharp.Notes
{
    internal class Notes_CovarianceAndContravariance
    {
        private static void Main(string[] args)
        {
            // 作用（结合里氏替换原则理解）
            // 协变 父类总能被子类替换
            // 此处的 = 是在给 TestOut<T>这个委托赋值
            // 看起来 son -> father
            TestOut<Son> os = () => new Son();

            TestOut<Father> of = os;
            Father f = of(); // 实际上返回的是 os返回值：Son

            // 逆变 父类总能被子类替换
            // 看起来 father -> son，明明是传父类，但是传了子类，不和谐
            TestIn<Father> iF = value => { };
            TestIn<Son> iS = iF;
            iS(new Son()); // 实际上 调用的是 iF
        }
        // ---------------------------------------- 协变逆变
        // 协变：
        // 和谐的变化，自然的变化
        // 因为里氏替换原则，父类可以装子类。所以 子类变父类（比如string -> object）感受是和谐的

        // 逆变：
        // 逆常规的变化，不正常的变化
        // 因为里氏替换原则，子类不能装父类。所以 父类变子类（比如object -> string）感受是不和谐的

        // 协变 out
        // 逆变 in
        // 用于在泛型中，修饰泛型字母的
        // 只有泛型接口和泛型委托可以使用


        // ---------------------------------------- 作用
        // 1，返回值 和 参数
        // 用 out修饰的泛型，只能作为返回值
        private delegate T TestOut<out T>();

        // 用 in修饰的泛型，只能作为参数
        private delegate void TestIn<in T>(T t);

        private interface ITest<out T, in K>
        {
            T TestFun();
            void TestFun(K k);
        }

        // 2，结合里氏替换原则
        private class Father
        {
        }

        private class Son : Father
        {
        }
    }

// 总结：
// 协变 out
// 逆变 in
// 用来修饰 泛型替代符（只能修饰接口和委托中的泛型）

// 作用：
// 1，out修饰的泛型类型，只能作为返回值类型；in修饰的泛型类型，只能作为参数类型
// 2，遵循里氏替换原则，用 out 和 in 修饰的泛型委托，可以相互装载（有父子关系的泛型）

// 协变，父类泛型委托 装 子类泛型委托
// 逆变，子类泛型委托 装 父类泛型委托
}