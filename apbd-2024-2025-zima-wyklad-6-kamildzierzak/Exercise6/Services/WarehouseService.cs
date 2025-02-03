
using Exercise6.Repositories;

namespace Exercise6.Services;

public interface IWarehouseService
{
    Task<int> UpdateWarehouse(int idProduct, int idWarehouse, int amount, DateTime createdAt);
}

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductWarehouseRepository _productWarehouseRepository;

    public WarehouseService(IWarehouseRepository warehouseRepository, IProductRepository productRepository, IOrderRepository orderRepository, IProductWarehouseRepository productWarehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
        _productWarehouseRepository = productWarehouseRepository;
    }

    public async Task<int> UpdateWarehouse(int idProduct, int idWarehouse, int amount, DateTime createdAt)
    {

        // 1
        if (!await _productRepository.ProductExists(idProduct))
        {
            throw new Exception($"Product with id {idProduct} does not exist");
        }

        if (!await _warehouseRepository.WarehouseExists(idWarehouse))
        {
            throw new Exception($"Warehouse with id {idWarehouse} does not exist");
        }

        if (amount <= 0)
        {
            throw new Exception("Amount must be greater than 0");
        }
        //Console.WriteLine("Punkt 1 osiągniety");

        // 2
        var (idOrder, orderCreatedAt) = await _orderRepository.GetOrder(idProduct, amount);
        if (orderCreatedAt > createdAt)
        {
            throw new Exception("Order's creation date is later than the provided date.");
        }
        //Console.WriteLine("Punkt 2 osiągniety");

        // 3
        if (await _productWarehouseRepository.OrderExists(idOrder))
        {
            throw new Exception($"Order with id {idOrder} is already fullfilled");
        }
        //Console.WriteLine("Punkt 3 osiągniety");

        // 4
        await _orderRepository.FulfillOrder(idOrder);
        //Console.WriteLine("Punkt 4 osiągniety");

        // 5
        var productPrice = await _productRepository.GetProductPrice(idProduct);
        var totalPrice = productPrice * amount;

        var productWarehouseId = await _productWarehouseRepository.AddProductToWarehouse(idWarehouse, idProduct, idOrder, amount, totalPrice);
        //Console.WriteLine("Punkt 5 osiągniety");

        // 6
        return productWarehouseId;
    }
}
