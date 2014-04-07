#import "NativeDialog.h"
#import "GameConfig.h"

@implementation NativeDialog

-(void) ShowDialog:(NSString *)title
       MessageStr :(NSString *)message{
    
    UIAlertView *alert =
    [[UIAlertView alloc] initWithTitle:title message:message delegate:self  cancelButtonTitle:@"知道了"
        otherButtonTitle:nil];
	[alert show];
	[alert release];
}



-(void) alertView:(UIAlertView *)alertView clickedButtonAtIndex:(NSInteger)buttonIndex{
    NSLog(@"index:%d",buttonIndex);
    exit(0);
}
//AlertView已经消失时执行的事件
-(void)alertView:(UIAlertView *)alertView didDismissWithButtonIndex:(NSInteger)buttonIndex
{
    NSLog(@"didDismissWithButtonIndex");
}

//ALertView即将消失时的事件
-(void)alertView:(UIAlertView *)alertView willDismissWithButtonIndex:(NSInteger)buttonIndex
{
    NSLog(@"willDismissWithButtonIndex");
}

//AlertView的取消按钮的事件
-(void)alertViewCancel:(UIAlertView *)alertView
{
    NSLog(@"alertViewCancel");
}

//AlertView已经显示时的事件
-(void)didPresentAlertView:(UIAlertView *)alertView
{
    NSLog(@"didPresentAlertView");
}

//AlertView即将显示时
-(void)willPresentAlertView:(UIAlertView *)alertView
{
    NSLog(@"willPresentAlertView");
}

@end