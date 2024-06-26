﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace lab17
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
            {
                listView1.Items.Clear();
                //MessageBox.Show("Null tx1 and tx2");
                using HttpResponseMessage response =
                await client.GetAsync(@$"https://openlibrary.org/search.json?author={HttpUtility.UrlEncode(textBox2.Text)}&title={HttpUtility.UrlEncode(textBox1.Text)}&fields=title,author_name,first_publish_year");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadFromJsonAsync<ListBooks>();
                if (content.num_found == 0)
                {
                    listView1.Items.Clear();
                    MessageBox.Show("Not found");
                }
                foreach (var item in content.docs)
                {
                    ListViewItem listViewItem = new ListViewItem(new string[] { item.author_name?[0] ?? "Not indicated", item.first_publish_year.ToString(), item.title});
                    listView1.Items.Add(listViewItem);
                }
                //MessageBox.Show(content.docs.Count.ToString());
                return;

            }
            if(!string.IsNullOrEmpty(textBox1.Text))
            {
                listView1.Items.Clear();
                //var test = HttpUtility.UrlEncode("https://openlibrary.org/search.json?title=Мамонт и трава&fields=title,author_name,first_publish_year");
                //MessageBox.Show(test);
                //MessageBox.Show("Null tx1");
                using HttpResponseMessage response =
                await client.GetAsync(@$"https://openlibrary.org/search.json?title={HttpUtility.UrlEncode(textBox1.Text)}&fields=title,author_name,first_publish_year");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadFromJsonAsync<ListBooks>();
                if (content.num_found == 0)
                {
                    listView1.Items.Clear();
                    MessageBox.Show("Not found");
                    return;
                }
                foreach (var item in content.docs)
                {
                    ListViewItem listViewItem = new ListViewItem(new string[] { item.author_name?[0] ?? "Not indicated", item.first_publish_year.ToString(), item.title});
                    listView1.Items.Add(listViewItem);
                }
                //MessageBox.Show(content.docs.Count.ToString());
                return;
            }
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                listView1.Items.Clear();
                using HttpResponseMessage response =
                await client.GetAsync(@$"https://openlibrary.org/search.json?author={HttpUtility.UrlEncode(textBox2.Text)}&fields=title,author_name,first_publish_year");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadFromJsonAsync<ListBooks>();
                if (content.num_found == 0)
                {
                    listView1.Items.Clear();
                    MessageBox.Show("Not found");
                    return;
                }
                foreach (var item in content.docs)
                {
                    ListViewItem listViewItem = new ListViewItem(new string[] { item.author_name?[0] ?? "Not indicated", item.first_publish_year.ToString(),item.title});
                    listView1.Items.Add(listViewItem);
                }
                //MessageBox.Show(content.docs.Count.ToString());
                return;
            }

        }
        public async Task<ListBooks> ScreachBooks(string FindAuthor,string FindTitle)
        {
            //MessageBox.Show(@$"https://openlibrary.org/search.json?title={HttpUtility.UrlEncode((!string.IsNullOrEmpty(FindTitle) ? FindTitle : "\\"))}&author={HttpUtility.UrlEncode((!string.IsNullOrEmpty(FindAuthor) ? FindAuthor : "\\"))}&fields=title,author_name,first_publish_year");
            HttpClient client = new HttpClient();
            using HttpResponseMessage response =
            await client.GetAsync(@$"https://openlibrary.org/search.json?title={HttpUtility.UrlEncode((!string.IsNullOrEmpty(FindTitle) ? FindTitle : "\\"))}&author={HttpUtility.UrlEncode((!string.IsNullOrEmpty(FindAuthor) ? FindAuthor : "\\"))}&fields=title,author_name,first_publish_year");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<ListBooks>();
            return content;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            var books = await ScreachBooks(textBox2.Text, textBox1.Text);
            UpdateList(books);


        }
        private void UpdateList(ListBooks books)
        {
            listView1.Items.Clear();
            if(books.num_found == 0) {
                MessageBox.Show("Not found");
                return;
            }
            foreach (var item in books.docs)
            {
                ListViewItem listViewItem = new ListViewItem(new string[] { item.author_name?[0] ?? "Not indicated", item.first_publish_year.ToString(), item.title });
                listView1.Items.Add(listViewItem);
            }

        }
    }
}
