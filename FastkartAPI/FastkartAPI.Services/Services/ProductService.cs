using AutoMapper;
using FastkartAPI.Contracts.DTOs;
using FastkartAPI.DataBase.Models;
using FastkartAPI.DataBase.Models.Enums;
using FastkartAPI.DataBase.Repositories.Interfaces;

namespace FastkartAPI.Services.Services
{
    public class ProductService
    {
        private readonly IItemStoreRepository _itemStoreRepository;
        private readonly IMapper _mapper;

        public ProductService(
            IItemStoreRepository itemStoreRepository, 
            IMapper mapper)
        {
            _itemStoreRepository = itemStoreRepository;
            _mapper = mapper;
        }

        public async Task Create(CreateProductDTO itemDTO) 
        {
            var item = _mapper.Map<ItemStore>(itemDTO); 
            await _itemStoreRepository.Create(item);
        }

        public async Task Update(ItemStore item) 
        {
            await _itemStoreRepository.Update(item);
        }

        public async Task Delete(Guid id) 
        {
            await _itemStoreRepository.Delete(id);
        }

        public async Task<List<ItemStore>> GetAll() 
        {
            return await _itemStoreRepository.GetAll();
        }

        public async Task<ItemStore> GetById(Guid id) 
        {
            return await _itemStoreRepository.GetById(id);
        }

        public async Task<ItemStore> GetByName(string name) 
        {
            return await _itemStoreRepository.GetByName(name);
        }

        public async Task<List<ItemStore>> GetByCategory(TypeItemEnum type) 
        {
            return await _itemStoreRepository.GetByCategory(type);
        }
    }
}
