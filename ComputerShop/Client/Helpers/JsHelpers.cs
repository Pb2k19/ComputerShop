using Microsoft.JSInterop;

namespace ComputerShop.Client.Helpers
{
    public static class JsHelpers
    {
        public static async Task ScrollToElementAsync(this IJSRuntime js, string elementName)
        {
            await js.InvokeVoidAsync("goToElementById", elementName);
        }
    }
}
