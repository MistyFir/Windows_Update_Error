using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows_Update_Error
{
    // 定义一个名为Key_Decrypt的内部类，该类主要用于对密钥进行解密相关的操作
    internal class Key_Decrypt
    {
        // 私有字段，用于存储传入的待处理的密钥字符串
        private string _key;

        // 定义一个字符数组，存储用于加密的字符集合，包含大小写字母和数字，用于后续查找对应索引等操作
        private char[] Encrypt = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz123456789".ToCharArray();

        // 定义一个字符数组，存储用于解密的字符集合，与Encrypt数组中的字符有对应关系，用于根据索引获取解密后的字符
        private char[] Decrypt = "fG93p8qXWbLz2vHtU6Sj1rIeCkY4oADmNw5aRQOPcxZJghuMn7siyBdTlfK0V".ToCharArray();

        // Key_Decrypt类的构造函数，接收一个字符串类型的参数key，用于初始化类中的_key字段，即将传入的密钥保存起来以便后续处理
        public Key_Decrypt(string key)
        {
            _key = key;
        }

        // 定义一个名为GetDecryptKey的方法，该方法的主要功能是根据既定的加密、解密字符对应关系，对_key字段存储的密钥进行解密操作，并返回解密后的密钥字符串
        public string GetDecryptKey()
        {
            // 将_key字符串转换为字符数组，方便逐个字符进行处理
            char[] Char_key = _key.ToCharArray();
            // 创建一个新的字符数组，用于存储解密后的字符，其长度与传入的密钥字符数组长度一致
            char[] Char_Decrypt_key = new char[Char_key.Length];
            // 定义一个变量，用于记录在加密字符数组中查找对应字符时的索引位置
            int index;
            // 循环遍历传入密钥的每个字符，进行解密操作
            for (int i = 0; i < Char_key.Length; i++)
            {
                // 在加密字符数组Encrypt中查找当前字符的索引位置，如果找到则返回对应的索引值，否则返回 -1
                index = Array.IndexOf(Encrypt, Char_key[i]);
                // 如果找到的索引值大于等于0（表示找到了对应字符）并且该索引值小于解密字符数组Decrypt的长度，说明可以进行解密替换操作
                if (index >= 0 && index < Decrypt.Length)
                {
                    // 将解密字符数组中对应索引位置的字符赋值给用于存储解密后字符的数组，实现当前字符的解密
                    Char_Decrypt_key[i] = Decrypt[index];
                }
                else
                {
                    // 如果在加密字符数组中未找到对应字符（可能是特殊字符等情况），则直接将原字符赋值给解密后的字符数组，保持原样
                    Char_Decrypt_key[i] = Char_key[i];
                }
            }
            // 将解密后的字符数组转换为字符串，并返回该字符串，即得到最终的解密密钥
            return new string(Char_Decrypt_key);
        }
    }
}
