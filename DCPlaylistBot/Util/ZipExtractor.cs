using System;
using System.IO;
using System.IO.Compression;

public class ZipExtractor {
    public static void ExtractZipFromByteArray(byte[] zipData, string destinationFolderPath) {

        if(File.Exists(destinationFolderPath)) File.Delete(destinationFolderPath);

        // Step 1: Convert byte array to a stream
        MemoryStream memoryStream = new MemoryStream(zipData);

        // Step 2: Write the stream to a temporary file
        string tempFilePath = Path.GetTempFileName();
        using (FileStream tempFileStream = File.Create(tempFilePath)) {
            memoryStream.CopyTo(tempFileStream);
        }

        try {
            ZipFile.ExtractToDirectory(tempFilePath, destinationFolderPath);
        } catch (Exception ex) {
            Console.WriteLine($"Failed to extract zip file: {ex.Message}");
        } finally {
            File.Delete(tempFilePath);
        }
    }
}
