using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace tpc
{
    /// <summary>
    /// 加解密
    /// </summary>
    public class ende
    {
        #region DES加密,指定用于加密的块密码模式为ECB
        /// <summary>
        /// des加密
        /// </summary>
        /// <param name="encryptString">待加密字符串</param>
        /// <param name="key">密钥</param>
        public static string DESEncrypt(string encryptString, string key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            des.Key = Encoding.UTF8.GetBytes(key);
            des.Mode = CipherMode.ECB;
            using (MemoryStream ms = new MemoryStream())
            {
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                StringBuilder ret = new StringBuilder();
                foreach (byte b in ms.ToArray())
                {
                    ret.AppendFormat("{0:X2}", b);
                }
                return ret.ToString();
            }
        }
        #endregion

        #region DES解密,指定用于加密的块密码模式为ECB
        /// <summary>
        /// des解密
        /// </summary>
        /// <param name="decryptString">待解密字符串</param>
        /// <param name="key">密钥</param>
        public static string DESDecrypt(string decryptString, string key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Mode = CipherMode.ECB;
            int len;
            len = decryptString.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(decryptString.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.UTF8.GetBytes(key);
            using (MemoryStream ms = new MemoryStream())
            {
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
        #endregion

        #region SHA1
        /// <summary>
        /// SHA1加密，返回的字符串为大写
        /// </summary>
        /// <param name="strSource">需要加密的明文</param>
        /// <returns>返回16位加密结果，该结果取32位加密结果的第9位到25位</returns>
        public static string SHA1(string strSource)
        {
            //new
            SHA1 sha = new SHA1CryptoServiceProvider();
            //获取密文字节数组
            byte[] bytResult = sha.ComputeHash(System.Text.Encoding.Default.GetBytes(strSource));
            //转换成字符串，并取9到25位
            //string strResult = BitConverter.ToString(bytResult, 4, 8);
            //转换成字符串，32位
            string strResult = BitConverter.ToString(bytResult);
            //BitConverter转换出来的字符串会在每个字符中间产生一个分隔符，需要去除掉
            strResult = strResult.Replace("-", "");
            return strResult;
        }
        #endregion

        #region MD5 32位加密(转换为小写)
        /// <summary>
        /// MD5 32位加密(转换为小写)
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        public static string MD5_32(string strSource)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encryptedBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(strSource));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                sb.AppendFormat("{0:x2}", encryptedBytes[i]);
            }
            return sb.ToString();
        }
        #endregion

        #region MD5 16位加密(转换为小写)
        /// <summary>
        /// MD5 16位加密(转换为小写)
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        public static string MD5_16(string strSource)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(strSource)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2.ToLower();
        }
        #endregion

        #region 获取大写的MD5签名结果
        /// <summary>
        /// 获取大写的MD5签名结果
        /// </summary>
        /// <param name="encypStr"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string MD5_WX(string encypStr, Encoding encoding)
        {
            string retStr;
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

            //创建md5对象
            byte[] inputBye;
            byte[] outputBye;

            //使用GB2312编码方式把字符串转化为字节数组．
            try
            {
                inputBye = encoding.GetBytes(encypStr);
            }
            catch
            {
                inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);//此处用System.Web.HttpContext.Current.Request.ContentEncoding?
            }
            outputBye = m5.ComputeHash(inputBye);

            retStr = System.BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToUpper();
            return retStr;
        }
        #endregion

        #region AES加密
        /// <summary>  
        /// AES加密  
        /// </summary>  
        /// <param name="plainStr">明文字符串</param>  
        /// <returns>密文</returns>  
        public static string AESEncrypt(string plainStr, string Key, string IV)
        {
            byte[] bKey = Encoding.UTF8.GetBytes(Key);
            byte[] bIV = Encoding.UTF8.GetBytes(IV);
            byte[] byteArray = Encoding.UTF8.GetBytes(plainStr);

            string encrypt = string.Empty;
            Rijndael aes = Rijndael.Create();
            using (MemoryStream mStream = new MemoryStream())
            {
                using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateEncryptor(bKey, bIV), CryptoStreamMode.Write))
                {
                    cStream.Write(byteArray, 0, byteArray.Length);
                    cStream.FlushFinalBlock();

                    //十六进制形式字符串
                    encrypt = string.Concat(mStream.ToArray().Select(b => b.ToString("X2")));
                    //Base64数字编码的等效字符串
                    //encrypt = Convert.ToBase64String(mStream.ToArray());
                }
            }
            aes.Clear();
            return encrypt;
        }
        #endregion

        #region AES解密
        /// <summary>  
        /// AES解密  
        /// </summary>  
        /// <param name="encryptStr">密文字符串</param>  
        /// <returns>明文</returns>  
        public static string AESDecrypt(string encryptStr, string Key, string IV)
        {
            byte[] bKey = Encoding.UTF8.GetBytes(Key);
            byte[] bIV = Encoding.UTF8.GetBytes(IV);

            //如果之前加密用Base64
            //byte[] byteArray = Convert.FromBase64String(encryptStr);

            //如果之前加密用十六进制字符串
            byte[] byteArray = new byte[encryptStr.Length / 2];
            for (int i = 0; i < encryptStr.Length; i += 2)
            {
                byteArray[i / 2] = Convert.ToByte(encryptStr.Substring(i, 2), 16);
            }

            string decrypt = string.Empty;
            Rijndael aes = Rijndael.Create();
            using (MemoryStream mStream = new MemoryStream())
            {
                using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateDecryptor(bKey, bIV), CryptoStreamMode.Write))
                {
                    cStream.Write(byteArray, 0, byteArray.Length);
                    cStream.FlushFinalBlock();
                    decrypt = Encoding.UTF8.GetString(mStream.ToArray());
                }
            }
            aes.Clear();
            return decrypt;
        }
        #endregion

        #region 3Des加密字符串并转换为16进制大写
        /// <summary>
        /// 3Des加密字符串并转换为16进制大写
        /// </summary>
        /// <param name="value">源字符串</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string DES3Encrypt(string value, string key)
        {
            byte[] keybyte = System.Text.Encoding.UTF8.GetBytes(key);
            byte[] data = System.Text.Encoding.UTF8.GetBytes(value);
            MemoryStream mStream = new MemoryStream();
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
            DES.Mode = CipherMode.ECB;
            DES.Padding = PaddingMode.PKCS7;
            CryptoStream cStream = new CryptoStream(mStream, DES.CreateEncryptor(keybyte, null), CryptoStreamMode.Write);
            // Write the byte array to the crypto stream and flush it.
            cStream.Write(data, 0, data.Length);
            cStream.FlushFinalBlock();
            // Get an array of bytes from the 
            // MemoryStream that holds the 
            // encrypted data.
            byte[] ret = mStream.ToArray();
            // Close the streams.
            cStream.Close();
            mStream.Close();

            return String.Concat(ret.Select(b => b.ToString("X2")));
        }
        #endregion

        #region Base64加密
        public static string EncodeBase64(Encoding encode, string source)
        {
            var en = "";
            byte[] bytes = encode.GetBytes(source);
            try
            {
                en = Convert.ToBase64String(bytes);
            }
            catch
            {
                en = source;
            }
            return en;
        }

        public static string EncodeBase64(string source)
        {
            return EncodeBase64(Encoding.UTF8, source);
        }
        #endregion

        #region Base64解密
        public static string DecodeBase64(Encoding encode, string source)
        {
            string de = "";
            byte[] bytes = Convert.FromBase64String(source);
            try
            {
                de = encode.GetString(bytes);
            }
            catch
            {
                de = source;
            }
            return de;
        }

        public static string DecodeBase64(string source)
        {
            return DecodeBase64(Encoding.UTF8, source);
        }
        #endregion
    }
}
