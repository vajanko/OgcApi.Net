﻿using OgcApi.SQLite.Tests.Utils;
using System.IO;
using System.Runtime.InteropServices;
using System;
using System.Linq;

namespace OgcApi.SQLite.Tests;

public class DatabaseFixture
{
    public DatabaseFixture()
    {
        InitSQLite();

        DatabaseUtils.RecreateDatabase();
    }

    private void InitSQLite()
    {
        string baseDir = AppContext.BaseDirectory;

        // add native library location to the path variable so the native libraries are correctly loaded
        // this is only neccesary when running the tests
        string rid = RuntimeInformation.RuntimeIdentifier;
        string nativeDir = Path.Combine(baseDir, "runtimes", rid, "native");

        if (Directory.Exists(nativeDir))
        {
            PatchEnvironmentPath(nativeDir);
        }
    }

    private void PatchEnvironmentPath(string nativeDir)
    {
        string envVar;
        string currentValue;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            envVar = "PATH";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            envVar = "LD_LIBRARY_PATH";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            envVar = "DYLD_LIBRARY_PATH";
        }
        else
        {
            throw new PlatformNotSupportedException();
        }

        currentValue = Environment.GetEnvironmentVariable(envVar) ?? string.Empty;

        if (!currentValue.Split(Path.PathSeparator).Contains(nativeDir))
        {
            string newValue = nativeDir + Path.PathSeparator + currentValue;
            Environment.SetEnvironmentVariable(envVar, newValue);
        }
    }
}