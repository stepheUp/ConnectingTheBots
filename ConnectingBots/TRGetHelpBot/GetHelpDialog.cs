using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TRGetHelpBot
{
    [LuisModel("253d1d82-9133-4915-bae5-f2c7952ab16a", "4a00615c199e49cfb8b13c4f72cdf965")]
    [Serializable]
    public class GetHelpDialog : LuisDialog<object>
    {
        IDialogContext CurrentContext;



        protected override async Task MessageReceived(IDialogContext context, IAwaitable<IMessageActivity> item)
        {
         //   CurrentContext = context;
            await base.MessageReceived(context, item);
        }


        private void StartAssitJob()
        {
            try
            {
                var timer = new System.Timers.Timer();
                timer.Interval = 1000;
              //  timer.Elapsed += new System.Timers.ElapsedEventHandler(GetAssitMessage);
                timer.Enabled = true;
                // If AutoReset=false then the timer will only tick once
                timer.AutoReset = true;
                timer.Start();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        //private async void GetAssitMessage(object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    try
        //    {
        //        await CurrentContext.PostAsync("ping");
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}




        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            //string message = $"Sorry I did not understand: " + string.Join(", ", result.Intents.Select(i => i.Intent));
            //await context.PostAsync(message);
            //context.Wait(MessageReceived);

            PromptDialog.Confirm(context, AfterConfirming_StartAssist, "Sorry, I am not able to help you on this subject. Do you want to me to contact a human for you?", promptStyle: PromptStyle.Inline);

        }

        public async Task AfterConfirming_StartAssist(IDialogContext context, IAwaitable<bool> confirmation)

        {
            try
            {
                if (await confirmation)
                {
                    await context.PostAsync($"Ok, I call some body.");
                    //                CacheContext = context as Microsoft.Bot.Builder.Dialogs.Internals.DialogContext;

                    StartAssitJob();
                }
                else
                {
                    await context.PostAsync("Ok! no assist!");
                }

                context.Wait(MessageReceived);
            }
            catch (Exception e)
            {

                throw e;
            }




        }



        [LuisIntent("Questions")]
        public async Task DeleteAlarm(IDialogContext context, LuisResult result)
        {

            await context.PostAsync("Question");



            context.Wait(MessageReceived);
            return;
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
