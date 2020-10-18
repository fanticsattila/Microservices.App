using EventBusRabbitMQ.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace EventBusRabbitMQ.Producers
{
    public class EventBusRabbitMQProducer
    {
        private readonly IRabbitMQConnection _connection;

        public EventBusRabbitMQProducer(IRabbitMQConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public void PublishBasketCheckout(string queueName, BasketCheckoutEvent publishModel)
        {
            using(IModel channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queueName, false, false, false, null);
                string message = JsonConvert.SerializeObject(publishModel);
                byte[] body = Encoding.UTF8.GetBytes(message);

                IBasicProperties properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                properties.DeliveryMode = 2;

                channel.ConfirmSelect();
                channel.BasicPublish("", queueName, true, properties, body);
                channel.WaitForConfirmsOrDie();

                channel.BasicAcks += (sender, eventArgs) =>
                 {
                     Console.WriteLine("Sent RabbitMQ");
                 };
                channel.ConfirmSelect();
            }
        }
    }
}
