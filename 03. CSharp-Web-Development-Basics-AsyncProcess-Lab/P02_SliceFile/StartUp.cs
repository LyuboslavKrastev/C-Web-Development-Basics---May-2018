﻿using System;
using System.IO;
using System.Threading.Tasks;

namespace P02_SliceFile
{
    public class StartUp
    {
        static void Main()
        {
            Console.WriteLine("Where is the file you would like to be sliced?");
            Console.Write("File path: ");
            var videoPath = Console.ReadLine();
            Console.WriteLine("Where would you want to save the slices?");
            Console.Write("Destination path: ");
            var destination = Console.ReadLine();
            Console.Write("Enter the number of slices: ");
            var pieces = int.Parse(Console.ReadLine());

            SliceAsync(videoPath, destination, pieces);

            Console.WriteLine("Anything else?");

            while (true)
            {
                Console.ReadLine();
            }
        }

        static void SliceAsync(string sourceFile, string destinationPath, int parts)
        {
            Task.Run(() =>
            {
                Slice(sourceFile, destinationPath, parts);
            });
        }

        static void Slice(
            string sourceFile, string destinationPath, int parts)
        {
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            using (var source = new FileStream(sourceFile, FileMode.Open))
            {
                FileInfo fileInfo = new FileInfo(sourceFile);

                long partLength = (source.Length / parts) + 1;
                long currentByte = 0;

                for (int currentPart = 1; currentPart <= parts; currentPart++)
                {
                    string filePath = string.Format($"{destinationPath}/Part-{currentPart}{fileInfo.Extension}");

                    using (var destination = new FileStream(filePath, FileMode.Create))
                    {
                        byte[] buffer = new byte[1024];

                        while (currentByte <= partLength * currentPart)
                        {
                            int readBytesCount = source.Read(buffer, 0, buffer.Length);
                            if (readBytesCount == 0)
                            {
                                break;
                            }

                            destination.Write(buffer, 0, readBytesCount);
                            currentByte += readBytesCount;
                        }
                    }
                    Console.WriteLine("Slice Complete");
                }
            }
        }
    }
}

