//
//  GameConfig.h
//  umtest
//
//  Created by Pubilc-YD003 on 13-8-5.
//  Copyright (c) 2013年 Pubilc-YD003. All rights reserved.
//

#ifndef QueenGame_GameConfig_h
#define QueenGame_GameConfig_h

//#define TLMODE_DEBUG
//this version code use to help user to update new app version
//todo
#define V_NUMBER @"1.2.0.0"

//OPEN_UMENG统计SDK
//#define OPEN_UMENG

//FLURRY统计SDK
//#define OPEN_FLURRY

//IMAdTracker网络广告投放监测系统，国内开启，海外关闭
//#define OPEN_INMOBIAD

//GoogleConversionPing统计广告的有效性，国内开启，海外关闭
//#define OPEN_ADMOB

//cyou自己的统计系统，国内开启，海外关闭
//#define OPEN_CYTRACKER

//UMFEEDBACK用户反馈SDK
//#define OPEN_UMFEEDBACK

//#define OPEN_PPSDK

#define KEY_YOUMENG @"51f8823056240bee940a0601"
#define KEY_FLURRY @"BMGN4F2HBMG2MKCQN83B"

#ifdef TLMODE_DEBUG
#define IAPPPAY_KEY @"NDY0QTI2NTMxODJFN0MxMDM1ODkyMkY1QzU4NzRGNDhFQjhGRTkyN09USTNNamMzTXpVd09UazFNalU0TmpnNU1Tc3hOekF3TURFNE9UWTFOall6TmpBek9EYzBOalkwTURZeE9UWXdOalV4T0RNNE16ZzBNRE09"
#define IAPPPAY_APPID @"10058600000014100586"
#else
#define IAPPPAY_KEY @"MDMwM0JCMEVEMDEwMEE3ODMyRkMxNzhGQjY0QTM1NEUzMEEzOTFEQk1UUXdNemsxTkRJMU9ETTVOak13TXpZMk1URXJNVFUyTnpRMU5qVTVOREUxTmprMk56ZzBNVFkyTnpZeE5UZzFOakUyTlRjeU1qVXhNREkz"
#define IAPPPAY_APPID @"10058600000002100586"
#endif

#define PP_KEY @"fbc70dcc8fdf24e1b55eb59397e6990c"
#define PP_APPID 1793
#define INMOBIAD_APPID @"e4b5662b1b1340e7b76c74ada21d3922"

#define CYTRACKER_BUNDLEID @"com.cyou.mrd.tlydtw"
#define CYTRACKER_KEY @"1376036742169"

#define ADMOB_ID @"982048704"
#define ADMOB_LABEL @"WddJCPi3rwkQwL-j1AM"
#define ADMOB_PRICE @"0"

#define CONFIG_IP @"10.127.131.24:8083"

#ifdef TLMODE_DEBUG
#define UPDATESERVER_URL @"http://files2.changyou.com"
#define UPDATESERVER_ROOT @"tlyd/"
#else
#define UPDATESERVER_URL @"http://files2.changyou.com"
#define UPDATESERVER_ROOT @"tlyd/"
#endif

// 更改version 使用不同配置
#define VERSION_APPSTORE

#ifdef VERSION_TB
#define CHANNELID @"tb" //tb:同步推 91:91平台 app:App Store
// ipa付费方式 1——网页充值 2——爱贝充值 3——APPSTORE充值
#define IAPSTYLE 2
#define SERVERLISTURL @"http://files2.changyou.com/tlydios/servers.txt"
#define NOTICE_URL @"http://tlyd.changyou.com/PubImgSour/tlyd/article_content/notice_ios_yueyu.html"
#define OPEN_IAPPPAY
#define APP_UPDATE_URL @""
#endif

#ifdef VERSION_APPSTORE
#define CHANNELID @"App_Store"
#define IAPSTYLE 3
//#define SERVERLISTURL @"http://download.gotoplay.com/tlbb/ios/servers.txt"
#define SERVERLISTURL @"http://115.160.140.207:81/servers_android.txt"
#define NOTICE_URL @"http://mobtlbb.gotoplay.com/notice/notice.html"
#define APP_UPDATE_URL @"https://itunes.apple.com/us/app/tian-long-ba-bu-yi-dong-ban/id744943894?ls=1&mt=8"
#endif

#ifdef VERSION_91
#define CHANNELID @"91"
#define IAPSTYLE 2
#define SERVERLISTURL @"http://files2.changyou.com/tlydios/servers.txt"
#define OPEN_IAPPPAY
#define NOTICE_URL @"http://tlyd.changyou.com/PubImgSour/tlyd/article_content/notice_ios_yueyu.html"
#define APP_UPDATE_URL @"http://app.91.com/Soft/iPhone/com.cyou.mrd.tlbbkdtl-1.0-1.0.html"
#endif

#ifdef VERSION_pp
#define CHANNELID @"pp"
#define IAPSTYLE 4
#define SERVERLISTURL @"http://files2.changyou.com/tlydios_pp/servers.txt"
#define OPEN_PPSDK
#define NOTICE_URL @"http://tlyd.changyou.com/PubImgSour/tlyd/article_content/notice_ios_pp.html"
#define APP_UPDATE_URL @"http://www.25pp.com/ipad/game/info_1117581.html"
#endif

#ifdef VERSION
#define CHANNELID @"unknown"
#define IAPSTYLE 2
#define SERVERLISTURL @"http://files2.changyou.com/tlydios/servers.txt"
#define OPEN_IAPPPAY
#define NOTICE_URL @"http://tlyd.changyou.com/PubImgSour/tlyd/article_content/notice_ios.html"
#define APP_UPDATE_URL @"";
#endif











#endif
