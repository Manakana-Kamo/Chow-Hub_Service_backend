using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Chow_Hub_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IChowHubService" in both code and config file together.
    [ServiceContract]
    public interface IChowHubService
    {
        [OperationContract]
        bool EditCredentials(int id, string Email, string password);

        [OperationContract]
        User ViewProfile(int id);

        [OperationContract]
        bool EditUser(User user);

        [OperationContract]
        User Login(string username, string hash_password);

        // Client-Side Methods
        [OperationContract]
        string TrackOrderStatus(int orderId);

        [OperationContract]
        string RegisterUser(User newUser);

        [OperationContract]
        string OrderDish(int userID, List<Order_Item> items, string Paymentmethod, string Address, DateTime date);

        [OperationContract]
        Menu_Item GetMenuItem(int id);

        [OperationContract]
        List<Menu_Item> GetMenuItems();

        [OperationContract]
        string Book_Reservation(Reservation reservation);

        [OperationContract]
        Reservation GetReservation(string email);

        [OperationContract]
        string Update_Reservation(Reservation reservation);

        [OperationContract]
        string CreateCart(int UserId);

        [OperationContract]
        string AddItemToCart(int OrderID, int Pid);

        [OperationContract]
        string IncreaseItem(int OrderID, int Pid);

        [OperationContract]
        string DecreaseItem(int OrderID, int Pid);

        [OperationContract]
        string RemoveFromCart(int OrderID, int Pid);

        [OperationContract]
        List<Order_Item> GetCartItems(int OrderID);


        [OperationContract]
        int GetCartId(int UserId);

        [OperationContract]
        int GetCartCount(int CartId);

        [OperationContract]
        User_Order GetInvoice(int InvID);

        [OperationContract]
        List<User_Order> GetInvoices(int userID);

        // Admin-Side Methods 
        [OperationContract]
        List<Menu_Item> GetAllMenuItems();

        [OperationContract]
        string RemoveAdmin(string Email);

        [OperationContract]
        int AddDish(Menu_Item newitem);

        [OperationContract]
        string EditDish(Menu_Item updatedItem);

        [OperationContract]
        List<User> ViewAllEmployees();

        [OperationContract]
        List<User> ViewAllClients();

        [OperationContract]
        List<User_Order> ViewOrders();

        [OperationContract]
        string AddReview(Review review);

        [OperationContract]
        bool ApplyDiscount(int userId, decimal discountPercentage);

        [OperationContract]
        bool UpdateOrderPreparationStatus(int orderId, string status);

        [OperationContract]
        bool UpdateDeliveryStatus(int orderId, string status);

        [OperationContract]
        List<User_Order> GenerateReport(DateTime startDate, DateTime endDate);

        [OperationContract]
        bool AssignTask(int orderId, int userId, string taskType);

        [OperationContract]
        bool UpdateReview(int reID, int itemId, string reviewText, int rating);

        [OperationContract]
        string UpdateActiveStatus(int Pid);

        [OperationContract]
        decimal ApplyTip(decimal totalAmount, decimal tipAmount);

        [OperationContract]
        User GetUser(string email);

        [OperationContract]
        int ChangeStatus(User user);


        [OperationContract]
        int AddPayment_And_Address(int id, string address, string payment);

        [OperationContract]
        Menu_Item GetItemInfo(int Id);

        [OperationContract]
        string GetCurrentPicture(int Pid);

        [OperationContract]
        string GetDashboardSummary();


        [OperationContract]
        decimal getTotalSales();

        [OperationContract]
        int getTotalNumberOfProducts();

        [OperationContract]
        int getTotalNumberOfUsers();


    }
}
