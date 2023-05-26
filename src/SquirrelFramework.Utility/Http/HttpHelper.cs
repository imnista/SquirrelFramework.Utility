namespace SquirrelFramework.Utility.Common.Http
{
    public static class HttpHelper
    {
        public static async Task DownloadFileTaskAsync(this HttpClient client, Uri uri, string FileName)
        {
            using var stream = await client.GetStreamAsync(uri);
            using var fileStream = new FileStream(FileName, FileMode.CreateNew);
            await stream.CopyToAsync(fileStream);
        }
    }
}