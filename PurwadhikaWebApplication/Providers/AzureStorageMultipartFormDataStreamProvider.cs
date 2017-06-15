using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Net.Http.Headers;

namespace PurwadhikaWebApplication.Providers
{
    public class AzureStorageMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public readonly CloudBlobContainer _blobContainer;
        private readonly string[] _supportedMimeTypes = { "image/png", "image/jpeg", "image/jpg" };

        public AzureStorageMultipartFormDataStreamProvider(CloudBlobContainer blobContainer) : base("azure")
        {
            _blobContainer = blobContainer;
            
        }

        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            if (parent == null) throw new ArgumentException(nameof(parent));
            if (headers == null) throw new ArgumentNullException(nameof(headers));

            if (!_supportedMimeTypes.Contains(headers.ContentType.ToString().ToLower()))
            {
                throw new NotSupportedException("Unsupported Picture Format!");
            }

            var fileName = Guid.NewGuid().ToString();

            CloudBlockBlob blob = _blobContainer.GetBlockBlobReference(fileName);

            if(headers.ContentType != null)
            {
                blob.Properties.ContentType = headers.ContentType.MediaType;
            }

            this.FileData.Add(new MultipartFileData(headers, blob.Name));

            return blob.OpenWrite();
        }
    }
}