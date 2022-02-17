using Microsoft.AspNetCore.Components;

namespace ComputerShop.Client.Shared.Components
{
    public partial class DropdownMenu<TItem>
    {
        [Parameter] public RenderFragment? InitTitle { get; set; }
        [Parameter] public EventCallback<int> OnSelected { get; set; }
        [Parameter] public List<TItem>? Items { get; set; }
        [Parameter] public bool ChangeTitleToSelectedValue { get; set; } = true;
        [Parameter] public string ButtonClassCss { get; set; } = "btn-primary";
        public bool IsMenuShown { get; set; } = false;
        public RenderFragment? Title { get; set; }

        public async Task HandleSelect(TItem item, RenderFragment<TItem> contentFragment)
        {
            if(Items == null)
                return;
            if(ChangeTitleToSelectedValue)
            {
                Title = contentFragment?.Invoke(item);
            }            
            IsMenuShown = false;
            StateHasChanged();
            int index = Items.IndexOf(item);
            await OnSelected.InvokeAsync(index);
        }

        protected override void OnInitialized()
        {
            Title = InitTitle;
            base.OnInitialized();
        }

        public void ChangeIsMenuShown()
        {
            IsMenuShown = !IsMenuShown;
        }

        public void OnFocusOut()
        {
            IsMenuShown = false;
            StateHasChanged();
        }
    }
}
