//
//  ConnectionHelper.m
//  ocTest
//
//  Created by Pubilc-YD003 on 13-9-6.
//  Copyright (c) 2013年 Pubilc-YD003. All rights reserved.
//

#import "ConnectionHelper.h"
#ifdef OPEN_UMFEEDBACK
#import "UMFeedback.h"
#endif
@implementation ConnectionHelper


-(void)SendConnect:(NSString *)url andTimeOut:(int)timeOut andRetryTimes:(int)times
{
    self.retryTimes = times;
    self.timeOut = timeOut;
    self.curUrl = url;
    [self ConnectUrl:url];
    
}
-(void)ConnectUrl:(NSString *)url
{
    
    NSLog(@"connecting url: %@", url);
    NSURLRequest *theRequest = [NSURLRequest requestWithURL:[NSURL URLWithString:url] cachePolicy: NSURLRequestUseProtocolCachePolicy timeoutInterval:self.timeOut];
    
    NSURLConnection *theConnection = [[NSURLConnection alloc] initWithRequest:theRequest delegate:self];
    if(!theConnection)
    {
        NSLog(@"无法建立链接");
    }
    else
    {
        self.receivedData = [[NSMutableData alloc] initWithData:nil];
    }
}

-(void)TryReconnect
{
    self.retryTimes--;
    if(self.retryTimes > 0)
    {
        [self ConnectUrl:self.curUrl];
    }
}
-(void)connection:(NSURLConnection *)connection didReceiveResponse:(NSURLResponse *)response
{
    NSLog(@"连接已建立");
}

-(void)connection:(NSURLConnection *)connection didReceiveData:(NSData *)data{
    NSLog(@"正在传输数据");
    [self.receivedData appendData:data];
    
    
}

-(void)connection:(NSURLConnection *)connection didFailWithError:(NSError *)error
{
    NSLog(@"数据传输失败 %@", error);
    [connection release];
    [self.receivedData release];
    [self TryReconnect];
}

-(void)connectionDidFinishLoading:(NSURLConnection *)connection{
    
    NSLog(@"数据接收完成");
    NSString *results = [[NSString alloc]
                         initWithBytes:[self.receivedData bytes]
                         length:[self.receivedData length]
                         encoding:NSUTF8StringEncoding];
    
    NSLog(@"%@, %d", results, [self.receivedData length]);
    
    [connection release];
    
    JSONDecoder *jd = [[JSONDecoder alloc] init];
    NSDictionary *dic = [jd objectWithData:self.receivedData];
    NSNumber *value = [dic objectForKey:@"status"];
    NSLog(@"reveive value %@", value);
    [self.receivedData release];
    [jd release];
    if([value isEqualToNumber: [NSNumber numberWithInt:0]])
    {
        [self TryReconnect];
    }
}
@end
