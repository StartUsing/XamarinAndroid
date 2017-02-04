using System.Collections.Generic;
using System.Text.RegularExpressions;
using Android.Content;
using Android.Net;
using Android.Net.Wifi;
using Java.IO;
using Java.Lang;
using Java.Net;
using Java.Util.Concurrent;

namespace App1
{
    public class WifiToolUtils
    {
        public static WifiApState GetWifiApState(Context context)
        {
            var wifiManager = (WifiManager)context.GetSystemService(Context.WifiService);
            var method = wifiManager.Class.GetMethod("getWifiApState");
            var i = (int)method.Invoke(wifiManager);
            return (WifiApState)i;
        }

        public static bool SetWifiApEnabled(Context context, bool enabled, WifiapViewModel model)
        {
            var wifiManager = (WifiManager)context.GetSystemService(Context.WifiService);
            WifiConfiguration myConfig;
            if (model != null)
            {
                myConfig = new WifiConfiguration
                {
                    Ssid = model.Ssid,
                    PreSharedKey = model.PreSharedKey
                };
                myConfig.AllowedAuthAlgorithms.Set((int)AuthAlgorithmType.Open);
                myConfig.AllowedKeyManagement.Set((int)GroupCipherType.Wep40);
            }
            else
            {
                var getWifiConfig = wifiManager.Class.GetMethod("getWifiApConfiguration", null);
                myConfig = (WifiConfiguration)getWifiConfig.Invoke(wifiManager, null);
                var setWifiConfig = wifiManager.Class.GetMethod("setWifiApConfiguration", myConfig.Class);
                setWifiConfig.Invoke(wifiManager, myConfig);
            }
            var enableWifi = wifiManager.Class.GetMethod("setWifiApEnabled", myConfig.Class, Boolean.Type);
            return (bool)enableWifi.Invoke(wifiManager, myConfig, enabled);
        }

        public static bool SetWifiEnabled(Context context, bool enabled)
        {
            var wifiManager = (WifiManager)context.GetSystemService(Context.WifiService);
            return wifiManager.SetWifiEnabled(enabled);
        }

        public static WifiState GetWifiState(Context context)
        {
            var wifiManager = (WifiManager)context.GetSystemService(Context.WifiService);
            return wifiManager.WifiState;
        }

        public static void SetMobileData(Context context, bool enabled)
        {
            var mobileDataManager = (ConnectivityManager)context.GetSystemService(Context.ConnectivityService);
            var mobileClass = mobileDataManager.Class;
            var argsClass = new Class[1];
            argsClass[0] = Boolean.Type;
            var method = mobileClass.GetMethod("setMobileDataEnabled", argsClass);
            method.Invoke(mobileDataManager, enabled);
        }

        public static List<WifiApStateViewModel> GetConnectedHotIp()
        {
            var result = new List<WifiApStateViewModel>();
            var fr = new FileReader("/proc/net/arp");
            var br = new BufferedReader(fr);
            string line;
            while ((line = br.ReadLine()) != null)
            {
                var splitted = Regex.Split(line, " +");
                if (splitted.Length < 4 || splitted[0] == "IP") continue;
                
                var model = new WifiApStateViewModel()
                {
                    Ip = splitted[0],
                    State = splitted[2]
                };
               
                result.Add(model);
            }
            fr.Close();
            br.Close();
            return result;
        }
    }
}
