#import <UIKit/UIKit.h>

@interface NativeDialog:UIViewController<UIAlertViewDelegate>
-(void) ShowDialog:(NSString *)title
       MessageStr :(NSString *)message;
@end