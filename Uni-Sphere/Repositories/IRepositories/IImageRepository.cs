namespace Uni_Sphere.Repositories.IRepositories
{
    public interface IImageRepository
    {
        Task<string?> UploadAsync(IFormFile file);
    }
}
