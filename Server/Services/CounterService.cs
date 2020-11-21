using System;
using System.Threading.Tasks;

using Grpc.Core;
using openpos.Shared.Proto;
using Counter = openpos.Client.Pages.Counter;

namespace openpos.Server.Services
{
    public class CounterService: Shared.Proto.Counter.CounterBase 
    {
        public override async Task StartCounter(CounterRequest request, IServerStreamWriter<CounterResponse> responseStream, ServerCallContext context)
        {
            var count = request.Start;

            while (!context.CancellationToken.IsCancellationRequested)
            {
                await responseStream.WriteAsync(new CounterResponse
                {
                    Count = ++count
                });

                await Task.Delay(TimeSpan.FromSeconds(1));
            }
            
        }
        
    }
}