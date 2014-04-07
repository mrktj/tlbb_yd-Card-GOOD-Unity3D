//
//  KDTLWebView.m
//  Unity-iPhone
//
//  Created by ChenBin on 13-10-29.
//
//

#import "KDTLWebView.h"

@implementation KDTLWebView

- (id)initWithFrame:(CGRect)frame
{
    self = [super initWithFrame:frame];
    if (self) {
        // Initialization code
        self.delegate = self;
    }
    return self;
}

/*
// Only override drawRect: if you perform custom drawing.
// An empty implementation adversely affects performance during animation.
- (void)drawRect:(CGRect)rect
{
    // Drawing code
}
*/
- (BOOL)webView:(UIWebView *)webView shouldStartLoadWithRequest:(NSURLRequest *)request navigationType:(UIWebViewNavigationType)navigationType
{
    NSString *urlStr = [request.URL absoluteString];
    
    // here is your request URL
    NSString *theURL = @"http://member.changyou.com/webphoregist/closeView?";
    
    if ([urlStr hasPrefix:theURL]){
        // send message to game
        NSString * username = [urlStr substringFromIndex:theURL.length];
        UnitySendMessage( "AccountManager", "otherRegFinish", [username cStringUsingEncoding:NSUTF8StringEncoding]);
        return NO;
    }
    
    return YES;

}
@end
