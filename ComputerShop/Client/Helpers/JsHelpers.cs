using Microsoft.JSInterop;

namespace ComputerShop.Client.Helpers
{
    public static class JsHelpers
    {
        public static async Task ScrollToElement(this IJSRuntime js, string elementName)
        {
            await js.InvokeVoidAsync("goToElementById", elementName);
        }
    }
}
