//
//  PPPay.h
//  SDKDEMO
//
//  Created by MRDUnity on 13-10-17.
//  Copyright (c) 2013年 Seven. All rights reserved.
//

#import "GameConfig.h"
#ifdef OPEN_PPSDK
#import <PPAppPlatformKit/PPAppPlatformKit.h>
@interface PPHelper :NSObject<PPAppPlatformKitDelegate>

+(PPHelper *)Instance;
-(void)InitState;
-(void)Login;
-(void)ShowCenter;
-(void)Buy:(int)paramPrice BillNo:(NSString *)paramBillNo BillTitle:(NSString *)paramBillTitle RoleId:(NSString *)paramRoleId ZoneId:(int)paramZoneId;
@end

#endif
