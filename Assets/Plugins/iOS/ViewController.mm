@implementation ViewController : UIViewController

-(void) shareMethod: (const char *) path Message : (const char *) shareMessage
{
    NSString *imagePath = [NSString stringWithUTF8String:path];
    
    UIImage *image = [UIImage imageWithContentsOfFile:imagePath];
    NSString *message   = [NSString stringWithUTF8String:shareMessage];
    NSArray *postItems  = @[message,image];
    
    UIActivityViewController *activityVc = [[UIActivityViewController alloc]initWithActivityItems:postItems applicationActivities:nil];
    
    if (UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPhone && [activityVc respondsToSelector:@selector(popoverPresentationController)])
    {
        [[UIApplication sharedApplication].keyWindow.rootViewController presentViewController:activityVc animated:YES completion:nil];
    }
    else
    {
        UIPopoverController *popup = [[UIPopoverController alloc] initWithContentViewController:activityVc];

        [popup presentPopoverFromRect:CGRectMake(self.view.frame.size.width/2, self.view.frame.size.height/4, 0, 0)
        inView:[UIApplication sharedApplication].keyWindow.rootViewController.view permittedArrowDirections:UIPopoverArrowDirectionAny animated:YES];
    }
}

-(void) shareOnlyTextMethod: (const char *) shareMessage
{
    
    NSString *message   = [NSString stringWithUTF8String:shareMessage];
    NSArray *postItems  = @[message];
    
    UIActivityViewController *activityVc = [[UIActivityViewController alloc] initWithActivityItems:postItems applicationActivities:nil];
    
    
    if (UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPhone && [activityVc respondsToSelector:@selector(popoverPresentationController)])
    {
        [[UIApplication sharedApplication].keyWindow.rootViewController presentViewController:activityVc animated:YES completion:nil];
    }
    else
    {
        UIPopoverController *popup = [[UIPopoverController alloc] initWithContentViewController:activityVc];

        [popup presentPopoverFromRect:CGRectMake(self.view.frame.size.width/2, self.view.frame.size.height/4, 0, 0)
        inView:[UIApplication sharedApplication].keyWindow.rootViewController.view permittedArrowDirections:UIPopoverArrowDirectionAny animated:YES];
    }
}
@end

extern "C"{
    void _IB_ShareTextWithImage(const char * path, const char * message)
    {
        ViewController *vc = [[ViewController alloc] init];
        [vc shareMethod:path Message:message];
    }
}
extern "C"{
    void _IB_ShareSimpleText(const char * message)
    {
        ViewController *vc = [[ViewController alloc] init];
        [vc shareOnlyTextMethod: message];
    }
}
