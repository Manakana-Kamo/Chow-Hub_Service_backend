using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Chow_Hub_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ChowHubService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ChowHubService.svc or ChowHubService.svc.cs at the Solution Explorer and start debugging.
    public class ChowHubService : IChowHubService
    {
        ChowHubDataContext data = new ChowHubDataContext();

        public int AddDish(Menu_Item newitem)
        {
            //Checking if product already exists
            var item = (from d in data.Menu_Items
                        where d.MI_Name.Equals(newitem.MI_Name) &&
d.MI_Description.Equals(newitem.MI_Description) &&
d.MI_Image.Equals(newitem.MI_Image)
                        select d).FirstOrDefault();

            if (item != null)
            {
                //item already exists!
                return 1;
            }
            else
            {

                data.Menu_Items.InsertOnSubmit(newitem);
                try
                {
                    // if dish is not null we return zero to show success
                    data.SubmitChanges();
                    return 0;
                }
                catch (Exception ex)
                {
                    ex.GetBaseException();
                    //else -1 to show that something went wrong
                    return -1;
                }
            }

        }

        public string EditDish(Menu_Item updatedItem)
        {
            // Find the item by its ID
            var item = (from i in data.Menu_Items where i.MI_ID.Equals(updatedItem.MI_ID) select i).FirstOrDefault();

            if (item == null) return "inf!"; // If not found, return an error message [inf - item not found]

            // Update item details
            item.MI_Name = updatedItem.MI_Name;
            item.MI_Description = updatedItem.MI_Description;
            item.MI_Type = updatedItem.MI_Type;
            item.MI_Price = updatedItem.MI_Price;
            item.MI_Image = updatedItem.MI_Image;
            //item.MI_Promo = updatedItem.MI_Promo;

            try
            {
                data.SubmitChanges(); // Save changes to the database
                return "updated!";
            }
            catch (Exception e)
            {
                e.GetBaseException();
                return "failed!";
            }
        }

        public string Book_Reservation(Reservation reservation)
        {
            //checking if the customer already has a booking for that day
            var res = (from r in data.Reservations
                       where r.ClientEmail.Equals(reservation.ClientEmail)
&& r.R_Date.Equals(reservation.R_Date)
                       select r).FirstOrDefault();

            //return a response if the client has a booking for the day
            if (res != null)
            {
                return "exists!";
            }
            else
            {
                //creating a new booking if there's none
                data.Reservations.InsertOnSubmit(reservation);
                try
                {
                    //creating a new booking if there's none
                    data.SubmitChanges();
                    return "booked!";
                }
                catch (Exception ex)
                {
                    //reponse upon failed booking!
                    ex.GetBaseException();
                    return "failed!";
                }
            }
        }

        public Menu_Item GetMenuItem(int id)
        {
            //checking for an item with that Id
            var item = (from i in data.Menu_Items where i.MI_ID.Equals(id) select i).FirstOrDefault();

            if (item != null)
            {
                //Temporary item
                Menu_Item menu_item = new Menu_Item
                {
                    MI_ID = item.MI_ID,
                    MI_Name = item.MI_Name,
                    MI_Description = item.MI_Description,
                    MI_Image = item.MI_Image,
                    MI_Price = item.MI_Price
                };

                //returning temporary item
                return menu_item;
            }
            else { return null; }
        }

        public List<Menu_Item> GetMenuItems()
        {
            //pulling all active menu item
            dynamic items = (from mi in data.Menu_Items
                             where mi.MI_Availability.Equals(1)
                             select mi).DefaultIfEmpty();

            if (items != null)
            {
                List<Menu_Item> list = new List<Menu_Item>();  //creating a temp list

                foreach (Menu_Item mi in items)     //storing items in a temp list
                {
                    Menu_Item Tempitem = new Menu_Item
                    {
                        MI_ID = mi.MI_ID,
                        MI_Name = mi.MI_Name,
                        MI_Description = mi.MI_Description,
                        MI_Price = mi.MI_Price,
                        MI_Image = mi.MI_Image,
                        MI_Type = mi.MI_Type

                    };

                    list.Add(Tempitem);
                }

                //returning the data
                return list;
            }
            else { return null; }
        }

        public List<Menu_Item> GetAllMenuItems()
        {
            //pulling all active menu item
            dynamic items = (from mi in data.Menu_Items
                             select mi).DefaultIfEmpty();

            if (items != null)
            {
                List<Menu_Item> list = new List<Menu_Item>();  //creating a temp list

                foreach (Menu_Item i in items)     //storing items in a temp list
                {
                    if (i != null)
                    {
                        Menu_Item tempItem = new Menu_Item
                        {
                            MI_ID = i.MI_ID,
                            MI_Name = i.MI_Name,
                            MI_Description = i.MI_Description,
                            MI_Price = i.MI_Price,
                            MI_Image = i.MI_Image,
                            MI_Type = i.MI_Type,
                            MI_Availability = i.MI_Availability
                        };
                        list.Add(tempItem);
                    }
                    else { return null; }

                }

                //returning the data
                return list;
            }
            else
            {
                return null;
            }
        }

        public Reservation GetReservation(string email)
        {
            //retrieving a reservation for the given email
            var resv = (from r in data.Reservations
                        where r.ClientEmail.Equals(email)
                        select r).FirstOrDefault();

            if (resv != null)
            {
                Reservation reservation = new Reservation
                {
                    ClientEmail = resv.ClientEmail,
                    ClientPhone = resv.ClientPhone,
                    ClientNames = resv.ClientNames,
                    R_Date = resv.R_Date,
                    R_Time = resv.R_Time,
                    R_Persons = resv.R_Persons
                };

                //returning a temp reservation if(resv != null)
                return reservation;
            }
            else { return null; }
        }

        public User Login(string username, string hash_password)
        {
            //searching a user that matches the user credentials
            var user = (from u in data.Users
                        where u.U_Email.Equals(username) && u.U_Password.Equals(hash_password)
                        select u).FirstOrDefault();

            if (user != null)
            {
                //returning user data if user is found
                var tempUser = new User
                {
                    U_ID = user.U_ID,
                    U_Title = user.U_Title,
                    U_LastName = user.U_LastName,
                    U_Address = user.U_Address,
                    U_Email = user.U_Email,
                    U_PhoneNumber = user.U_PhoneNumber,
                    U_Type = user.U_Type

                };

                return tempUser;
            }
            else
            {
                return null;
            }
        }

        public string RegisterUser(User newUser)
        {
            //checking if user already exists!
            var user = (from u in data.Users
                        where u.U_Email.Equals(newUser.U_Email)
                        select u).FirstOrDefault();

            if (user != null)
            {
                return "exists!";       // response for an already existing user
            }
            else
            {
                data.Users.InsertOnSubmit(newUser);
                try
                {
                    data.SubmitChanges();
                    return "registered!";       //response upon successful registration.
                }
                catch (Exception ex)
                {
                    ex.GetBaseException();
                    return "failed!";       //response upon failed registration.
                }
            }
        }

        public string Update_Reservation(Reservation reservation)
        {
            var _reservation = (from res in data.Reservations
                                where res.ClientEmail.Equals(reservation.ClientEmail)
                                select res).FirstOrDefault();

            if (_reservation != null)
            {
                _reservation.ClientEmail = reservation.ClientEmail;
                _reservation.ClientPhone = reservation.ClientPhone;
                _reservation.ClientNames = reservation.ClientNames;
                _reservation.R_Date = reservation.R_Date;
                _reservation.R_Time = reservation.R_Time;
                _reservation.R_Persons = reservation.R_Persons;

                try
                {
                    data.SubmitChanges();
                    return "updated!";
                }
                catch (Exception ex)
                {
                    ex.GetBaseException();
                    return "failed!";
                }

            }
            else
            {
                return "rnf!"; //What does this mean "rnf!"?
            }
        }

        public string AddReview(Review review)
        {
            // Adds a review for a menu item
            data.Reviews.InsertOnSubmit(review);
            try
            {
                data.SubmitChanges(); // Save changes to the database
                return "Rating added successfully.";
            }
            catch (Exception ex)
            {
                ex.GetBaseException();
                return "Rating Failed!";
            }
        }

        public decimal ApplyDiscount(decimal totalAmount, decimal discountPercentage)
        {
            return totalAmount - (totalAmount * (discountPercentage / 100)); // Calculate the discounted amount
        }

        public decimal ApplyTip(decimal totalAmount, decimal tipAmount)
        {
            return totalAmount + tipAmount; // Add the tip amount to the total
        }

        public bool EditCredentials(int id, string Email, string password)
        {
            var eu = (from u in data.Users where u.U_ID.Equals(id) || u.U_Email.Equals(Email) select u).FirstOrDefault();
            if (eu != null)
            {
                eu.U_Email = Email;
                eu.U_Password = password;
                try
                {
                    data.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    ex.GetBaseException();
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public User ViewProfile(int id)
        {
            var cu = (from u in data.Users where u.U_ID.Equals(id) select u).FirstOrDefault();
            if (cu != null)
            {
                var tempuser = new User
                {
                    U_Name = cu.U_Name,
                    U_LastName = cu.U_LastName,
                    U_Email = cu.U_Email,
                    U_Type = cu.U_Type,
                    U_Address = cu.U_Address,
                    U_PhoneNumber = cu.U_PhoneNumber,
                };
                return tempuser;
            }
            else
            {
                return null;
            }
        }

        public bool EditUser(User user)
        {
            var cu = (from u in data.Users where u.U_ID.Equals(user.U_ID) select u).FirstOrDefault();
            if (cu != null)
            {
                cu.U_Address = user.U_Address;
                cu.U_Email = user.U_Email;
                cu.U_PhoneNumber = user.U_PhoneNumber;
                cu.U_Name = user.U_Name;
                cu.U_LastName = user.U_LastName;
                cu.U_Title = user.U_Title;

                try
                {
                    data.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    ex.GetBaseException();
                    return false;
                }

            }
            else
            {
                var result = RegisterUser(user);
                return true;
            }

        }
        public string OrderDish(int userID, List<Order_Item> items, string Paymentmethod, string Address, DateTime date)
        {
            decimal totalAmount = 0;
            foreach (Order_Item i in items)
            {
                totalAmount += (i.Price * i.Quantity);// Calculate the total amount
            };
            // Create a new order object
            var order = new User_Order
            {
                U_ID = userID,
                OrderDate = date,
                Status = "Processing",
                DeliveryAddress = Address,

                PaymentDetails = Paymentmethod,
                //TotalAmount = totalAmount
            };
            // Insert the order and its items into the database
            try
            {
                data.User_Orders.InsertOnSubmit(order);
                data.Order_Items.InsertAllOnSubmit(items);
                data.SubmitChanges();
                return "Order placed successfully.";
            }
            catch (Exception e)
            {
                e.GetBaseException();
                return "Failed";
            }
        }

        public string RemoveAdmin(string Email)
        {
            // Find the admin user by their ID
            var admin = (from a in data.Users where a.U_Email.Equals(Email) select a).FirstOrDefault();
            if (admin == null) return "Admin not found."; // If not found, return an error message

            try
            {
                admin.U_Type = "customer";
                data.SubmitChanges();// Save changes to the database
                return "Admin access removed successfully.";
            }
            catch (Exception e)
            {
                e.GetBaseException();
                return "Failed to removed";
            }
        }

        public List<User_Order> GenerateReport(DateTime startDate, DateTime endDate)
        {
            //pulling all reports User Orders
            dynamic userOrder = (from uO in data.User_Orders
                                 where uO.OrderDate >= startDate && uO.OrderDate <= endDate
                                 select uO).DefaultIfEmpty();

            if (userOrder != null)
            {
                List<User_Order> Orderlist = new List<User_Order>();  //creating a temp list

                foreach (User_Order u in userOrder)     //storing User Orders in a temp list
                {

                    User_Order tempOrder = new User_Order //filtering the data to display
                    {
                        OrderId = u.OrderId,
                        U_ID = u.U_ID,
                        OrderDate = u.OrderDate,
                        Status = u.Status,
                        Order_Items = u.Order_Items,
                        PaymentDetails = u.PaymentDetails

                    };

                    Orderlist.Add(tempOrder);
                }

                //returning the data
                return Orderlist;
            }
            else
            {
                return null;
            }

        }

        // Chef updates order preparation status
        public bool UpdateOrderPreparationStatus(int orderId, string status)
        {
            var order = data.User_Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order != null && order.Status == "Preparing")
            {
                order.Status = status;
                data.SubmitChanges();
                return true;
            }
            return false;
        }

        // Update a product review
        public bool UpdateReview(int reID, int itemId, string reviewText, int rating)
        {
            var review = data.Reviews.FirstOrDefault(r => r.R_ID == reID && r.MI_ID == itemId);
            if (review == null)
            {
                review = new Review
                {
                    R_ID = reID,
                    MI_ID = itemId,
                    R_Comment = reviewText,
                    R_Value = Convert.ToByte(rating) // R_value is type byte
                };
            }
            else
            {
                review.R_Comment = reviewText;
                review.R_Value = Convert.ToByte(rating);
            }
            data.SubmitChanges();
            return true;
        }

        // View all users (Chef, Driver, Manager)
        public List<User> ViewAllEmployees()
        {
            dynamic user = (from u in data.Users where u.U_Type != "customer" select u).DefaultIfEmpty();
            if (user != null)
            {
                List<User> userlist = new List<User>();
                foreach (User i in user)
                {
                    userlist.Add(i);
                }
                return userlist;
            }
            else
            {
                return null;
            }
        }

        // View all clients
        public List<User> ViewAllClients()
        {
            dynamic user = (from u in data.Users where u.U_Type.Equals("customer") select u).DefaultIfEmpty();
            if (user != null)
            {
                List<User> clientList = new List<User>();
                foreach (User i in user)
                {
                    clientList.Add(i);
                }
                return clientList;
            }
            else
            {
                return null;
            }

        }

        // View all orders
        public List<User_Order> ViewOrders()
        {
            dynamic user = (from o in data.User_Orders select o).DefaultIfEmpty();
            if (user != null)
            {
                List<User_Order> orders = new List<User_Order>();
                foreach (User_Order i in orders)
                {
                    orders.Add(i);
                }
                return orders;
            }
            else
            {
                return null;
            }
        }

        public string TrackOrderStatus(int orderId)
        {
            var order = (from o in data.User_Orders where o.OrderId.Equals(orderId) select o).FirstOrDefault();
            return order != null ? order.Status : "Order not found";
        }

        // Assign tasks (Chef handles preparation, Driver handles delivery)
        public bool AssignTask(int orderId, int userId, string taskType)
        {
            var order = (from o in data.User_Orders where o.OrderId.Equals(orderId) select o).FirstOrDefault();
            if (order != null)
            {
                if (taskType == "Preparation" && data.Users.Any(u => u.U_ID.Equals(userId) && u.U_Type == "Chef"))
                {
                    order.Status = "Preparing";
                    data.SubmitChanges();
                    return true;
                }
                else if (taskType == "Delivery" && data.Users.Any(u => u.U_ID.Equals(userId) && u.U_Type == "Driver"))
                {
                    order.Status = "Out for Delivery";
                    data.SubmitChanges();
                    return true;
                }
            }
            return false;
        }

        public bool UpdateDeliveryStatus(int orderId, string status)
        {
            var order = (from i in data.User_Orders where i.OrderId.Equals(orderId) select i).FirstOrDefault();
            if (order != null && order.Status == "Out for Delivery")
            {
                order.Status = status;
                data.SubmitChanges();
                return true;
            }
            return false;
        }

        // Apply discount if an item is on special
        public bool ApplyDiscount(int itemId, decimal discountPercentage)
        {
            var item = (from i in data.Menu_Items where i.MI_ID.Equals(itemId) select i).FirstOrDefault();
            if (item != null)
            {
                item.MI_Price -= (item.MI_Price * (discountPercentage / 100));
                data.SubmitChanges();
                return true;
            }
            return false;
        }

        /*
         * Update the active status of a product
         *  -> Removes and adds an Item  to the Catalog
         */
        public string UpdateActiveStatus(int Pid)
        {
            string rVal = "";
            var mi = (from i in data.Menu_Items where i.MI_ID.Equals(Pid) select i).FirstOrDefault();

            if (mi != null)
            {
                if (mi.MI_Availability == 1)
                {
                    mi.MI_Availability = 0;
                }
                else
                {
                    mi.MI_Availability = 1;
                }

                try
                {
                    data.SubmitChanges();
                    rVal = "updated!";
                }
                catch (Exception ex)
                {
                    ex.GetBaseException();
                    rVal = "failed!";
                }
            }
            else { rVal = "inf!"; }

            return rVal;
        }

        /*
         * Create a Shopping Cart For The User
         *  -> To allow them to add products or remove (Purchase them as well)
         */
        public string CreateCart(int UserId)
        {

            string rVal = null;

            var cart = (from c in data.User_Orders
                        where c.U_ID.Equals(UserId)
&& c.Status.Equals("active")
                        select c).FirstOrDefault();

            if (cart == null)
            {
                User_Order newCart = new User_Order
                {
                    U_ID = UserId,
                    Status = "active"
                };

                data.User_Orders.InsertOnSubmit(newCart);
                try
                {
                    data.SubmitChanges();
                    rVal = "created!";
                }
                catch (Exception ex)
                {
                    ex.GetBaseException();
                    rVal = "failed";
                }
            }
            else
            {
                rVal = "exists!";
            }

            return rVal;
        }

        //Adds a product to the shopping cart
        public string AddItemToCart(int OrderID, int Pid)
        {
            string rVal = "";
            //checking if item exixts in database
            var cartitem = (from ci in data.Order_Items
                            where ci.OrderId.Equals(OrderID)
&& ci.MI_ID.Equals(Pid)
                            select ci).FirstOrDefault();

            if (cartitem != null)
            {
                //increasing the quantity by 1 if item exists                
                rVal = IncreaseItem(OrderID, Pid);
            }
            else
            {
                Order_Item newCI = new Order_Item
                {
                    OrderId = OrderID,
                    MI_ID = Pid,
                    Item_Name = GetItemName(Pid),
                    Price = GetItemPrice(Pid),
                    Quantity = 1,
                    Img = GetImage(Pid)
                };

                data.Order_Items.InsertOnSubmit(newCI);
                try
                {
                    data.SubmitChanges();
                    rVal = "done!";
                }
                catch (Exception ex)
                {
                    ex.GetBaseException();
                    rVal = "failed!";
                }
            }
            return rVal;
        }


        //Remove Item From Cart
        public string RemoveFromCart(int OrderID, int Pid)
        {
            string rVal = "";
            //checking if item exixts in database
            var cartitem = (from ci in data.Order_Items
                            where ci.OrderId.Equals(OrderID)
&& ci.MI_ID.Equals(Pid)
                            select ci).FirstOrDefault();

            if (cartitem != null)
            {
                data.Order_Items.DeleteOnSubmit(cartitem);
                try
                {
                    data.SubmitChanges();
                    rVal = "done!";
                }
                catch (Exception ex)
                {
                    ex.GetBaseException();
                    rVal = "failed!";
                }
            }
            else
            {
                rVal = "sww!"; //[sw - something went wrong!]
            }
            return rVal;
        }

        //This increases the quantity of product by 1
        public string IncreaseItem(int OrderID, int Pid)
        {
            string rVal = "";
            //checking if item exixts in database
            var cartitem = (from ci in data.Order_Items
                            where ci.OrderId.Equals(OrderID)
&& ci.MI_ID.Equals(Pid)
                            select ci).FirstOrDefault();

            if (cartitem != null)
            {
                cartitem.Quantity += 1;

                try
                {
                    data.SubmitChanges();
                    rVal = "done!";
                }
                catch (Exception ex)
                {
                    ex.GetBaseException();
                    rVal = "failed!";
                }
            }
            else
            {
                rVal = "sww!"; //[sw - something went wrong!]
            }
            return rVal;
        }

        //decreases the number of cart items by one
        public string DecreaseItem(int OrderID, int Pid)
        {
            string rVal = "";
            //checking if item exixts in database
            var cartitem = (from ci in data.Order_Items
                            where ci.OrderId.Equals(OrderID)
&& ci.MI_ID.Equals(Pid)
                            select ci).FirstOrDefault();

            if (cartitem != null)
            {
                if (cartitem.Quantity > 1)
                {
                    cartitem.Quantity--;
                    try
                    {
                        data.SubmitChanges();
                        rVal = "done!";
                    }
                    catch (Exception ex)
                    {
                        ex.GetBaseException();
                        rVal = "failed!";
                    }
                }
                else if (cartitem.Quantity == 1)
                {
                    rVal = RemoveFromCart(OrderID, Pid);
                }
            }
            else
            {
                rVal = "sww!"; //[sw - something went wrong!]
            }

            return rVal;
        }

        private string GetItemName(int Pid)
        {
            string rVal = "No Name";

            var item = (from i in data.Menu_Items where i.MI_ID.Equals(Pid) select i).FirstOrDefault();

            if (item != null)
            {
                rVal = item.MI_Name;
            }

            return rVal;
        }

        private string GetImage(int Pid)
        {
            string rVal = "No Image";

            var item = (from i in data.Menu_Items where i.MI_ID.Equals(Pid) select i).FirstOrDefault();

            if (item != null)
            {
                rVal = item.MI_Image;
            }

            return rVal;
        }

        private decimal GetItemPrice(int Pid)
        {
            decimal rVal = 0;

            var item = (from i in data.Menu_Items where i.MI_ID.Equals(Pid) select i).FirstOrDefault();

            if (item != null)
            {
                rVal = item.MI_Price;
            }

            return rVal;
        }

        /*
         * Checks if the user has items in their cart &
         * Returns A list of items (if the cart is not empty)
         */
        public List<Order_Item> GetCartItems(int OrderID)
        {
            dynamic list = (from citem in data.Order_Items
                            where
citem.OrderId.Equals(OrderID)
                            select citem).DefaultIfEmpty();

            List<Order_Item> tempList = new List<Order_Item>();

            if (list != null)
            {
                foreach (Order_Item item in list)
                {
                    if (item != null)
                    {
                        Order_Item tItem = new Order_Item
                        {
                            MI_ID = item.MI_ID,
                            Item_Name = item.Item_Name,
                            Quantity = item.Quantity,
                            Price = item.Price,
                            Img = item.Img
                        };

                        tempList.Add(tItem);
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            return tempList;
        }

        //returns the cartID for a user (if the user has an active cart)
        public int GetCartId(int UserId)
        {
            int value = -1;

            var cart = (from c in data.User_Orders
                        where c.U_ID.Equals(UserId)
&& c.Status.Equals("active")
                        select c).FirstOrDefault();

            if (cart != null)
            {
                value = cart.OrderId;
            }

            return value;
        }

        public User_Order GetInvoice(int InvID)
        {
            User_Order tempOrder = null;

            var invoice = (from i in data.User_Orders
                           where i.OrderId.Equals(InvID) &&
i.Status.Equals("purchased")
                           select i).FirstOrDefault();

            if (invoice != null)
            {
                tempOrder = new User_Order
                {
                    OrderId = invoice.OrderId,
                    OrderDate = invoice.OrderDate,
                    Progress = invoice.Progress,
                    TotalAmount = invoice.TotalAmount,
                    DeliveryAddress = invoice.DeliveryAddress
                };
            }

            return tempOrder;
        }

        public List<User_Order> GetInvoices(int userID)
        {
            List<User_Order> invoices = new List<User_Order>();

            dynamic list = (from i in data.User_Orders
                            where i.U_ID.Equals(userID) &&
i.Status.Equals("purchased")
                            select i).DefaultIfEmpty();

            if (list != null)
            {
                foreach (User_Order invoice in list)
                {
                    if (invoice != null)
                    {
                        User_Order tInvoice = new User_Order
                        {
                            OrderId = invoice.OrderId,
                            OrderDate = invoice.OrderDate,
                            Progress = invoice.Progress,
                            TotalAmount = invoice.TotalAmount,
                        };

                        invoices.Add(tInvoice);
                    }
                    else
                    {
                        invoices = null;
                    }
                }
            }
            else
            {
                invoices = null;
            }

            return invoices;
        }

        public int GetCartCount(int CartId)
        {
            int count = 0;

            dynamic citems = (from ci in data.Order_Items
                              where ci.OrderId.Equals(CartId)
                              select ci).DefaultIfEmpty();

            if (citems != null)
            {
                foreach (Order_Item item in citems)
                {
                    if (item != null)
                    {
                        count += item.Quantity;
                    }
                }
            }

            return count;
        }

        //for getting user when changing status
        public User GetUser(string email)
        {
            var user = (from u in data.Users where u.U_Email.Equals(email) select u).FirstOrDefault();

            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public int ChangeStatus(User user)
        {
            var existing = (from u in data.Users where u.U_Email.Equals(user.U_Email) && u.U_ID.Equals(user.U_ID) select u).FirstOrDefault();

            if (existing == null)
            {
                //could not find user
                return 1;
            }
            else
            {
                existing.U_Type = user.U_Type;
                try
                {

                    data.SubmitChanges();
                    //succeful
                    return 0;
                }
                catch (Exception ex)
                {

                    ex.GetBaseException();
                    //something went wrong
                    return -1;
                }
            }
        }
        private decimal GetCartTotal(int cartid)
        {
            decimal total = 0;
            dynamic items = GetCartItems(cartid);

            if (items != null)
            {
                foreach (Order_Item item in items)
                {
                    total += item.Quantity * item.Price;
                }
            }

            return total;
        }

        public int AddPayment_And_Address(int id, string address, string payment)
        {
            var cart = (from c in data.User_Orders where c.OrderId == id select c).FirstOrDefault();
            if (cart != null)
            {
                cart.PaymentDetails = payment;
                cart.DeliveryAddress = address;
                cart.Status = "purchased";
                cart.TotalAmount = GetCartTotal(id);
                cart.OrderDate = DateTime.Now;

                if (cart.TotalAmount > 300)
                {
                    cart.DeliveryFee = 45;
                }

                try
                {
                    data.SubmitChanges();
                    return 0;
                }
                catch (Exception ex)
                {
                    ex.GetBaseException();
                    return -1;
                }

            }
            else
            {
                return 1;
            }
        }

        public Menu_Item GetItemInfo(int Id)
        {
            var item = (from i in data.Menu_Items
                        where i.MI_ID.Equals(Id)
                        select i).FirstOrDefault();

            if (item != null)
            {
                Menu_Item tempItem = new Menu_Item()
                {
                    MI_ID = item.MI_ID,
                    MI_Name = item.MI_Name,
                    MI_Description = item.MI_Description,
                    MI_Image = item.MI_Image,
                    MI_Price = item.MI_Price,
                    MI_Type = item.MI_Type,
                    MI_Promo = item.MI_Promo
                };

                return tempItem;
            }

            return null;
        }

        public string GetCurrentPicture(int Pid)
        {
            string value = "No Picture";

            var item = (from i in data.Menu_Items
                        where
i.MI_ID.Equals(Pid)
                        select i).FirstOrDefault();

            if (item != null)
            {
                value = item.MI_Image;
            }

            return value;
        }

        public decimal getTotalSales()
        {
            decimal sales = data.Order_Items.Sum(o => o.Price);
            return sales;
        }

        public int getTotalNumberOfUsers()
        {
            int totalUsers = data.Users.Count();
            return totalUsers;
        }

        public int getTotalNumberOfProducts()
        {
            int totalNumberOfUsers = data.Menu_Items.Count();
            return totalNumberOfUsers;
        }

        public string GetDashboardSummary()
        {
            StringBuilder html = new StringBuilder();

            html.Append("<div class='flex flex-1 flex-col md:flex-row lg:flex-row mx-2'>");

            // Total Sales Section
            html.Append("<div class='shadow-lg bg-red-vibrant border-l-8 hover:bg-red-vibrant-dark border-red-vibrant-dark mb-2 p-2 md:w-1/4 mx-2'>");
            html.Append("<div class='p-4 flex flex-col'>");
            html.AppendFormat("<a href=\"#\" class=\"no-underline text-white text-2xl\">R\"" + getTotalSales() + "\"</a>\"");
            html.Append("<a href='#' class='no-underline text-white text-lg'>Total Sales</a>");
            html.Append("</div>");
            html.Append("</div>");

            // Total Cost Section
            html.Append("<div class='shadow bg-info border-l-8 hover:bg-info-dark border-info-dark mb-2 p-2 md:w-1/4 mx-2'>");
            html.Append("<div class='p-4 flex flex-col'>");
            html.Append("<a href='#' class='no-underline text-white text-2xl'>R 10000</a>");
            html.Append("<a href='#' class='no-underline text-white text-lg'>Total Cost</a>");
            html.Append("</div>");
            html.Append("</div>");

            // Total Users Section
            html.Append("<div class='shadow bg-warning border-l-8 hover:bg-warning-dark border-warning-dark mb-2 p-2 md:w-1/4 mx-2'>");
            html.Append("<div class='p-4 flex flex-col'>");
            html.Append("<a href='#' class='no-underline text-white text-2xl'>\"" + getTotalNumberOfUsers() + "\"</a>");
            html.Append("<a href='#' class='no-underline text-white text-lg'>Total Users</a>");
            html.Append("</div>");
            html.Append("</div>");

            // Total Products Section
            html.Append("<div class='shadow bg-success border-l-8 hover:bg-success-dark border-success-dark mb-2 p-2 md:w-1/4 mx-2'>");
            html.Append("<div class='p-4 flex flex-col'>");
            html.Append("<a href='#' class='no-underline text-white text-2xl'>\"" + getTotalNumberOfProducts() + "\"</a>");
            html.Append("<a href='#' class='no-underline text-white text-lg'>Total Products</a>");
            html.Append("</div>");
            html.Append("</div>");

            html.Append("</div>");

            return html.ToString();
        }


    }
}
