﻿using ComputerShop.Client.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace ComputerShop.Client.Shared.Components
{
    public partial class ProductSearch
    {
        private List<string>? suggestions;
        protected ElementReference searchInput;
        private bool isSuggestionsVisable = false;
        protected string Text { get; set; } = string.Empty;

        protected override async Task OnAfterRenderAsync(bool first)
        {
            if(first)
            {
                await searchInput.FocusAsync();
            }
        }

        public void Find()
        {
            NavigationManager.GoToFindPage(Text);
        }

        protected async Task OnSearch(KeyboardEventArgs keyboardEventArgs)
        {
            if(keyboardEventArgs.Key == "Enter")
            {
                isSuggestionsVisable = false;
                Find();
            }
            else if(Text?.Length > 2)
            {
                isSuggestionsVisable = true;
                suggestions = await ProductService.GetProductSuggestionsAsync(Text);
                if(keyboardEventArgs.Key == "ArrowDown" && suggestions?.Count>0)
                {
                    Text = suggestions.First();
                }
            }
            else
            {
                isSuggestionsVisable = false;
                suggestions?.Clear();
            }
        }

        protected void OnFocusOut()
        {
            isSuggestionsVisable = false;
        }

        protected void OnFocus()
        {
            if(suggestions != null && suggestions.Count > 0)
                isSuggestionsVisable = true;
        }

        protected void OnMouseDown(string text)
        {
            Text = text;
            isSuggestionsVisable = false;
            Find();
        }
    }
}
