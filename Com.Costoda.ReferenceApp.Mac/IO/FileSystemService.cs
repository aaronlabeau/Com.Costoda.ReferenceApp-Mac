using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using AppKit;
using Foundation;
using static System.Environment;

namespace Com.Costoda.ReferenceApp.Mac.IO
{
    public class FileSystemService
    {
        #region Singleton

        private FileSystemService() { }

        private readonly static Lazy<FileSystemService> _instance = new Lazy<FileSystemService>(() => new FileSystemService());

        public static FileSystemService Instance => _instance.Value;

        #endregion

        public string LocalStoragePath => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public string FilesStoragePath => Path.Combine(LocalStoragePath, "XamRefApp");

        public void CreateDirectory(string path)
            => NSApplication.SharedApplication.InvokeOnMainThread(() =>
        {
            Directory.CreateDirectory(path);
        });

        public void DeleteDirectory(string path)
        => NSApplication.SharedApplication.InvokeOnMainThread(() =>
        {
            NSFileManager.DefaultManager.Remove(path, out NSError error);
            if (error != null)
            {
                Console.WriteLine($"{DateTime.Now} :: Error - {error.DebugDescription}");
                throw new NSErrorException(error);
            }
        });

        public bool FileExists(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                var result = false;
                NSApplication.SharedApplication.InvokeOnMainThread(() =>
                {
                    result = NSFileManager.DefaultManager.FileExists(path);
                });

                return result;
            }

            return false;
        }

        public void DeleteFile(string path)
        {
            if (FileExists(path))
            {
                NSError error = null;

                NSApplication.SharedApplication.InvokeOnMainThread(() =>
                {
                    NSFileManager.DefaultManager.Remove(NSUrl.CreateFileUrl(new[] { path }), out error);
                });

                if (error != null)
                {
                    Console.WriteLine($"{DateTime.Now} :: Error - {error.DebugDescription}");
                    throw new NSErrorException(error);
                }
            }
        }

        public long FileSize(string path)
        {
            if (FileExists(path))
            {
                NSError error = null;
                NSFileAttributes fileAttributes = null;
                ulong? size = null;

                NSApplication.SharedApplication.InvokeOnMainThread(() =>
                {
                    fileAttributes = NSFileManager.DefaultManager.GetAttributes(path, out error);
                });

                if (error != null)
                {
                    Console.WriteLine($"{DateTime.Now} :: Error - {error.DebugDescription}");
                    throw new NSErrorException(error);
                }

                size = fileAttributes.Size;

                return size.HasValue ? (long)size.Value : 0;
            }

            return 0;
        }

        public IList<string> GetFilePathsInDirectory(string path)
        {
            var files = new List<string>();

            NSApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                files = Directory.GetFiles(path).ToList();
            });

            return files;
        }

        public Stream GetInputStream(string path, string userIdentity)
        {
            NSFileHandle fileHandle = null;
            NSError error = null;
            NSData result = null;
            Exception exception = null;

            try
            {
                NSApplication.SharedApplication.InvokeOnMainThread(() =>
                {
                    fileHandle = NSFileHandle.OpenReadUrl(NSUrl.CreateFileUrl(new[] { path }), out error);
                    result = fileHandle.ReadDataToEndOfFile();
                });
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            finally
            {
                if (fileHandle != null)
                {
                    NSApplication.SharedApplication.InvokeOnMainThread(() =>
                    {
                        fileHandle.CloseFile();
                        fileHandle.Dispose();
                        fileHandle = null;
                    });
                }
                if (error != null)
                {
                    Console.WriteLine($"{DateTime.Now} :: Error - {error.DebugDescription}");
                    throw new NSErrorException(error);
                }
                else if (exception != null)
                {
                    Console.WriteLine($"{DateTime.Now} :: Error - {exception.Message} Stack Trace: {exception.StackTrace}");
                    throw exception;
                }
            }

            return result?.AsStream() ?? null;
        }

        public Stream GetOutputStream(string path, bool overwrite)
        {
            return new FileStream(path, overwrite ? FileMode.Create : FileMode.CreateNew);
        }

        public string GetSpecialFolderPath(SpecialFolder folder)
        {
            switch (folder)
            {
                case SpecialFolder.DesktopDirectory:
                    return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                case SpecialFolder.MyDocuments:
                    return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                default:
                    return string.Empty;
            }
        }

        public string ReadFile(string path)
        {
            string result = String.Empty;

            if (FileExists(path))
            {
                NSFileHandle fileHandle = null;
                NSError error = null;
                Exception exception = null;

                try
                {
                    NSApplication.SharedApplication.InvokeOnMainThread(() =>
                    {
                        fileHandle = NSFileHandle.OpenReadUrl(NSUrl.CreateFileUrl(new[] { path }), out error);
                        if (fileHandle != null)
                        {
                            var nsStringResult = NSString.FromData(fileHandle.ReadDataToEndOfFile(), NSStringEncoding.UTF8);
                            if (nsStringResult != null)
                            {
                                result = nsStringResult.ToString();
                            }
                        }
                    });
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
                finally
                {
                    if (fileHandle != null)
                    {
                        NSApplication.SharedApplication.InvokeOnMainThread(() =>
                        {
                            fileHandle.CloseFile();
                            fileHandle.Dispose();
                            fileHandle = null;
                        });
                    }
                }
                if (error != null)
                {
                    Console.WriteLine($"{DateTime.Now} :: Error - {error.DebugDescription}");
                    throw new NSErrorException(error);
                }
                else if (exception != null)
                {
                    Console.WriteLine($"{DateTime.Now} :: Error - {exception.Message} Stack Trace: {exception.StackTrace}");
                    throw exception;
                }
            }
            return result;
        }

        public void WriteFile(string path, byte[] contents)
        {

            if (!string.IsNullOrEmpty(path))
            {
                if (NSFileManager.DefaultManager.FileExists(path))
                {
                    DeleteFile(path);
                }

                var dict = new NSMutableDictionary();

                NSApplication.SharedApplication.InvokeOnMainThread(() =>
                {
                    NSFileManager.DefaultManager.CreateFile(path, NSData.FromArray(contents), dict);
                });
            }
        }

        public void WriteFile(string path, string contents)
        {
            if (!string.IsNullOrEmpty(path) || !string.IsNullOrEmpty(contents))
            {
                if (!NSFileManager.DefaultManager.FileExists(path))
                {
                    var dict = new NSMutableDictionary();
                    NSApplication.SharedApplication.InvokeOnMainThread(() =>
                    {
                        NSFileManager.DefaultManager.CreateFile(path, NSData.FromString(contents), dict);
                    });
                }
                else
                {
                    NSFileHandle fileHandle = null;
                    NSError error = null;
                    Exception exception = null;

                    try
                    {
                        NSApplication.SharedApplication.InvokeOnMainThread(() =>
                        {
                            fileHandle = NSFileHandle.OpenUpdateUrl(NSUrl.CreateFileUrl(new[] { path }), out error);
                            fileHandle.SeekToEndOfFile();
                            fileHandle.WriteData(NSData.FromString(contents));
                        });
                    }
                    catch (Exception ex)
                    {
                        exception = ex;
                    }
                    finally
                    {
                        if (fileHandle != null)
                        {
                            NSApplication.SharedApplication.InvokeOnMainThread(() =>
                            {
                                fileHandle.CloseFile();
                                fileHandle.Dispose();
                                fileHandle = null;
                            });
                        }
                    }

                    if (error != null)
                    {
                        Console.WriteLine($"{DateTime.Now} :: Error - {error.DebugDescription}");
                        throw new NSErrorException(error);
                    }
                    else if (exception != null)
                    {
                        Console.WriteLine($"{DateTime.Now} :: Error - {exception.Message} Stack Trace: {exception.StackTrace}");
                        throw exception;
                    }
                }
            }
        }
    }
}
