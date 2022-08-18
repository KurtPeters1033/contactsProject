using Contacts.Client.Enums;
using Contacts.Client.Model;
using Microsoft.AspNetCore.Components;
using static System.Net.WebRequestMethods;

namespace Contacts.Client.Pages
{
    public partial class ContactPopover
    {
        [Parameter]
        public bool IsDetailDialogOpen { get; set; } = false;

        [Parameter]
        public Contact? SelectedContact { get; set; }

        [Parameter] 
        public List<Flag> FlagData { get; set; }

        [Parameter]
        public PopoverMode Mode { get; set; }

        private Contact localContact;

        protected override Task OnParametersSetAsync()
        {
            localContact = SelectedContact;
            return base.OnParametersSetAsync();
        }

        private void OnClose()
        {
            localContact = SelectedContact;
            IsDetailDialogOpen = false;
        }

        private void OnSave()
        {
            SelectedContact = localContact;
        }

    }
}
