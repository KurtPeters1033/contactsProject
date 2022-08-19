using Contacts.Client.Enums;
using Contacts.Client.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
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

        [Parameter]
        public EventCallback OnSubmitted { get; set; }

        [Parameter]
        public EventCallback OnClosed { get; set; }

        public Contact NewContact { get { return SelectedContact; } }

        private bool success = false;
        private Contact localContact = new Contact();

        protected override Task OnParametersSetAsync()
        {
            IsDetailDialogOpen = false;

            if (SelectedContact != null)
            {
                localContact = new Contact()
                {
                    name = SelectedContact.name,
                    office = SelectedContact.office,
                    position = SelectedContact.position,
                    salary = SelectedContact.salary,
                    startDate = SelectedContact.startDate,
                    extn = SelectedContact.extn,
                };
            }

            return base.OnParametersSetAsync();
        }

        private void OnClose()
        {
            if (SelectedContact != null)
            {
                localContact = new Contact()
                {
                    name = SelectedContact.name,
                    office = SelectedContact.office,
                    position = SelectedContact.position,
                    salary = SelectedContact.salary,
                    startDate = SelectedContact.startDate,
                    extn = SelectedContact.extn,
                };
            }

            IsDetailDialogOpen = false;
            success = false;
            OnClosed.InvokeAsync();
        }

        private void OnValidSubmit(EditContext context)
        {
            success = true;

            SelectedContact = new Contact()
            {
                name = localContact.name,
                office = localContact.office,
                position = localContact.position,
                salary = localContact.salary,
                startDate = localContact.startDate,
                extn = localContact.extn,
            };

            OnSubmitted.InvokeAsync();
            StateHasChanged();
        }
    }
}
