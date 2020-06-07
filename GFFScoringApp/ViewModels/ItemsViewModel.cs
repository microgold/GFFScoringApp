﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using GFFScoringApp.Models;
using GFFScoringApp.Views;

namespace GFFScoringApp.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "GFF Cards";
            Items = new ObservableCollection<Item>() {new Item() {ImageUrl = ImageSource.FromFile("gff.jpg"), Name="GFF" }};
            var carrot = new Item() { ImageUrl = ImageSource.FromFile("carrot.jpg"), Name = "Carrot" };
            var apple = new Item() { ImageUrl = ImageSource.FromFile("apple.jpg"), Name = "apple" } ;
            var avocado = new Item() { ImageUrl = ImageSource.FromFile("avocado.jpg"), Name = "avocado" };
            Items.Add(carrot);
            Items.Add(apple);
            Items.Add(avocado);

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}