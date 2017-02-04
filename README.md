# XamarinAndroid
## Describe

### Have function

Root 
WifiControl
WifiApControl
WifiApConnectionList
FlightModeControl
MobileDataControl

### Version
android : 4.4+ or api 4.4+

Because working relationship only provide tools do not provide the logic and UI

### Other
```
//It can be real-time detection
var es = Executors.NewCachedThreadPool();
es.Submit(new Runnable(() =>
{
    var ipAddress = InetAddress.GetByName(Ip);
    ipAddress.IsReachable(100) 
}));
```
