const cartInfo = {
    totalItems: 0,
    totalSum: 0,
}

// Функция обновления бейджа корзины
function updateCartBadge() {
    const badge = document.querySelector('.header-wishlist .badge');
    if (badge) {
        badge.textContent = cartInfo.totalItems;
    }
}

// Функция обновления выпадающего списка корзины
function updateCartDropdown(cartItems) {
    const cartList = document.querySelector('.cart-list');
    const totalPriceElement = document.querySelector('.price-box h4.theme-color');

    // Очистка списка
    cartList.innerHTML = '';

    let totalSum = 0;
    cartItems.forEach(item => {
        // Используем правильные свойства из вашего ответа сервера
        const price = item.itemPrice || 0;
        const quantity = item.qty || 0;
        const itemId = item.itemId || '';
        const itemName = item.itemName || '';
        const itemImage = item.itemImage || '';

        const li = document.createElement('li');
        li.className = 'product-box-contain';
        li.innerHTML = `
            <div class="drop-cart">
                <a href="/items/product/${itemId}" class="drop-image">
                    <img src="${itemImage}" class="blur-up lazyload" alt="${itemName}">
                </a>
                <div class="drop-contain">
                    <a href="/items/product/${itemId}">
                        <h5>${itemName}</h5>
                    </a>
                    <h6><span>${quantity} x</span> $${price.toFixed(2)}</h6>
                    <button class="close-button close_button" data-cart-item-id="${item.id}">
                        <i class="fa-solid fa-xmark"></i>
                    </button>
                </div>
            </div>
        `;
        cartList.appendChild(li);
        totalSum += price * quantity;
    });

    // Обновление общей суммы
    if (totalPriceElement) {
        totalPriceElement.textContent = `$${totalSum.toFixed(2)}`;
    }
    cartInfo.totalSum = totalSum;
}

// Функция показа/скрытия счетчика
function toggleCartUI(card, quantity) {
    const addToCartBtn = card.querySelector('.addtocart_btn');
    if (!addToCartBtn) return;

    const buyButton = addToCartBtn.querySelector('.buy-button');
    const counterBox = addToCartBtn.querySelector('.qty-box-2');
    const input = counterBox?.querySelector('.qty-input');

    if (buyButton && counterBox && input) {
        if (quantity > 0) {
            buyButton.classList.add('d-none');
            counterBox.classList.remove('d-none');
            input.value = quantity;
        } else {
            buyButton.classList.remove('d-none');
            counterBox.classList.add('d-none');
            input.value = 0;
        }
    }
}

// Основная функция инициализации
document.addEventListener('DOMContentLoaded', async function () {
    // Загрузка корзины при старте
    await loadCart();

    // Обработчики для кнопок
    document.body.addEventListener('click', async function(e) {
        // Добавление товара
        if (e.target.closest('.buy-button')) {
            const button = e.target.closest('.buy-button');
            const card = button.closest('.product-box');
            await addToCart(card.id);
        }

        // Увеличение количества
        if (e.target.closest('.qty-right-plus')) {
            const button = e.target.closest('.qty-right-plus');
            const input = button.closest('.input-group').querySelector('.qty-input');
            const card = button.closest('.product-box');
            await updateCartItem(card, parseInt(input.value) + 1);
        }

        // Уменьшение количества
        if (e.target.closest('.qty-left-minus')) {
            const button = e.target.closest('.qty-left-minus');
            const input = button.closest('.input-group').querySelector('.qty-input');
            const card = button.closest('.product-box');
            const newQty = parseInt(input.value) - 1;

            if (newQty > 0) {
                await updateCartItem(card, newQty);
            } else {
                await removeFromCart(card);
            }
        }

        // Удаление из корзины (крестик)
        if (e.target.closest('.close_button')) {
            const button = e.target.closest('.close_button');
            const cartItemId = button.dataset.cartItemId;
            await removeCartItemById(cartItemId);
        }
    });
});

// Загрузка корзины с сервера
async function loadCart() {
    try {
        const response = await fetch("/Buy/cart", {
            method: 'GET',
            credentials: "include"
        });

        if (response.ok) {
            const cartItems = await response.json();

            // Обновляем totalItems на основе qty
            cartInfo.totalItems = cartItems.reduce((total, item) => total + (item.qty || 0), 0);

            updateCartBadge();
            updateCartDropdown(cartItems);

            // Обновление UI для каждого товара
            cartItems.forEach(item => {
                const card = document.getElementById(item.itemId);
                if (card) {
                    // Сохраняем cartItemId в карточке
                    card.dataset.cartItemId = item.id;
                    toggleCartUI(card, item.qty || 0);
                }
            });
        }
    } catch (error) {
        console.error("Ошибка загрузки корзины:", error);
    }
}

// Добавление товара в корзину
async function addToCart(itemId) {
    try {
        const response = await fetch("/Buy/add", {
            method: 'POST',
            credentials: "include",
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ ItemId: itemId, Quantity: 1 })
        });

        if (response.ok) await loadCart();
    } catch (error) {
        console.error("Ошибка добавления товара:", error);
    }
}

// Обновление количества товара
async function updateCartItem(card, quantity) {
    try {
        const cartItemId = card.dataset.cartItemId;
        if (!cartItemId) {
            console.error("CartItemId not found for card:", card);
            return;
        }

        const response = await fetch(`/Buy/${cartItemId}`, {
            method: 'PUT',
            credentials: "include",
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ NewQuantity: quantity })
        });

        if (response.ok) await loadCart();
    } catch (error) {
        console.error("Ошибка обновления товара:", error);
    }
}

// Удаление товара из корзины по элементу карточки
async function removeFromCart(card) {
    try {
        const cartItemId = card.dataset.cartItemId;
        if (!cartItemId) {
            console.error("CartItemId not found for card:", card);
            return;
        }

        await removeCartItemById(cartItemId);
    } catch (error) {
        console.error("Ошибка удаления товара:", error);
    }
}

// Удаление товара по ID
async function removeCartItemById(cartItemId) {
    try {
        const response = await fetch(`/Buy/${cartItemId}`, {
            method: 'DELETE',
            credentials: "include"
        });

        if (response.ok) await loadCart();
    } catch (error) {
        console.error("Ошибка удаления товара:", error);
    }
}