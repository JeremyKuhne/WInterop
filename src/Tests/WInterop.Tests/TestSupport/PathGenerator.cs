// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Text;
using WInterop.Support;

namespace Tests.Support;

public static class PathGenerator
{
    // Note that 254 is the safe max segment (works on optical drives)
    public static string CreatePathOfLength(string root, int totalLength, int directoryLength = 254)
    {
        var sb = new StringBuilder(directoryLength);

        string guid = Guid.NewGuid().ToString();
        if (directoryLength < guid.Length)
        {
            sb.Append(guid, 0, directoryLength);
        }
        else
        {
            sb.Append('0', directoryLength - guid.Length);
        }
        string directory = sb.ToString();

        int neededLength = totalLength - root.Length;
        int directoryCount = neededLength / (directory.Length + 1);
        int lastDirectory = neededLength % (directory.Length + 1) + 1;

        var fullPath = new StringBuilder(totalLength);
        fullPath.Append(root);

        for (int i = 0; i < directoryCount; i++)
        {
            Paths.Join(fullPath, directory);
        }

        if (lastDirectory > 0)
        {
            Paths.Join(fullPath, directory.Substring(0, lastDirectory));
        }

        return fullPath.ToString();
    }
}
