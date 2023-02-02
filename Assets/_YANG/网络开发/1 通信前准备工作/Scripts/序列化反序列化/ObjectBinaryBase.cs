using System;
using System.Text;

namespace Yang.Net.Serialize
{
    public abstract class ObjectBinaryBase
    {
        /// <summary>
        ///     用于子类重写的 获取字节数组容器大小 的方法
        /// </summary>
        /// <returns></returns>
        public abstract int GetBytesNumber();

        /// <summary>
        ///     把成员变量 序列化为 对应的字节数组
        /// </summary>
        /// <returns></returns>
        public abstract byte[] Writing();

        /// <summary>
        ///     把二进制字节数组 反序列化到 成员变量当中
        /// </summary>
        /// <param name="bytes">反序列化使用的字节数组</param>
        /// <param name="beginIndex">从该字节数组的第几个位置开始解析，默认是0</param>
        /// <returns>一共读取了多少字节</returns>
        public abstract int Reading(byte[] bytes, int beginIndex = 0);

        /// <summary>
        ///     存储int类型变量到指定的字节数组当中
        /// </summary>
        /// <param name="bytes">指定字节数组</param>
        /// <param name="value">具体的int值</param>
        /// <param name="index">每次存储后用于记录当前索引位置的变量</param>
        protected static void WriteInt(byte[] bytes, int value, ref int index)
        {
            BitConverter.GetBytes(value).CopyTo(bytes, index);
            index += sizeof(int);
        }

        protected static void WriteShort(byte[] bytes, short value, ref int index)
        {
            BitConverter.GetBytes(value).CopyTo(bytes, index);
            index += sizeof(short);
        }

        protected static void WriteLong(byte[] bytes, long value, ref int index)
        {
            BitConverter.GetBytes(value).CopyTo(bytes, index);
            index += sizeof(long);
        }

        protected static void WriteFloat(byte[] bytes, float value, ref int index)
        {
            BitConverter.GetBytes(value).CopyTo(bytes, index);
            index += sizeof(float);
        }

        protected static void WriteByte(byte[] bytes, byte value, ref int index)
        {
            bytes[index] = value;
            index += sizeof(byte);
        }

        protected static void WriteBool(byte[] bytes, bool value, ref int index)
        {
            BitConverter.GetBytes(value).CopyTo(bytes, index);
            index += sizeof(bool);
        }

        protected static void WriteString(byte[] bytes, string value, ref int index)
        {
            // 先存储string字节数组的长度
            byte[] strBytes = Encoding.UTF8.GetBytes(value);
            // BitConverter.GetBytes(strBytes.Length).CopyTo(bytes, index);
            // index += sizeof(int);
            WriteInt(bytes, strBytes.Length, ref index);
            // 再存 string字节数组
            strBytes.CopyTo(bytes, index);
            index += strBytes.Length;
        }

        /// <summary>
        ///     自定义类数据序列化。将自定义类内部序列化好的整体数据 拷贝到 总字节数组的尾部
        /// </summary>
        /// <param name="bytes">总字节数组</param>
        /// <param name="data">继承自 ObjectBinaryBase 的自定义信息类</param>
        /// <param name="index">插入到总字节数组的位置</param>
        protected static void WriteData(byte[] bytes, ObjectBinaryBase data, ref int index)
        {
            data.Writing().CopyTo(bytes, index);
            index += data.GetBytesNumber();
        }

        /// <summary>
        ///     根据字节数组读取整形
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="index">开始读取的索引数</param>
        /// <returns></returns>
        protected static int ReadInt(byte[] bytes, ref int index)
        {
            int value = BitConverter.ToInt32(bytes, index);
            index += sizeof(int);
            return value;
        }

        protected static short ReadShort(byte[] bytes, ref int index)
        {
            short value = BitConverter.ToInt16(bytes, index);
            index += sizeof(short);
            return value;
        }

        protected static long ReadLong(byte[] bytes, ref int index)
        {
            long value = BitConverter.ToInt64(bytes, index);
            index += sizeof(long);
            return value;
        }

        protected static float ReadFloat(byte[] bytes, ref int index)
        {
            float value = BitConverter.ToSingle(bytes, index);
            index += sizeof(float);
            return value;
        }

        protected static byte ReadByte(byte[] bytes, ref int index)
        {
            byte value = bytes[index];
            index += sizeof(byte);
            return value;
        }

        protected static bool ReadBool(byte[] bytes, ref int index)
        {
            bool value = BitConverter.ToBoolean(bytes, index);
            index += sizeof(bool);
            return value;
        }

        protected static string ReadString(byte[] bytes, ref int index)
        {
            // 首先读取长度
            int length = ReadInt(bytes, ref index);
            // 再读取string
            string value = Encoding.UTF8.GetString(bytes, index, length);
            index += length;
            return value;
        }

        /// <summary>
        ///     根据 自定义数据类 读取数据（并不是自定义信息类）
        ///     调用其自身封装好的反序列化方法（写好了顺序了）
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="index">开始读取的索引数</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>自定义类对象</returns>
        protected static T ReadData<T>(byte[] bytes, ref int index) where T : ObjectBinaryBase, new()
        {
            T value = new T();
            index += value.Reading(bytes, index);
            return value;
        }
    }
}