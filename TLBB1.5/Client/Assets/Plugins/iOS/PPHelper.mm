//
//  PPPay.m
//  SDKDEMO
//
//  Created by MRDUnity on 13-10-17.
//  Copyright (c) 2013年 Seven. All rights reserved.
//

#import "PPHelper.h"

#ifdef OPEN_PPSDK
@implementation PPHelper

static PPHelper *s_instance = nil;

+(PPHelper *)Instance
{
    if(s_instance == nil)
    {
        s_instance = [[PPHelper alloc] init];
    }
    
    return s_instance;
}
-(void)InitState
{
    [[PPAppPlatformKit sharedInstance] setAppId:PP_APPID AppKey:PP_KEY];
    [[PPAppPlatformKit sharedInstance] setIsNSlogData:NO];
    [[PPAppPlatformKit sharedInstance] setRechargeAmount:10];
    [[PPAppPlatformKit sharedInstance] setIsLongComet:NO];
    [[PPAppPlatformKit sharedInstance] setIsLogOutPushLoginView:YES];
    [[PPAppPlatformKit sharedInstance] setIsOpenRecharge:YES];
    [[PPAppPlatformKit sharedInstance] setCloseRechargeAlertMessage:@"关闭充值提示语"];
    [PPUIKit sharedInstance];
    
    
    [PPUIKit setIsDeviceOrientationLandscapeLeft:NO];
    [PPUIKit setIsDeviceOrientationLandscapeRight:NO];
    [PPUIKit setIsDeviceOrientationPortrait:YES];
    [PPUIKit setIsDeviceOrientationPortraitUpsideDown:NO];
    [[PPAppPlatformKit sharedInstance] setDelegate:self];
}

-(void)Login
{
    [[PPAppPlatformKit sharedInstance] showLogin];
}

-(void)ShowCenter
{
    [[PPAppPlatformKit sharedInstance] showCenter];
}

-(void)Buy:(int)paramPrice BillNo:(NSString *)paramBillNo BillTitle:(NSString *)paramBillTitle RoleId:(NSString *)paramRoleId ZoneId:(int)paramZoneId
{
    [[PPAppPlatformKit sharedInstance] exchangeGoods:paramPrice BillNo:paramBillNo BillTitle:paramBillTitle RoleId:paramRoleId ZoneId:paramZoneId];
}

#pragma mark    ---------------SDK CALLBACK---------------
//字符串登录成功回调【实现其中一个就可以】
- (void)ppLoginStrCallBack:(NSString *)paramStrToKenKey{
    //字符串token验证方式
    NSLog(@"登陆成功 token:%@", paramStrToKenKey);
    UnitySendMessage( "PPHelper", "GetTokenKey", [paramStrToKenKey UTF8String]);
    [[PPAppPlatformKit sharedInstance] getUserInfoSecurity];
}

//关闭客户端页面回调方法
-(void)ppClosePageViewCallBack:(PPPageCode)paramPPPageCode{
    //可根据关闭的VIEW页面做你需要的业务处理
    NSLog(@"当前关闭的VIEW页面回调是%d", paramPPPageCode);
}



//关闭WEB页面回调方法
- (void)ppCloseWebViewCallBack:(PPWebViewCode)paramPPWebViewCode{
    //可根据关闭的WEB页面做你需要的业务处理
    NSLog(@"当前关闭的WEB页面回调是%d", paramPPWebViewCode);
}

//注销回调方法
- (void)ppLogOffCallBack{
    NSLog(@"注销的回调");
    UnitySendMessage( "PPHelper", "PPLogout", "");
}

//兑换回调接口【只有兑换会执行此回调】
- (void)ppPayResultCallBack:(PPPayResultCode)paramPPPayResultCode{
    NSLog(@"兑换回调返回编码%d",paramPPPayResultCode);
    //回调购买成功。其余都是失败
    if(paramPPPayResultCode == PPPayResultCodeSucceed){
        //购买成功发放道具
        NSLog(@"PP 支付成功");
        UnitySendMessage( "PPHelper", "PPPayResult", "0");
        
    }else{
        NSLog(@"PP 支付失败");
        UnitySendMessage( "PPHelper", "PPPayResult", "1");
    }
}

-(void)ppVerifyingUpdatePassCallBack{
    NSLog(@"验证游戏版本完毕回调");
    [[PPAppPlatformKit sharedInstance] showLogin];
}

@end
#endif