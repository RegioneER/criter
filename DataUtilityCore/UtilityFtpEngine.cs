using System;
using System.Configuration;
using System.IO;
using Renci.SshNet;

namespace DataUtilityCore
{
    public class UtilityFtpEngine
    {
        //public static void SftClientPrivateKey()
        //{
        //    PrivateKeyFile keyFile = new PrivateKeyFile(@"path/to/OpenSsh-RSA-key.ppk");
        //    var keyFiles = new[] { keyFile };

        //    var methods = new List<AuthenticationMethod>();
        //    methods.Add(new PasswordAuthenticationMethod(ConfigurationManager.AppSettings["NexiveSftpUsername"], ConfigurationManager.AppSettings["NexiveSftpPassword"]));
        //    methods.Add(new PrivateKeyAuthenticationMethod(ConfigurationManager.AppSettings["NexiveSftpUsername"], keyFiles));

        //    ConnectionInfo con = new ConnectionInfo(ConfigurationManager.AppSettings["NexiveSftpHost"], 
        //                                            int.Parse(ConfigurationManager.AppSettings["NexiveSftpPort"], 
        //                                            ConfigurationManager.AppSettings["NexiveSftpUsername"], 
        //                                            methods.ToArray());

        //    using (var client = new SftpClient(con))
        //    {
        //        client.Connect();

        //        // Do what you need with the client !

        //        client.Disconnect();
        //    }
        //}
        
        public static SftpClient sftpClient()
        {
            var client = new SftpClient(ConfigurationManager.AppSettings["NexiveSftpHost"],
                                                      int.Parse(ConfigurationManager.AppSettings["NexiveSftpPort"]),
                                                      ConfigurationManager.AppSettings["NexiveSftpUsername"],
                                                      ConfigurationManager.AppSettings["NexiveSftpPassword"]);

            return client;
        }

        public static bool UploadSftpNexive(string pathLocalfile)
        {
            bool fSend = false;

            using (var client = sftpClient())
            {
                try
                {
                    client.Connect();
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["NexiveSftpUploadDirectory"]))
                    {
                        client.ChangeDirectory(ConfigurationManager.AppSettings["NexiveSftpUploadDirectory"]);
                    }
                    
                    if (client.IsConnected)
                    {
                        using (FileStream fs = new FileStream(pathLocalfile, FileMode.Open))
                        {
                            try
                            {
                                client.BufferSize = 4 * 1024;
                                client.UploadFile(fs, Path.GetFileName(pathLocalfile));
                                fSend = true;
                            }
                            catch (Exception)
                            {
                                fSend = false;
                            }
                        }
                    }
                    else
                    {
                        fSend = false;
                    }
                    client.Disconnect();
                }
                catch (Exception)
                {
                    fSend = false;
                }
            }

            return fSend;
        }

        public static bool DownloadSftpNexive(string pathLocalfile)
        {
            bool fDownload = true;

            using (var client = sftpClient())
            {
                try
                {
                    client.Connect();
                    
                    string remoteDirectory = ConfigurationManager.AppSettings["NexiveSftpDownloadDirectory"];
                    if (client.IsConnected)
                    {
                        foreach (Renci.SshNet.Sftp.SftpFile ftpfile in client.ListDirectory(remoteDirectory))
                        {
                            if (ftpfile.Name.Contains(".zip") || ftpfile.Name.Contains(".txt"))
                            {
                                using (FileStream fs = new FileStream(pathLocalfile + @"\" + ftpfile.Name, FileMode.Create))
                                {
                                    client.DownloadFile(ftpfile.FullName, fs);
                                }
                            }
                        }
                    }
                    else
                    {
                        fDownload = false;
                    }
                    client.Disconnect();
                }
                catch (Exception)
                {
                    fDownload = false;
                }
            }

            return fDownload;
        }

        public static bool RenameFileSftpNexive(string remoteFileName)
        {
            bool fRename = false;

            using (var client = sftpClient())
            {
                try
                {
                    client.Connect();

                    string remoteDirectory = ConfigurationManager.AppSettings["NexiveSftpDownloadDirectory"];
                    if (client.IsConnected)
                    {
                        if (remoteFileName.Contains(".zip"))
                        {
                            client.RenameFile(remoteDirectory + @"/" + remoteFileName, remoteDirectory + @"/" + remoteFileName.Replace(".zip", ".zip.STAMPATO"));
                        }
                        if (remoteFileName.Contains(".txt"))
                        {
                            client.RenameFile(remoteDirectory + @"/" + remoteFileName, remoteDirectory + @"/" + remoteFileName.Replace(".txt", ".txt.STAMPATO"));
                        }
                        
                        fRename = true;
                    }
                    else
                    {
                        fRename = false;
                    }
                    client.Disconnect();
                }
                catch (Exception ex)
                {
                    fRename = false;
                }
            }

            return fRename;
        }

    }
}
