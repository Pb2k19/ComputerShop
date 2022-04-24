using Microsoft.AspNetCore.Components;

namespace ComputerShop.Client.Shared.Components
{
    public partial class Carousel
    {
        [Parameter] public List<string> Images { get; set; } = new List<string>();
        private readonly List<string> slideCss = new();

        readonly CancellationTokenSource cts = new();
        CancellationToken ct;

        int currentPosition = 0;

        protected override void OnInitialized()
        {
            if(Images == null)
            {
                Images = new List<string>();
            }
            ct = cts.Token;
            if(Images.Count > 0)
            {
                ChangeCss(0);                
                Task.Run(() => AutoChange(ct));
            }
        }

        public async Task AutoChange(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                await Task.Delay(4000, ct);
                currentPosition++;
                ChangeSlide();
                await InvokeAsync(() => StateHasChanged());
            }
        }
        public void GoToNext(bool backwards)
        {
            if (!cts.IsCancellationRequested)
                cts.Cancel();
            if (backwards)
                currentPosition--;
            else
                currentPosition++;
            ChangeSlide();
        }
        private void GoToSelectedSlide(int slideNumber)
        {
            if(!cts.IsCancellationRequested)
                cts.Cancel();
            currentPosition = slideNumber;
            ChangeSlide();
        }
        private void ChangeSlide()
        {
            ChangeCss(Math.Abs(currentPosition % Images.Count));
        }
        private void ChangeCss(int activeSlide)
        {
            slideCss.Clear();
            for (int i = 0; i < Images.Count; i++)
            {
                slideCss.Add("");
            }
            slideCss[activeSlide] = "active";
        }
    }
}
