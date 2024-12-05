namespace AssetManagement.Interface
{
    public interface IAsset
    {
        Task<List<Asset>> GetAllAssets();
        Task<Asset?> GetAssetById(int id);
        Task AddAsset(Asset asset);
        Task<Asset> UpdateAsset(Asset asset);
        Task DeleteAsset(int id);
        Task Save();
    }
}
