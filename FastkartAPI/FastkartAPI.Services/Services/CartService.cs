using FastkartAPI.DataBase.Models;
using FastkartAPI.DataBase.Repositories.Extensions;
using FastkartAPI.DataBase.Repositories.Interfaces;

public class CartService
{
    private readonly ICartModelRepository _cartRepository;
    private readonly IItemStoreRepository _itemRepository;

    public CartService(
        ICartModelRepository cartRepository,
        IItemStoreRepository itemRepository)
    {
        _cartRepository = cartRepository;
        _itemRepository = itemRepository;
    }

    public async Task AddToCart(Guid userId, Guid itemId, int quantity)
    {
        await _cartRepository.Create(userId, itemId, quantity);
    }

    public async Task UpdateCartItem(Guid cartItemId, int newQuantity)
    {
        await _cartRepository.Update(cartItemId, newQuantity);
    }

    public async Task RemoveFromCart(Guid cartItemId)
    {
        await _cartRepository.Delete(cartItemId);
    }

    public async Task<List<CartModel>> GetUserCart(Guid userId)
    {
        return await _cartRepository.GetByUserId(userId);
    }

    public async Task ClearCart(Guid userId)
    {
        await _cartRepository.DeleteByID(userId);
    }

    public async Task ProcessOrder(Guid userId)
    {
        var cartItems = await _cartRepository.GetByUserId(userId);

        foreach (var item in cartItems)
        {
            // Уменьшаем количество товара на складе
            await _itemRepository.DecreaseStock(item.ItemId, item.Qty);
        }

        // Очищаем корзину после оформления заказа
        await _cartRepository.DeleteByID(userId);
    }
}