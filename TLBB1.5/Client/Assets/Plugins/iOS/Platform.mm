//
//  Platform.m
//  umtest
//
//  Created by Pubilc-YD003 on 13-8-5.
//  Copyright (c) 2013年 Pubilc-YD003. All rights reserved.
//

#import "Platform.h"

#ifdef OPEN_UMENG
#import "MobClick.h"
#endif

#ifdef OPEN_FLURRY
#import "Flurry.h"
#endif

#include <arpa/inet.h>
#include <netdb.h>
#import <dlfcn.h>
#include <net/if.h>
#include <ifaddrs.h>
#include <sys/socket.h>
#include <sys/sysctl.h>
#include <net/if_dl.h>
#import <AdSupport/AdSupport.h>
void UnitySendMessage( const char * className, const char * methodName, const char * param );

extern UIViewController*   UnityGetGLViewController();

@interface Platform ()

@end

@implementation Platform

static Platform *s_instance = nil;
+(Platform *)Instance
{
    if(nil == s_instance)
    {
        s_instance = [[Platform alloc] init];
    }
    
    return s_instance;
}

+(void)InitState
{
#ifdef OPEN_UMENG
    [MobClick startWithAppkey:KEY_YOUMENG reportPolicy:BATCH channelId:CHANNELID];
#endif
    
#ifdef OPEN_FLURRY
    [Flurry startSession:KEY_FLURRY];
#endif
    

#ifdef OPEN_INMOBIAD
    [IMAdTracker initWithAppID:INMOBIAD_APPID];
    [IMAdTracker reportAppDownloadGoal];
#endif
#ifdef OPEN_ADMOB
    [GoogleConversionPing pingWithConversionId:ADMOB_ID label:ADMOB_LABEL value:ADMOB_PRICE isRepeatable:NO];
#endif
#ifdef OPEN_IAPPPAY
    [[IPAYiAppPay sharediAppPay] initializeWithAppKey:IAPPPAY_KEY andAppID:IAPPPAY_APPID andWaresID:1 andUIOrientation:UIInterfaceOrientationPortrait andCPDelegate:nil];
#endif
}

+(void)SetupAnalytics
{
#ifdef OPEN_CYTRACKER
    [CYTracker BeginConnect: CYTRACKER_BUNDLEID andMacAddr: [[Platform Instance] GetCYTrackUDID] andSecretkey :CYTRACKER_KEY];
#endif
}


+(void)SendTrack:(NSString *)strGoal
{
#ifdef OPEN_INMOBIAD
    //[IMAdTracker reportCustomGoal:strGoal];
#endif
#ifdef OPEN_CYTRACKER
    if([strGoal isEqualToString:@"login"])
    {
        [CYTracker SendLogin:CYTRACKER_BUNDLEID andMacAddr:[[Platform Instance] GetCYTrackUDID] andSecretkey :CYTRACKER_KEY];
    }
    else if([strGoal isEqualToString:@"purchase"])
    {
        [CYTracker SendPurchase:CYTRACKER_BUNDLEID andMacAddr:[[Platform Instance] GetCYTrackUDID] andSecretkey :CYTRACKER_KEY];
    }
#endif
    
}

+(void)MakeIAppPay:(NSString *)strOrder
       andwaresid :(int)waresid
{
    [[Platform Instance] IAppPay :strOrder andwaresid:waresid];
}

static bool isUMFeedSetup = false;
+(void)OpenUMFeed
{
#ifdef OPEN_UMFEEDBACK
    if(!isUMFeedSetup)
    {
        [[UMFeedback sharedInstance] setAppkey:KEY_YOUMENG delegate:self];
        isUMFeedSetup = true;
    }
    
   [ UMFeedback showFeedback:UnityGetGLViewController() withAppkey:KEY_YOUMENG];
#endif
}

-(NSString *)GetCYTrackUDID
{
    float version = [[[UIDevice currentDevice] systemVersion] floatValue];  
    if (version >= 7.0) 
    {
        
        return [[[ASIdentifierManager sharedManager] advertisingIdentifier] UUIDString];
    }
    else
    {
        return[[Platform Instance] GetMacAddr];
    }
}
-(NSString *)GetMacAddr
{
    int                    mib[6];
    size_t                len;
    char                *buf;
    unsigned char        *ptr;
    struct if_msghdr    *ifm;
    struct sockaddr_dl    *sdl;
    
    mib[0] = CTL_NET;
    mib[1] = AF_ROUTE;
    mib[2] = 0;
    mib[3] = AF_LINK;
    mib[4] = NET_RT_IFLIST;
    
    if ((mib[5] = if_nametoindex("en0")) == 0) {
        printf("Error: if_nametoindex error/n");
        return NULL;
    }
    
    if (sysctl(mib, 6, NULL, &len, NULL, 0) < 0) {
        printf("Error: sysctl, take 1/n");
        return NULL;
    }
    
    if ((buf = (char *)malloc(len)) == NULL) {
        printf("Could not allocate memory. error!/n");
        return NULL;
    }
    
    if (sysctl(mib, 6, buf, &len, NULL, 0) < 0) {
        printf("Error: sysctl, take 2");
        return NULL;
    }
    
    ifm = (struct if_msghdr *)buf;
    sdl = (struct sockaddr_dl *)(ifm + 1);
    ptr = (unsigned char *)LLADDR(sdl);
    // NSString *outstring = [NSString stringWithFormat:@"%02x:%02x:%02x:%02x:%02x:%02x", *ptr, *(ptr+1), *(ptr+2), *(ptr+3), *(ptr+4), *(ptr+5)];
    NSString *outstring = [NSString stringWithFormat:@"%02X:%02X:%02X:%02X:%02X:%02X", *ptr, *(ptr+1), *(ptr+2), *(ptr+3), *(ptr+4), *(ptr+5)];
    free(buf);
    return  [outstring uppercaseString];
    
}

-(void)IAppPay:(NSString *)strOrder
   andwaresid :(int)waresid
{
#ifdef OPEN_IAPPPAY
    IPAYiAppPayOrder* iAppPayOrder = [[IPAYiAppPayOrder alloc] init];
    iAppPayOrder.exorderno = strOrder;
    iAppPayOrder.waresid = waresid;
    
    NSString * orderSignature = [[IPAYiAppPay sharediAppPay] getOrderSignature: iAppPayOrder
                                                                     withAppID: IAPPPAY_APPID
                                                                     andAppKey: IAPPPAY_KEY];
    
    [[IPAYiAppPay sharediAppPay] checkoutWithOrder:iAppPayOrder andOrderSignature:orderSignature andPaymentDelegate:self];
#endif
}

#ifdef OPEN_IAPPPAY
#pragma mark IPAYiAppPayPaymentDelegate
- (void)paymentStatusCode: (IPAYiAppPayPaymentStatusCodeType)statusCode
                signature: (NSString *)signature
               resultInfo: (NSString *)resultInfo
{
    NSString *strResult;
    if(statusCode == IPAY_PAYMENT_SUCCESS)
    {
        if([[IPAYiAppPay sharediAppPay] verifyPaymentSignature:signature withAppKey:IAPPPAY_KEY])
        {
            /*
            UIAlertView * alertView = [[UIAlertView alloc] initWithTitle:@"提示"
                                                                 message:@"支付成功！"
                                                                delegate:nil
                                                       cancelButtonTitle:@"确定"
                                                       otherButtonTitles:nil];
            [alertView show];
            */
            strResult = @"0";
        }
        else
        {
            /*
            UIAlertView * alertView = [[UIAlertView alloc] initWithTitle:@"提示"
                                                                 message:@"支付成功,验签失败！"
                                                                delegate:nil
                                                       cancelButtonTitle:@"确定"
                                                       otherButtonTitles:nil];
                                                    
            [alertView show];
            */
            strResult = @"1";
        }
    }
    else if(statusCode == IPAY_PAYMENT_CANCELED)
    {
        /*
        UIAlertView * alertView = [[UIAlertView alloc] initWithTitle:@"提示"
                                                             message:@"支付失败，用户取消支付！"
                                                            delegate:nil
                                                   cancelButtonTitle:@"确定"
                                                   otherButtonTitles:nil];
        [alertView show];
        */
        strResult = @"2";

    }
    else
    {
        /*
        UIAlertView * alertView = [[UIAlertView alloc] initWithTitle:@"提示"
                                                             message:@"支付失败！"
                                                            delegate:nil
                                                   cancelButtonTitle:@"确定"
                                                   otherButtonTitles:nil];
        [alertView show];
        */
        strResult = @"3";
    }
    
    UnitySendMessage( "IAppPayManager", "IAppPayReuslt", [strResult UTF8String]);
}


#pragma mark- IPAYiAppPayContentProviderDelegate

- (void)getStatusInitialized {
    NSLog(@"IPAY iAPP PAY CONTENT PROVIDER STATUS INITIALIZATION");
}

#endif


@end
