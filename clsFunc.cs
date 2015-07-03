using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ConvertStreamAndBytesAndFiles
{
    class clsFunc
    {
        /// <summary>
        /// 將 Stream 轉換成 byte[]
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            //設定目前 stream 的位置為起始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        /// <summary>
        /// 將 byte[] 轉換成 Stream
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        /// <summary>
        /// 將 Stream 轉換成實體檔案
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileFullPath"></param>
        public void StreamToFile(Stream stream, string fileFullPath)
        {
            byte[] bytes = StreamToBytes(stream);

            using (FileStream fs = new FileStream(fileFullPath, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(bytes);
                    bw.Close();
                }
                fs.Close();
            }
        }
        
        /// <summary>
        /// 將實體檔案轉換成 Stream
        /// </summary>
        /// <param name="fileFullPath"></param>
        /// <returns></returns>
        public Stream FileToStream(string fileFullPath)
        {
            FileStream fs = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            Stream stream = new MemoryStream(bytes);
            return stream;
        }
    }
}
