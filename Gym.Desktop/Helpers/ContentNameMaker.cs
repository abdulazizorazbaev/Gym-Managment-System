using System;
using System.IO;

namespace Gym.Desktop.Helpers;

public class ContentNameMaker
{
    public static string GetImageName(string filePath)
    {
        FileInfo fileInfo = new FileInfo(filePath);
        return "IMG_" + Guid.NewGuid().ToString() + fileInfo.Extension;
    }
}
