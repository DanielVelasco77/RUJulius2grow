using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RUJulius2grow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagenController : ControllerBase
    {
        private readonly IAmazonS3 _amazons;
        public ImagenController(IAmazonS3 amazons)
        {
            _amazons = amazons;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] IFormFile file)  
        {
            var putRequest = new PutObjectRequest()
            {
                BucketName = "notengocuenta",
                Key = file.FileName,
                InputStream = file.OpenReadStream(),
                ContentType = file.ContentType,
            };
            var result = await _amazons.PutObjectAsync(putRequest);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string imag)
        {
            var request = new GetObjectRequest()
            {
                BucketName = "notengocuenta",
                Key = imag,
            };

            using GetObjectResponse response = await _amazons.GetObjectAsync(request);
            using Stream resStream = response.ResponseStream;

            var stream = new MemoryStream();

            await resStream.CopyToAsync(stream);

            stream.Position = 0;

            return new FileStreamResult(stream, response.Headers["Content-Type"])
            {
                FileDownloadName = imag
            };
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] string imag)
        {
            var request = new DeleteObjectRequest()
            {
                BucketName = "notengocuenta",
                Key = imag,
            };
            var response = await _amazons.DeleteObjectAsync(request);
            return Ok(response);

        }

    }
}
