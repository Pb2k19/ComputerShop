using ComputerShop.Server.Cryptography.Encryption;
using ComputerShop.Server.Services.KeyService;
using ComputerShop.Shared.Models.User;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Text;

namespace ComputerShop.Server.DataAccess;

public class EncryptedUserData : IUserData
{
    private readonly IMongoCollection<UserModel> users;
    private readonly IEncryption encryption;
    private readonly IKeyService keyService;

    public EncryptedUserData(IDbConnection dbConnection, IEncryption encryption, IKeyService keyService)
    {
        users = dbConnection.UserCollection;
        this.encryption = encryption;
        this.keyService = keyService;
    }

    public async Task<List<UserModel>> GetAllUsersAsync()
    {
        return (await users.FindAsync(_ => true)).ToList();
    }

    public async Task<UserModel> GetUserByIdAsync(string id)
    {
        UserModel user = (await users.FindAsync(x => x.Id.Equals(id))).FirstOrDefault();
        return DecryptUser(user);
    }

    public Task CreateUser(UserModel user)
    {
        if (user is RegisteredUser)
            return users.InsertOneAsync(EncryptUser((RegisteredUser)user));
        else
            return users.InsertOneAsync(EncryptUser(user));
    }

    public Task UpdateUserAsync(UserModel user)
    {
        UserModel encryptedUser;

        if (user is RegisteredUser)
            encryptedUser = EncryptUser((RegisteredUser)user);
        else
            encryptedUser = EncryptUser(user);

        FilterDefinition<UserModel>? filter = Builders<UserModel>.Filter.Eq(nameof(UserModel.Id), encryptedUser.Id);
        return users.ReplaceOneAsync(filter, encryptedUser, new ReplaceOptions { IsUpsert = true });
    }

    public async Task<OrderModel?> GetOrderAsync(string id)
    {
        var user = (await users.FindAsync(x => x.Orders.Any(o => o.Id.Equals(id)))).FirstOrDefault();
        if (user is null)
            return null;

        var order = user.Orders.FirstOrDefault(o => o.Id.Equals(id));
        if (order is null)
            return null;

        return DecryptOrder(order, keyService.GetDecryptionKey(encryption.KeyLengthBytes, user.Id, GetSalt(order.InvoiceDetails.City)).key);
    }

    public async Task<OrderModel?> GetOrderAsync(string orderId, string userId)
    {
        var order = (await GetUserByIdAsync(userId)).Orders.FirstOrDefault(o => o.Id.Equals(orderId));
        if (order is null)
            return null;

        return DecryptOrder(order, keyService.GetDecryptionKey(encryption.KeyLengthBytes, userId, GetSalt(order.InvoiceDetails.City)).key);
    }

    public async Task<OrderModel?> GetFirstUnpaidOrderAsync(string userId)
    {
        var order = (await GetUserByIdAsync(userId)).Orders.OrderByDescending(o => o.OrderDate).FirstOrDefault(o => o.State == OrderStates.Unpaid);
        if (order is null)
            return null;

        return DecryptOrder(order, keyService.GetDecryptionKey(encryption.KeyLengthBytes, userId, GetSalt(order.InvoiceDetails.City)).key);
    }

    public async Task UpdateOrderAsync(OrderModel order)
    {
        var user = (await GetAllUsersAsync()).FirstOrDefault(u => u.Orders.Any(o => o.Id.Equals(order.Id)));
        if (user is null)
            throw new Exception("Order not found");

        await UpdateOrderAsync(order, user.Id);
    }

    public async Task UpdateOrderAsync(OrderModel order, string userId)
    {
        var user = await GetUserByIdAsync(userId);
        if (user == null)
            throw new Exception("User not found");
        int index = user.Orders.IndexOf(user.Orders.FirstOrDefault(order => order.Id.Equals(order.Id)));
        if (index == -1)
            user.Orders.Add(order);
        else
            user.Orders[index] = order;
        await UpdateUserAsync(user);
    }

    public async Task<RegisteredUser?> GetRegisteredUserByEmailAsync(string email)
    {
        var user = (await users.FindAsync(x => x is RegisteredUser && x.Email.Equals(email))).FirstOrDefault() as RegisteredUser;
        if (user is null)
            return null;

        return DecryptUser(user);
    }

    public async Task<RegisteredUser?> GetAnyUserByEmailAsync(string email)
    {
        var user = (await users.FindAsync(x => x.Email.Equals(email))).FirstOrDefault() as RegisteredUser;
        if (user is null)
            return null;

        return DecryptUser(user);
    }

    public async Task<RegisteredUser?> GetRegisteredUserByIdAsync(string id)
    {
        var user = (await users.FindAsync(x => x is RegisteredUser && x.Id.Equals(id))).FirstOrDefault() as RegisteredUser;
        if (user is null)
            return null;

        return DecryptUser(user);
    }

    #region Encrypt / Decrypt
    private UserModel EncryptUser(UserModel user)
    {
        (byte[] key, byte[] salt) = keyService.GetEncryptionKey(encryption.KeyLengthBytes, user.Id);

        UserModel encryptedUser = new()
        {
            Id = user.Id,
            Email = user.Email,
            CreationDate = user.CreationDate,
            Orders = EncryptOrders(user.Orders, key, salt),
        };

        return encryptedUser;
    }

    private RegisteredUser EncryptUser(RegisteredUser user)
    {
        (byte[] key, byte[] salt) = keyService.GetEncryptionKey(encryption.KeyLengthBytes, user.Id);

        RegisteredUser encryptedUser = new()
        {
            CreationDate = user.CreationDate,
            WishList = user.WishList,
            Email = user.Email,
            Id = user.Id,
            Password = user.Password,
            Role = user.Role,
            Orders = EncryptOrders(user.Orders, key, salt),
            InvoiceDetails = EncryptInvoiceDetails(user.InvoiceDetails, key, salt),
            DeliveryDetails = EncryptDeliveryDetailsDetails(user.DeliveryDetails, key, salt),
        };

        return encryptedUser;
    }

    private List<OrderModel> EncryptOrders(List<OrderModel> orders, byte[] key, byte[] salt)
    {
        List<OrderModel> encryptedOrders = new();

        foreach (var order in orders)
        {
            encryptedOrders.Add(EncryptOrder(order, key, salt));
        }

        return encryptedOrders;
    }

    private OrderModel EncryptOrder(OrderModel order, byte[] key, byte[] salt)
    {
        return new()
        {
            Id = order.Id,
            CartItems = order.CartItems,
            DeliveryDetails = EncryptDeliveryDetailsDetails(order.DeliveryDetails, key, salt),
            InvoiceDetails = EncryptInvoiceDetails(order.InvoiceDetails, key, salt),
            OrderDate = order.OrderDate,
            State = order.State,
            Total = order.Total,
        };
    }

    private InvoiceDetails EncryptInvoiceDetails(InvoiceDetails invoiceDetails, byte[] key, byte[] salt)
    {
        return new()
        {
            Street = Encrypt(invoiceDetails.Street, key, salt),
            City = Encrypt(invoiceDetails.City, key, salt),
            Name = Encrypt(invoiceDetails.Name, key, salt),
            Nip = Encrypt(invoiceDetails.Nip, key, salt),
            IsBusiness = invoiceDetails.IsBusiness,
        };
    }

    private DeliveryDetails EncryptDeliveryDetailsDetails(DeliveryDetails deliveryDetails, byte[] key, byte[] salt)
    {
        return new()
        {
            Street = Encrypt(deliveryDetails.Street, key, salt),
            City = Encrypt(deliveryDetails.City, key, salt),
            Name = Encrypt(deliveryDetails.Name, key, salt),
            PhoneNumber = Encrypt(deliveryDetails.PhoneNumber, key, salt),
            HouseNumber = Encrypt(deliveryDetails.HouseNumber, key, salt),
            PostCode = Encrypt(deliveryDetails.PostCode, key, salt),
            Email = deliveryDetails.Email,
            DeliveryMethod = deliveryDetails.DeliveryMethod,
            PaymentMethod = deliveryDetails.PaymentMethod,
        };
    }

    private UserModel DecryptUser(UserModel user)
    {
        string? x = user.Orders.FirstOrDefault()?.DeliveryDetails.City;
        x ??= user.Orders.FirstOrDefault()?.InvoiceDetails.City;

        if (x is null)
            return user;

        byte[] key = keyService.GetDecryptionKey(encryption.KeyLengthBytes, user.Id, GetSalt(x)).key;

        UserModel decryptedUser = new()
        {
            Id = user.Id,
            Email = user.Email,
            CreationDate = user.CreationDate,
            Orders = DecryptOrders(user.Orders, key),
        };

        return decryptedUser;
    }

    private RegisteredUser DecryptUser(RegisteredUser user)
    {
        string? x = user.InvoiceDetails.City;
        x ??= user.DeliveryDetails.City;
        x ??= user.Orders.FirstOrDefault()?.DeliveryDetails.City;
        x ??= user.Orders.FirstOrDefault()?.InvoiceDetails.City;

        if (string.IsNullOrEmpty(x))
            return user;

        byte[] key = keyService.GetDecryptionKey(encryption.KeyLengthBytes, user.Id, GetSalt(x)).key;

        RegisteredUser decryptedUser = new()
        {
            CreationDate = user.CreationDate,
            WishList = user.WishList,
            Email = user.Email,
            Id = user.Id,
            Password = user.Password,
            Role = user.Role,
            Orders = DecryptOrders(user.Orders, key),
            InvoiceDetails = DecryptInvoiceDetails(user.InvoiceDetails, key),
            DeliveryDetails = DecryptDeliveryDetailsDetails(user.DeliveryDetails, key),
        };

        return decryptedUser;
    }

    private List<OrderModel> DecryptOrders(List<OrderModel> orders, byte[] key)
    {
        List<OrderModel> decryptedOrders = new();

        foreach (var order in orders)
        {
            decryptedOrders.Add(DecryptOrder(order, key));
        }

        return decryptedOrders;
    }

    private OrderModel DecryptOrder(OrderModel order, byte[] key)
    {
        return new()
        {
            Id = order.Id,
            CartItems = order.CartItems,
            DeliveryDetails = DecryptDeliveryDetailsDetails(order.DeliveryDetails, key),
            InvoiceDetails = DecryptInvoiceDetails(order.InvoiceDetails, key),
            OrderDate = order.OrderDate,
            State = order.State,
            Total = order.Total,
        };
    }

    private InvoiceDetails DecryptInvoiceDetails(InvoiceDetails invoiceDetails, byte[] key)
    {
        return new()
        {
            Street = Decrypt(invoiceDetails.Street, key),
            City = Decrypt(invoiceDetails.City, key),
            Name = Decrypt(invoiceDetails.Name, key),
            Nip = Decrypt(invoiceDetails.Nip, key),
            IsBusiness = invoiceDetails.IsBusiness,
        };
    }

    private DeliveryDetails DecryptDeliveryDetailsDetails(DeliveryDetails deliveryDetails, byte[] key)
    {
        return new()
        {
            Street = Decrypt(deliveryDetails.Street, key),
            City = Decrypt(deliveryDetails.City, key),
            Name = Decrypt(deliveryDetails.Name, key),
            PhoneNumber = Decrypt(deliveryDetails.PhoneNumber, key),
            HouseNumber = Decrypt(deliveryDetails.HouseNumber, key),
            PostCode = Decrypt(deliveryDetails.PostCode, key),
            Email = deliveryDetails.Email,
            DeliveryMethod = deliveryDetails.DeliveryMethod,
            PaymentMethod = deliveryDetails.PaymentMethod,
        };
    }

    private string Encrypt(string plainText, byte[] key, byte[]? salt)
    {
        string encrypted = encryption.Encrypt(Encoding.UTF8.GetBytes(plainText), key);

        if (salt is not null && salt.Length > 0)
            return $"{keyService.HashAlgorythmName}$${Base64UrlEncoder.Encode(salt)}$${encrypted}";

        return encrypted;
    }

    private byte[] GetSalt(string encrypted)
    {
        string[] splited = encrypted.Split("$$");

        if (splited.Length <= 1)
            return Array.Empty<byte>();

        return Base64UrlEncoder.DecodeBytes(splited[1]);
    }

    private string GetEncryptedPart(string encrypted)
    {
        string[] splited = encrypted.Split("$$");

        if (splited.Length <= 2)
            return string.Empty;

        return splited[2];
    }

    private string Decrypt(string encrypted, byte[] key)
    {
        string[] splited = encrypted.Split("$$");

        return Encoding.UTF8.GetString(encryption.Decrypt(GetEncryptedPart(splited.Length <= 2 ? splited[0] : splited[2]), key));
    }
    #endregion
}
