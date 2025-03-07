using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;

namespace ServiceBusQueueEmailTrigger
{
    public static class Orchestrator
    {
        [Function(nameof(Orchestrator))]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] TaskOrchestrationContext context)
        {
            var outBoundMessage = new List<string>();
            string inboundMessage = context.GetInput<string>();

            // Replace name and input with values relevant for your Durable Functions Activity
            //More ActivityTrigger can be added here
            outBoundMessage.Add(await context.CallActivityAsync<string>(nameof(SendEmailActivity), inboundMessage));           
            return outBoundMessage;
        }

        [Function(nameof(SendEmailActivity))]
        public static string SendEmailActivity([ActivityTrigger] string inboundMessage, FunctionContext executionContext)
        {
            //Send email logic to be implemented here          
            return $"Email sent!";
        }

        //[Function("Function_HttpStart")]
        //public static async Task<HttpResponseData> HttpStart(
        //    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
        //    [DurableClient] DurableTaskClient client,
        //    FunctionContext executionContext)
        //{
        //    ILogger logger = executionContext.GetLogger("Function_HttpStart");

        //    // Function input comes from the request content.
        //    string instanceId = await client.ScheduleNewOrchestrationInstanceAsync(
        //        nameof(Orchestrator));

        //    logger.LogInformation("Started orchestration with ID = '{instanceId}'.", instanceId);

        //    // Returns an HTTP 202 response with an instance management payload.
        //    // See https://learn.microsoft.com/azure/azure-functions/durable/durable-functions-http-api#start-orchestration
        //    return client.CreateCheckStatusResponse(req, instanceId);
        //}
    }
}
