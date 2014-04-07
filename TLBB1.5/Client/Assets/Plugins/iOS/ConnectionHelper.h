//
//  ConnectionHelper.h
//  ocTest
//
//  Created by Pubilc-YD003 on 13-9-6.
//  Copyright (c) 2013年 Pubilc-YD003. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "JSONKit.h"
@interface ConnectionHelper : NSObject<NSURLConnectionDelegate, NSURLConnectionDataDelegate>

@property(nonatomic, retain)NSMutableData *receivedData;
@property(nonatomic, retain)NSString *curUrl;
@property int retryTimes;
@property int timeOut;
-(void)ConnectUrl:(NSString *)url;
-(void)SendConnect:(NSString *)url andTimeOut:(int)timeOut andRetryTimes:(int)times;
-(void)TryReconnect;
@end
