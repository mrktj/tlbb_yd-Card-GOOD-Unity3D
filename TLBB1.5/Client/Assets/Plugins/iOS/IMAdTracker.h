//
//  IMAdTracker.h
//  InMobi Ad Tracker SDK
//
//  Copyright (c) 2013 InMobi Technology Services Ltd. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

/**
 * Main class of InMobi Ad Tracker iOS SDK.
 * This contains the code required to track all different application goals.
 * It helps you track the post-click conversions of your iOS App campaigns
 * across all your advertising channels. There is no limitation to what can
 * be tracked: downloads, leads, registrations, purchases, and so on.
 */
@interface IMAdTracker : NSObject {

}
/**
 * Initializes the class with InMobi Ad Tracker App ID.
 * This is a non blocking API (returns immediately).
 * Once it is initialized, you can report any goal whereever in the code.
 * Therefore, it is recommended that you do call this API at the launch time
 * in your AppDelegate.m file.
 * @param adTrackerAppId InMobi Ad Tracker Application Id.
 */
+ (void)initWithAppID:(NSString *)adTrackerAppId;

/**
 * Reports the Application Download Goal to InMobi Ad Tracker.
 * This is a non blocking API (returns immediately), and performs the
 * reporting job in the background.
 * When a goal is reported, it is made sure that it reaches InMobi Ad Tracker
 * server whenever the device is connected to internet. You can use this API
 * to report download goal without worrying about the internet connection.
 * @Note: It is made sure in the API that the download is reported only once
 * in the App's lifetime.
 */
+ (void)reportAppDownloadGoal;

/**
 * Reports the Application Custom Goals to InMobi Ad Tracker.
 * This is a non blocking API (returns immediately), and performs the
 * reporting job in the background.
 * When a goal is reported, it is made sure that it reaches InMobi Ad Tracker
 * server whenever the device is connected to internet. You can report
 * different goals without worrying about the internet connection.
 * @Note: Do not use this API to report App Download. Please use
 * reportAppDownloadGoal API for reporting App Download.
 * @param goalName Your custom goal name.
 */
+ (void)reportCustomGoal:(NSString *)goalName;

@end
