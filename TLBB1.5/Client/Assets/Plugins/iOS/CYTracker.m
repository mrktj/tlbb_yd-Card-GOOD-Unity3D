//
//  CYTracker.m
//  Unity-iPhone
//
//  Created by Pubilc-YD003 on 13-9-9.
//
//

#import "CYTracker.h"

@implementation CYTracker

+(void)BeginConnect:(NSString *)bundleID andMacAddr:(NSString *)macAddr andSecretkey:(NSString *)key
{
    [CYTracker SendTrack:@"activate" andBundleID:bundleID andMacAddr:macAddr andSecretkey:key];
}

+(void)SendLogin:(NSString *)bundleID andMacAddr:(NSString *)macAddr andSecretkey:(NSString *)key
{
    [CYTracker SendTrack:@"login" andBundleID:bundleID andMacAddr:macAddr andSecretkey:key];
}

+(void)SendPurchase:(NSString *)bundleID andMacAddr:(NSString *)macAddr andSecretkey:(NSString *)key
{
    [CYTracker SendTrack:@"payment" andBundleID:bundleID andMacAddr:macAddr andSecretkey:key];
}

+(void)SendTrack:(NSString *)track andBundleID:(NSString *)bundleID andMacAddr:(NSString *)macAddr andSecretkey:(NSString *)key
{
    NSString *strUrl = @"http://mobilemad.changyou.com/adapi/";
    NSString *strBundleID = @"?bundleid=";
    NSString *strMac = @"&uid=";
    NSString *strKey = @"&secretkey=";
    strUrl = [strUrl stringByAppendingString:track];
    strBundleID=  [strBundleID stringByAppendingString:bundleID];
    strMac = [strMac stringByAppendingString:macAddr];
    strKey = [strKey stringByAppendingString:key];
    strUrl = [[[strUrl stringByAppendingString:strBundleID] stringByAppendingString:strMac] stringByAppendingString:strKey];
    NSLog(@"%@", strUrl);

    ConnectionHelper *newConnection = [[ConnectionHelper alloc] init];
    [newConnection SendConnect:strUrl andTimeOut:10 andRetryTimes:3];

}

@end
