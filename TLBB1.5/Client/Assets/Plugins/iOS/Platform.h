//
//  Platform.h
//  umtest
//
//  Created by Pubilc-YD003 on 13-8-5.
//  Copyright (c) 2013年 Pubilc-YD003. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "GameConfig.h"

#ifdef OPEN_IAPPPAY
#import "IPAYiAppPay.h"
#endif

#ifdef OPEN_INMOBIAD
#import "IMAdTracker.h"
#import "IMCommonUtil.h"
#endif

#ifdef OPEN_ADMOB
#import "GoogleConversionPing.h"
#endif

#ifdef OPEN_CYTRACKER
#import "CYTracker.h"
#endif

#ifdef OPEN_UMFEEDBACK
#import "UMFeedback.h"
#endif
#ifdef OPEN_IAPPPAY
@interface Platform : NSObject<IPAYiAppPayPaymentDelegate
#ifdef OPEN_UMFEEDBACK
,UMFeedbackDataDelegate
#endif
>
#else
@interface Platform : NSObject
#endif
{
    
}

+ (Platform *)Instance;
+ (void)InitState;
+ (void)SetupAnalytics;
+ (void)SetupIAppPay;
+ (void)SendTrack:(NSString *)strGoal;
+ (void)MakeIAppPay:(NSString *)strOrder
        andwaresid :(int)waresid;
+ (void)OpenUMFeed;
- (void)IAppPay:(NSString *)strOrder
    andwaresid :(int)waresid;

- (NSString *)GetMacAddr;
- (NSString *)GetCYTrackUDID;
@end
