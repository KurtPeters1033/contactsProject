using Contacts.Client.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.IO;
using System;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;
using static MudBlazor.Colors;
using Microsoft.AspNetCore.Components.Web;
using System.Text;
using System.Xml.Linq;
using Contacts.Client.Enums;

namespace Contacts.Client.Pages
{
    public partial class ManageContacts
    {
        [Inject] private HttpClient Http { get; set; }

        private string searchText = string.Empty;
        private bool sortByName = false;
        private bool sortByLocation = false;
        private bool sortByPosition = false;
        private bool isDetailDialogOpen = false;
        private bool isFilterOpen = false;
        private object selectedValue { get; set; }
        private MudListItem selectedItem;
        private MudTextField<string> mudTextFieldRef;
        private PopoverMode contactDetailMode;

        private List<Contact> OriginalContactData { get; set; }
        private List<Contact> FilteredContactData { get; set; }
        private List<Flag> FlagData { get; set; }
        protected Contact SelectedContact { get { return (Contact)selectedValue; } }
        
        protected override async Task OnInitializedAsync()
        {
            OriginalContactData = await Http.GetFromJsonAsync<List<Contact>>("users.json");
            FilteredContactData = OriginalContactData;

            FlagData = await Http.GetFromJsonAsync<List<Flag>>("flags.json");


            OnSortByNameToggled(true);
        }

        private void ToggleOpenFilter()
        {
            if (isFilterOpen)
                isFilterOpen = false;
            else
                isFilterOpen = true;
        }

        private void OnSelectedValueChanged()
        {
            contactDetailMode = PopoverMode.Readonly;
            isDetailDialogOpen = true;
        }

        private void OnSortByNameToggled(bool value)
        {
            sortByName = value;
            sortByLocation = false;
            sortByPosition = false;

            FilteredContactData.Sort((x, y) => string.Compare(x.name, y.name));
        }

        private void OnSortByLocationToggled(bool value)
        {
            sortByLocation = value;
            sortByName = false;
            sortByPosition = false;

            FilteredContactData.Sort((x, y) => string.Compare(x.office, y.office));
        }

        private void OnSortByPositionToggled(bool value)
        {
            sortByPosition = value;
            sortByName = false;
            sortByLocation = false;

            FilteredContactData.Sort((x, y) => string.Compare(x.position, y.position));
        }

        private void OnSearch()
        {
            if (string.IsNullOrEmpty(mudTextFieldRef.Text))
            {
                FilteredContactData = OriginalContactData;
                return;
            }

            var name = OriginalContactData.Select(x => x).Where(x => x.name.ToLower().Contains(mudTextFieldRef.Text.ToLower())).ToList();
            var office = OriginalContactData.Select(x => x).Where(x => x.office.ToLower().Contains(mudTextFieldRef.Text.ToLower())).ToList();
            var position = OriginalContactData.Select(x => x).Where(x => x.position.ToLower().Contains(mudTextFieldRef.Text.ToLower())).ToList();
            var salary = OriginalContactData.Select(x => x).Where(x => x.salary.ToLower().Contains(mudTextFieldRef.Text.ToLower())).ToList();
            var extn = OriginalContactData.Select(x => x).Where(x => x.extn.ToLower().Contains(mudTextFieldRef.Text.ToLower())).ToList();
            var startDate = OriginalContactData.Select(x => x).Where(x => x.startDate.ToLower().Contains(mudTextFieldRef.Text.ToLower())).ToList();

            if (name.Any())
            {
                FilteredContactData = name;
            }
            else if (office.Any())
            {
                FilteredContactData = office;
            }
            else if (position.Any())
            {
                FilteredContactData = position;
            }
            else if (salary.Any())
            {
                FilteredContactData = salary;
            }
            else if (extn.Any())
            {
                FilteredContactData = extn;
            }
            else if (startDate.Any())
            {
                FilteredContactData = startDate;
            }
            
        }

        private void OnDelete(Contact contact)
        {
            OriginalContactData.Remove(contact);
            FilteredContactData = OriginalContactData;
        }

        private void OnEdit(Contact contact)
        {
            contactDetailMode = PopoverMode.Edit;
            selectedValue = contact;
            isDetailDialogOpen = true;
        }

        private void OnAdd()
        {
            contactDetailMode = PopoverMode.Add;

            var emptyContact = new Contact()
            {
                name = String.Empty,
                position = String.Empty,
                office = String.Empty,
                salary = String.Empty,
                startDate = String.Empty,
                extn = String.Empty
            };

            selectedValue = emptyContact;

            isDetailDialogOpen = true;
        }
    }
}
