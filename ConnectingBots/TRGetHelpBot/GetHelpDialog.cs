using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TRGetHelpBot
{
    [LuisModel("253d1d82-9133-4915-bae5-f2c7952ab16a", "4a00615c199e49cfb8b13c4f72cdf965")]
    [Serializable]
    public class GetHelpDialog : LuisDialog<object>
    {

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Sorry I did not understand: " + string.Join(", ", result.Intents.Select(i => i.Intent));
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Questions")]
        public async Task DeleteAlarm(IDialogContext context, LuisResult result)
        {

            await context.PostAsync("Question");

            context.Wait(MessageReceived);
        }
        
        [LuisIntent("Greating")]
        public async Task FindAlarm(IDialogContext context, LuisResult result)
        {

            await context.PostAsync("Hello, how can I help you!");

            context.Wait(MessageReceived);
        }

        public GetHelpDialog()
        {
        }

        public GetHelpDialog(ILuisService service) : base(service)
        {
        }
        
    }
}
