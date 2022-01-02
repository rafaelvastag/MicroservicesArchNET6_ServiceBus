using System;
using System.Threading.Tasks;

namespace MessageBus
{
    public interface IMessageBus
    {
        Task PublishMessage(BaseMessage message, string topic);
    }
}
