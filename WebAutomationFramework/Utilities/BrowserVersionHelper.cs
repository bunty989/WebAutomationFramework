﻿using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Serilog;
using Browser = WebAutomationFramework.Utilities.TestConstant.BrowserType;

namespace WebAutomationFramework.Utilities
{
    internal class BrowserVersionHelper
    {
        public static string? GetBrowserVersion(Browser browserName)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                string? exePath = browserName switch
                {
                    Browser.Chrome or Browser.ChromeHeadless or Browser.ChromeIncognito =>
                        Registry.GetValue(
                            @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\chrome.exe", "",
                            "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe")?.ToString(),
                    Browser.Firefox =>
                        Registry.GetValue(
                            @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\firefox.exe", "",
                            "C:\\Program Files\\Mozilla Firefox\\firefox.exe")?.ToString(),
                    Browser.Edge =>
                        Registry.GetValue(
                            @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\msedge.exe", "",
                            "C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe")?.ToString(),
                    Browser.InternetExplorer =>
                        Registry.GetValue(
                            @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\iexplore.exe", "",
                            "C:\\Program Files (x86)\\Internet Explorer\\iexplore.exe")?.ToString(),
                    Browser.Safari => null, // Safari is not available on Windows
                    _ => null
                };
                if (!string.IsNullOrEmpty(exePath) && File.Exists(exePath))
                {
                    return FileVersionInfo.GetVersionInfo(exePath).FileVersion;
                }
                return null;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
                     RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                string? browserCmd = browserName switch
                {
                    Browser.Chrome or Browser.ChromeHeadless or Browser.ChromeIncognito => "google-chrome",
                    Browser.Firefox => "firefox",
                    Browser.Edge => "microsoft-edge",
                    Browser.Safari => "safari",
                    _ => null
                };

                if (browserName == Browser.Safari && RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    try
                    {
                        var process = new Process
                        {
                            StartInfo = new ProcessStartInfo
                            {
                                FileName = "defaults",
                                Arguments = "read /Applications/Safari.app/Contents/Info CFBundleShortVersionString",
                                RedirectStandardOutput = true,
                                UseShellExecute = false,
                                CreateNoWindow = true
                            }
                        };
                        process.Start();
                        string? output = process.StandardOutput.ReadLine();
                        process.WaitForExit();

                        if (!string.IsNullOrEmpty(output))
                        {
                            return output.Trim();
                        }
                    }
                    catch
                    {
                        Log.Error("Error occurred while trying to get the Safari version. Please check if Safari is installed and accessible.");
                    }
                    return null;
                }

                if (browserCmd == null || browserCmd == "safari")
                    return null;

                try
                {
                    var process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = browserCmd,
                            Arguments = "--version",
                            RedirectStandardOutput = true,
                            UseShellExecute = false,
                            CreateNoWindow = true
                        }
                    };
                    process.Start();
                    string? output = process.StandardOutput.ReadLine();
                    process.WaitForExit();

                    // Output is like "Google Chrome 123.0.1234.56"
                    if (!string.IsNullOrEmpty(output))
                    {
                        var parts = output.Split(' ');
                        foreach (var part in parts)
                        {
                            if (Version.TryParse(part, out _))
                                return part;
                        }
                    }
                }
                catch
                {
                    Log.Error("Error occurred while trying to get the browser version. Please check if the browser is installed and accessible.");
                }
                return null;
            }
            else
            {
                Log.Error("Unsupported OS platform. This method only supports Windows, Linux, and macOS.");
                return null;
            }
        }
    }
}