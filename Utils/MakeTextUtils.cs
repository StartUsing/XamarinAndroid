using Android.Bluetooth;
using Android.Content;
using Android.Widget;

namespace App1
{
    /// <summary>
    /// ������ʾ
    /// </summary>
    public class MakeTextUtils
    {
        public static void MakeText(Context context, string content)
        {
            Toast.MakeText(context, content, ToastLength.Short).Show();
        }
    }
}