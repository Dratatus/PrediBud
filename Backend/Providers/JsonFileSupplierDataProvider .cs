using Backend.Conventer;
using Backend.Data.Models.Suppliers;
using Newtonsoft.Json;

namespace Backend.Providers
{
    public class JsonFileSupplierDataProvider: ISupplierDataProvider
    {
        private readonly string _path;
        private readonly JsonSerializerSettings _settings;

        public JsonFileSupplierDataProvider(string path)
        {
            _path = path;
            _settings = new JsonSerializerSettings
            {
                Converters = { new MaterialPriceJsonConverter() }
            };
        }

        public async Task<List<Supplier>> GetSuppliersAsync()
        {
            var json = await File.ReadAllTextAsync(_path);
            var suppliers = JsonConvert.DeserializeObject<List<Supplier>>(json, _settings);
            return suppliers ?? new List<Supplier>();
        }
    }
}
