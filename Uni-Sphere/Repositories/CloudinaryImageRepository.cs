using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Uni_Sphere.Repositories
{
    public class CloudinaryImageRepository : IImageRepository
    {
        private readonly IConfiguration _configuration;
        private readonly Account cloudinaryAccount;

        public CloudinaryImageRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            cloudinaryAccount = new Account(
                _configuration.GetSection("Cloudinary")["CloudName"],
                _configuration.GetSection("Cloudinary")["ApiKey"],
                _configuration.GetSection("Cloudinary")["ApiSecret"]
              );
        }   

        public async Task<string> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(cloudinaryAccount);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName
            };

            var uploadResult = await client.UploadAsync(uploadParams);

            if(uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUri.ToString();
            }

            return null;
        }
    }
}
