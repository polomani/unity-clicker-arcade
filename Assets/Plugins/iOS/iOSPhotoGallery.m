bool saveToGallery(const char * path)
{
    NSString *imagePath = [NSString stringWithUTF8String:path];
    
    NSLog(@"###### This is the file path being passed: %@", imagePath);
    
    if(![[NSFileManager defaultManager] fileExistsAtPath:imagePath]) 
    {
        NSLog(@"###### Early exit - file doesn't exist");
        return false;
    }
    
    UIImage *image = [UIImage imageWithContentsOfFile:imagePath];
    
    if (image) 
    {
        UIImageWriteToSavedPhotosAlbum(image, nil, NULL, NULL);
        return true;
    }
    
    return false;
}
