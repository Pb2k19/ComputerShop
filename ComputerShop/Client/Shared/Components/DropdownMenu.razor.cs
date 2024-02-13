using Microsoft.AspNetCore.Components;

namespace ComputerShop.Client.Shared.Components
{
    public partial class DropdownMenu<TItem>
    {
        private TItem? value;
        [Parameter] public RenderFragment? InitTitle { get; set; }
        [Parameter] public EventCallback<int> OnSelected { get; set; }
        [Parameter] public EventCallback OnTitleClick { get; set; }
        [Parameter] public IList<TItem>? Items { get; set; }
        [Parameter] public bool ChangeTitleToSelectedValue { get; set; } = true;
        [Parameter] public string ButtonClassCss { get; set; } = "btn-primary";
        [Parameter] public string? Icon { get; set; } = null;
        [Parameter] public string MenuMarginLeft { get; set; } = "0";
        [Parameter] public bool OnMouseOverEnabled { get; set; } = true;
        [Parameter] public TItem? Value
        {
            get => value;
            set
            {
                if (this.value?.Equals(value) ?? false) 
                    return;
                this.value = value;
                ValueChanged.InvokeAsync(this.value);
            }
        }        
        [Parameter] public EventCallback<TItem> ValueChanged { get; set; }

        public bool IsMenuShown { get; set; } = false;
        public RenderFragment? Title { get; set; }

        public async Task HandleSelect(TItem item, RenderFragment<TItem> contentFragment)
        {
            if(Items == null)
                return;
            Value = item;
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

        public async Task ChangeIsMenuShown()
        {
            IsMenuShown = !IsMenuShown;            
            await OnTitleClick.InvokeAsync();
            StateHasChanged();
        }

        public void ChangeIsMenuShown(bool isShown)
        {
            if(OnMouseOverEnabled)
                IsMenuShown = isShown;
        }

        public void OnFocusOut()
        {
            IsMenuShown = false;
            StateHasChanged();
        }
    }
}
