﻿using Microsoft.Bot.Builder.Adapters.Webex;
using Microsoft.Bot.Builder.TraceExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EchoBot.Bots
{
    public class WebexAdapterWithErrorHandler : WebexAdapter
    {
        public WebexAdapterWithErrorHandler(IConfiguration configuration, ILogger logger)
            : base(configuration, logger) {
            OnTurnError = async (turnContext, exception) => {
                // Log any leaked exception from the application.
                logger.LogError(exception, $"[OnTurnError] unhandled error : {exception.Message}");

                // Send a message to the user
                await turnContext.SendActivityAsync("The bot encountered an error or bug.");
                await turnContext.SendActivityAsync("To continue to run this bot, please fix the bot source code.");

                // Send a trace activity, which will be displayed in the Bot Framework Emulator
                await turnContext.TraceActivityAsync("OnTurnError Trace", exception.Message, "https://www.botframework.com/schemas/error", "TurnError");
            };
        }
    }
}
