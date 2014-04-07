//
//  CYTracker.h
//  Unity-iPhone
//
//  Created by Pubilc-YD003 on 13-9-9.
//
//

#import <Foundation/Foundation.h>
#import "ConnectionHelper.h"
@interface CYTracker : NSObject

+(void)BeginConnect: (NSString *)bundleID andMacAddr:(NSString *)macAddr andSecretkey:(NSString *)key;
+(void)SendPurchase: (NSString *)bundleID andMacAddr:(NSString *)macAddr andSecretkey:(NSString *)key;
+(void)SendLogin: (NSString *)bundleID andMacAddr:(NSString *)macAddr andSecretkey:(NSString *)key;
+(void)SendTrack: (NSString *)track andBundleID:(NSString *)bundleID andMacAddr:(NSString *)macAddr andSecretkey:(NSString *)key;
@end
