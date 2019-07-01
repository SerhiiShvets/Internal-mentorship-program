using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1Task3ToCoverWithUnitTests
{
    public class Order
    {
        public IEnumerable<string> ExtrasForAdding { get; set; }

        public string FoodToOrder { get; set; }

        private ILogger _logger;


        //While using EventHandler<FoodReadyEventArgs> event, as required,  we dont need to use this delegate
        //public delegate void EventHandler<FoodReadyEventArgs> (object sender, FoodReadyEventArgs e);

        public event EventHandler<FoodReadyEventArgs> FoodReady;

        protected virtual void OnFoodReady(FoodReadyEventArgs e)
        {
            EventHandler<FoodReadyEventArgs> handler = FoodReady;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnFoodReadyForTest(FoodReadyEventArgs e)
        {
            EventHandler<FoodReadyEventArgs> handler = FoodReady;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void NotifyReady(IFood food)
        {
            _logger.Write($"Order: Notifying observers of Order: [food={FoodToOrder}, extras=" + string.Join(" ", ExtrasForAdding) + "]");
            OnFoodReady(new FoodReadyEventArgs(food));
            _logger.Write("Order: Notification done");
        }
        public Order(string food, IEnumerable<string> extras, ILogger logger)
        {
            ExtrasForAdding = extras;
            FoodToOrder = food;
            _logger = logger;
        }
    }
}
