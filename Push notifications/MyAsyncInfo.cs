using System.Net.Security;

namespace Push_notifications
{
    public class MyAsyncInfo
    {
        public byte[] ByteArray { get; set; }
        public SslStream MyStream { get; set; }
        public MyAsyncInfo(byte[] array, SslStream stream)
        {
            ByteArray = array;
            MyStream = stream;
        }
    }
}