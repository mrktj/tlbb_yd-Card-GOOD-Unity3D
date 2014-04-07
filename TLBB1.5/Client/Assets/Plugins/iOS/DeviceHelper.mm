
#import "GameConfig.h"

#import <CoreTelephony/CTTelephonyNetworkInfo.h>
#import <CoreTelephony/CTCarrier.h>
#import <SystemConfiguration/SystemConfiguration.h>
#import <UIKit/UIKit.h>

#include <arpa/inet.h>
#include <netdb.h>
#import <dlfcn.h>
#include <net/if.h>
#include <ifaddrs.h>
#include <sys/socket.h>
#include <sys/sysctl.h>
#include <net/if_dl.h>
#import "Platform.h"
#import "PPHelper.h"
#import "NativeDialog.h"

#define MakeStringCopy(_x_) ((_x_ != NULL && [_x_ isKindOfClass:[NSString class]]) ? strdup ([_x_ UTF8String]) : NULL)


extern UIView*             UnityGetGLView();

extern "C" bool _isJailbroken()
{
	bool jailbroken = false;
	NSString *cydiaPahth = @"/Applications/Cydia.app";
	NSString *aptPath = @"/private/var/lib/apt";
	if([[NSFileManager defaultManager] fileExistsAtPath:cydiaPahth])
	{
		jailbroken = true;
	}
	if([[NSFileManager defaultManager] fileExistsAtPath:aptPath])
	{
		jailbroken = true;
	}
	return jailbroken;
}

extern "C" const char *_getNetworkProvider()
{
	CTTelephonyNetworkInfo *info = [[CTTelephonyNetworkInfo alloc] init];
	NSString *carrier;
	if([info.subscriberCellularProvider.carrierName length] > 1)
	{
		carrier = [NSString stringWithString:info.subscriberCellularProvider.carrierName];
	}
	else
	{
		carrier = @"Unknow";
	}
	[info release];
	return MakeStringCopy(carrier);
}

extern "C" const char *_getUserLocale()
{
	NSLocale *currentUserLocale = [NSLocale currentLocale];
	NSString *localeInderfier = [currentUserLocale localeIdentifier];
	return MakeStringCopy(localeInderfier);
}

extern "C" const char *_getUserContry()
{
	NSLocale *currentUsersLocale = [NSLocale currentLocale];
	NSString *localIdentifier = [currentUsersLocale localeIdentifier];
	NSString *region = nil;
	NSArray *codes = [NSLocale ISOCountryCodes];
	BOOL findContry = NO;
	NSRange range = [localIdentifier rangeOfString:@"_"];
	NSString *contry = nil;
	if(range.location != NSNotFound)
	{
		contry = [localIdentifier substringFromIndex:(range.location+range.length)];
		NSUInteger idx = NSUIntegerMax;
		idx = [codes indexOfObject:contry];
		if(idx < [codes count])
		{
			findContry = YES;
		}
	}
    
	if(findContry)
	{
		if([[contry uppercaseString] isEqualToString:@"HK"] ||
		   [[contry uppercaseString] isEqualToString:@"MO"] ||
		   [[contry uppercaseString] isEqualToString:@"TW"])
		{
			contry = @"CN";
		}
        
		region = contry;
	}
	else
	{
		region = @"Unknown";
	}
    
	return MakeStringCopy(region);
}

extern "C" const char *_localWifiIPAddress()
{
	BOOL success;
	struct ifaddrs *addrs;
	const struct ifaddrs *cursor;
	success = getifaddrs(&addrs) == 0;
	NSString *wifiAddrs = nil;
	if(success)
	{
		cursor = addrs;
		NSString *name = nil;
		while(cursor != NULL	)
		{
			if(cursor->ifa_addr->sa_family == AF_INET && (cursor->ifa_flags & IFF_LOOPBACK) == 0)
			{
				name = [NSString stringWithUTF8String:cursor->ifa_name];
				if([name isEqualToString:@"en0"])
				{
					wifiAddrs = [NSString stringWithUTF8String:inet_ntoa(((struct sockaddr_in *)cursor->ifa_addr)->sin_addr)];
					break;
				}
			}
			cursor = cursor->ifa_next;
		}
		freeifaddrs(addrs);
	}
    
	if(nil == wifiAddrs)
	{
		return NULL;
	}
    
	return MakeStringCopy(wifiAddrs);
}

extern "C" int _getNetworkState()
{
	struct sockaddr_in zeroAddress;
	bzero(&zeroAddress, sizeof(zeroAddress));
	zeroAddress.sin_len = sizeof(zeroAddress);
	zeroAddress.sin_family = AF_INET;
    
	SCNetworkReachabilityRef defaultRouteReachability = SCNetworkReachabilityCreateWithAddress(NULL, (struct sockaddr *)&zeroAddress);
	SCNetworkReachabilityFlags flags;
	bool didRetrieveFlags = SCNetworkReachabilityGetFlags(defaultRouteReachability, &flags);
	CFRelease(defaultRouteReachability);
	if(!didRetrieveFlags)
	{
		return -1;
	}
    
	BOOL IsReachable = flags & kSCNetworkFlagsReachable;
	BOOL needConnection = flags & kSCNetworkFlagsInterventionRequired;
    
	if(!IsReachable || needConnection)
	{
		return 0;
	}
    
	if(flags & kSCNetworkReachabilityFlagsIsWWAN)
	{
		return 1;
	}
	
	if(_localWifiIPAddress() != NULL)
	{
		return 2;
	}
    
	return 3;
}

extern "C" const char *_getSystemName()
{
    
	NSString *nsSystemName = [NSString stringWithString:[[UIDevice currentDevice] systemName]];
	return MakeStringCopy(nsSystemName);
}

extern "C" const char *_getDeviceName()
{
	NSString *nsDeviceName = [NSString stringWithString:[[UIDevice currentDevice] name]];
	return MakeStringCopy(nsDeviceName);
}

extern "C" const char *_getMacAddress()
{
    NSString *retString = [[Platform Instance] GetMacAddr];
    return MakeStringCopy(retString);
    
}

extern "C" const char *_getChannelID()
{
    return MakeStringCopy(CHANNELID);
}

extern "C" void _setupAnalytics()
{
    
    [Platform SetupAnalytics];
    
}

extern "C" bool _useConfigIp()
{
    return true;
}

extern "C" const char *_getConfigIp()
{
    return MakeStringCopy(CONFIG_IP);
}

extern "C" const char *_getServerListUrl()
{
	return MakeStringCopy(SERVERLISTURL);
}

extern "C" const char *_getUpdateServerUrl()
{
	return MakeStringCopy(UPDATESERVER_URL);
}

extern "C" const char *_getUpdateServerRoot()
{
	return MakeStringCopy(UPDATESERVER_ROOT);
}

extern "C" const char *_getNoticeUrl()
{
	return MakeStringCopy(NOTICE_URL);
}

extern "C" const char *_getVersionNumber()
{
	return MakeStringCopy(V_NUMBER);
}
extern "C" void _iapppayBuy(const char *szOrder, int orderID)
{
    [Platform MakeIAppPay:[NSString stringWithUTF8String:szOrder] andwaresid:orderID];
}

extern "C" int _iapSytle()
{
    return IAPSTYLE;
}

extern "C" void _openUMFeed()
{
    [Platform OpenUMFeed];
}

extern "C" void _sendTrackPurchase()
{
    [Platform SendTrack:@"purchase"];
}

extern "C" void _sendTrackLogin()
{
    [Platform SendTrack:@"login"];
}

extern "C" void _initState()
{
	[Platform InitState];
	//[UnityGetGLView() setMultipleTouchEnabled:NO];
#ifdef OPEN_PPSDK
    [[PPHelper Instance] InitState];
#endif
}

extern "C" void _prepareToUpdate()
{
}

extern "C" void _ppLogin()
{
#ifdef OPEN_PPSDK
    [[PPHelper Instance] Login];
#endif
}

extern "C" void _ppCenter()
{
#ifdef OPEN_PPSDK
    [[PPHelper Instance] ShowCenter];
#endif
}

extern "C" void _ppBuy(int price, const char *szOrder, const char *szTitle, const char *szUserID, int zoneID)
{
#ifdef OPEN_PPSDK
    [[PPHelper Instance] Buy:price BillNo:[NSString stringWithUTF8String:szOrder] BillTitle:[NSString stringWithUTF8String:szTitle] RoleId:[NSString stringWithUTF8String:szUserID] ZoneId:zoneID];
#endif
}

extern "C" bool _openVersionUpdateUrl()
{
#ifdef APP_UPDATE_URL
    NSURL *update_url = [NSURL URLWithString:APP_UPDATE_URL];
    if (![[UIApplication sharedApplication] canOpenURL:update_url]) {
        NSLog(@"Can't open version update url!");
        NSLog(APP_UPDATE_URL);
        return false;
    }
    
    if (![[UIApplication sharedApplication] openURL:update_url]) {
        NSLog(@"Can't open version update url!");
        NSLog(APP_UPDATE_URL);
        return false;
    }
    
    return true;
    
#endif
    return false;
}

extern "C" void _autoLockScreen(bool lock)
{
	[UIApplication sharedApplication].idleTimerDisabled = !lock;
}

extern "C" void _showExitDialog(char * title ,char * message){
    NSString *m_title = [NSString stringWithFormat:@"%s",title];
    NSString *m_message = [NSString stringWithFormat:@"%s",message];
    NativeDialog *nativeDialog = [[NativeDialog alloc] init];
    [nativeDialog ShowDialog:m_title MessageStr:m_message];
}
